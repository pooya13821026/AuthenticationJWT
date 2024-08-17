using Authentication.Infra.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Infra;

public class Product
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime ProductDate { get; set; }
    
    public string ManufacturePhone { get; set; }
    
    public string ManufactureEmail { get; set; }

    public bool IsAvailable { get; set; }
    
    public ApplicationUser User { get; set; }
    public string UserId { get; set; }
}