using System.Collections.Generic;

using ASPNetCoreSwagger.Domain.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreSwagger.Controllers
{
    [Route("api/v1/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet("/api/v1/[controller]")]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        [HttpGet("/api/v2/[controller]")]
        public IEnumerable<string> Get2()
        {
            return new[] { "value1", "value2" };
        }

        [HttpGet("/api/v1/[controller]/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("/api/v1/[controller]")]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("/api/v1/[controller]/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("/api/v1/[controller]/{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("/api/v1/GetSomething")]
        public IActionResult GetSomething([FromBody] SomeRequest request)
        {
            if (request.SomeId > 0)
                return Ok(new SomeResponse
                {
                    SomeCode = 200,
                    SomeMessage = "Everything OK!"
                });

            return new BadRequestObjectResult(
                new SomeResponse
                {
                    SomeCode = 404,
                    SomeMessage = "Not Found!"
                });
        }
    }
}
