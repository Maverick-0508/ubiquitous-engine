namespace UbiquitousEngine.Api.Models;

public class ServiceTicket
{
    public int Id { get; set; }
    
    public int VehicleId { get; set; }
    
    public required string Description { get; set; }
    
    public ServiceStatus Status { get; set; } = ServiceStatus.Open;
    
    public decimal LaborCost { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? CompletedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public Vehicle? Vehicle { get; set; }
    
    public ICollection<ServiceTicketPart> ServiceTicketParts { get; set; } = new List<ServiceTicketPart>();
}

public enum ServiceStatus
{
    Open,
    InProgress,
    Completed,
    Cancelled
}
