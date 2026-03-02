namespace UbiquitousEngine.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using UbiquitousEngine.Api.Models;
using UbiquitousEngine.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class ServiceTicketsController : ControllerBase
{
    private readonly IServiceTicketService _serviceTicketService;

    public ServiceTicketsController(IServiceTicketService serviceTicketService)
    {
        _serviceTicketService = serviceTicketService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceTicket>> GetServiceTicket(int id)
    {
        var serviceTicket = await _serviceTicketService.GetServiceTicketByIdAsync(id);
        if (serviceTicket == null)
            return NotFound();

        return Ok(serviceTicket);
    }

    [HttpGet("vehicle/{vehicleId}")]
    public async Task<ActionResult<List<ServiceTicket>>> GetServiceTicketsByVehicleId(int vehicleId)
    {
        var serviceTickets = await _serviceTicketService.GetServiceTicketsByVehicleIdAsync(vehicleId);
        return Ok(serviceTickets);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceTicket>> CreateServiceTicket(ServiceTicket serviceTicket)
    {
        if (string.IsNullOrWhiteSpace(serviceTicket.Description) || serviceTicket.VehicleId <= 0)
            return BadRequest("Description and valid VehicleId are required.");

        var createdServiceTicket = await _serviceTicketService.CreateServiceTicketAsync(serviceTicket);
        return CreatedAtAction(nameof(GetServiceTicket), new { id = createdServiceTicket.Id }, createdServiceTicket);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceTicket>> UpdateServiceTicket(int id, ServiceTicket serviceTicket)
    {
        var existingServiceTicket = await _serviceTicketService.GetServiceTicketByIdAsync(id);
        if (existingServiceTicket == null)
            return NotFound();

        serviceTicket.Id = id;
        serviceTicket.CreatedAt = existingServiceTicket.CreatedAt;
        
        var updatedServiceTicket = await _serviceTicketService.UpdateServiceTicketAsync(serviceTicket);
        return Ok(updatedServiceTicket);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceTicket(int id)
    {
        var result = await _serviceTicketService.DeleteServiceTicketAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpGet("{id}/total-cost")]
    public async Task<ActionResult<decimal>> GetTotalCost(int id)
    {
        var serviceTicket = await _serviceTicketService.GetServiceTicketByIdAsync(id);
        if (serviceTicket == null)
            return NotFound();

        var totalCost = await _serviceTicketService.CalculateTotalCostAsync(id);
        return Ok(new { totalCost });
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteServiceTicket(int id)
    {
        var result = await _serviceTicketService.MarkServiceTicketCompleteAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
