using DefensiveProject.Authentication;
using MongoDB.Driver;

namespace DefensiveProject.Data
{
    public class BasketUser
    {
        //public double Currency { get; set; }
        public List<UnitOfGoods> cart;

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
            //if (cart.Exists(x => x.Name == product.Name))
            //{
            //    var current = cart.Find(x => x.Name == product.Name);
            //    current.quantity += product.quantity;
            //}
            cart = GetCart(name).cart;
            cart.Add(product);
        }
        public int SetCurrency()
        {
            int localCurrency = 0;
            foreach (var item in cart)
            {
                localCurrency += item.Price * item.quantity;
            }
            return localCurrency;
        }

    }
}
