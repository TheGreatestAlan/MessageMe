using MessageMe.Exceptions;
using MessageMe.Interfaces;
using MessageMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace MessageMe
{
    public class ClientManager : IClientManager
    {
        MemoryCache memoryCache;

        public ClientManager(MemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public bool CheckIn(Client client)
        {
            var memClient = memoryCache[string.Format(MessageMeConstants.TEMPLATE_CLIENT_ONLINE, client.Id)];
            if (memClient != null)
            {
                memoryCache[string.Format(MessageMeConstants.TEMPLATE_CLIENT_ONLINE, client.Id)] = "true";
                return true;
            }
            return false;
        }

        public string GetClientAddress(string clientId)
        {
            var clientAddress = memoryCache[string.Format(MessageMeConstants.TEMPLATE_CLIENT_ADDRESS, clientId)];
            if (clientAddress == null)
            {
                throw new ClientNotFoundException();
            }
            return (string)clientAddress;
        }

        public bool IsOnline(string clientId)
        {
            var memClient = memoryCache[string.Format(MessageMeConstants.TEMPLATE_CLIENT_ONLINE, clientId)];
            if (memClient != null && ((string)memClient).Equals("true"))
            {
                return true;
            }
            return false;
        }

        public string Register(Client client)
        {
            var guid = Guid.NewGuid();
            memoryCache[string.Format(MessageMeConstants.TEMPLATE_CLIENT_ID, guid.ToString())] = guid.ToString();
            memoryCache[string.Format(MessageMeConstants.TEMPLATE_CLIENT_ADDRESS, guid.ToString())] = client.Address;
            memoryCache[string.Format(MessageMeConstants.TEMPLATE_CLIENT_NAME, guid.ToString())] = client.Name;
            memoryCache[string.Format(MessageMeConstants.TEMPLATE_CLIENT_ONLINE, guid.ToString())] = "true";
            return guid.ToString();
        }

        public void SetOffline(string clientId)
        {
            memoryCache[string.Format(MessageMeConstants.TEMPLATE_CLIENT_ONLINE, clientId)] = "false";
        }
    }
}
