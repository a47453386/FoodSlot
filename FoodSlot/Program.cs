using FoodSlot.Models;
using FoodSlot.Seed;
using FoodSlot.Seed.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FoodSlotContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodSlotConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<SeedRunner>();
builder.Services.AddScoped<SeedSystemSetting>();
builder.Services.AddScoped<SeedSystemSettingCategory>();
builder.Services.AddScoped<SeedFood>();
builder.Services.AddScoped<SeedUser>();
builder.Services.AddScoped<SeedUserRange>();
builder.Services.AddScoped<SeedUserRangeSettings>();
builder.Services.AddScoped<SeedUserFoodSettings>();



var app = builder.Build();


// CLI Seeder 控制（取代原本自動 Seed）
if (args.Contains("Seed"))
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var db = services.GetRequiredService<FoodSlotContext>();
    var baseSeeder = services.GetRequiredService<SeedRunner>();
    

    bool force = args.Contains("--force");

    bool runBase = args.Contains("base") || (!args.Contains("base") && !args.Contains("demo"));
    bool runDemo = args.Contains("demo") || (!args.Contains("base") && !args.Contains("demo"));

    Console.WriteLine("=== Seeder Start ===");

    // Base Seed（平台基礎資料）
    if (runBase)
    {
        Console.WriteLine("Running Base Seed...");

        if (!force && db.Users.Any()) // ⚠️ 用你「一定存在的表」
        {
            Console.WriteLine("Base Seed 已存在，略過");
        }
        else
        {
            baseSeeder.Run();
            Console.WriteLine("Base Seed 完成");
        }
    }

    

    Console.WriteLine("=== Seeder End ===");

    return; // 跑完 Seeder 就結束，不啟動網站
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
