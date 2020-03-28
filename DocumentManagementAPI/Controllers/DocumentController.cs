using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pitstop.Infrastructure.Messaging;

namespace DocumentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
          IMessagePublisher _messagePublisher;

        public DocumentController(IMessagePublisher messagePublisher) {
            _messagePublisher = messagePublisher;
        }

        [HttpGet]
        public ActionResult<string> GetAllDocuments()
        {
            return "Returning all documents";
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetDocumentWithId(int id)
        {
            return "Returning document with id " + id;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostDocument([FromBody] string value)
        {
            Console.Write("Adding new document");
            await _messagePublisher.PublishMessageAsync("NewDocument", "New document posted", "");
            return "Adding new document";
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> EditDocument(int id, [FromBody] string value)
        {
            Console.Write("Editing document with id " + id);
            await _messagePublisher.PublishMessageAsync("DocumentEdited", "Document with id " + id + " has been edited", "");
            return "Editing document with id " + id;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteDocument(int id)
        {
            Console.Write("Deleting document with id" + id);
            await _messagePublisher.PublishMessageAsync("DocumentDeleted", "Document with id " + id + " has been deleted", "");
            return "Deleting document with id " + id;
        }
    }
}
