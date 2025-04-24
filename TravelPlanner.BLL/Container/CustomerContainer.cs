using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.BLL.Container;

public class CustomerContainer : ICustomerContainer
{
    private readonly DbManager _db;

    public CustomerContainer(DbManager db)
    {
        _db = db;
    }

    public void CreateCustomer(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }

        if (string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Firstname) || string.IsNullOrEmpty(customer.Surname))
        {
            throw new ArgumentException("Essential customer data (Email, Firstname, or Surname) is missing");
        }

        var existingCustomer = _db.Customers.FirstOrDefault(c => c.Email == customer.Email);
        if (existingCustomer != null)
        {
            throw new InvalidOperationException($"A customer with the email {customer.Email} already exists.");
        }

        var result = _db.InsertWithInt32Identity(customer);
        if (result <= 0)
        {
            throw new InvalidOperationException("Failed to create customer in the database");
        }
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Customer Id must be positive", nameof(id));
        }

        return await _db.Customers.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateCustomer(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }

        if (customer.Id <= 0)
        {
            throw new ArgumentException("Customer must have a valid Id");
        }

        var existingCustomer = await GetCustomerById(customer.Id);
        if (existingCustomer == null)
        {
            throw new InvalidOperationException("Customer does not exist and cannot be updated");
        }

        var result = await _db.UpdateAsync(customer);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update customer");
        }
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        var customers = await _db.Customers
                        .ToListAsync();
        if (customers == null || !customers.Any())
        {
            throw new InvalidOperationException("No customers found");
        }
        return customers;
    }
}
