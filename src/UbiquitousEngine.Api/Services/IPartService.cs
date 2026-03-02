namespace UbiquitousEngine.Api.Services;

using UbiquitousEngine.Api.Models;

public interface IPartService
{
    Task<Part?> GetPartByIdAsync(int id);
    
    Task<List<Part>> GetAllPartsAsync();
    
    Task<Part> CreatePartAsync(Part part);
    
    Task<Part> UpdatePartAsync(Part part);
    
    Task<bool> DeletePartAsync(int id);
    
    Task<bool> DeductPartQuantityAsync(int partId, int quantity);
}
