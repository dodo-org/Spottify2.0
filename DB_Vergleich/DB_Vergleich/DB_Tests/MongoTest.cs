using DB_Vergleich.DataStructs;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DB_Vergleich.DB_Tests
{
    public class MongoTest
    {
        private List<User> users = new();
        private Times times = new Times();
        private int[] getList = new int[0];
        public Times RunTest(List<User> _users, int[] _getList, Times _times)
        {
            users = _users;
            getList = _getList;
            times = _times;

            try
            {
                // Verbindung zur MongoDB aufbauen (lokale MongoDB auf Standardport)
                string connectionString = "mongodb://localhost:27017";
                MongoClient client = new MongoClient(connectionString);

                // Datenbank und Kollektion auswählen
                IMongoDatabase database = client.GetDatabase("testdb");
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("users");
                // User speichern
                times.StartWrite = DateTime.Now;
                foreach (User user in users)
                {
                    var thmpUser = new BsonDocument
                        {
                            { "id", user.Id },
                            { "name", user.Name }
                        };

                    // User in die MongoDB einfügen
                    collection.InsertOne(thmpUser);
                    Console.WriteLine("User: " + user.Id);

                }
                times.EndWrite = DateTime.Now;
                // User auslesen
                times.StartRead = DateTime.Now;
                for (int counter1 = 0; counter1 < getList.Count(); counter1++)
                {    
                    var filter = Builders<BsonDocument>.Filter.Eq("id", getList[counter1]);
                    var foundUser = collection.Find(filter).FirstOrDefault();

                    if (foundUser != null)
                    {
                        Console.WriteLine($"Gefundener User: {foundUser["_id"]} - {foundUser["name"]}");
                    }
                    else
                    {
                        Console.WriteLine("User nicht gefunden.");
                    }
                }
                times.EndRead = DateTime.Now;
            }
            catch 
            {
                return new();
            }


            times.diffRead = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;

            return times;
        }
    }
}
