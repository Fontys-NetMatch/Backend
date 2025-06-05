using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IAddonDateContainer
    {
        Task<int> CreateAddonDateAsync(AddonDate addonDate);
        Task<AddonDate?> GetAddonDateByIdAsync(int id);
        Task UpdateAddonDateAsync(AddonDate addonDate);
        Task<IEnumerable<AddonDate>> GetAddonDatesByProductDateIdAsync(int productDateId);
        Task<IEnumerable<AddonDate>> GetAddonDatesByProductAddonIdAsync(int productAddonId);
        Task SoftDeleteAddonDateAsync(int id);
    }
}
