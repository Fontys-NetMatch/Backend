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

namespace TravelPlanner.BLL.Container;

public class QuotationContainer(DbManager db, IPDFService pdf) : IQuotationContainer
{
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
        
        if (db.InsertWithInt32Identity(quotation) <= 0)
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

        try
        {
            // Attempt to load the quotation with the associated customer
            var quotation = await db.Quotations
                .LoadWith(q => q.Customer)  // Load related customer data
                .FirstOrDefaultAsync(q => q.Id == id);  // Query for the specific id

            if (quotation == null)
            {
                // Handle case where no quotation is found (optional)
                Console.WriteLine("No quotation found with the provided ID.");
            }

            return quotation;
        }
        catch (LinqToDBException ex)
        {
            // Handle specific LINQ to DB exceptions
            Console.Error.WriteLine($"An error occurred while querying the database: {ex.Message}");
            // Optionally, you can rethrow or return null
            throw;
        }
        catch (Exception ex)
        {
            // Catch any other unexpected exceptions
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
            // Optionally, you can rethrow or return null
            throw;
        }
    }


    public async Task<List<ProductDate>> GetQuotationProducts(int quotationId)
    {
        var results = await db.QuotationProductDates
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
        
        if (await db.UpdateAsync(quotation) == 0)
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
        await db.UpdateAsync(quotation);
    }

    public async Task<List<Quotation>> GetAllQuotations()
    {
        var quotations = await db.Quotations
                                  .LoadWith(q => q.Customer)
                                  .ToListAsync();
        if (quotations.Count == 0)
        {
            throw new InvalidOperationException("No active quotations found.");
        }
        return quotations;
    }

    public async Task<List<Quotation>> GetAllActiveQuotations()
    {
        var quotations = await db.Quotations
            .LoadWith(q => q.Customer)
            .Where(q => q.Status != QuotationStatus.Archived)
            .ToListAsync();

        if (quotations.Count == 0)
        {
            throw new InvalidOperationException("No active quotations found.");
        }
        return quotations;
    }
    
    public async Task<FileContentResult?> GeneratePdfAsync(int id)
    {
        // Get the quotation (or return null if not found)
        var quotation = await GetQuotationById(id);

        // Return null if the quotation is not found
        if (quotation == null)
        {
            return null;
        }

        // Proceed with generating the PDF if the quotation exists
        try
        {
            return await pdf.GenerateQuotation(quotation);
        }
        catch (Exception ex)
        {
            // Log the error and rethrow or handle accordingly
            Console.Error.WriteLine($"Error generating PDF for Quotation ID {id}: {ex.Message}");
            return null; // You could also return an error-specific result if needed
        }
    }
}
