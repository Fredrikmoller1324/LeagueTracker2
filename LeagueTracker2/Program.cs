using LeagueTracker2;
using LeagueTracker2.Controllers;
using LeagueTracker2.Data;
using LeagueTracker2.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register your services here
builder.Services.AddTransient<SummonerController>();
builder.Services.AddTransient<RiotApiService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add your connection string and database context here
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LeagueTrackerDBContext>(options => options.UseSqlServer(connectionString));

await builder.Build().RunAsync();
