using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.BLL.Container
{
    public class AddonDateContainer : IAddonDateContainer
    {
        private readonly DbManager _db;

        public AddonDateContainer(DbManager db)
        {
            _db = db;
        }

        public async Task<int> CreateAddonDateAsync(AddonDate addonDate)
        {
            if (addonDate == null)
            {
                throw new ArgumentNullException(nameof(addonDate), "AddonDate cannot be null");
            }

            var result = await _db.InsertWithInt32IdentityAsync(addonDate);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to create AddonDate in the database");
            }

            return result;
        }

        public async Task<AddonDate?> GetAddonDateByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("AddonDate Id must be positive", nameof(id));
            }

            return await _db.AddonDates.FirstOrDefaultAsync(ad => ad.Id == id);
        }

        public async Task UpdateAddonDateAsync(AddonDate addonDate)
        {
            if (addonDate == null)
            {
                throw new ArgumentNullException(nameof(addonDate), "AddonDate cannot be null");
            }

            if (addonDate.Id <= 0)
            {
                throw new ArgumentException("AddonDate must have a valid Id");
            }

            var result = await _db.UpdateAsync(addonDate);
            if (result == 0)
            {
                throw new InvalidOperationException("Failed to update AddonDate");
            }
        }

        public async Task<IEnumerable<AddonDate>> GetAddonDatesByProductDateIdAsync(int productDateId)
        {
            if (productDateId <= 0)
            {
                throw new ArgumentException("ProductDate Id must be positive", nameof(productDateId));
            }

            var addonDates = await _db.AddonDates
                                  .Where(ad => ad.ProductDateId == productDateId)
                                  .ToListAsync();

            if (addonDates == null || !addonDates.Any())
            {
                throw new InvalidOperationException("No AddonDates found for the given ProductDate");
            }

            return addonDates;
        }

        public async Task<IEnumerable<AddonDate>> GetAddonDatesByProductAddonIdAsync(int productAddonId)
        {
            if (productAddonId <= 0)
            {
                throw new ArgumentException("ProductAddon Id must be positive", nameof(productAddonId));
            }

            var addonDates = await _db.AddonDates
                                  .Where(ad => ad.ProductAddonId == productAddonId)
                                  .ToListAsync();

            if (addonDates == null || !addonDates.Any())
            {
                throw new InvalidOperationException("No AddonDates found for the given ProductAddon");
            }

            return addonDates;
        }

        public async Task SoftDeleteAddonDateAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("AddonDate Id must be positive", nameof(id));
            }

            var addonDate = await GetAddonDateByIdAsync(id);
            if (addonDate == null)
            {
                throw new InvalidOperationException("AddonDate does not exist and cannot be soft-deleted");
            }

            addonDate.Slots = 0; // Assuming 0 slots mark it as deleted
            await UpdateAddonDateAsync(addonDate);
        }
    }
}
