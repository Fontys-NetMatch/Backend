using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Request.Product;

namespace TravelPlanner.DB.Repositories
{
    public class ProductRepository(DbManager db) : IProductRepository
    {
        public Task<Product?> GetByIdAsync(int id) =>
            db.Products
                .LoadWith(p => p.ProductType)
                .LoadWith(p => p.ProductType.Translations)
                .LoadWith(p => p.Translations)
                .LoadWith(p => p.Dates)
                .FirstOrDefaultAsync(p => p.Id == id);

        public Task<List<Product>> GetFilteredAsync(ProductFiltersData filters)
        {
            var query = db.Products.AsQueryable();

            if (filters.IsDeleted != null)
                query = query.Where(p => filters.IsDeleted.Value ? p.DeletedAt != null : p.DeletedAt == null);

            if (filters.TypeId != null)
                query = query.Where(p => p.ProductTypeId == filters.TypeId);

            if (filters.SearchQuery != null)
                query = query.Where(p =>
                    p.EndLocation != null &&
                    (p.StartLocation.Contains(filters.SearchQuery) ||
                     p.EndLocation.Contains(filters.SearchQuery) ||
                     p.Translations.Any(t => t.Name.Contains(filters.SearchQuery)) ||
                     p.Translations.Any(t => t.Description != null && t.Description.Contains(filters.SearchQuery))));

            if (filters.StartLocation != null)
                query = query.Where(p => p.StartLocation == filters.StartLocation);

            if (filters.EndLocation != null)
                query = query.Where(p => p.EndLocation == filters.EndLocation);

            if (filters.StartDateTime != null)
                query = query.Where(p => p.Dates.Any(d => d.StartDate == filters.StartDateTime));

            if (filters.EndDateTime != null)
                query = query.Where(p => p.Dates.Any(d => d.EndDate == filters.EndDateTime));

            if (filters.MinPrice != null)
                query = query.Where(p => p.Dates.Any(d => d.Price >= filters.MinPrice));

            if (filters.MaxPrice != null)
                query = query.Where(p => p.Dates.Any(d => d.Price <= filters.MaxPrice));

            if (filters.MinPeople != null)
                query = query.Where(p => p.Dates.Any(d => d.Slots >= filters.MinPeople));

            return query
                .LoadWith(p => p.ProductType)
                .LoadWith(p => p.ProductType.Translations)
                .LoadWith(p => p.Translations)
                .LoadWith(p => p.Dates)
                .ToListAsync();
        }

        public Task<int> CreateAsync(Product product) => db.InsertWithInt32IdentityAsync(product);
        public Task<int> UpdateAsync(Product product) => db.UpdateAsync(product);
    }
}
