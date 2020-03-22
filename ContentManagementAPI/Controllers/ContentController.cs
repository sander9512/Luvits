using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pitstop.Infrastructure.Messaging;

namespace ContentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        IMessagePublisher _messagePublisher;

        public ContentController(IMessagePublisher messagePublisher) {
            _messagePublisher = messagePublisher;
        }
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
        public async Task<ActionResult<string>> Post([FromBody] string value)
        {
            Console.Write("Adding new webpage");
            await _messagePublisher.PublishMessageAsync("NewPage", "New web page posted", "");
            return "Adding new webpage";
        }

        // PUT api/content/5
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Put(int id, [FromBody] string value)
        {
            Console.Write("Editing webpage with id " + id);
            await _messagePublisher.PublishMessageAsync("PageEdited", "Page with id " + id + " has been edited", "");
            return "Editing webpage with id " + id;
        }

        // DELETE api/content/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            Console.Write("Deleting webpage with id" + id);
            await _messagePublisher.PublishMessageAsync("PageDeleted", "Page with id " + id + " has been deleted", "");
            return "Deleting webpage with id " + id;
        }
    }
}
