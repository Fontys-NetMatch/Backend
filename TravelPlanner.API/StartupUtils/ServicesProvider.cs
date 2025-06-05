using Microsoft.AspNetCore.Mvc.Razor;
using PDF_Generator;
using TravelPlanner.API.Controllers;
using TravelPlanner.BLL.Container;
using TravelPlanner.BLL.Service;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Interfaces.BLL.MockGeneration;
using TravelPlanner.Domain.Interfaces.BLL.Service;
using TravelPlanner.Domain.Interfaces.PDF;

namespace TravelPlanner.API.StartupUtils;

public static class ServicesProvider
{

    public static void Register(IServiceCollection services)
    {
        // Database
        services.AddTransient<DbContext>();
        services.AddTransient<DbManager>();

        // Controllers
        services.AddTransient<StatusController>();
        services.AddTransient<AuthController>();
        services.AddTransient<ProductController>();
        services.AddTransient<ProductTranslationController>();
        services.AddTransient<ProductTypeController>();
        services.AddTransient<QuotationController>();
        services.AddTransient <ProductImageController>();
        services.AddTransient<MockController>();

        // Containers
        services.AddSingleton<IAddonDateContainer, AddonDateContainer>();
        services.AddSingleton<IAuthContainer, AuthContainer>();
        services.AddSingleton<ICustomerContainer, CustomerContainer>();
        services.AddSingleton<IProductAddonContainer, ProductAddonContainer>();
        services.AddSingleton<IProductContainer, ProductContainer>();
        services.AddSingleton<IProductDateContainer, ProductDateContainer>();
        services.AddSingleton<IProductImageContainer, ProductImageContainer>();
        services.AddSingleton<IProductTranslationContainer, ProductTranslationContainer>();
        services.AddSingleton<IProductTypeContainer, ProductTypeContainer>();
        services.AddSingleton<IQuotationContainer, QuotationContainer>();
        services.AddSingleton<IUserContainer, UserContainer>();

        // Services
        services.AddRazorPages();
        services.AddSingleton<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
        services.AddSingleton<IRazorViewEngine, RazorViewEngine>();
        services.AddSingleton<IPDFService, PDFService>();
        services.AddSingleton<IQuotationService, QuotationService>();
        services.AddSingleton<IProductRestoreService, ProductRestoreService>();
        services.AddSingleton<IProductDeleteService, ProductDeleteService>();
        
        // Mock Generation
        services.AddScoped<IProductInformationFactory, ProductInformationFactory>();
    }
}