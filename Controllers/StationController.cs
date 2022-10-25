using System.Xml.Linq;
using EcoGasBackend.Models;
using EcoGasBackend.Services;
using Microsoft.AspNetCore.Mvc;

/**
 * This is the controller for station service endpoints
 * 
 * Auther: IT19153414 Akeel M.N.M
 * **/

namespace EcoGasBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationController : Controller
    {
        private readonly StationService _stationService;

        public StationController(StationService stationService) =>
            _stationService = stationService;

        // Get all station details endpoint
        [HttpGet]
        public async Task<List<Station>> Get() =>
            await _stationService.GetAsync();

        // Get station details by id endpoint
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

        // Create station details by location endpoint
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

        // Create station details by owner id endpoint
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

        // Create new station endpoint
        [HttpPost]
        public async Task<IActionResult> Post(Station station)
        {
            await _stationService.CreateAsync(station);

            return CreatedAtAction(nameof(Get), new { id = station.Id }, station);
        }

        // Update station fuel status endpoint
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

        // Increase queue count of fuel in a station endpoint
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

        // Decrease queue count of fuel in a station endpoint
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

        // Remove station endpoint
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
