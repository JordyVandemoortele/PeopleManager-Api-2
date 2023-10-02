using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Services;

namespace PeopleManager.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;

        public VehicleController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var vehicles = await _vehicleService.FindAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id:int}", Name = "GetVehicleRoute")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var vehicles = await _vehicleService.GetAsync(id);
            return Ok(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vehicle model)
        {
            var vehicle = await _vehicleService.CreateAsync(model);
            if (vehicle is null)
            {
                return NotFound();
            }
            return CreatedAtRoute("GetVehicleRoute", new { id = vehicle.Id }, vehicle);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Vehicle model)
        {
            var person = await _vehicleService.UpdateAsync(id, model);
            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleService.DeleteAsync(id);

            return Ok();
        }
    }
}
