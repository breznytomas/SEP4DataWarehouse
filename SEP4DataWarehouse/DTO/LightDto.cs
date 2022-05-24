using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP4DataWarehouse.DTO;

public class LightDto
{
    [Required, Key]
    [JsonPropertyName("Id")]
    public long Id { get; set; }
    [Required]
    [JsonPropertyName("Timestamp")]
    public long Timestamp { get; set; }
    [Required]
    [JsonPropertyName("Value")]
    public float Value { get; set; }
}