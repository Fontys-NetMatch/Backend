using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Interfaces
{
    public interface IAddonDateRepository
    {
        Task<int> CreateAsync(AddonDate addonDate);
        Task<AddonDate?> GetByIdAsync(int id);
        Task<List<AddonDate>> GetByProductAddonIdAsync(int productAddonId);
        Task<List<AddonDate>> GetByProductDateIdAsync(int productDateId);
        Task<int> UpdateAsync(AddonDate addonDate);
    }
}