using DefensiveProject.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DefensiveProject.Authentication
{
   
    public class UserAccount
    {
        public UserAccount()
        {
        }


        public UserAccount(string login, string email, string password, string role)
        {
            Login = login;
            Email = email;
            Password = password;
            Role = role;
        }

        public UserAccount(string login, string email, string password, string role, string numberPhone)
        {
            Login = login;
            Email = email;
            Password = password;
            Role = role;
            NumberPhone = numberPhone;
        }

        public UserAccount(string login, string email, string password, string role, string numberPhone, BasketUser busketUser) 
        {
            Login = login;
            Email = email;
            Password = password;
            Role = role;
            NumberPhone = numberPhone;
            this.busketUser = busketUser;
        }

        public UserAccount(string login, string email, string password, string role, string numberPhone, BasketUser busketUser, ShoppingList shoppingList) 
        {
            Login = login;
            Email = email;
            Password = password;
            Role = role;
            NumberPhone = numberPhone;
            this.busketUser = busketUser;
            this.shoppingList = shoppingList;
        }

        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [Required (ErrorMessage ="Необходимо заполнить это поле")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        [EmailAddress(ErrorMessage ="Недействительный адрес электронной почты")]
        public string Email { get; set; }
        public string NumberPhone { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        public string Password { get; set; }
        public string Role { get; set; } = "Client"; 
        [BsonElement("Basket")]
        public BasketUser busketUser = new BasketUser();
        [BsonElement("ShoppingList")]
        public ShoppingList shoppingList = new ShoppingList();

       
        public static void AddMongoDB(UserAccount user)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            collection.InsertOne(user);
        }

        public static UserAccount Authorization(string email, string password)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            var item = collection.Find(x => x.Email == email && x.Password == password).FirstOrDefault();
            
            return item;
        }
        public static UserAccount GetUserLogin(string login)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            var item = collection.Find(x => x.Login == login).FirstOrDefault();
            return item;
        }
       
        public static UserAccount GetUserAccount(string email)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            var item = collection.Find(x => x.Email == email).FirstOrDefault();
            return item;
        }
        public static List<UserAccount> GetListDB()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            return collection.Find(x => true).ToList();
        }
        public static void ReplaceUserToDataBase(string email, UserAccount user)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            collection.ReplaceOne(x => x.Email == email, user);
        }
        public static void ReplaceUser(UserAccount user)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            collection.ReplaceOne(x => x.Email == user.Email, user);
        }
        public void AddToShoppingList(string category, int article, int size, int quantity)
        {
            var list = UnitOfGoods.ShowProductsInCategory(category);
            var current = list.Find(x => x.ArticleProduct == article);
            current.quantity = quantity;
            current.size = size;
            busketUser.AddToCart(current, Login);
        }
        public void AddToBasket(string category, int article, int size, int quantity)
        {
            var list = UnitOfGoods.ShowProductsInCategory(category);
            var current = list.Find(x => x.ArticleProduct == article);
            current.quantity = quantity;
            current.size = size;    
            busketUser.AddToCart(current, Login);
        }
        public void DeleteOneBasket(int article, int size, string name)
        {
            var ListInCart =BasketUser.GetCart(name).ListInCart;
            var current = ListInCart.Find(x => x.ArticleProduct == article && x.size == size);
            busketUser.DeleteOneToCart(current, name);
        }

        public void Buy(int article, int size, int quantity)
        {
            var ListInCart = BasketUser.GetCart(Login).ListInCart;
            var current = ListInCart.Find(x => x.ArticleProduct == article && x.size == size);
            if (size == 104)
            {
                current.Size104 -= quantity;
                UnitOfGoods.UpdateSize104(current.ArticleProduct, current.Size104);
                current.quantity = quantity;
                shoppingList.AddToShoppingList(current, Login);
            }
            if (size == 110)
            {
                current.Size110 -= quantity;
                UnitOfGoods.UpdateSize110(current.ArticleProduct, current.Size110);
                current.quantity = quantity;
                shoppingList.AddToShoppingList(current, Login);
            }
            if (size == 116)
            {
                current.Size116 -= quantity;
                UnitOfGoods.UpdateSize116(current.ArticleProduct, current.Size116);
                current.quantity = quantity;
                shoppingList.AddToShoppingList(current, Login);
            }
            if (size == 128)
            {
                current.Size128 -= quantity;
                UnitOfGoods.UpdateSize128(current.ArticleProduct, current.Size128);
                current.quantity = quantity;
                shoppingList.AddToShoppingList(current, Login);
            }
        }
        public static List<UserAccount> GetListUserDB(string name)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            return collection.Find(x => x.Login == name).ToList();
        }

        public void DeleteBasket(string name)
        {
           busketUser.DeleteListInCart(name);
        }
       
    }
}
