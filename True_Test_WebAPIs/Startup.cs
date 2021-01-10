using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using True_Test_WebAPIs.Models;
using True_Test_WebAPIs.Services;
using True_Test_WebAPIs.Services.Producer;
using True_Test_WebAPIs.Services.Producer.Interface;

namespace True_Test_WebAPIs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // SetUp Database Config
            services.Configure<WorkerDatabaseSettings>(Configuration.GetSection(nameof(WorkerDatabaseSettings)));
            services.AddSingleton<IWorkerDatabaseSettings>(sp =>sp.GetRequiredService<IOptions<WorkerDatabaseSettings>>().Value);

            // Setup Service
            services.AddSingleton<WorkerService>();

            // Setup Kafka Producer
            var producerConfig = new ProducerConfig();
            Configuration.Bind("KafkaProducer", producerConfig);
            services.AddSingleton(producerConfig);
            services.AddScoped<IKafkaProducerService, KafkaProducerService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
