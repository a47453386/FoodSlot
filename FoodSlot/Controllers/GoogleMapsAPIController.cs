using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodSlot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleMapsAPIController : ControllerBase
    {
        // GET: api/<GoogleMapsAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GoogleMapsAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GoogleMapsAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GoogleMapsAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GoogleMapsAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
