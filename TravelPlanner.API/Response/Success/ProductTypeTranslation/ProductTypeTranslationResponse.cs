namespace TravelPlanner.API.Response.Success.ProductTypeTranslation;

public record ProductTypeTranslationResponse : BaseResponse
{

    public int Id { get; set; }
    public int ProductTypeId { get; set; }
    public string? LangIsoCode { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }

    public ProductTypeTranslationResponse(int id, int productTypeId, string langIsoCode, string name, bool isActive)
    {
        Id = id;
        ProductTypeId = productTypeId;
        LangIsoCode = langIsoCode;
        Name = name;
        IsActive = isActive;
    }

}