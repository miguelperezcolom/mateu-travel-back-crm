using System.Net;
using System.Text.Json;
using Application.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests;


public class CustomerApiTests : IClassFixture<ApiWebApplicationFactory>
{

    private readonly ApiWebApplicationFactory _fixture;

    public CustomerApiTests(ApiWebApplicationFactory fixture)
    {
        _fixture = fixture;
    }


    [Fact]
    public async Task given_customer_when_save_then_saved()
    {
        //given
        var customer = new Customer()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Mateu",
            Address = "Calle X, 38, 7B",
            City = "Palma de Mallorca",
            Country = "Spain"
        };
        using var client = _fixture.CreateClient();

        //when
        await client.PostAsJsonAsync($"/api/v1/customers", customer);

        //then
        var response = await client.GetAsync($"/api/v1/customers/{customer.Id}");
        var read = await response.Content.ReadAsAsync<Customer>();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        JsonSerializer.Serialize(customer).Should().Match(JsonSerializer.Serialize(read));
    }
    
    
    [Fact]
    public async Task given_customer_when_remove_then_removed()
    {
        //given
        var customer = new Customer()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Mateu",
            Address = "Calle X 38, 7B",
            City = "Palma de Mallorca",
            Country = "Spain"
        };
        using var client = _fixture.CreateClient();
        await client.PostAsJsonAsync($"/api/v1/customers", customer);

        //when
        await client.DeleteAsync($"/api/v1/customers/{customer.Id}");

        //then
        var response = await client.GetAsync($"/api/v1/customers/{customer.Id}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task given_customer_when_update_then_updated()
    {
        //given
        var customer = new Customer()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Mateu",
            Address = "Calle X, 38, 7B",
            City = "Palma de Mallorca",
            Country = "Spain"
        };
        using var client = _fixture.CreateClient();
        await client.PostAsJsonAsync($"/api/v1/customers", customer);

        //when
        var customerUpdated = new Customer()
        {
            Id = customer.Id,
            Name = "Antonia",
            Address = "Calle Y, 12A, 7D",
            City = "Paris",
            Country = "Framce"
        };
        await client.PutAsJsonAsync($"/api/v1/customers", customerUpdated);

        //then
        var response = await client.GetAsync($"/api/v1/customers/{customer.Id}");
        var read = await response.Content.ReadAsAsync<Customer>();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        JsonSerializer.Serialize(customerUpdated).Should().Match(JsonSerializer.Serialize(read));
    }

    [Fact]
    public async Task given_customer_when_save_then_listed()
    {
        //given
        var customer = new Customer()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Mateu",
            Address = "Calle X, 38, 7B",
            City = "Palma de Mallorca",
            Country = "Spain"
        };
        using var client = _fixture.CreateClient();

        //when
        await client.PostAsJsonAsync($"/api/v1/customers", customer);

        //then
        var response = await client.GetAsync($"/api/v1/customers");
        var read = await response.Content.ReadAsAsync<List<Customer>>();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        read.Select(c => JsonSerializer.Serialize(c)).Should().Contain(JsonSerializer.Serialize(customer));
    }

}