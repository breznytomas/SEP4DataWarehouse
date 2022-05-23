using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP4DataWarehouse.Models;

public class TriggerDTO
{
    [Required, Key]
    [JsonPropertyName("Id")]
    public long Id { get; set; }

    [Required]
    [JsonPropertyName("Timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("IsTop")]
    [Required] public bool IsTop { get; set; }

    [Required]
    [JsonPropertyName("TriggerValue")]
    public float TriggerValue { get; set; }
    
    [Required]
    [JsonPropertyName("LimitValue")]
    public float LimitValue { get; set; }

}