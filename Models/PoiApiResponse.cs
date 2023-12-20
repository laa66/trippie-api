using System.Text.Json.Serialization;

namespace Models;

public record PoiApiResponse (string Name, List<Feature> Features)
{
    public string Name { get; init; } = Name;

    public List<Feature> Features { get; init; } = Features;
}