using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP4DataWarehouse.DTO.DbDTO;

public class BoardDto
{
    [JsonPropertyName("Id")] 
    public string Id { get; set; }

    [Required] 
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    
    [Required]
    [JsonPropertyName("Description")]
    public string Description { get; set; }
    
   


}