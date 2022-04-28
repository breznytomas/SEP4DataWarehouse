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
    
    public ICollection<Temperature> TemperatureList { get; set; }
    public ICollection<Humidity> HumidityList { get; set; }
    public ICollection<Light> LightLists { get; set; }
    public ICollection<CarbonDioxide> CarbonDioxideList { get; set; }
}