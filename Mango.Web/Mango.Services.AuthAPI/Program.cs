using AutoMapper;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Service;
using Mango.Services.AuthAPI.Service.IService;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<DbContextAuthApi>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//We use dependency injection to be able to use the configurations to use JWT
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*In this part we AddIdentity User and role by default and add the EntityFramework to work as a bridge between the dbcontext of the app
 and the Identity system*/
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DbContextAuthApi>()

    .AddDefaultTokenProviders();

builder.Services.AddControllers();

//Add the service with Scoped(one per request)
builder.Services.AddScoped<IAuthService, AuthService>();

//Add the JSON Web Token Service
builder.Services.AddScoped<IJWTTokenGenerator,JWTTokenGenerator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.MapControllers();
ApplyMigration();
app.Run();

//Create a function to run automatically the migrations which are pending
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope()) 
    {
        var _db = scope.ServiceProvider.GetRequiredService<DbContextAuthApi>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        { 
            _db.Database.Migrate(); 
        
        } 
    }
}