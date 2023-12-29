using System.Text.Json.Serialization;

namespace Dtos;

public class PoisDTO (string[] pois) {
    
    [JsonPropertyName("pois")]
    public string[] Pois { get; set; } = pois;

}