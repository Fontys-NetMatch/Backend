using LinqToDB;
using LinqToDB.Linq;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Enums;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.Infrastructure.Repositories;

public class QuotationRepository : IQuotationRepository
{
    private readonly DbManager _db;

    public QuotationRepository(DbManager db)
    {
        _db = db;
    }

    public Task<int> CreateAsync(Quotation quotation)
    {
        return _db.InsertWithInt32IdentityAsync(quotation);
    }

    public Task<Quotation?> GetByIdAsync(int id)
    {
        return _db.Quotations
            .LoadWith(q => q.Customer)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public Task<List<Quotation>> GetAllAsync()
    {
        return _db.Quotations
            .LoadWith(q => q.Customer)
            .ToListAsync();
    }

    public Task<List<Quotation>> GetAllActiveAsync()
    {
        return _db.Quotations
            .LoadWith(q => q.Customer)
            .Where(q => q.Status != QuotationStatus.Archived)
            .ToListAsync();
    }

    public Task<int> UpdateAsync(Quotation quotation)
    {
        return _db.UpdateAsync(quotation);
    }

    public Task<List<ProductDate>> GetQuotationProductsAsync(int quotationId)
    {
        return _db.QuotationProductDates
            .LoadWith(qpd => qpd.ProductDate)
            .Where(qpd => qpd.QuotationId == quotationId)
            .Select(qpd => qpd.ProductDate)
            .ToListAsync();
    }
}