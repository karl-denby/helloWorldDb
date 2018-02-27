using System;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter your connection String here.
            string connnection_string = "";
            string database_name = "hello";
            string collection_name = "world";

            Console.WriteLine("Hello World!");
            Console.WriteLine("Connecting to Mongo");

            var client = new MongoClient(connnection_string);
            var database = client.GetDatabase(database_name);
            var collection = database.GetCollection<BsonDocument>(collection_name);

            var cursor = collection.Find(new BsonDocument()).ToCursor();

            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document.ToString());
            }
            
            Console.WriteLine("Create 10,000 Documents");
            for (int i = 0; i < 10000; i++)
            {
                collection.InsertOne(new BsonDocument{
                    {"_id", i},
                    {"desc","Created document _id: " + i.ToString()}
                });
            }

            Console.WriteLine("Delete 10,000 Documents");
            for (int i = 1; i > 10000; i++)
            {
                collection.DeleteOne(new BsonDocument{
                    {"_id", i}
                });
            }

            Console.WriteLine("Done");

        }
    }
}
