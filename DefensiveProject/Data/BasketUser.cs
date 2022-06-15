using DefensiveProject.Authentication;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DefensiveProject.Data
{
    public class BasketUser
    {
        public double Currency { get; set; }
        public List<UnitOfGoods> ListInCart;

        public BasketUser()
        {
            Currency = 0;
            ListInCart = new List<UnitOfGoods>();
        }

        public static BasketUser GetCart(string name) 
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            var client1 = collection.Find(x => x.Login == name).FirstOrDefault();
            return client1.busketUser;
        }

        public void AddToCart(UnitOfGoods product, string name)
        {
            ListInCart = GetCart(name).ListInCart;
            if (ListInCart.Exists(x => x.ArticleProduct == product.ArticleProduct && x.size == product.size))
            {
                var current = ListInCart.Find(x => x.ArticleProduct == product.ArticleProduct && x.size == product.size); //при совпадении имен в корзинке обновляется количество
                current.quantity += product.quantity;
            }
            else
            {
               ListInCart.Add(product);
            }
           Currency = SetCurrency();
        }
        public void DeleteOneToCart(UnitOfGoods product, string name)
        {
            ListInCart = GetCart(name).ListInCart;
            if (ListInCart.Exists(x => x.ArticleProduct == product.ArticleProduct && x.size == product.size))
            {
                var current = ListInCart.Find(x => x.ArticleProduct == product.ArticleProduct && x.size == product.size); 
                ListInCart.Remove(current);
            }
            Currency = SetCurrency();
        }
        
        public double SetCurrency() //общая стоимость
        {
            double localCurrency = 0;
            foreach (var item in ListInCart)
            {
                localCurrency += item.Price * item.quantity;
            }
            return localCurrency;
        }

        public void DeleteListInCart(string name)
        {
            ListInCart = GetCart(name).ListInCart;
            ListInCart.Clear();
            Currency = 0;
        }
    }
}
