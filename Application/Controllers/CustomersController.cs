using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/v1/customers")]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;

    private static readonly Dictionary<string, Customer> _data = new Dictionary<string, Customer>();

    public CustomersController(ILogger<CustomersController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Customer> GetAll()
    {
        return _data.Values;
    }
    
    [HttpGet]
    [Route("{customerId}")]
    public ActionResult Get(string customerId)
    {
        if (!_data.ContainsKey(customerId)) return new NotFoundResult();
        return new OkObjectResult(_data[customerId]);
    }

    [HttpPost]
    public void Add(Customer customer)
    {
        _data[customer.Id] = customer;
    }

    [HttpPut]
    public void Update(Customer customer)
    {
        _data[customer.Id] = customer;
    }
    
    [HttpDelete]
    [Route("{customerId}")]
    public void Delete(string customerId)
    {
        _data.Remove(customerId);
    }

}