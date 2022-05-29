using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP4DataWarehouse.DTO.DbDTO;

public class TemperatureDto
{
    
    //todo by tomas if no usages delete this class
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