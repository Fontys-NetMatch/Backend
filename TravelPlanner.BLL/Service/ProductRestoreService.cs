using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Interfaces.BLL.Service;

namespace TravelPlanner.BLL.Service;

public class ProductRestoreService : IProductRestoreService
{
    private readonly IProductContainer _productService;
    private readonly IProductDateContainer _productDateService;
    private readonly IProductImageContainer _productImageService;
    private readonly IProductTranslationContainer _productTranslationService;

    private bool product = false, productdate = false, productimage = false, producttranslation = false;

    public ProductRestoreService(
        IProductContainer productService,
        IProductDateContainer productDateService,
        IProductImageContainer productImageService,
        IProductTranslationContainer productTranslationService)
    {
        _productService = productService;
        _productDateService = productDateService;
        _productImageService = productImageService;
        _productTranslationService = productTranslationService;
    }

    public async Task RestoreProduct(int id)
    {
        try
        {
            product = await _productService.Restore(id);
            productdate = await _productDateService.Restore(id);
            productimage = await _productImageService.Restore(id);
            producttranslation = await _productTranslationService.Restore(id);
        }
        catch (Exception e)
        {
            if (producttranslation) await _productTranslationService.SoftDelete(id);
            if (productimage) await _productImageService.SoftDelete(id);
            if (productdate) await _productDateService.SoftDelete(id);
            if (product) await _productService.SoftDelete(id);
            throw new Exception("Failed to restore product. Rollback attempted.", e);
        }
    }
}
