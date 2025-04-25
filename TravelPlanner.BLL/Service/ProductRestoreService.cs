using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Interfaces.BLL.Service;

namespace TravelPlanner.BLL.Service;

public class ProductRestoreService(
    IProductContainer ProductService,
    IProductDateContainer ProductDateService,
    IProductImageContainer ProductImageService,
    IProductTranslationContainer ProductTranslationService,
    IProductTypeContainer ProductTypeService)
    : IProductRestoreService
{
    private bool product = false, productdate = false, productimage = false, producttranslation = false, producttype = false;
    
    public async Task DeleteProduct(int id)
    {
        try
        {
            product = await ProductService.Restore(id);
            productdate = await ProductDateService.Restore(id);
            productimage = await ProductImageService.Restore(id);
            producttranslation = await ProductTranslationService.Restore(id);
            producttype = await ProductTypeService.Restore(id);
        }
        catch (Exception e)
        {
            if (producttype) await ProductTypeService.SoftDelete(id);
            if (producttranslation) await ProductTranslationService.Delete(id);
            if (productimage) await ProductImageService.SoftDeleteProductImageAsync(id);
            if (productdate) await ProductDateService.SoftDeleteProductDateAsync(id);
            if (product) await ProductService.SoftDelete(id);
            throw new Exception("Failed to retore product");
        }
    }
}