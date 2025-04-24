namespace TravelPlanner.API.Response.Success.ProductTranslation;

public record ProductTranslationResponse : BaseResponse
{

    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? LangIsoCode { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<string>? Tags { get; set; } = new();
    public bool IsActive { get; set; }

    public ProductTranslationResponse(int id, int productId, string langIsoCode, string name, string? description, List<string>? tags, bool isActive)
    {
        Id = id;
        ProductId = productId;
        LangIsoCode = langIsoCode;
        Name = name;
        Description = description;
        Tags = tags;
        IsActive = isActive;
    }

}