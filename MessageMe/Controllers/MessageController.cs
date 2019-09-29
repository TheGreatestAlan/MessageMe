using MessageMe.Interfaces;
using MessageMe.Model;

namespace MessageMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageManager messageManager;
        public MessageController(IMessageManager messageManager)
        {
            this.messageManager = messageManager;
        }
        // POST: api/Message
        public bool Post([FromBody]Message message)
        {
            return messageManager.EnqueueMessage(message);
        }
    }
}
