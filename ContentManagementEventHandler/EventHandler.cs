using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Luvits.ContentManagementEventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        IMessageHandler _messageHandler;

        public EventHandler(IMessageHandler messageHandler) {
            _messageHandler = messageHandler;
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
            Log.Information("Event with messagetype " + messageType + " logged");
            Log.Information("Event with message " + message + " logged");

            return true;
        }
    }
}