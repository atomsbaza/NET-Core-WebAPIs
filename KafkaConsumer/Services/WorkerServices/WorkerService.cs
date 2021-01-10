using KafkaConsumer.Models;
using KafkaConsumer.Mongo;
using KafkaConsumer.Mongo.Repositories.Interface;
using KafkaConsumer.Services.WorkerServices.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.WorkerServices
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository repo;

        public WorkerService(IWorkerRepository repo)
        {
            this.repo = repo;
        }
        public async Task CreateUser(Worker worker)
        {
            await this.repo.AddUser(worker);
        }
    }
}
