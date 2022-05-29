﻿using MongoDB.Bson;
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

        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [Required (ErrorMessage ="Необходимо заполнить это поле")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        [EmailAddress(ErrorMessage ="Недействительный адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить это поле")]
        public string Password { get; set; }
        public string Role { get; set; } = "Administrator";

       
        public static void AddMongoDB(UserAccount user)
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            collection.InsertOne(user);
        }

        public static UserAccount Authorization(string login, string password)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            var item = collection.Find(x => x.Login == login && x.Password == password).FirstOrDefault();
            return item;
        }
        public static List<UserAccount> GetListDB()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("DefensiveProject");
            var collection = database.GetCollection<UserAccount>("User");
            return collection.Find(x => true).ToList();
        }
    }
}