using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using True_Test_WebAPIs.Models;
using True_Test_WebAPIs.Services;

namespace True_Test_WebAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly WokerService _workerService;

        public WorkerController(WokerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet]
        public ActionResult<List<Worker>> Get() => _workerService.Get();

        [HttpGet("{Msg_id}", Name = "GetWorker")]
        public ActionResult<Worker> Get(string id)
        {
            var worker = _workerService.Get(id);

            if (worker == null)
            {
                return NotFound();
            }

            return worker;
        }

        [HttpPost]
        public ActionResult<Worker> Create(Worker worker)
        {
            worker.Received_Time = DateTime.Now;
            _workerService.Create(worker);

            return CreatedAtRoute("GetWorker", new { Msg_id = worker.Msg_id.ToString() }, worker);
        }

        [HttpPut("{Msg_id}")]
        public IActionResult Update(string Msg_id, Worker workerIn)
        {
            var worker = _workerService.Get(Msg_id);

            if (worker == null)
            {
                return NotFound();
            }

            _workerService.Update(Msg_id, workerIn);

            return NoContent();
        }

        [HttpDelete("{Msg_id}")]
        public IActionResult Delete(string Msg_id)
        {
            var worker = _workerService.Get(Msg_id);

            if (worker == null)
            {
                return NotFound();
            }

            _workerService.Remove(worker);

            return NoContent();
        }
    }
}
