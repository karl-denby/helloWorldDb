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
            string connnection_string = "mongodb://127.0.0.1/admin?retryWrites=true";
            string database_name = "hello";
            string collection_name = "world";

            Console.WriteLine("Connecting to Mongo");
            
            var client = new MongoClient(connnection_string);
            var database = client.GetDatabase(database_name);
            var collection = database.GetCollection<BsonDocument>(collection_name);

            Console.WriteLine("Delete Documents");
            collection.DeleteMany(new BsonDocument{});

            Console.WriteLine("ServerSelectionTimeoutMS: " + client.Settings.ServerSelectionTimeout);

            Console.WriteLine("Insert Documents with 100ms interval");
            for (int i = 0; i < 10 * 60 * 10; i++)
            {
                collection.InsertOne(new BsonDocument{
                    {"_id", i},
                    {"desc","Created document _id: " + i.ToString()}
                });
                Console.Write(i + ", ");
                if (i % 20 == 0 ){
                    Console.WriteLine(": Documents Written");
                }
                Thread.Sleep(10);
            }
            Console.WriteLine("Delete Documents");
            collection.DeleteMany(new BsonDocument{});
            Console.WriteLine("Finished");
        }
    }
}
