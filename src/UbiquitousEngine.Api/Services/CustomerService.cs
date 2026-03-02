namespace UbiquitousEngine.Api.Services;

using Microsoft.EntityFrameworkCore;
using UbiquitousEngine.Api.Data;
using UbiquitousEngine.Api.Models;

public class CustomerService : ICustomerService
{
    private readonly ServiceManagementContext _context;

    public CustomerService(ServiceManagementContext context)
    {
        _context = context;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Vehicles)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers
            .Include(c => c.Vehicles)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ToListAsync();
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        customer.CreatedAt = DateTime.UtcNow;
        customer.UpdatedAt = DateTime.UtcNow;
        
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        
        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        customer.UpdatedAt = DateTime.UtcNow;
        
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
        
        return customer;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
            return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
