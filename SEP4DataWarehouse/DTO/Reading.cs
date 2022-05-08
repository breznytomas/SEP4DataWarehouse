using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP4DataWarehouse.Models;

public class ReadingDTO
{
    [Required, Key]
    [JsonPropertyName("Id")]
    public long Id { get; set; }
    
    [Required]
    [JsonPropertyName("BoardId")]
    public long BoardId { get; set; }
    
    [JsonPropertyName("TemperatureList")]
    public ICollection<Temperature> TemperatureList { get; set; }
    [JsonPropertyName("HumidityList")]
    public ICollection<Humidity> HumidityList { get; set; }
    [JsonPropertyName("LightLists")]
    public ICollection<Light> LightLists { get; set; }
    [JsonPropertyName("CarbonDioxideList")]
    public ICollection<CarbonDioxide> CarbonDioxideList { get; set; }
}