namespace UbiquitousEngine.Api.Services;

using UbiquitousEngine.Api.Models;

public interface IServiceTicketService
{
    Task<ServiceTicket?> GetServiceTicketByIdAsync(int id);
    
    Task<List<ServiceTicket>> GetServiceTicketsByVehicleIdAsync(int vehicleId);
    
    Task<ServiceTicket> CreateServiceTicketAsync(ServiceTicket serviceTicket);
    
    Task<ServiceTicket> UpdateServiceTicketAsync(ServiceTicket serviceTicket);
    
    Task<bool> DeleteServiceTicketAsync(int id);
    
    Task<decimal> CalculateTotalCostAsync(int serviceTicketId);
    
    Task<bool> MarkServiceTicketCompleteAsync(int id);
}
