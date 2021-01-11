using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaProducer.Services.Producer.Interface;

namespace KafkaProducer.Services.Producer
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly ProducerConfig config;
        public KafkaProducerService(ProducerConfig config)
        {
            this.config = config;
        }

        public async Task ProduceAsync(string topic, string message)
        {
            using (var producer = new ProducerBuilder<Null, string>(this.config).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string>()
                {
                    Value = message,
                });
                producer.Flush();
            }
        }
    }
}
