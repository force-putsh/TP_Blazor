using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TP_Blazor.Data;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Microsoft.Extensions.DependencyInjection;
using Blazorise.Bootstrap;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHttpClient();
builder.Services.AddBlazorise()
    .AddBootstrapProviders()
    .AddBlazoredLocalStorage()
    .AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
