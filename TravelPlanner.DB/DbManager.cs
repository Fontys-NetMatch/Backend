using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Product;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB;

public class DbManager : DbContext
{

    public ITable<User> Users => this.GetTable<User>();

    public ITable<Customer> Customers => this.GetTable<Customer>();

    public ITable<Product> Products => this.GetTable<Product>();
    public ITable<ProductTranslation> ProductTranslations => this.GetTable<ProductTranslation>();
    public ITable<ProductImage> ProductImages => this.GetTable<ProductImage>();

    public ITable<ProductType> ProductTypes => this.GetTable<ProductType>();
    public ITable<ProductTypeTranslation> ProductTypeTranslations => this.GetTable<ProductTypeTranslation>();

    public ITable<ProductAddon> ProductAddons => this.GetTable<ProductAddon>();
    public ITable<ProductAddonTranslation> ProductAddonTranslations => this.GetTable<ProductAddonTranslation>();

    public ITable<ProductDate> ProductDates => this.GetTable<ProductDate>();
    public ITable<AddonDate> AddonDates => this.GetTable<AddonDate>();

    public ITable<Quotation> Quotations => this.GetTable<Quotation>();

}