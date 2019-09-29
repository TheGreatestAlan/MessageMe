using System.Collections.Generic;
using System.Net.Http;
using MessageMe.Model;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageClientController : ControllerBase
    {
        private Queue<Message> incomingMessages;
        private IServerManager serverManager;

        public MessageClientController(Queue<Message> incomingMessages, IServerManager serverManager)
        {
            this.incomingMessages = incomingMessages;
            this.serverManager = serverManager;
        }
        // GET: api/MessageClient
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            var messageList = new List<Message>();
            while(incomingMessages.Count > 0)
            {
                messageList.Add(incomingMessages.Dequeue());
            }
            return messageList;
        }

        // sends message TO SERVER as client.  This exists because I'm bad with frontend stuff.
        [HttpPost]
        [Route(ClientConstants.ROUTE_SEND)]
        public HttpResponseMessage Send([FromBody] Message message)
        {
            return serverManager.SendMessage(message.RecipientId, message.MessageText).Result;
        }

        // POST: api/MessageClient
        // receives messages FROM SERVER
        [HttpPost]
        public void Post([FromBody] Message message)
        {
            incomingMessages.Enqueue(message);
        }
    }
}
