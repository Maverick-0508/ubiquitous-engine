namespace UbiquitousEngine.Api.Models;

public class Customer
{
    public int Id { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    
    public required string PhoneNumber { get; set; }
    
    public required string Address { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
