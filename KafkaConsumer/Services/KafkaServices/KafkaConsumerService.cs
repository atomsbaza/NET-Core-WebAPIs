using Confluent.Kafka;
using KafkaConsumer.Models;
using KafkaConsumer.Services.KafkaServices.Interface;
using KafkaConsumer.Services.WorkerServices;
using KafkaConsumer.Services.WorkerServices.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.KafkaServices
{
    public class KafkaConsumerService : IKafkaConsumerService
    {
        private readonly ConsumerConfig _config;
        private readonly IWorkerService _workerService;

        public KafkaConsumerService(ConsumerConfig config, IWorkerService workerService)
        {
            this._config = config;
            this._workerService = workerService;
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
                        await this._workerService.CreateUser(worker);

                        Console.WriteLine("# {0}, {1}: {2}, {3}", worker.Msg_id, worker.Sender, worker.Msg, worker.Received_Time);
                    }
                }
            }
        }
    }
}
