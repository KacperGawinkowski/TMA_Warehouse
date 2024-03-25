using Microsoft.AspNetCore.ResponseCompression;
using TMA_Warehouse.Server;
using TMA_Warehouse.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSqlServer<Context>(builder.Configuration.GetConnectionString("WarehouseConnection"), optionsAction: opt =>
{
    opt.EnableSensitiveDataLogging(true);
});

builder.Services.AddScoped<ItemGroupRepository>();
builder.Services.AddScoped<UnitOfMeasureRepository>();
builder.Services.AddScoped<ItemRepository>();
//builder.Services.AddScoped<RequestRepository>();
//builder.Services.AddScoped<RequestRowRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
