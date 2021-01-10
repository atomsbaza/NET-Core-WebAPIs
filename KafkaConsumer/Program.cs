using Confluent.Kafka;
using KafkaConsumer.Models;
using KafkaConsumer.Mongo.Repositories;
using KafkaConsumer.Mongo.Repositories.Interface;
using KafkaConsumer.Services.KafkaServices;
using KafkaConsumer.Services.KafkaServices.Interface;
using KafkaConsumer.Services.WorkerServices;
using KafkaConsumer.Services.WorkerServices.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostContext, services) =>
              {
                  IConfiguration config = hostContext.Configuration;

                  var consumerConfig = new ConsumerConfig();
                  config.Bind("KafkaConsumer", consumerConfig);
                  services.AddSingleton(consumerConfig);

                  services.Configure<MongoSettings>(options =>
                  {
                      options.ConnectionString = config.GetSection("WorkerDatabaseSettings:ConnectionString").Value;
                      options.Database = config.GetSection("WorkerDatabaseSettings:DatabaseName").Value;
                  });

                  services.AddSingleton<IWorkerRepository, WorkerRepository>();
                  services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();
                  services.AddSingleton<IWorkerService, WorkerService>();
                  services.AddHostedService<WorkerProcess>();
              })
              .ConfigureLogging(logging =>
              {
                  logging.ClearProviders();
                  logging.AddConsole();
              });

    }
}
