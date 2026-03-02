namespace UbiquitousEngine.Api.Models;

public class Part
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string PartNumber { get; set; }
    
    public decimal Price { get; set; }
    
    public int QuantityInStock { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public ICollection<ServiceTicketPart> ServiceTicketParts { get; set; } = new List<ServiceTicketPart>();
}
