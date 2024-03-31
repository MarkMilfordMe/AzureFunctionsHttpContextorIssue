using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


  
[assembly: FunctionsStartup(typeof(Net6App.Startup))]
namespace Net6App
    {
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient(sp =>
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
        }
    }
    public class InjectedSettings
    {
        public string ValueFromParameter { get; set; }
    }

}
