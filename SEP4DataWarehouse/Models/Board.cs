using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP4DataWarehouse.Models;

public class Board
{
    [Key]
    [JsonPropertyName("Id")]
    public long Id { get; set; }
    
    [Required] 
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    
    [Required]
    [JsonPropertyName("Description")]
    public string Description { get; set; }
    
    [JsonIgnore]
    public ICollection<Event>? EventList { get; set; }
    
    [JsonIgnore]
    public ICollection<User>? UserList { get; set; }
    
    /*[JsonIgnore]*/
    public ICollection<Temperature>? TemperatureList { get; set; }
    /*
    [JsonIgnore]
    */
    public ICollection<Humidity>? HumidityList { get; set; }
    /*
    [JsonIgnore]
    */
    public ICollection<Light>? LightLists { get; set; }
    /*
    [JsonIgnore]
    */
    public ICollection<CarbonDioxide>? CarbonDioxideList { get; set; }


}