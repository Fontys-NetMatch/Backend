using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.Domain.Interfaces.BLL.Container
{
    public interface IAddonDateContainer
    {
        Task<int> CreateAddonDateAsync(AddonDate addonDate);
        Task<AddonDate?> GetAddonDateByIdAsync(int id);
        Task<IEnumerable<AddonDate>> GetAddonDatesByProductAddonIdAsync(int productAddonId);
        Task<IEnumerable<AddonDate>> GetAddonDatesByProductDateIdAsync(int productDateId);
        Task SoftDeleteAddonDateAsync(int id);
        Task UpdateAddonDateAsync(AddonDate addonDate);
    }
}
