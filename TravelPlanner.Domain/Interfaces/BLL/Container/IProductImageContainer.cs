using TravelPlanner.Domain.Models.Entities.Products;

namespace  TravelPlanner.Domain.Interfaces.BLL.Container;

public interface IProductImageContainer
{
    public Task<int> Create(ProductImage productImage);

    public Task<ProductImage?> GetProductImageById(int id);

    public Task Update(ProductImage productImage);

    public Task<IEnumerable<ProductImage>> GetAllActiveProductImages();
    public Task<bool> SoftDelete(int id);
    public Task<bool> Restore(int id);
    public Task<List<ProductImage>> GetImagesByProductIdAsync(int productId);
}