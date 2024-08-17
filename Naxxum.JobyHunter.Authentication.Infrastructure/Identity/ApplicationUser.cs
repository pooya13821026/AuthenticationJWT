using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Infra.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        [NotMapped]
        public List<Product>? ProductsList { get; set; }
    }
}