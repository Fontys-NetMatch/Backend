using TravelPlanner.Domain.Models.Entities.Products;

namespace  TravelPlanner.Domain.Interfaces.BLL.Container;

public interface IProductImageContainer
{
    public Task<int> CreateAsync(ProductImage productImage);

    public Task<ProductImage?> GetByIdAsync(int id);

    public Task UpdateAsync(ProductImage productImage);

    public Task<IEnumerable<ProductImage>> GetAllActiveAsync();
    public Task<bool> SoftDeleteAsync(int id);
    public Task<bool> Restore(int id);
    public Task<List<ProductImage>> GetImagesByProductIdAsync(int productId);
}