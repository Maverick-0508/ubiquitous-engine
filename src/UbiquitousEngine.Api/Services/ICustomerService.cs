namespace UbiquitousEngine.Api.Services;

using UbiquitousEngine.Api.Models;

public interface ICustomerService
{
    Task<Customer?> GetCustomerByIdAsync(int id);
    
    Task<List<Customer>> GetAllCustomersAsync();
    
    Task<Customer> CreateCustomerAsync(Customer customer);
    
    Task<Customer> UpdateCustomerAsync(Customer customer);
    
    Task<bool> DeleteCustomerAsync(int id);
}
