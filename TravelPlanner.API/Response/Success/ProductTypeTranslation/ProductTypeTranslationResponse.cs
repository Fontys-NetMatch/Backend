namespace TravelPlanner.API.Response.Success.ProductTypeTranslation;

public record ProductTypeTranslationResponse : BaseResponse
{

    public int ID { get; set; }
    public int ProductType_ID { get; set; }
    public string? LangIsoCode { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }

    public ProductTypeTranslationResponse(int id, int productTypeID, string langIsoCode, string name, bool isActive)
    {
        ID = id;
        ProductType_ID = productTypeID;
        LangIsoCode = langIsoCode;
        Name = name;
        IsActive = isActive;
    }

}