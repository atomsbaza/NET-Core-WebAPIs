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

                  // Setup Kafka Consumer Config
                  var consumerConfig = new ConsumerConfig();
                  config.Bind("KafkaConsumer", consumerConfig);

                  // AddSingleton นั่น object จะถูกสร้างขึ้นมาแค่ครั้งเดียว ไม่ว่าเราจะ request เข้ามากี่รอบ object จะไม่ถูกสร้างใหม่แล้ว
                  // AddTransient นั่น object จะถูก create instance ใหม่ตลอด
                  // AddScoped นั้นใน request ใดๆ ที่เข้ามา ถ้า object ถูก create ไว้แล้ว จะไม่สร้างใหม่อีก
                  services.AddSingleton(consumerConfig);

                  // Setup MongoDB Config
                  services.Configure<MongoSettings>(options =>
                  {
                      options.ConnectionString = config.GetSection("WorkerDatabaseSettings:ConnectionString").Value;
                      options.Database = config.GetSection("WorkerDatabaseSettings:DatabaseName").Value;
                  });

                  // Setup Service
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
