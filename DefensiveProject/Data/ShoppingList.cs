using DefensiveProject.Authentication;

using MongoDB.Driver;

namespace DefensiveProject.Data
{
    public class ShoppingList 
    {
        public double Currency { get; set; }
        public List<UnitOfGoods> shoppingList; 
       public ShoppingList()
        {
            Currency = 0;
            shoppingList = new List<UnitOfGoods>();
        }
      
        public static ShoppingList GetShoppingList(string name)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            var client1 = collection.Find(x => x.Login == name).FirstOrDefault();
            return client1.shoppingList;
        }

        public static void UpdateShoppingList(UnitOfGoods unit, string login)//push - добавляем новый обект в список cart
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            var defenition = Builders<UserAccount>.Update.Push(x => x.shoppingList.shoppingList, unit);
            collection.UpdateOne(x => x.Login == login, defenition);

        }
        public void AddToShoppingList(UnitOfGoods product, string name)
        {
            shoppingList = GetShoppingList(name).shoppingList;
            shoppingList.Add(product);
            Currency = SetCurrency();
        }
       
        public double SetCurrency() //общая стоимость
        {
            double localCurrency = 0;
            foreach (var item in shoppingList)
            {
                localCurrency += item.Price * item.quantity;
            }
            return localCurrency;
        }
    }
}
