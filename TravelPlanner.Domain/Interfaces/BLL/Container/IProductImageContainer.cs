using TravelPlanner.Domain.Models.Entities.Products;

namespace  TravelPlanner.Domain.Interfaces.BLL.Container;

public interface IProductImageContainer
{
    public Task<int> CreateProductImage(ProductImage productImage);

    public Task<ProductImage?> GetProductImageById(int id);

    public Task UpdateProductImage(ProductImage productImage);

    public Task<IEnumerable<ProductImage>> GetAllActiveProductImages();
    public Task<bool> SoftDelete(int id);
    public Task<bool> Restore(int id);
}