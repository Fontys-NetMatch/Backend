using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Repositories
{
    public class AddonDateRepository : IAddonDateRepository
    {
        private readonly DbManager _db;

        public AddonDateRepository(DbManager db)
        {
            _db = db;
        }

        public Task<int> CreateAsync(AddonDate addonDate) =>
            _db.InsertWithInt32IdentityAsync(addonDate);

        public Task<AddonDate?> GetByIdAsync(int id) =>
            _db.AddonDates.FirstOrDefaultAsync(ad => ad.Id == id);

        public Task<int> UpdateAsync(AddonDate addonDate) =>
            _db.UpdateAsync(addonDate);

        public Task<List<AddonDate>> GetByProductDateIdAsync(int productDateId) =>
            _db.AddonDates.Where(ad => ad.ProductDateId == productDateId).ToListAsync();

        public Task<List<AddonDate>> GetByProductAddonIdAsync(int productAddonId) =>
            _db.AddonDates.Where(ad => ad.ProductAddonId == productAddonId).ToListAsync();
    }
}
