namespace UbiquitousEngine.Api.Models;

public class Vehicle
{
    public int Id { get; set; }
    
    public required string VIN { get; set; }
    
    public required string Make { get; set; }
    
    public required string Model { get; set; }
    
    public int Year { get; set; }
    
    public required string LicensePlate { get; set; }
    
    public int CustomerId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public Customer? Customer { get; set; }
    
    public ICollection<ServiceTicket> ServiceTickets { get; set; } = new List<ServiceTicket>();
}
