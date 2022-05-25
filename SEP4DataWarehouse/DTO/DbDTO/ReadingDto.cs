using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SEP4DataWarehouse.Models.DbModels;

namespace SEP4DataWarehouse.DTO.DbDTO;

public class ReadingDto
{
   
    
    [Required]
    [JsonPropertyName("BoardId")]
    public string BoardId { get; set; }
    
    [JsonPropertyName("TemperatureList")]
    public ICollection<Temperature> TemperatureList { get; set; }
    [JsonPropertyName("HumidityList")]
    public ICollection<Humidity> HumidityList { get; set; }
    [JsonPropertyName("LightLists")]
    public ICollection<Light> LightLists { get; set; }
    [JsonPropertyName("CarbonDioxideList")]
    public ICollection<CarbonDioxide> CarbonDioxideList { get; set; }
}