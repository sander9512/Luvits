using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pitstop.Infrastructure.Messaging;

namespace CustomerRelationManagementAPI.Controllers
{
   [Route("api/[controller]")]
    public class CustomerRelationController : Controller
    {
        IMessagePublisher _messagePublisher;

        public CustomerRelationController(IMessagePublisher messagePublisher) {
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public ActionResult<string> GetAllRelations()
        {
            return "Returning all customer relations";
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetRelationWithId(int id)
        {
            return "Returning customer relation with id " + id;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostRelation([FromBody] string value)
        {
            Console.Write("Adding new customer relation");
            await _messagePublisher.PublishMessageAsync("NewCustomerRelation", "New customer relation posted", "");
            return "Adding new customer relation";
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<string>> EditRelation(int id, [FromBody] string value)
        {
            Console.Write("Editing customer relation with id " + id);
            await _messagePublisher.PublishMessageAsync("CustomerRelationEdited", "Customer relation with id " + id + " has been edited", "");
            return "Editing customer relation with id " + id;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteRelation(int id)
        {
            Console.Write("Deleting customer relation with id" + id);
            await _messagePublisher.PublishMessageAsync("CustomerRelattionDeleted", "Customer relation with id " + id + " has been deleted", "");
            return "Deleting customer relation with id " + id;
        }

    }
}
