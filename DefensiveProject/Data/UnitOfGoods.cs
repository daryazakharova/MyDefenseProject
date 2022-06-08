using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;


namespace DefensiveProject.Data
{
    public class UnitOfGoods
    {
        public int size;//размер в корзине
        public int quantity; //количество в корзине

        public UnitOfGoods()
        {
        }

        public UnitOfGoods(int articleProduct,string name, string gender, string category, int price, byte[] photo, string description) 
        { //для добавления в корзину
            ArticleProduct = articleProduct;
            Name = name;
            Gender = gender;
            Category = category;
            Price = price;
            Photo = photo;
            Description = description;
        }

        public UnitOfGoods(int articleProduct,string name, string gender, string category, int price, int size104, int size110, int size116, int size128, byte[] photo, string description )
        {
            ArticleProduct = articleProduct;
            Name = name;
            Gender = gender;
            Category = category;
            Price = price;
            Size104 = size104;
            Size110 = size110;
            Size116 = size116;
            Size128 = size128;
            Photo = photo;
            Description = description;
        }

        public UnitOfGoods(string name, int price, int articleProduct, int size, int quantity)
        {
            Name = name;
            Price = price;
            ArticleProduct = articleProduct;
            this.size = size;
            this.quantity = quantity;
        }

        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
    
        public string Name { get; set; }
     
        public string Gender { get; set; }

        public string Category { get; set; }
      
        public int Price { get; set; }
  
        public byte[] Photo { get; set; }
       
        public string Description { get; set; }
        public int ArticleProduct { get; set; }
        public int Size104 { get; set; } //наличие штук по размерам
        public int Size110 { get; set; }
        public int Size116 { get; set; }
        public int Size128 { get; set; }


        public static void AddMongoDB(UnitOfGoods unit)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("Clothes");
            collection.InsertOne(unit);
        }
        public static List<UnitOfGoods> GetListDB()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("Clothes");
            return collection.Find(x => true).ToList();

        }
        public static UnitOfGoods GetArticleClothes(int article)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("Clothes");
            if (collection == null)
            {
                //сделать обработку 0
            }
            return collection.Find(x => x.ArticleProduct == article).FirstOrDefault();
        }
       
        //public static void EditingMongoDB(string name, string gender, UnitOfGoods unitOfGoods)
        //{
        //    MongoClient client = new MongoClient("mongodb://localhost");
        //    var database = client.GetDatabase("DefensiveProject");
        //    var collection = database.GetCollection<UnitOfGoods>("Clothes");
           
        //    collection.ReplaceOne(x => x.Name == name && x.Gender == gender, unitOfGoods);
           
        //}
        public static void ReplaceToDataBase(int article, UnitOfGoods unitOfGoods)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("Clothes");
            collection.ReplaceOne(x => x.ArticleProduct == article, unitOfGoods);
        }
        
        public static void DeletingMongoDB(int article)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("Clothes");
            collection.DeleteOne(x => x.ArticleProduct == article);
        }
        public static List<UnitOfGoods> GetProductsInGender(string gender)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("Clothes");
            return collection.Find(x => x.Gender == gender).ToList();
        }
        public static List<UnitOfGoods> ShowProductsInCategory(string gender, string category)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("Clothes");
            return collection.Find(x => x.Gender == gender && x.Category == category).ToList();
        }


    }
}
