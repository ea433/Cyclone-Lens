using Business_Logic_Layer.Services;
using Data_Access_Layer.Repositories;
using Interface_Layer.InterfaceRepositories;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<IObservatieService, ObservatieService>();
builder.Services.AddScoped<IObservatieRepository>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();

    var connectionString = config.GetConnectionString("DefaultConnection")
        ?? throw new Exception("Connection string not found");

    return new ObservatieDAL(connectionString);
});

builder.Services.AddScoped<ICycloonRepository>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");

    if (string.IsNullOrEmpty(connectionString))
        throw new Exception("Connection string not found");

    return new CycloonRepository(connectionString);
});

builder.Services.AddScoped<ICycloonDataRepository>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
        throw new Exception("Connection string not found");
    else
        return new CycloonDataRepository(connectionString);
});

builder.Services.AddScoped<ILoggingRepository>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("DefaultConnection");

    if (string.IsNullOrEmpty(connectionString))
        throw new Exception("Connection string not found");
    else
        return new LoggingRepository(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

// . 
