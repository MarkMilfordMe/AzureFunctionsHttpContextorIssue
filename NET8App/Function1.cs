using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DemoApp
{
    public class Function1
    {
        
        public Function1(InjectedSettings settings)
        {
            if (settings.ValueFromParameter == null)
                throw new Exception("Call Function with GET Parameter ValueFromParameter");
        }

        [Function("Function1")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
