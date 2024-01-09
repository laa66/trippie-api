using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dtos;

public class PoisDTO (string[] pois) {
    
    [JsonPropertyName("pois")]
    [Required]
    public string[] Pois { get; set; } = pois;

}