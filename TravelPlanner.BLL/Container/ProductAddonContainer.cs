using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Repositories;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities.Product;

namespace TravelPlanner.BLL.Container
{
    public class ProductAddonContainer : IProductAddonContainer
    {
        private readonly ProductAddonRepository _repository;

        public ProductAddonContainer(ProductAddonRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductAddon?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ProductAddon Id must be positive", nameof(id));

            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(ProductAddon addon)
        {
            if (addon == null)
                throw new ArgumentNullException(nameof(addon), "ProductAddon cannot be null");

            var result = await _repository.CreateAsync(addon);
            if (result <= 0)
                throw new InvalidOperationException("Failed to create ProductAddon in the database");

            return result;
        }

        public async Task UpdateAsync(ProductAddon addon)
        {
            if (addon == null)
                throw new ArgumentNullException(nameof(addon), "ProductAddon cannot be null");

            if (addon.Id <= 0)
                throw new ArgumentException("ProductAddon must have a valid Id");

            var result = await _repository.UpdateAsync(addon);
            if (result == 0)
                throw new InvalidOperationException("Failed to update ProductAddon");
        }

        public async Task<List<ProductAddon>> GetAllActiveAsync()
        {
            var addons = await _repository.GetAllActiveAsync();
            if (addons is not { Count: > 0 })
                throw new InvalidOperationException("No active ProductAddons found");

            return addons;
        }

        public async Task SoftDeleteAsync(int id)
        {
            var addon = await GetByIdAsync(id) ?? throw new InvalidOperationException("ProductAddon does not exist");

            if (!addon.IsActive)
                throw new InvalidOperationException("ProductAddon is already inactive");

            addon.IsActive = false;
            await UpdateAsync(addon);
        }
    }
}
