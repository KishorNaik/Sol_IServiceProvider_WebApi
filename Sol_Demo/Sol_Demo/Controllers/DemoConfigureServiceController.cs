using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Controllers
{
    [Route("api/demo")]
    [ApiController]
    public class DemoConfigureServiceController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider = null;

        public DemoConfigureServiceController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpGet("getdata")]
        public IActionResult GetDataDemo()
        {
            var memoryCacheService = serviceProvider.GetRequiredService<IMemoryCache>();

            string data = null;

            if (!memoryCacheService.TryGetValue("key", out data))
            {
                data = "Hello";

                var cahceEntryOption = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10));

                memoryCacheService.Set<String>("key", data, cahceEntryOption);
            }

            return base.Ok(data);
        }
    }
}