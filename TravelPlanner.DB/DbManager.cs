using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Product;
using TravelPlanner.Domain.Models.Entities.Products;
using EntityProductData = TravelPlanner.Domain.Models.Entities.Products.Productdata;
using TravelPlanner.Domain.Models.Entities.Translations;
using ProductAddonTranslation = TravelPlanner.Domain.Models.Entities.Translations.ProductAddonTranslation;
using TravelPlanner.Domain.Models.Entities.UniqueSellingPoints;
using TravelPlanner.Domain.Models.Entities.Transport;
using TravelPlanner.Domain.New_Models.Entities.Facility;
using TravelPlanner.Domain.New_Models.Enums.EnumsEntity;
using TravelPlanner.Domain.New_Models.Entities.Product;

namespace TravelPlanner.DB;

public class DbManager : DbContext
{

    public ITable<User> Users => this.GetTable<User>();
    public ITable<Customer> Customers => this.GetTable<Customer>();
    public ITable<Product> Products => this.GetTable<Product>();
    public ITable<ProductTranslation> ProductTranslations => this.GetTable<ProductTranslation>();
    public ITable<ProductImage> ProductImages => this.GetTable<ProductImage>();
    public ITable<ProductTypeEntity> ProductTypes => this.GetTable<ProductTypeEntity>();
    public ITable<ProductTypeTranslation> ProductTypeTranslations => this.GetTable<ProductTypeTranslation>();
    public ITable<ProductAddon> ProductAddons => this.GetTable<ProductAddon>();
    public ITable<ProductAddonTranslation> ProductAddonTranslations => this.GetTable<ProductAddonTranslation>();
    public ITable<ProductDate> ProductDates => this.GetTable<ProductDate>();
    public ITable<AddonDate> AddonDates => this.GetTable<AddonDate>();
    public ITable<Quotation> Quotations => this.GetTable<Quotation>();
    public ITable<QuotationProductDate> QuotationProductDates => this.GetTable<QuotationProductDate>();

    public ITable<EntityProductData> ProductDatas => this.GetTable<EntityProductData>();
    public ITable<UniqueSellingPoint> UniqueSellingPoints => this.GetTable<UniqueSellingPoint>();
    public ITable<TransportTypeValueEntity> TransportTypeValues => this.GetTable<TransportTypeValueEntity>();
    public ITable<TransportSegment> TransportSegments => this.GetTable<TransportSegment>();
    public ITable<TransportInformationDescription> TransportInformationDescriptions => this.GetTable<TransportInformationDescription>();
    public ITable<TransportInformation> TransportInformations => this.GetTable<TransportInformation>();
    public ITable<RoomTypeEntity> RoomTypes => this.GetTable<RoomTypeEntity>();
    public ITable<ProductSubTitle> ProductSubTitles => this.GetTable<ProductSubTitle>();
    //public ITable<FacilityInformation> FacilityInformations => this.GetTable<FacilityInformation>();
    public ITable<FacilityTypeEntity> FacilityTypes => this.GetTable<FacilityTypeEntity>();
    public ITable<ProductCharacteristics> ProductCharacteristics => this.GetTable<ProductCharacteristics>();
    public ITable<IdentifierTypeEntity> IdentifierTypes => this.GetTable<IdentifierTypeEntity>();





}