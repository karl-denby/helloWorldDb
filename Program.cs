using System;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Threading;

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter your connection String here.
            string connnection_string = "mongodb+srv://csharp:csharp18@m0-playground-ay8ei.mongodb.net/test?retryWrites=true";
            string database_name = "hello";
            string collection_name = "world";

            Console.WriteLine("Connecting to Mongo");

            var client = new MongoClient(connnection_string);
            var database = client.GetDatabase(database_name);
            var collection = database.GetCollection<BsonDocument>(collection_name);

            var cursor = collection.Find(new BsonDocument()).ToCursor();

            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document.ToString());
            }
            
            Console.WriteLine("Insert Documents with 100ms interval");
            for (int i = 0; i < 10 * 60 * 1; i++)
            {
                collection.InsertOne(new BsonDocument{
                    {"_id", i},
                    {"desc","Created document _id: " + i.ToString()}
                });
                Thread.Sleep(100);
            }
            Console.WriteLine("Delete 1M Documents");
            collection.DeleteMany(new BsonDocument{});
            Console.WriteLine("Finished");
        }
    }
}
