using Domain.Customers;
using Domain.Services;
using Domain.Services.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route("api/v1/customers")]
[ApiController]
public class CustomersController : ControllerBase

{
    private readonly ILogger<CustomersController> _logger;
    private readonly IHandler _handler;
    private readonly CommandQueryFactory _commandQueryFactory;


    public CustomersController(ILogger<CustomersController> logger, IHandler handler, CommandQueryFactory commandQueryFactory)
    {
        _logger = logger;
        _handler = handler;
        _commandQueryFactory = commandQueryFactory;
    }

    [HttpGet]
    [Route("")]
    public async Task<List<Customer>> GetAll()
    {
        return await _handler.RunQueryAsync(_commandQueryFactory.CreateGetCustomersQuery(0));
    }
    
    [HttpGet]
    [Route("{customerId}")]
    public async Task<ActionResult> Get(string customerId)
    {
        var customer = await _handler.RunQueryAsync(_commandQueryFactory.CreateGetCustomerQuery(customerId));
        if (customer == null) return new NotFoundObjectResult(null);
        return new OkObjectResult(customer);
    }

    [HttpPost]
    [Route("")]
    public async Task Add(Models.Customer customer)
    {
        await _handler.RunCommandAsync(_commandQueryFactory.CreateAddCustomerCommand(new Customer
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            City = customer.City,
            Country = customer.Country,
        }));
    }

    [HttpPut]
    [Route("{customerId}")]
    public async Task Update(string customerId, Models.Customer customer)
    {
        if (!customerId.Equals(customer.Id)) throw new BadHttpRequestException("url does not match id in customer");
        await _handler.RunCommandAsync(_commandQueryFactory.CreateUpdateCustomerCommand(new Customer
        {
            Id = customer.Id,
            Name = customer.Name,
            Address = customer.Address,
            City = customer.City,
            Country = customer.Country,
        }));
    }
    
    [HttpDelete]
    [Route("{customerId}")]
    public async Task Delete(string customerId)
    {
        await _handler.RunCommandAsync(_commandQueryFactory.CreateDeleteCustomerCommand(customerId));
    }

}