using TravelPlanner.API.Response.Success.ProductTypeTranslation;

namespace TravelPlanner.API.Response.Success.ProductType;

public record ProductTypeResponse : BaseResponse
{

    public int ID { get; set; }

    public bool IsActive { get; set; }

    public ProductTypeTranslationsResponse Translations { get; set; }

    public ProductTypeResponse(int id, bool isActive, ProductTypeTranslationsResponse translations) : base("Product found")
    {
        ID = id;
        IsActive = isActive;
        Translations = translations;
    }

}