using System.Text.Json.Serialization;

namespace Models;

public record Properties(string Name, string FullAddress, string[] PoiCategory)
{
    public string Name { get; init; } = Name;

    [JsonPropertyName("full_address")]
    public string FullAddress { get; init; } = FullAddress;

    [JsonPropertyName("poi_category")]
    public string[] PoiCategory { get; init; } = PoiCategory;
}