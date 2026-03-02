namespace UbiquitousEngine.Api.Services;

using Microsoft.EntityFrameworkCore;
using UbiquitousEngine.Api.Data;
using UbiquitousEngine.Api.Models;

public class VehicleService : IVehicleService
{
    private readonly ServiceManagementContext _context;

    public VehicleService(ServiceManagementContext context)
    {
        _context = context;
    }

    public async Task<Vehicle?> GetVehicleByIdAsync(int id)
    {
        return await _context.Vehicles
            .Include(v => v.Customer)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<List<Vehicle>> GetVehiclesByCustomerIdAsync(int customerId)
    {
        return await _context.Vehicles
            .Where(v => v.CustomerId == customerId)
            .Include(v => v.Customer)
            .OrderBy(v => v.Make)
            .ThenBy(v => v.Model)
            .ToListAsync();
    }

    public async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle)
    {
        vehicle.CreatedAt = DateTime.UtcNow;
        vehicle.UpdatedAt = DateTime.UtcNow;
        
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        
        return vehicle;
    }

    public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
    {
        vehicle.UpdatedAt = DateTime.UtcNow;
        
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();
        
        return vehicle;
    }

    public async Task<bool> DeleteVehicleAsync(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle == null)
            return false;

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
