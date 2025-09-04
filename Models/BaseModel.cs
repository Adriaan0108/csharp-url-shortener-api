using System.ComponentModel.DataAnnotations;

namespace csharp_url_shortener_api.Models;

public class BaseModel
{
    public BaseModel()
    {
        CreatedAt = DateTime.Now;
    }
    
    [Key]
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
}