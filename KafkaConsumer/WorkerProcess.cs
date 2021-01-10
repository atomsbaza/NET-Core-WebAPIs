using KafkaConsumer.Services.KafkaServices.Interface;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer
{
    public class WorkerProcess : BackgroundService
    {
        private readonly IKafkaConsumerService kafkaConsumerService;

        public WorkerProcess(IKafkaConsumerService kafkaConsumerService)
        {
            this.kafkaConsumerService = kafkaConsumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await kafkaConsumerService.Consume(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
