using dymaptic.GeoBlazor.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WasmCreateGraphicsLayers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddGeoBlazor();

var mapSettings = new Dictionary<string, string> { { "ArcGISApiKey", "INSERT YOUR KEY HERE" } };
builder.Configuration.AddInMemoryCollection(mapSettings);


await builder.Build().RunAsync();
