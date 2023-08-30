
using Mango.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    //IdentityDbContext<T> ---> We must write the user , in this case the default
    //IdentityUser was changed to AppUser and this class extends from IdentiyUser
    public class DbContextAuthApi : IdentityDbContext<AppUser>
    {

        /*DbContextOptions*/
        public DbContextAuthApi(DbContextOptions<DbContextAuthApi> options) : base(options)
        { }

        //The DBSet t is very important bcos it allows to create tables
        //Remember like a prop so it is used get and set
        public DbSet<AppUser> AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
