namespace UbiquitousEngine.Api.Services;

using Microsoft.EntityFrameworkCore;
using UbiquitousEngine.Api.Data;
using UbiquitousEngine.Api.Models;

public class ServiceTicketService : IServiceTicketService
{
    private readonly ServiceManagementContext _context;

    public ServiceTicketService(ServiceManagementContext context)
    {
        _context = context;
    }

    public async Task<ServiceTicket?> GetServiceTicketByIdAsync(int id)
    {
        return await _context.ServiceTickets
            .Include(st => st.Vehicle)
            .Include(st => st.ServiceTicketParts)
            .ThenInclude(stp => stp.Part)
            .FirstOrDefaultAsync(st => st.Id == id);
    }

    public async Task<List<ServiceTicket>> GetServiceTicketsByVehicleIdAsync(int vehicleId)
    {
        return await _context.ServiceTickets
            .Where(st => st.VehicleId == vehicleId)
            .Include(st => st.ServiceTicketParts)
            .ThenInclude(stp => stp.Part)
            .OrderByDescending(st => st.CreatedAt)
            .ToListAsync();
    }

    public async Task<ServiceTicket> CreateServiceTicketAsync(ServiceTicket serviceTicket)
    {
        serviceTicket.CreatedAt = DateTime.UtcNow;
        serviceTicket.UpdatedAt = DateTime.UtcNow;
        
        _context.ServiceTickets.Add(serviceTicket);
        await _context.SaveChangesAsync();
        
        return serviceTicket;
    }

    public async Task<ServiceTicket> UpdateServiceTicketAsync(ServiceTicket serviceTicket)
    {
        serviceTicket.UpdatedAt = DateTime.UtcNow;
        
        _context.ServiceTickets.Update(serviceTicket);
        await _context.SaveChangesAsync();
        
        return serviceTicket;
    }

    public async Task<bool> DeleteServiceTicketAsync(int id)
    {
        var serviceTicket = await _context.ServiceTickets.FindAsync(id);
        if (serviceTicket == null)
            return false;

        _context.ServiceTickets.Remove(serviceTicket);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<decimal> CalculateTotalCostAsync(int serviceTicketId)
    {
        var serviceTicket = await _context.ServiceTickets
            .Include(st => st.ServiceTicketParts)
            .ThenInclude(stp => stp.Part)
            .FirstOrDefaultAsync(st => st.Id == serviceTicketId);

        if (serviceTicket == null)
            return 0;

        decimal totalCost = serviceTicket.LaborCost;

        foreach (var serviceTicketPart in serviceTicket.ServiceTicketParts)
        {
            if (serviceTicketPart.Part != null)
            {
                totalCost += serviceTicketPart.Part.Price * serviceTicketPart.Quantity;
            }
        }

        return totalCost;
    }

    public async Task<bool> MarkServiceTicketCompleteAsync(int id)
    {
        var serviceTicket = await _context.ServiceTickets.FindAsync(id);
        if (serviceTicket == null)
            return false;

        if (serviceTicket.Status != ServiceStatus.Open && serviceTicket.Status != ServiceStatus.InProgress)
            return false;

        serviceTicket.Status = ServiceStatus.Completed;
        serviceTicket.CompletedAt = DateTime.UtcNow;
        serviceTicket.UpdatedAt = DateTime.UtcNow;

        _context.ServiceTickets.Update(serviceTicket);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
