using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Luvits.CustomerRelationManagementEventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        IMessageHandler _messageHandler;
        private List<String> PoCEvents;

        public EventHandler(IMessageHandler messageHandler) {
            _messageHandler = messageHandler;
            PoCEvents = new List<String> {"NewContact", "ContactEdited", "ContactDeleted", "NewCustomer", "CustomerEdited", "CustomerDeleted", 
            "NewDocument", "DocumentEdited", "DocumentDeleted"};
        }
        
        public void Start() {
            _messageHandler.Start(this);
        }

        public void Stop() {
            _messageHandler.Stop();
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            _messageHandler.Start(this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _messageHandler.Stop();
            return Task.CompletedTask;
        }

        public async Task<bool> HandleMessageAsync(string messageType, string message) {
            if(PoCEvents.Contains(messageType)) {
            Log.Information("Event with messagetype " + messageType + " logged");
            Log.Information("Event with message " + message + " logged");
            }

            return true;
        }
    }
}