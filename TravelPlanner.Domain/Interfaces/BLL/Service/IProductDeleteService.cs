namespace TravelPlanner.Domain.Interfaces.BLL.Service
{
    public interface IProductDeleteService
    {
        public Task DeleteProduct(int id);
    }
}