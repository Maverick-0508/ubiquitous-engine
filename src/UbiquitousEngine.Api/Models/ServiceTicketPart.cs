namespace UbiquitousEngine.Api.Models;

public class ServiceTicketPart
{
    public int Id { get; set; }
    
    public int ServiceTicketId { get; set; }
    
    public int PartId { get; set; }
    
    public int Quantity { get; set; }
    
    // Navigation properties
    public ServiceTicket? ServiceTicket { get; set; }
    
    public Part? Part { get; set; }
}
