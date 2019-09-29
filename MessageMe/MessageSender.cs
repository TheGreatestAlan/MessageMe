using MessageMe.Exceptions;
using MessageMe.Interfaces;
using MessageMe.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MessageMe
{
    public class MessageSender : IMessageSender
    {
        private IClientManager clientManager;
        private Queue<Message> messageQueue;
        private HttpClient client;

        public MessageSender(IClientManager clientManager, Queue<Message> messageQueue, HttpClient client)
        {
            this.clientManager = clientManager;
            this.messageQueue = messageQueue;
            this.client = client;
        }

        public async Task SendMessage()
        {
            while (true)
            {
                if (messageQueue.Count > 0)
                {
                    var message = messageQueue.Dequeue();
                    if (clientManager.IsOnline(message.RecipientId))
                    {
                        var res = await SendMessage(message);
                        if(!res.IsSuccessStatusCode)
                        {
                            clientManager.SetOffline(message.RecipientId);
                            messageQueue.Enqueue(message);
                        }
                    }
                    else
                    {
                        messageQueue.Enqueue(message);
                    }
                }
            }

        }

        private async Task<HttpResponseMessage> SendMessage(Message message)
        {
            try
            {
                var address = clientManager.GetClientAddress(message.RecipientId);
                address = address + "/api/MessageClient";
                return await client.PostAsync(address, new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json"));
            }
            catch (ClientNotFoundException)
            {
                //ruh roh
                return null;
            }
        }
    }
}
