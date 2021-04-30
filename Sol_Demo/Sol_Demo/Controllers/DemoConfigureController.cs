using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Controllers
{
    [Route("api/demo1")]
    [ApiController]
    public class DemoConfigureController : ControllerBase
    {
        private readonly IMemoryCache memoryCache = null;

        public DemoConfigureController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        [HttpGet("data")]
        public IActionResult GetData()
        {
            return base.Ok(memoryCache.Get("key1"));
        }
    }
}