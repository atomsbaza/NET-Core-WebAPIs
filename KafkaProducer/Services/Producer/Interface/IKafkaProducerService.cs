using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafkaProducer.Services.Producer.Interface
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync(string topic, string message);
    }
}
