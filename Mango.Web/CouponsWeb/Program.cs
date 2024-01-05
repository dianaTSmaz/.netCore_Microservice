using CouponsWeb.Models;
using CouponsWeb.Service;
using CouponsWeb.Service.IService;
using CouponsWeb.Utility;
using Mango.Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;
using CouponService = CouponsWeb.Service.CouponService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add the httpClientFactory to program.CS to be able to create clients
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ICouponService, CouponService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();

StartingDetails.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];

//We Assign to AuthAPIBase the API url that is on lauch setting.jon file
//here is where the the base url page used in the controller takes its value
StartingDetails.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];



builder.Services.AddEndpointsApiExplorer();

//Inject and specify the life cycle of the services
builder.Services.AddScoped<IBaseService, BaseService>();

//builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//We assign to CouponAPIBASE the API URL which can be find in the launchSetting.json file
StartingDetails.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllers();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


