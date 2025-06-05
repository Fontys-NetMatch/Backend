using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Repositories;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.BLL.Container;

public class CustomerContainer : ICustomerContainer
{
    
   

    private readonly CustomerRepository _repository;

    public CustomerContainer(CustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");

        var missingFields = new List<string>();

        if (string.IsNullOrEmpty(customer.Email)) missingFields.Add("Email");
        if (string.IsNullOrEmpty(customer.Firstname)) missingFields.Add("Firstname");
        if (string.IsNullOrEmpty(customer.Surname)) missingFields.Add("Surname");

        switch (missingFields.Count)
        {
            case > 0:
                throw new ArgumentException($"Missing customer data: {string.Join(", ", missingFields)}");
        }

        if (_repository.GetByEmail(customer.Email) != null)
            throw new InvalidOperationException($"A customer with the email {customer.Email} already exists.");

        if (_repository.Create(customer) <= 0)
            throw new InvalidOperationException("Failed to create customer in the database");
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Customer Id must be positive", nameof(id));

        return await _repository.GetByIdAsync(id);
    }

    public async Task UpdateCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");

        if (customer.Id <= 0)
            throw new ArgumentException("Customer must have a valid Id");

        var existing = await _repository.GetByIdAsync(customer.Id);
        if (existing == null)
            throw new InvalidOperationException("Customer does not exist and cannot be updated");

        if (await _repository.UpdateAsync(customer) == 0)
            throw new InvalidOperationException("Failed to update customer");
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        var customers = await _repository.GetAllAsync();

        if (customers is not { Count: > 0 })
            throw new InvalidOperationException("No customers found");

        return customers;
    }
}
