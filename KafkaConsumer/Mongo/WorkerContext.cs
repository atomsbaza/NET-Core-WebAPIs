using KafkaConsumer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafkaConsumer.Mongo
{
    public class WorkerContext
    {
        // Setup MongoDB
        private readonly IMongoDatabase database = null;
        public WorkerContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                database = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<Worker> Workers
        {
            get
            {
                return database.GetCollection<Worker>("Workers");
            }
        }
    }
}
