using KafkaConsumer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Mongo.Repositories.Interface
{
    public interface IWorkerRepository
    {
        Task AddUser(Worker worker);
    }
}
