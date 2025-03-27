using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Request.Quotation;

namespace TravelPlanner.BLL;

public class QuotationContainer : IQuotationContainer
{
    private readonly DbManager _db;

    public QuotationContainer(DbManager db)
    {
        _db = db;
    }

    public void CreateQuotation(QuotationData quotation)
    {
        if (quotation == null)
        {
            throw new ArgumentNullException(nameof(quotation), "Quotation cannot be null");
        }

        if (string.IsNullOrEmpty(quotation.Name))
        {
            throw new ArgumentException("Quotation name cannot be null or empty");
        }

        if (quotation.Customer_ID <= 0)
        {
            throw new ArgumentException("Invalid Customer ID");
        }

        var result = _db.InsertWithInt32Identity(quotation);
        if (result <= 0)
        {
            throw new InvalidOperationException("Failed to create quotation in the database");
        }
    }

    public async Task<Quotation?> GetQuotationByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Quotation ID must be positive", nameof(id));
        }

        return await _db.Quotations.LoadWith(q => q.Customer).FirstOrDefaultAsync(q => q.ID == id);
    }

    public async Task UpdateQuotation(QuotationUpdateData quotation)
    {
        if (quotation == null)
        {
            throw new ArgumentNullException(nameof(quotation), "Quotation cannot be null");
        }

        if (quotation.ID <= 0)
        {
            throw new ArgumentException("Quotation must have a valid ID");
        }

        var existingQuotation = await GetQuotationByIdAsync(quotation.ID);
        if (existingQuotation == null)
        {
            throw new InvalidOperationException("Quotation does not exist and cannot be updated");
        }

        var result = await _db.UpdateAsync(quotation);
        if (result == 0)
        {
            throw new InvalidOperationException("Failed to update quotation");
        }
    }

    public async Task SoftDeleteQuotation(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Quotation ID must be positive", nameof(id));
        }

        var quotation = await GetQuotationByIdAsync(id);
        if (quotation == null)
        {
            throw new InvalidOperationException("Quotation does not exist and cannot be soft-deleted");
        }

        if (!quotation.IsActive)
        {
            throw new InvalidOperationException("Quotation is already inactive");
        }

        quotation.IsActive = false;
        await _db.UpdateAsync(quotation);
    }

    public async Task<List<Quotation>> GetAllActiveQuotationsAsync()
    {
        var quotations = await _db.Quotations
                                  .LoadWith(q => q.Customer)
                                  .Where(q => q.IsActive)
                                  .ToListAsync();

        if (!quotations.Any())
        {
            throw new InvalidOperationException("No active quotations found.");
        }
        return quotations;
    }
}
