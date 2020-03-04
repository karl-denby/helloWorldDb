using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading;

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter your connection String here.
            string connection_string = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            string database_name = "drivers";
            string collection_name = "csharp";

            Console.WriteLine("Connecting to Mongo");

            var client = new MongoClient(connection_string);
            var database = client.GetDatabase(database_name);
            var collection = database.GetCollection<BsonDocument>(collection_name);

            Console.WriteLine("Delete Documents");
            collection.DeleteMany(new BsonDocument { });

            Console.WriteLine("ServerSelectionTimeoutMS: " + client.Settings.ServerSelectionTimeout);

            Console.WriteLine("Insert Documents with 100ms interval");
            for (int i = 0; i < 10 * 60 * 10; i++)
            {
                collection.InsertOne(new BsonDocument{
                    {"_id", i},
                    {"desc","Created document _id: " + i.ToString()}
                });
                Console.Write(i + ", ");
                if ((i % 20 == 0) && (i > 0))
                {
                    Console.WriteLine(": Documents Written");
                }
                Thread.Sleep(10);
            }
            Console.WriteLine("Delete Documents");
            collection.DeleteMany(new BsonDocument { });
            Console.WriteLine("Finished");
        }
    }
}
