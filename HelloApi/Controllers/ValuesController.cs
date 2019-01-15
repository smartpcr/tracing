using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace HelloApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITracer tracer;

        public ValuesController(ITracer tracer)
        {
            this.tracer = tracer;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            this.SlowCall();
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private void SlowCall()
        {
            var span = tracer.BuildSpan("SlowCall").Start();
            var startTime = DateTime.UtcNow;
            Thread.Sleep(TimeSpan.FromSeconds(2));
            var endTime = DateTime.UtcNow;
            span.Log(new List<KeyValuePair<string, object>>()
            {
                {new KeyValuePair<string, object>("start", startTime) },
                {new KeyValuePair<string, object>("stop", endTime) },
            });
            span.Finish();
        }
    }
}
