using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DockerAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        // GET api/content
        [HttpGet]
        public ActionResult<string> GetAllWebPages()
        {
            return "Returning all web pages";
        }

        // GET api/content/5
        [HttpGet("{id}")]
        public ActionResult<string> GetWebPageWithId(int id)
        {
            return "Returning webpage with id " + id;
        }

        // POST api/content
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            Console.Write("Adding new webpage");
            return "Adding new webpage";
        }

        // PUT api/content/5
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] string value)
        {
            Console.Write("Editing webpage with id " + id);
            return "Editing webpage with id " + id;
        }

        // DELETE api/content/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            Console.Write("Deleting webpage with id" + id);
            return "Deleting webpage with id " + id;
        }
    }
}
