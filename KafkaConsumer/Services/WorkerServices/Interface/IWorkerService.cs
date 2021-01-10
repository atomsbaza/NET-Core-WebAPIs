using KafkaConsumer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.WorkerServices.Interface
{
    public interface IWorkerService
    {
        Task CreateUser(Worker worker);
    }
}
