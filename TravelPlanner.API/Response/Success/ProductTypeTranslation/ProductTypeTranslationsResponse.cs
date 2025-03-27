namespace TravelPlanner.API.Response.Success.ProductTypeTranslation;

public record ProductTypeTranslationsResponse : BaseResponse
{
    public List<ProductTypeTranslationResponse> Translations { get; set; }

    public ProductTypeTranslationsResponse(List<ProductTypeTranslationResponse> translations, string message) : base(message)
    {
        Translations = translations;
    }
}
