using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.BLL;

public class QuotationContainer
{
    private readonly DbManager _db;

    // Constructor to initialize the DbManager
    public QuotationContainer(DbManager db)
    {
        _db = db;
    }

    // Method to create a new quotation
    public void CreateQuotation(Quotation quotation)
    {
        // Check if the quotation is null
        if (quotation == null)
        {
            throw new ArgumentNullException(nameof(quotation), "Quotation cannot be null");
        }

        // Check if the quotation name is valid
        if (string.IsNullOrEmpty(quotation.Name))
        {
            throw new ArgumentException("Quotation name cannot be null or empty");
        }

        // Check if the Customer ID is valid
        if (quotation.Customer_ID <= 0)
        {
            throw new ArgumentException("Invalid Customer ID");
        }

        // Insert the quotation into the database
        var result = _db.InsertWithInt32Identity(quotation);
        if (result <= 0)
        {
            throw new InvalidOperationException("Failed to create quotation in the database");
        }
    }

    // Method to get a quotation by its ID
    public async Task<Quotation?> GetQuotationByIdAsync(int id)
    {
        // Check if the ID is valid
        if (id <= 0)
        {
            throw new ArgumentException("Quotation ID must be positive", nameof(id));
        }

        // Retrieve the quotation and load the associated customer
        return await _db.Quotations.LoadWith(q => q.Customer).FirstOrDefaultAsync(q => q.ID == id);
    }

    // Method to update an existing quotation
    public async Task UpdateQuotation(Quotation quotation)
    {
        // Check if the quotation is null
        if (quotation == null)
        {
            throw new ArgumentNullException(nameof(quotation), "Quotation cannot be null");
        }

        // Check if the quotation has a valid ID
        if (quotation.ID <= 0)
        {
            throw new ArgumentException("Quotation must have a valid ID");
        }

        // Check if the quotation exists before updating
        var existingQuotation = await GetQuotationByIdAsync(quotation.ID);
        if (existingQuotation == null)
        {
            throw new InvalidOperationException("Quotation does not exist and cannot be updated");
        }

        // Update the quotation in the database
        var result = await _db.UpdateAsync(quotation);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update quotation");
        }
    }

    // Method to soft delete a quotation (deactivate it)
    public async Task SoftDeleteQuotation(int id)
    {
        // Check if the ID is valid
        if (id <= 0)
        {
            throw new ArgumentException("Quotation ID must be positive", nameof(id));
        }

        // Retrieve the quotation by ID
        var quotation = await GetQuotationByIdAsync(id);
        if (quotation == null)
        {
            throw new InvalidOperationException("Quotation does not exist and cannot be soft-deleted");
        }

        // Check if the quotation is already inactive
        if (!quotation.IsActive)
        {
            throw new InvalidOperationException("Quotation is already inactive");
        }

        // Set the quotation as inactive and update it
        quotation.IsActive = false;
        await UpdateQuotation(quotation);
    }

    // Method to get all active quotations
    public async Task<IEnumerable<Quotation>> GetAllActiveQuotationsAsync()
    {
        // Retrieve all active quotations from the database and load the associated customer
        var quotations = await _db.Quotations
                                  .LoadWith(q => q.Customer)
                                  .Where(q => q.IsActive)
                                  .ToListAsync();

        // Check if any active quotations exist
        if (!quotations.Any())
        {
            throw new InvalidOperationException("No active quotations found.");
        }

        return quotations;
    }
}
