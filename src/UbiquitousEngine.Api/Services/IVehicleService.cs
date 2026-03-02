namespace UbiquitousEngine.Api.Services;

using UbiquitousEngine.Api.Models;

public interface IVehicleService
{
    Task<Vehicle?> GetVehicleByIdAsync(int id);
    
    Task<List<Vehicle>> GetVehiclesByCustomerIdAsync(int customerId);
    
    Task<Vehicle> CreateVehicleAsync(Vehicle vehicle);
    
    Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
    
    Task<bool> DeleteVehicleAsync(int id);
}
