using FoodSlot.Models;
using FoodSlot.Seed;
using FoodSlot.Seed.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//註冊連線字串
builder.Services.AddDbContext<FoodSlotContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodSlotConnection")));
//註冊服務
builder.Services.AddScoped<IImageUploadService, ImageUploadService>();
//註冊種子資料
builder.Services.AddScoped<SeedRunner>();
builder.Services.AddScoped<SeedFood>();
builder.Services.AddScoped<SeedUsers>();
builder.Services.AddScoped<SeedSystemSetting>();
builder.Services.AddScoped<SeedSystemSettingCategory>();
builder.Services.AddScoped<SeedUserFoodSettings>();
builder.Services.AddScoped<SeedUserRange>();
builder.Services.AddScoped<SeedUserRangeSettings>();




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<SeedRunner>();
    seeder.Run();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
