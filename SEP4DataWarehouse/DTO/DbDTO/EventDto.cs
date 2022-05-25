using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.DTO.DbDTO;

public class EventDto
{
    
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

  
    
    
}