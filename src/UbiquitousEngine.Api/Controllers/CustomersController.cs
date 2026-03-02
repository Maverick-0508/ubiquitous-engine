namespace UbiquitousEngine.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using UbiquitousEngine.Api.Models;
using UbiquitousEngine.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Customer>>> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.FirstName) || 
            string.IsNullOrWhiteSpace(customer.LastName) ||
            string.IsNullOrWhiteSpace(customer.Email))
            return BadRequest("FirstName, LastName, and Email are required.");

        var createdCustomer = await _customerService.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
    {
        var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
        if (existingCustomer == null)
            return NotFound();

        customer.Id = id;
        customer.CreatedAt = existingCustomer.CreatedAt;
        
        var updatedCustomer = await _customerService.UpdateCustomerAsync(customer);
        return Ok(updatedCustomer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
