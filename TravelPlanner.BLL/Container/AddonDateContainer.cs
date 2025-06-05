using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.BLL.Container
{
    public class AddonDateContainer : IAddonDateContainer
    {
        private readonly IAddonDateRepository _repository;

        public AddonDateContainer(IAddonDateRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAddonDateAsync(AddonDate addonDate)
        {
            if (addonDate == null)
                throw new ArgumentNullException(nameof(addonDate), "AddonDate cannot be null");

            var result = await _repository.CreateAsync(addonDate);
            if (result == 0)
                throw new InvalidOperationException("Failed to create AddonDate");

            return result;
        }

        public async Task<AddonDate?> GetAddonDateByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid AddonDate Id", nameof(id));

            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAddonDateAsync(AddonDate addonDate)
        {
            if (addonDate == null || addonDate.Id <= 0)
                throw new ArgumentException("Invalid AddonDate");

            var result = await _repository.UpdateAsync(addonDate);
            if (result == 0)
                throw new InvalidOperationException("Failed to update AddonDate");
        }

        public async Task<IEnumerable<AddonDate>> GetAddonDatesByProductDateIdAsync(int productDateId)
        {
            if (productDateId <= 0)
                throw new ArgumentException("Invalid ProductDate Id");

            var dates = await _repository.GetByProductDateIdAsync(productDateId);
            if (!dates.Any())
                throw new InvalidOperationException("No AddonDates found for the ProductDate");

            return dates;
        }

        public async Task<IEnumerable<AddonDate>> GetAddonDatesByProductAddonIdAsync(int productAddonId)
        {
            if (productAddonId <= 0)
                throw new ArgumentException("Invalid ProductAddon Id");

            var dates = await _repository.GetByProductAddonIdAsync(productAddonId);
            if (!dates.Any())
                throw new InvalidOperationException("No AddonDates found for the ProductAddon");

            return dates;
        }

        public async Task SoftDeleteAddonDateAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid AddonDate Id");

            var addon = await _repository.GetByIdAsync(id);
            if (addon == null)
                throw new InvalidOperationException("AddonDate does not exist");

            addon.Slots = 0;
            await UpdateAddonDateAsync(addon);
        }
    }
}
