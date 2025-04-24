using Microsoft.AspNetCore.Mvc.Razor;
using PDF_Generator;
using TravelPlanner.API.Controllers;
using TravelPlanner.BLL.Container;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.Interfaces.BLL;
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

        // Containers
        services.AddSingleton<IAuthContainer, AuthContainer>();
        services.AddSingleton<IProductContainer, ProductContainer>();
        services.AddSingleton<IProductTranslationContainer, ProductTranslationContainer>();
        services.AddSingleton<IProductTypeContainer, ProductTypeContainer>();
        services.AddSingleton<IQuotationContainer, QuotationContainer>();

        // Services
        services.AddSingleton<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
        services.AddSingleton<IRazorViewEngine, RazorViewEngine>();
        services.AddRazorPages();
        services.AddSingleton<IPDFService, PDFService>();

    }

}