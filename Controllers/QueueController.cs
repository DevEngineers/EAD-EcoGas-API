using EcoGasBackend.Models;
using EcoGasBackend.Services;
using Microsoft.AspNetCore.Mvc;

/**
 * This is the controller for station service endpoints
 * 
 * Auther: IT19175126 Zumry A.M
 * **/

namespace EcoGasBackend.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class QueueController : Controller
        {

        private readonly QueueService _queueService;

        public QueueController(QueueService queueService) =>
            _queueService = queueService;

        // Get all Queue details endpoint
        [HttpGet]
        public async Task<List<Queue>> Get() =>
            await _queueService.GetAsync();

        // Get Queue details by id endpoint
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Queue>> Get(string id)
        {
            var queue = await _queueService.GetAsync(id);

            if (queue is null)
            {
                return NotFound();
            }

            return queue;
        }

        // Create Queue details by UserId endpoint
        [HttpGet("User/{id:length(24)}")]
        public async Task<ActionResult<Queue>> GetByUserID(string id)
        {
            var queue = await _queueService.GetAsyncByUserID(id);

            if (queue is null)
            {
                return NotFound();
            }

            return queue;
        }

        // Create new Queue endpoint
        [HttpPost]
        public async Task<IActionResult> Post(Queue queue)
        {
            await _queueService.CreateAsync(queue);

            return CreatedAtAction(nameof(Get), new { id = queue.Id }, queue);
        }

        // Update Queue endpoint
        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<Queue>> Update(string id, Queue updatedQueue)
        {
            var queue = await _queueService.GetAsync(id);

            if (queue is null)
            {
                return NotFound();
            }

            queue.StationID = updatedQueue.StationID;
            queue.ArrivalTime = updatedQueue.ArrivalTime;
            queue.ArrivalDate = updatedQueue.ArrivalDate;

            await _queueService.UpdateAsync(id, queue);

            return queue;
        }

        // Remove Queue endpoint
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var queue = await _queueService.GetAsync(id);

            if (queue is null)
            {
                return NotFound();
            }

            await _queueService.RemoveAsync(id);

            return NoContent();
        }

        }

   }
