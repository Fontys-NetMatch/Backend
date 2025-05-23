using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.DB.Repositories;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.BLL.Container;

public class CustomerContainer : ICustomerContainer
{
    private readonly ICustomerRepository _repository;

    public CustomerContainer(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");

        if (string.IsNullOrWhiteSpace(customer.Email) ||
            string.IsNullOrWhiteSpace(customer.Firstname) ||
            string.IsNullOrWhiteSpace(customer.Surname))
        {
            throw new ArgumentException("Essential customer data (Email, Firstname, or Surname) is missing");
        }

        if (_repository.GetByEmail(customer.Email) != null)
            throw new InvalidOperationException($"A customer with the email {customer.Email} already exists.");

        if (_repository.Create(customer) <= 0)
            throw new InvalidOperationException("Failed to create customer in the database");
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Customer Id must be positive", nameof(id));

        return await _repository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(Customer customer)
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

    public async Task<List<Customer>> GetAllAsync()
    {
        var customers = await _repository.GetAllAsync();

        if (customers is not { Count: > 0 })
            throw new InvalidOperationException("No customers found");

        return customers;
    }
}
