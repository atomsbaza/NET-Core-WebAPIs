using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaProducer.Models;
using KafkaProducer.Services;
using KafkaProducer.Services.Producer.Interface;


namespace KafkaProducer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly WorkerService _workerService;
        private readonly IKafkaProducerService _producerService;
        private const string _topic = "Worker";

        public WorkerController(WorkerService workerService, IKafkaProducerService producerService)
        {
            _workerService = workerService;
            _producerService = producerService;
        }

        [HttpGet]
        public ActionResult<List<Worker>> Get() => _workerService.Get();

        [HttpPost]
        public async Task<IActionResult> Create(string topic, Worker worker)
        {
            topic = _topic;
            if (string.IsNullOrEmpty(topic) || worker == null)
            {
                return BadRequest();
            }

            worker.Received_Time = DateTime.Now;

            string serializedUser = JsonConvert.SerializeObject(worker);
            await this._producerService.ProduceAsync(topic, serializedUser);

            // Return Response
            var returnResult = new ResponseResult();
            returnResult.Code = "OK";
            returnResult.Received_Time = worker.Received_Time.ToString();

            return Ok(returnResult);
        }
    }
}
