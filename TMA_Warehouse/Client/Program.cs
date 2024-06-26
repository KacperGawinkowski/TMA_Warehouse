using AntDesign;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TMA_Warehouse.Client;
using TMA_Warehouse.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<MessageService>();
builder.Services.AddAntDesign();

await builder.Build().RunAsync();
