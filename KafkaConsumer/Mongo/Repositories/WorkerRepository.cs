using KafkaConsumer.Models;
using KafkaConsumer.Mongo.Repositories.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Mongo.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly WorkerContext context = null;

        public WorkerRepository(IOptions<MongoSettings> settings)
        {
            context = new WorkerContext(settings);
        }
        public async Task AddUser(Worker worker)
        {
            var nextId = GetSequenceValue();
            worker.Msg_id = nextId;
            await context.Workers.InsertOneAsync(worker);
        }

        public string GetSequenceValue()
        {
            var checkId = context.Workers.Find(f => f.Msg_id != "0").SortByDescending(g => g.Msg_id).FirstOrDefault().Msg_id;
            var nextId = (Convert.ToInt32(checkId) + 1).ToString();

            return nextId;
        }
    }
}
