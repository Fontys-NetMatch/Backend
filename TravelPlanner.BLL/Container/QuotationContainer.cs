using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.DB;
using TravelPlanner.Domain.Enums;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.PDF;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Quotation;

namespace TravelPlanner.BLL.Container;

public class QuotationContainer : IQuotationContainer
{
    private readonly DbManager _db;
    private readonly IPDFService pdf;

    public QuotationContainer(DbManager db, IPDFService pdf)
    {
        _db = db;
        this.pdf = pdf;
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

    public async Task<Quotation?> GetQuotationById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Quotation ID must be positive", nameof(id));
        }

        return await _db.Quotations.LoadWith(q => q.Customer).FirstOrDefaultAsync(q => q.ID == id);
    }

    public Task<List<ProductDate>> GetQuotationProducts(int quotationId)
    {
        throw new NotImplementedException();
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

        var existingQuotation = await GetQuotationById(quotation.ID);
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

        var quotation = await GetQuotationById(id);
        if (quotation == null)
        {
            throw new InvalidOperationException("Quotation does not exist and cannot be soft-deleted");
        }

        if (quotation.Status == QuotationStatus.Archived)
        {
            throw new InvalidOperationException("Quotation is already inactive");
        }

        quotation.Status = QuotationStatus.Archived;
        await _db.UpdateAsync(quotation);
    }

    public async Task<List<Quotation>> GetAllQuotations()
    {
        var quotations = await _db.Quotations
                                  .LoadWith(q => q.Customer)
                                  .ToListAsync();
        if (!quotations.Any())
        {
            throw new InvalidOperationException("No active quotations found.");
        }
        return quotations;
    }

    public async Task<List<Quotation>> GetAllActiveQuotations()
    {
        var quotations = await _db.Quotations
            .LoadWith(q => q.Customer)
            .Where(q => q.Status != QuotationStatus.Archived)
            .ToListAsync();

        if (!quotations.Any())
        {
            throw new InvalidOperationException("No active quotations found.");
        }
        return quotations;
    }
    public async Task<FileContentResult> GeneratePdfAsync(int id)
    {
        var quotation = await GetQuotationById(id);
        if (quotation == null)
            throw new InvalidOperationException("Quotation not found");

        return await pdf.GenerateQuotation(quotation);
    }

}
