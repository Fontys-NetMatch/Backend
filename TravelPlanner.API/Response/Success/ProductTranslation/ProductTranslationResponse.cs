namespace TravelPlanner.API.Response.Success.ProductTranslation;

public record ProductTranslationResponse : BaseResponse
{

    public int ID { get; set; }
    public int Product_ID { get; set; }
    public string? LangIsoCode { get; set; }
    public string? Name { get; set; }
    public string? Desciption { get; set; }
    public bool IsActive { get; set; }

    public ProductTranslationResponse(int id, int productID, string langIsoCode, string name, string? desciption, bool isActive) : base("Product translation found")
    {
        ID = id;
        Product_ID = productID;
        LangIsoCode = langIsoCode;
        Name = name;
        Desciption = desciption;
        IsActive = isActive;
    }

}