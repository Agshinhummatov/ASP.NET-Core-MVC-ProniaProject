using Microsoft.AspNetCore.Identity;

namespace Pronia_BackEnd_Project.Models
{
    public class AppUser : IdentityUser 
    {
        public string FirstName { get; set; }

        public string LastName { get; set; } 
        public bool RememberMe { get; set; } 



    }
}
