using TravelPlanner.BLL.Container;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Interfaces.BLL.Service;

namespace TravelPlanner.BLL.Service;

public class ProductDeleteService(
    IProductContainer ProductService,
    IProductDateContainer ProductDateService,
    IProductImageContainer ProductImageService,
    IProductTranslationContainer ProductTranslationService,
    IProductTypeContainer ProductTypeService)
    : IProductDeleteService
{
    private bool product = false, productdate = false, productimage = false, producttranslation = false, producttype = false;
    
    public async Task DeleteProduct(int id)
    {
        try
        {
            product = await ProductService.SoftDelete(id);
            productdate = await ProductDateService.SoftDeleteProductDateAsync(id);
            productimage = await ProductImageService.SoftDeleteProductImageAsync(id);
            producttranslation = await ProductTranslationService.Delete(id);
            producttype = await ProductTypeService.SoftDelete(id);
        }
        catch (Exception e)
        {
            if (producttype) await ProductTypeService.Restore(id);
            if (producttranslation) await ProductTranslationService.Restore(id);
            if (productimage) await ProductImageService.Restore(id);
            if (productdate) await ProductDateService.Restore(id);
            if (product) await ProductService.Restore(id);
            throw new Exception("Failed to delete product. Rollback attempted.", e);
        }
    }
}