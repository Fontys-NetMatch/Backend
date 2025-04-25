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

        if (quotation.CustomerId <= 0)
        {
            throw new ArgumentException("Invalid Customer Id");
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
            throw new ArgumentException("Quotation Id must be positive", nameof(id));
        }

        return await _db.Quotations.LoadWith(q => q.Customer).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<List<ProductDate>> GetQuotationProducts(int quotationId)
    {
        var results = await _db.QuotationProductDates
        .LoadWith(qpd => qpd.ProductDate)
        .Where(qpd => qpd.QuotationId == quotationId)
        .ToListAsync();

        return results.Select(qpd => qpd.ProductDate).ToList();
    }

    public async Task UpdateQuotation(QuotationUpdateData quotation)
    {
        if (quotation == null)
        {
            throw new ArgumentNullException(nameof(quotation), "Quotation cannot be null");
        }

        if (quotation.Id <= 0)
        {
            throw new ArgumentException("Quotation must have a valid Id");
        }

        var existingQuotation = await GetQuotationById(quotation.Id);
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
            throw new ArgumentException("Quotation Id must be positive", nameof(id));
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

    public async Task<List<Quotation>> GetAllQuotations(QuotationFiltersData filters)
    {
        var query = _db.Quotations
            .LoadWith(q => q.Customer)
            .LoadWith(q => q.User)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.Name))
        {
            query = query.Where(q => q.Name.Contains(filters.Name));
        }

        if (filters.Statuses != null && filters.Statuses.Any())
        {
            query = query.Where(q => filters.Statuses.Contains(q.Status));
        }

        if (!string.IsNullOrWhiteSpace(filters.SearchQuery))
        {
            var keyword = filters.SearchQuery.ToLower();

            query = query.Where(q =>
                (q.Customer.Firstname + " " + q.Customer.Surname).ToLower().Contains(keyword) ||
                (q.User.Firstname + " " + q.User.Surname).ToLower().Contains(keyword)
            );
        }

        return await query.ToListAsync();
    }




    public async Task<FileContentResult> GeneratePdfAsync(int id)
    {
        var quotation = await GetQuotationById(id);
        if (quotation == null)
            throw new InvalidOperationException("Quotation not found");

        return await pdf.GenerateQuotation(quotation);
    }
}
