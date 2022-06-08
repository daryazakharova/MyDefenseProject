using DefensiveProject.Authentication;
using MongoDB.Driver;

namespace DefensiveProject.Data
{
    public class ShoppingList
    {
        public List<UnitOfGoods> shopping; 

        public static ShoppingList GetShoppingList(string name)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            var client1 = collection.Find(x => x.Login == name).FirstOrDefault();
            return client1.shoppingList;
        }

        public void AddToCart(UnitOfGoods product, string name)
        {
          
            shopping = GetShoppingList(name).shopping;
            shopping.Add(product);
        }
    }
}
