namespace UbiquitousEngine.Api.Services;

using Microsoft.EntityFrameworkCore;
using UbiquitousEngine.Api.Data;
using UbiquitousEngine.Api.Models;

public class PartService : IPartService
{
    private readonly ServiceManagementContext _context;

    public PartService(ServiceManagementContext context)
    {
        _context = context;
    }

    public async Task<Part?> GetPartByIdAsync(int id)
    {
        return await _context.Parts.FindAsync(id);
    }

    public async Task<List<Part>> GetAllPartsAsync()
    {
        return await _context.Parts
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Part> CreatePartAsync(Part part)
    {
        part.CreatedAt = DateTime.UtcNow;
        part.UpdatedAt = DateTime.UtcNow;
        
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();
        
        return part;
    }

    public async Task<Part> UpdatePartAsync(Part part)
    {
        part.UpdatedAt = DateTime.UtcNow;
        
        _context.Parts.Update(part);
        await _context.SaveChangesAsync();
        
        return part;
    }

    public async Task<bool> DeletePartAsync(int id)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null)
            return false;

        _context.Parts.Remove(part);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeductPartQuantityAsync(int partId, int quantity)
    {
        var part = await _context.Parts.FindAsync(partId);
        if (part == null || part.QuantityInStock < quantity)
            return false;

        part.QuantityInStock -= quantity;
        part.UpdatedAt = DateTime.UtcNow;
        
        _context.Parts.Update(part);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
