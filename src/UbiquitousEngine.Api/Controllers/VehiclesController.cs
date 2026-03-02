namespace UbiquitousEngine.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using UbiquitousEngine.Api.Models;
using UbiquitousEngine.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vehicle>> GetVehicle(int id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        if (vehicle == null)
            return NotFound();

        return Ok(vehicle);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<List<Vehicle>>> GetVehiclesByCustomerId(int customerId)
    {
        var vehicles = await _vehicleService.GetVehiclesByCustomerIdAsync(customerId);
        return Ok(vehicles);
    }

    [HttpPost]
    public async Task<ActionResult<Vehicle>> CreateVehicle(Vehicle vehicle)
    {
        if (string.IsNullOrWhiteSpace(vehicle.VIN) || 
            string.IsNullOrWhiteSpace(vehicle.Make) ||
            string.IsNullOrWhiteSpace(vehicle.Model) ||
            vehicle.CustomerId <= 0)
            return BadRequest("VIN, Make, Model, and valid CustomerId are required.");

        var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicle);
        return CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.Id }, createdVehicle);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Vehicle>> UpdateVehicle(int id, Vehicle vehicle)
    {
        var existingVehicle = await _vehicleService.GetVehicleByIdAsync(id);
        if (existingVehicle == null)
            return NotFound();

        vehicle.Id = id;
        vehicle.CreatedAt = existingVehicle.CreatedAt;
        
        var updatedVehicle = await _vehicleService.UpdateVehicleAsync(vehicle);
        return Ok(updatedVehicle);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var result = await _vehicleService.DeleteVehicleAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
