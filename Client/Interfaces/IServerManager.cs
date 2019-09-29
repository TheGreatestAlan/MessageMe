using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public interface IServerManager
    {
        Task StartUp();

        Task<HttpResponseMessage> SendMessage(string recipientId, string messageText);
    }
}
