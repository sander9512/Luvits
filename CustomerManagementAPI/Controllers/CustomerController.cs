using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pitstop.Infrastructure.Messaging;

namespace CustomerManagementAPI.Controllers
{

    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
          IMessagePublisher _messagePublisher;

        public CustomerController(IMessagePublisher messagePublisher) {
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public ActionResult<string> GetAllCustomers()
        {
            return "Returning all customers";
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetCustomerWithId(int id)
        {
            return "Returning customer with id " + id;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostCustomer([FromBody] string value)
        {
            Console.Write("Adding new customer");
            await _messagePublisher.PublishMessageAsync("NewCustomer", "New customer posted", "");
            return "Adding new customer";
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> EditCustomer(int id, [FromBody] string value)
        {
            Console.Write("Editing customer with id " + id);
            await _messagePublisher.PublishMessageAsync("CustomerEdited", "Customer with id " + id + " has been edited", "");
            return "Editing customer with id " + id;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteCustomer(int id)
        {
            Console.Write("Deleting customer with id" + id);
            await _messagePublisher.PublishMessageAsync("CustomerDeleted", "Customer with id " + id + " has been deleted", "");
            return "Deleting customer with id " + id;
        }
    }
}
