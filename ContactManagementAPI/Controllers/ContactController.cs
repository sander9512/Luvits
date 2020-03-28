using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pitstop.Infrastructure.Messaging;

namespace ContactManagementAPI.Controllers
{
    
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
          IMessagePublisher _messagePublisher;

        public ContactController(IMessagePublisher messagePublisher) {
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public ActionResult<string> GetAllContacts()
        {
            return "Returning all customer contacts";
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetContactWithId(int id)
        {
            return "Returning customer contact with id " + id;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostContact([FromBody] string value)
        {
            Console.Write("Adding new customer contact");
            await _messagePublisher.PublishMessageAsync("NewContact", "New customer contact posted", "");
            return "Adding new customer contact";
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> EditContact(int id, [FromBody] string value)
        {
            Console.Write("Editing customer contact with id " + id);
            await _messagePublisher.PublishMessageAsync("ContactEdited", "Customer contact with id " + id + " has been edited", "");
            return "Editing customer contact with id " + id;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteContact(int id)
        {
            Console.Write("Deleting customer contact with id" + id);
            await _messagePublisher.PublishMessageAsync("ContactDeleted", "Customer contact with id " + id + " has been deleted", "");
            return "Deleting customer contact with id " + id;
        }
    }
}
