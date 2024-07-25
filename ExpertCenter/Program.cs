using ExpertCenter.Repository;
using ExpertCenter.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json");

builder.Services.AddDbContextFactory<ExpertCenterDbContext>(optionBuilder =>
{
    string? conStr = builder.Configuration.GetConnectionString("default");

    if (string.IsNullOrEmpty(conStr))
        throw new Exception("Требуется строка подключения к БД");

    optionBuilder.UseSqlServer(conStr);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository, EFRepository>();

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
    pattern: "{controller=PriceLists}/{action=All}/{id?}");

app.Run();
