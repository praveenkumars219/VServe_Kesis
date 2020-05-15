using GraphQl.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl.Repository
{
    public class MongoContext : IDisposable
    {
        private MongoClient mongoClient;
        private IMongoDatabase mongoDb;
        public MongoContext(IMongoAppSettings settings)
        {
            mongoClient = new MongoClient(settings.ConnectionString);
            mongoDb = mongoClient.GetDatabase(settings.DatabaseName);
            mongoClient.GetDatabase(settings.DatabaseName);
            Collections = settings.Collections;
        }

        public void Dispose()
        {           
            mongoDb = null;
            mongoClient = null;
        }

        public IMongoDatabase MongoDb { get { return mongoDb; } set { mongoDb = value; } }
        public MongoClient MongoDbClient { get; set; }
        public string Collections { get; set; }
    }
}
