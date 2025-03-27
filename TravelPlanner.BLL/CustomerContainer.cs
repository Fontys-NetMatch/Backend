using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.BLL;

public class CustomerContainer
{
    // Private field to interact with the database
    private readonly DbManager _db;

    // Constructor to initialize DbManager
    public CustomerContainer(DbManager db)
    {
        _db = db;
    }

    // Method to create a new customer
    public void CreateCustomer(Customer customer)
    {
        // Check if the customer object is null
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }

        // Check if essential customer data is missing
        if (string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.Firstname) || string.IsNullOrEmpty(customer.Surname))
        {
            throw new ArgumentException("Essential customer data (Email, Firstname, or Surname) is missing");
        }

        // Check if a customer with the same email already exists
        var existingCustomer = _db.Customers.FirstOrDefault(c => c.Email == customer.Email);
        if (existingCustomer != null)
        {
            throw new InvalidOperationException($"A customer with the email {customer.Email} already exists.");
        }

        // Insert the new customer into the database
        var result = _db.InsertWithInt32Identity(customer);
        if (result <= 0)
        {
            throw new InvalidOperationException("Failed to create customer in the database");
        }
    }

    // Method to get a customer by ID
    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        // Check if the ID is valid
        if (id <= 0)
        {
            throw new ArgumentException("Customer ID must be positive", nameof(id));
        }

        // Retrieve customer by ID
        return await _db.Customers.FirstOrDefaultAsync(c => c.ID == id);
    }

    // Method to update customer details
    public async Task UpdateCustomer(Customer customer)
    {
        // Check if the customer object is null
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }

        // Check if the customer ID is valid
        if (customer.ID <= 0)
        {
            throw new ArgumentException("Customer must have a valid ID");
        }

        // Check if the customer exists before updating
        var existingCustomer = await GetCustomerByIdAsync(customer.ID);
        if (existingCustomer == null)
        {
            throw new InvalidOperationException("Customer does not exist and cannot be updated");
        }

        // Update the customer in the database
        var result = await _db.UpdateAsync(customer);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update customer");
        }
    }

    // Method to soft delete a customer (deactivate)
    public async Task SoftDeleteCustomer(int id)
    {
        // Check if the ID is valid
        if (id <= 0)
        {
            throw new ArgumentException("Customer ID must be positive", nameof(id));
        }

        // Retrieve customer by ID
        var customer = await GetCustomerByIdAsync(id);
        if (customer == null)
        {
            throw new InvalidOperationException("Customer does not exist and cannot be soft-deleted");
        }

        // Check if the customer is already inactive
        if (!customer.IsActive)
        {
            throw new InvalidOperationException("Customer is already inactive");
        }

        // Set customer as inactive and update
        customer.IsActive = false;
        await UpdateCustomer(customer);
    }

    // Method to get all active customers
    public async Task<IEnumerable<Customer>> GetAllActiveCustomersAsync()
    {
        // Retrieve all active customers from the database
        var customers = await _db.Customers
                        .Where(c => c.IsActive)
                        .ToListAsync();

        // Throw exception if no active customers found
        if (customers == null || !customers.Any())
        {
            throw new InvalidOperationException("No active customers found");
        }

        return customers;
    }
}
