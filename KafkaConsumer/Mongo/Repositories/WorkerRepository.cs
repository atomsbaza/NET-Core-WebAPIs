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
            try
            {
                var nextId = GetSequenceValue();
                worker.Msg_id = nextId;
                await context.Workers.InsertOneAsync(worker);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int GetSequenceValue()
        {
            var nextId = 0;
            var checkId = context.Workers.Find(Builders<Worker>.Filter.Empty).SortByDescending(g => g.Msg_id).FirstOrDefault();
            nextId = checkId == null ? 1 : checkId.Msg_id + 1;

            return nextId;
        }
    }
}
