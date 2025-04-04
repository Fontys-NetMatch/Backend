namespace TravelPlanner.API.Response.Success.ProductTranslation;

public record ProductTranslationsResponse : BaseResponse
{
    public List<ProductTranslationResponse> Translations { get; set; }

    public ProductTranslationsResponse(List<ProductTranslationResponse> translations, string message) : base(message)
    {
        Translations = translations;
    }
}
