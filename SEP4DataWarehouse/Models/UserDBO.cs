using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SEP4DataWarehouse.Models;

public class User
{
    
    [Key]
    [JsonPropertyName("Email")]
    public string Email { get; set; }
    
    [Required]
    [JsonPropertyName("Password")]
    public string Password { get; set; }
    
    public ICollection<Board> BoardList { get; set; }

}