using System.Xml.Linq;
using EcoGasBackend.Models;
using EcoGasBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoGasBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationController : Controller
    {
        private readonly StationService _stationService;

        public StationController(StationService stationService) =>
            _stationService = stationService;

        [HttpGet]
        public async Task<List<Station>> Get() =>
            await _stationService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Station>> Get(string id)
        {
            var book = await _stationService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Station station)
        {
            await _stationService.CreateAsync(station);

            return CreatedAtAction(nameof(Get), new { id = station.Id }, station);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Station updatedStation)
        {
            var book = await _stationService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedStation.Id = book.Id;

            await _stationService.UpdateAsync(id, updatedStation);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _stationService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _stationService.RemoveAsync(id);

            return NoContent();
        }
    }
}
