using Client.Interfaces;
using MessageMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class Display : IDisplay
    {
        private Queue<Message> incomingMessageQueue;
        public Display(Queue<Message> incomingMessageQueue)
        {
            this.incomingMessageQueue = incomingMessageQueue;
        }

        public void Show()
        {
            Console.WriteLine("Press ENTER to send message");
            do
            {
                while (!Console.KeyAvailable)
                {
                    if(incomingMessageQueue.Count > 0)
                    {
                        Console.WriteLine(BuildMessageDisplay(incomingMessageQueue.Dequeue()));
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }

        private string BuildMessageDisplay(Message message)
        {
            var template = "{0}:\n{1}";
            return string.Format(template, message.SenderId, message.MessageText);

        }
    }
}
