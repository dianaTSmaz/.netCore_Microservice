using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data
{
    public class DbContextCouponApi : DbContext
    {
 
        public DbContextCouponApi(DbContextOptions<DbContextCouponApi> options) : base(options)
        {
        }

        //Name assigned for the coupons dbase
        public DbSet<Coupon> Coupons { get; set; }

        //Seed the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           /* modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "25OFF",
                DiscountAccount = 25,
                MinAccount = 10,
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "15OFF",
                DiscountAccount = 15,
                MinAccount = 11,
            });*/
        }


    }
}
