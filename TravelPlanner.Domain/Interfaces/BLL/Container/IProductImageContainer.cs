using TravelPlanner.Domain.Models.Entities.Products;

namespace  TravelPlanner.Domain.Interfaces.BLL.Container;

public interface IProductImageContainer
{
    public Task<int> CreateProductImageAsync(ProductImage productImage);

    public Task<ProductImage?> GetProductImageByIdAsync(int id);

    public Task UpdateProductImageAsync(ProductImage productImage);

    public Task<IEnumerable<ProductImage>> GetAllActiveProductImagesAsync();
    public Task<bool> SoftDeleteProductImageAsync(int id);
    public Task<bool> Restore(int id);
}