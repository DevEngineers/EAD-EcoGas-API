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
            var station = await _stationService.GetAsync(id);

            if (station is null)
            {
                return NotFound();
            }

            return station;
        }

        [HttpGet("location/{id}")]
        public async Task<ActionResult<Station>> GetByLocation(string id)
        {
            var station = await _stationService.GetAsyncByLocation(id);

            if (station is null)
            {
                return NotFound();
            }

            return station;
        }

        [HttpGet("owner/{id:length(24)}")]
        public async Task<ActionResult<Station>> GetByOwnerID(string id)
        {
            var station = await _stationService.GetAsyncByOwnerID(id);

            if (station is null)
            {
                return NotFound();
            }

            return station;
        }


        [HttpPost]
        public async Task<IActionResult> Post(Station station)
        {
            await _stationService.CreateAsync(station);

            return CreatedAtAction(nameof(Get), new { id = station.Id }, station);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<Station>> Update(string id, Fuel fuel)
        {
            var station = await _stationService.GetAsync(id);

            if (station is null)
            {
                return NotFound();
            }

            switch (fuel.FuelName)
            {
                case "Petrol":
                    station.Fuel[0].Capacity += fuel.Capacity;
                    station.Fuel[0].ArrivalDate = fuel.ArrivalDate;
                    station.Fuel[0].ArrivalTime = fuel.ArrivalTime;
                     break;
                case "SuperPetrol":
                    station.Fuel[1].Capacity += fuel.Capacity;
                    station.Fuel[1].ArrivalDate = fuel.ArrivalDate;
                    station.Fuel[1].ArrivalTime = fuel.ArrivalTime;
                    break;
                case "Diesel":
                    station.Fuel[2].Capacity += fuel.Capacity;
                    station.Fuel[2].ArrivalDate = fuel.ArrivalDate;
                    station.Fuel[2].ArrivalTime = fuel.ArrivalTime;
                    break;
                case "SuperDiesel":
                    station.Fuel[3].Capacity += fuel.Capacity;
                    station.Fuel[3].ArrivalDate = fuel.ArrivalDate;
                    station.Fuel[3].ArrivalTime = fuel.ArrivalTime;
                    break;
            }

            await _stationService.UpdateAsync(id, station);

            return station;
        }

        [HttpPut("add/{id:length(24)}")]
        public async Task<ActionResult<Station>> IncreaseQueueCount(string id, Fuel fuel)
        {
            var station = await _stationService.GetAsync(id);

            if (station is null)
            {
                return NotFound();
            }

            switch (fuel.FuelName)
            {
                case "Petrol":
                    station.PetrolQueue += 1;
                    break;
                case "SuperPetrol":
                    station.SuperPetrolQueue += 1;
                    break;
                case "Diesel":
                    station.DieselQueue += 1;
                    break;
                case "SuperDiesel":
                    station.SuperPetrolQueue += 1;
                    break;
            }

            await _stationService.UpdateAsync(id, station);

            return station;
        }

        [HttpPut("remove/{id:length(24)}")]
        public async Task<ActionResult<Station>> DecreaseQueueCount(string id, Fuel fuel)
        {
            var station = await _stationService.GetAsync(id);

            if (station is null)
            {
                return NotFound();
            }

            switch (fuel.FuelName)
            {
                case "Petrol":
                    station.PetrolQueue -= 1;
                    break;
                case "SuperPetrol":
                    station.SuperPetrolQueue -= 1;
                    break;
                case "Diesel":
                    station.DieselQueue -= 1;
                    break;
                case "SuperDiesel":
                    station.SuperPetrolQueue -= 1;
                    break;
            }

            await _stationService.UpdateAsync(id, station);

            return station;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var station = await _stationService.GetAsync(id);

            if (station is null)
            {
                return NotFound();
            }

            await _stationService.RemoveAsync(id);

            return NoContent();
        }
    }
}
