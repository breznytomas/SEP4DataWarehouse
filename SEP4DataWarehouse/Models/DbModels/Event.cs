using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP4DataWarehouse.Models.DbModels;

public class Event
{
    [Required, Key]
    [JsonPropertyName("Id")]
    public long Id { get; set; }
    
    [Required]
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    
    [Required]
    [JsonPropertyName("Type")]
    public EventTypes EventTypes { get; set; }
    
    [Required]
    [JsonPropertyName("Top")]
    public float Top { get; set; }
    
    [Required]
    [JsonPropertyName("Bottom")]
    public float Bottom { get; set; }

    public ICollection<Trigger> TriggerList { get; set; }
    
    
}