namespace UbiquitousEngine.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using UbiquitousEngine.Api.Models;
using UbiquitousEngine.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class PartsController : ControllerBase
{
    private readonly IPartService _partService;

    public PartsController(IPartService partService)
    {
        _partService = partService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Part>>> GetAllParts()
    {
        var parts = await _partService.GetAllPartsAsync();
        return Ok(parts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Part>> GetPart(int id)
    {
        var part = await _partService.GetPartByIdAsync(id);
        if (part == null)
            return NotFound();

        return Ok(part);
    }

    [HttpPost]
    public async Task<ActionResult<Part>> CreatePart(Part part)
    {
        if (string.IsNullOrWhiteSpace(part.Name) || 
            string.IsNullOrWhiteSpace(part.PartNumber) ||
            part.Price <= 0)
            return BadRequest("Name, PartNumber, and valid Price are required.");

        var createdPart = await _partService.CreatePartAsync(part);
        return CreatedAtAction(nameof(GetPart), new { id = createdPart.Id }, createdPart);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Part>> UpdatePart(int id, Part part)
    {
        var existingPart = await _partService.GetPartByIdAsync(id);
        if (existingPart == null)
            return NotFound();

        part.Id = id;
        part.CreatedAt = existingPart.CreatedAt;
        
        var updatedPart = await _partService.UpdatePartAsync(part);
        return Ok(updatedPart);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePart(int id)
    {
        var result = await _partService.DeletePartAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
