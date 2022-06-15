using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace DefensiveProject.Data
{
    public class Article
    {
        public Article()
        {

        }

        public Article(string name, int value)
        {
            Name = name;
            Value = value;
        }

        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public static void AddArticle(Article article)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<Article>("Article");
            collection.InsertOne(article);
        }
        public static Article GetArticle(string name)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<Article>("Article");
            return collection.Find(x => x.Name == name).FirstOrDefault();
        }
        public static void ReplaceArticle(string name, Article article)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<Article>("Article");
            collection.ReplaceOne(x => x.Name == name, article);
        }

    }
}
