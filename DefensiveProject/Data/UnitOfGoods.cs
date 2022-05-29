using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DefensiveProject.Data
{
    public class UnitOfGoods
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        public string Price { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        public string Description { get; set; }
     

        public static void AddMongoDB(UnitOfGoods unit)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("");
            collection.InsertOne(unit);
        }
        public static void EditingMongoDB(UnitOfGoods unit)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("");
           //доделать метод
        }
        public static void DeletingMongoDB(UnitOfGoods unit)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("");
            //доделать метод
        }

        public static List<UnitOfGoods> GetListDB()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UnitOfGoods>("");
            return collection.Find(x => true).ToList();
            //доделать метод
        }
            
    }
}
