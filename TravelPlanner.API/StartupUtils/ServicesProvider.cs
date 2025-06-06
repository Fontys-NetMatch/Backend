using Microsoft.AspNetCore.Mvc.Razor;
using PDF_Generator;
using TravelPlanner.API.Controllers;
using TravelPlanner.BLL.Container;
using TravelPlanner.BLL.Service;
using TravelPlanner.DB;
using TravelPlanner.DB.Interfaces;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Repositories;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Interfaces.BLL.MockGeneration;
using TravelPlanner.Domain.Interfaces.BLL.Service;
using TravelPlanner.Domain.Interfaces.PDF;
using TravelPlanner.Infrastructure.Repositories;

namespace TravelPlanner.API.StartupUtils;

public static class ServicesProvider
{
    public static void Register(IServiceCollection services)
    {
        // Database
        services.AddTransient<DbContext>();
        services.AddTransient<DbManager>();

        // Controllers
        services.AddControllersWithViews();
        services.AddTransient<StatusController>();
        services.AddTransient<AuthController>();
        services.AddTransient<ProductController>();
        services.AddTransient<ProductTranslationController>();
        services.AddTransient<ProductTypeController>();
        services.AddTransient<QuotationController>();
        services.AddTransient<ProductImageController>();
        services.AddTransient<MockController>();

        // Containers
        services.AddTransient<IAddonDateContainer, AddonDateContainer>();
        services.AddTransient<IAuthContainer, AuthContainer>();
        services.AddTransient<ICustomerContainer, CustomerContainer>();
        services.AddTransient<IProductAddonContainer, ProductAddonContainer>();
        services.AddTransient<IProductContainer, ProductContainer>();
        services.AddTransient<IProductDateContainer, ProductDateContainer>();
        services.AddTransient<IProductImageContainer, ProductImageContainer>();
        services.AddTransient<IProductTranslationContainer, ProductTranslationContainer>();
        services.AddTransient<IProductTypeContainer, ProductTypeContainer>();
        services.AddTransient<IQuotationContainer, QuotationContainer>();
        services.AddTransient<IUserContainer, UserContainer>();

        // Services
        services.AddTransient<IPDFService, PDFService>();
        services.AddTransient<IQuotationService, QuotationService>();
        services.AddTransient<IProductRestoreService, ProductRestoreService>();
        services.AddTransient<IProductDeleteService, ProductDeleteService>();
        services.AddTransient<IRazorViewToStringRenderer, RazorViewToStringRenderer>();


        // Repositories
        services.AddTransient<IAuthRepository, AuthRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductTranslationRepository, ProductTranslationRepository>();
        services.AddTransient<IProductTypeRepository, ProductTypeRepository>();
        services.AddTransient<IProductImageRepository, ProductImageRepository>();
        services.AddTransient<IProductDateRepository, ProductDateRepository>();
        services.AddTransient<IProductAddonRepository, ProductAddonRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IQuotationRepository, QuotationRepository>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IAddonDateRepository, AddonDateRepository>();

        // Mock Generation
        services.AddTransient<IProductInformationFactory, ProductInformationFactory>();
    }
}
