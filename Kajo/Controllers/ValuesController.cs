using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Kajo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ValuesController : ControllerBase
    {
        // GET: api/xx
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "MoQa", "Kajo", "Framework" };
        }

        // GET: api/xx/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "Kajo";
        }

        // POST: api/xx
        [HttpPost]
        public void Post([FromBody] JObject value)
        {
        }

        // PUT: api/xx/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] JObject value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
