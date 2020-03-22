using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pitstop.Infrastructure.Messaging;

namespace MultimediaManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class MultimediaController : Controller
    {
        IMessagePublisher _messagePublisher;

        public MultimediaController(IMessagePublisher messagePublisher) {
            _messagePublisher = messagePublisher;
        }
         // GET api/multimedia
        [HttpGet]
        public ActionResult<string> GetAllMultiMedia()
        {
            return "Returning all multimedia";
        }

        // GET api/multimedia/5
        [HttpGet("{id}")]
        public ActionResult<string> GetMultimediaWithId(int id)
        {
            return "Returning multimedia with id " + id;
        }

        // POST api/multimedia
        [HttpPost]
        public async Task<ActionResult<string>> PostMultimedia([FromBody] string value)
        {
            Console.Write("Adding new webpage");
            await _messagePublisher.PublishMessageAsync("NewMultimedia", "New multimedia posted", "");
            return "Adding new multimedia";
        }

        // PUT api/multimedia/5
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> EditMultiMedia(int id, [FromBody] string value)
        {
            Console.Write("Editing multimedia with id " + id);
            await _messagePublisher.PublishMessageAsync("MultimediaEdited", "Multimedia with id " + id + " has been edited", "");
            return "Editing multimedia with id " + id;
        }

        // DELETE api/multimedia/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteMultimedia(int id)
        {
            Console.Write("Deleting multimedia with id" + id);
            await _messagePublisher.PublishMessageAsync("MultimediaDeleted", "Multimedia with id " + id + " has been deleted", "");
            return "Deleting multimedia with id " + id;
        }

    }
}
