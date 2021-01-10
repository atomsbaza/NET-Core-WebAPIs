using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using True_Test_WebAPIs.Models;

namespace True_Test_WebAPIs.Services
{
    public class WokerService
    {
        private readonly IMongoCollection<Worker> _woker;

        public WokerService(IWorkerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _woker = database.GetCollection<Worker>(settings.WorkerCollectionName);
        }

        public List<Worker> Get() =>
            _woker.Find(r => true).ToList();

        public Worker Get(string Msg_id) =>
            _woker.Find<Worker>(r => r.Msg_id == Msg_id).FirstOrDefault();

        public Worker Create(Worker worker)
        {
            _woker.InsertOne(worker);
            return worker;
        }

        public void Update(string Msg_id, Worker workerIn) =>
            _woker.ReplaceOne(r => r.Msg_id == Msg_id, workerIn);

        public void Remove(Worker workerIn) =>
            _woker.DeleteOne(r => r.Msg_id == workerIn.Msg_id);

        public void Remove(string Msg_id) =>
            _woker.DeleteOne(r => r.Msg_id == Msg_id);
    }
}
