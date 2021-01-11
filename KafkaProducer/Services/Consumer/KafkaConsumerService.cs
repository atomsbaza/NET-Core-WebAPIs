using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using True_Test_WebAPIs.Models;
using True_Test_WebAPIs.Services.Consumer.Interface;

namespace True_Test_WebAPIs.Services.Consumer
{
    public class KafkaConsumerService : IKafkaConsumerService
    {
        private readonly ConsumerConfig _config;
        private readonly WokerService _workerService;

        public KafkaConsumerService(ConsumerConfig config, WokerService wokerService)
        {
            this._config = config;
            this._workerService = wokerService;
        }

        public async Task Consume(CancellationToken cancellationToken)
        {
            using (var consumer = new ConsumerBuilder<Null, string>(this._config).Build())
            {
                consumer.Subscribe("Woker");
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumedResult = consumer.Consume(cancellationToken);
                    if (consumedResult != null)
                    {
                        var worker = JsonConvert.DeserializeObject<Worker>(consumedResult.Message.Value);
                        await this._workerService.Create(worker);
                    }
                }
            }
        }
    }
}
