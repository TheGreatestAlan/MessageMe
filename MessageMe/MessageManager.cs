using MessageMe.Interfaces;
using MessageMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageMe
{
    public class MessageManager : IMessageManager
    {

        private Queue<Message> messageQueue;
        public MessageManager(Queue<Message> queue)
        {
            this.messageQueue = queue;
        }
        public bool EnqueueMessage(Message message)
        {
            messageQueue.Enqueue(message);
            return true;
        }
    }
}
