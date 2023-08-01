using CouponsWeb.Models;
using CouponsWeb.Service;
using CouponsWeb.Service.IService;
using CouponsWeb.Utility;
using CouponService = CouponsWeb.Service.CouponService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add the httpClientFactory to program.CS to be able to create clients
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ICouponService, CouponService>();

StartingDetails.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];

//Inject and specify the life cycle of the services
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
