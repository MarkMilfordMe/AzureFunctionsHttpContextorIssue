using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net6AppOutProc;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddHttpContextAccessor();
        services.AddTransient(sp =>
        {
            if (sp.GetService<IHttpContextAccessor>() == null)
            {
                throw new Exception("Unable to read IHttpContextAccessor");
            }
            var http = sp.GetService<IHttpContextAccessor>();
            var x = new InjectedSettings();
            x.ValueFromParameter = http.HttpContext.Request.Query["ValueFromParameter"];
            return x;
        });
    })
    .Build();

host.Run();
