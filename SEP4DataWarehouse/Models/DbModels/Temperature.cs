using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP4DataWarehouse.Models.DbModels;

public class Temperature
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