using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.DB;
using TravelPlanner.Domain.Enums;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Interfaces.PDF;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Quotation;
using TravelPlanner.Infrastructure.Repositories;

namespace TravelPlanner.BLL.Container;

public class QuotationContainer: IQuotationContainer
{
    private readonly QuotationRepository _repository;
    private readonly IPDFService _pdf;

    public QuotationContainer(QuotationRepository repository, IPDFService pdf)
    {
        _repository = repository;
        _pdf = pdf;
    }

    public async Task<int> CreateQuotation(QuotationData data, int userId)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        var error = (data.Name, data.CustomerId, userId) switch
        {
            var (name, _, _) when string.IsNullOrWhiteSpace(name) => "Quotation name is required",
            var (_, customerId, _) when customerId <= 0 => "Invalid Customer Id",
            var (_, _, id) when id <= 0 => "Invalid User Id",
            _ => null
        };

        if (error is not null)
            throw new ArgumentException(error);
        var quotation = new Quotation
        {
            Name = data.Name,
            CustomerId = data.CustomerId,
            UserId = userId,
            Status = QuotationStatus.Open,
            Customer = null,//fix this
            User = null
        };

        var result = await _repository.CreateAsync(quotation);
        if (result <= 0)
            throw new InvalidOperationException("Failed to create quotation");

        return result;
    }


    public Task<Quotation?> GetQuotationById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid quotation Id");
        return _repository.GetByIdAsync(id);
    }

    public async Task<List<Quotation>> GetAllQuotations()
    {
        var list = await _repository.GetAllAsync();
        if (list.Count == 0)
            throw new InvalidOperationException("No quotations found");
        return list;
    }

    public async Task<List<Quotation>> GetAllActiveQuotations()
    {
        var list = await _repository.GetAllActiveAsync();
        if (list.Count == 0)
            throw new InvalidOperationException("No active quotations found");
        return list;
    }

    public async Task UpdateQuotation(QuotationUpdateData data)
    {
        if (data == null || data.Id <= 0)
            throw new ArgumentException("Invalid quotation data");

        var existing = await _repository.GetByIdAsync(data.Id)
                         ?? throw new InvalidOperationException("Quotation does not exist");

        existing.Name = data.Name;
        existing.CustomerId = data.CustomerId;
        existing.Status = data.Status;

        if (await _repository.UpdateAsync(existing) == 0)
            throw new InvalidOperationException("Failed to update quotation");
    }

    public async Task SoftDeleteQuotation(int id)
    {
        var quotation = await _repository.GetByIdAsync(id)
                         ?? throw new InvalidOperationException("Quotation does not exist");

        if (quotation.Status == QuotationStatus.Archived)
            throw new InvalidOperationException("Quotation already archived");

        quotation.Status = QuotationStatus.Archived;

        if (await _repository.UpdateAsync(quotation) == 0)
            throw new InvalidOperationException("Failed to archive quotation");
    }

    public async Task<List<ProductDate>> GetQuotationProducts(int quotationId)
    {
        return await _repository.GetQuotationProductsAsync(quotationId);
    }

    public async Task<FileContentResult?> GeneratePdfAsync(int id)
    {
        var quotation = await _repository.GetByIdAsync(id);
        if (quotation == null) return null;

        return await _pdf.GenerateQuotation(quotation);
    }
}

