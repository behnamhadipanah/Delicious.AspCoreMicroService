
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot()
      .AddCacheManager(x =>
      {
          x.WithDictionaryHandle();
      });

builder.Host
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json",true,true);
    })
    .ConfigureLogging((hostingContext, logginBuilder) =>
{
    logginBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logginBuilder.AddConsole();
    logginBuilder.AddDebug();
});

var app = builder.Build();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Hello World!");
//    });
//});


app.MapGet("/", () => "Hello World!");
await app.UseOcelot();
app.Run();
