using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaProducer.Models;

namespace KafkaProducer.Services
{
    public class WorkerService
    {
        private readonly IMongoCollection<Worker> _woker;

        public WorkerService(IWorkerDatabaseSettings settings)
        {
            //Setup for using MongoDB
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _woker = database.GetCollection<Worker>(settings.WorkerCollectionName);
        }

        // Get Worker in DB
        public List<Worker> Get() => _woker.Find(r => true).ToList();
    }
}
