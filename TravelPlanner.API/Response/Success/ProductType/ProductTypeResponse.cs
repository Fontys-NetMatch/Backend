using TravelPlanner.API.Response.Success.ProductTypeTranslation;

namespace TravelPlanner.API.Response.Success.ProductType;

public record ProductTypeResponse : BaseResponse
{

    public int ID { get; set; }

    public bool IsActive { get; set; }

    public List<ProductTypeTranslationResponse> Translations { get; set; }

    public ProductTypeResponse(int id, bool isActive, List<ProductTypeTranslationResponse> translations)
    {
        ID = id;
        IsActive = isActive;
        Translations = translations;
    }

}