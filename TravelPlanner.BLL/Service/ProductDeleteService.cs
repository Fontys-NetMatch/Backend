/*using TravelPlanner.BLL.Container;
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
            productdate = await ProductDateService.SoftDelete(id);
            productimage = await ProductImageService.SoftDelete(id);
            producttranslation = await ProductTranslationService.SoftDelete(id);
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
}*/


using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Interfaces.BLL.Service;

public class ProductDeleteService(
    IProductContainer ProductService,
    IProductDateContainer ProductDateService,
    IProductImageContainer ProductImageService,
    IProductTranslationContainer ProductTranslationService,
    IProductTypeContainer ProductTypeService)
    : IProductDeleteService
{
    private bool product = false, productdate = false, productimage = false, producttranslation = false, producttype = false;

    public async Task DeleteProduct(int productId)
    {
        try
        {
            var productEntity = await ProductService.GetById(productId);
            if (productEntity == null)
                throw new InvalidOperationException("Product not found");

            // ✅ SoftDelete Product zelf
            product = await ProductService.SoftDelete(productId);

            // ✅ SoftDelete gerelateerde ProductDates
            foreach (var date in productEntity.Dates)
            {
                var result = await ProductDateService.SoftDelete(date.Id);  // <-- JOUW METHODE
                productdate = productdate || result;
            }

            // ✅ SoftDelete gerelateerde ProductImages
            foreach (var image in productEntity.Images)
            {
                var result = await ProductImageService.SoftDelete(image.Id);  // <-- JOUW METHODE
                productimage = productimage || result;
            }

            // ✅ SoftDelete gerelateerde ProductTranslations
            foreach (var translation in productEntity.Translations)
            {
                var result = await ProductTranslationService.SoftDelete(translation.Id);  // <-- JOUW METHODE
                producttranslation = producttranslation || result;
            }

            // ✅ SoftDelete ProductType (optioneel)
            if (productEntity.ProductTypeId != 0)
            {
                var result = await ProductTypeService.SoftDelete(productEntity.ProductTypeId);
                producttype = producttype || result;
            }
        }
        catch (Exception e)
        {
            if (producttype) await ProductTypeService.Restore(productId);
            if (producttranslation) await ProductTranslationService.Restore(productId);
            if (productimage) await ProductImageService.Restore(productId);
            if (productdate) await ProductDateService.Restore(productId);
            if (product) await ProductService.Restore(productId);
            throw new Exception("Failed to delete product. Rollback attempted.", e);
        }
    }
}


