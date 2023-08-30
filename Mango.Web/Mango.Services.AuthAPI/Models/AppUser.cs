using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Models
{
    /*Example about how to custom some features of the identityUser from
     .Identity.EntityFrameworkCore*/
    public class AppUser : IdentityUser 
    {
        public string Name { get; set; }
    }
    

    
}
