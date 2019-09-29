using System;
using MessageMe.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace Client
{
    public class ServerManager : IServerManager
    {
        private HttpClient httpClient;
        public ServerManager(HttpClient client)
        {
            this.httpClient = client;
        }
        public async Task StartUp()
        {
            if (IsClientRegistered())
            {
                var res = await CheckIn();
            }
            else
            {
                var res = await Register();
            }
        }

        private bool IsClientRegistered()
        {
            try
            {
                var client = JsonConvert.DeserializeObject<MessageMe.Model.Client>(
                    File.ReadAllText(ClientConstants.REGISTRATION_FILENAME)
                );
                return true;
            } catch(Exception e)
            {
                return false;
            }
        }

        private async Task<HttpResponseMessage> CheckIn()
        {
            var address = ClientConstants.SERVER_BASE_ADDRESS + "/api/Client/Checkin";
            var res = await httpClient.PostAsync(address, new StringContent(JsonConvert.SerializeObject(GetCurrentClient()), Encoding.UTF8, "application/json"));
            return res;
        }


        private async Task<HttpResponseMessage> Register()
        {
            var address = ClientConstants.SERVER_BASE_ADDRESS + "/api/Client/Register";
            var currentClient = new MessageMe.Model.Client()
            {
                Id = "",
                Address = "http://localhost:64093",
                Online = true,
                Name = "Alan"
            };
            var res = await httpClient.PostAsync(address, new StringContent(JsonConvert.SerializeObject(currentClient), Encoding.UTF8, "application/json"));
            var clientId = await res.Content.ReadAsStringAsync();
            currentClient.Id = clientId;

            File.WriteAllText(ClientConstants.REGISTRATION_FILENAME,
                  JsonConvert.SerializeObject(currentClient));


            return res;
        }

        private MessageMe.Model.Client GetCurrentClient()
        {
            return JsonConvert.DeserializeObject<MessageMe.Model.Client>(
               File.ReadAllText(ClientConstants.REGISTRATION_FILENAME));
        }

        public async Task<HttpResponseMessage> SendMessage(string recipientId, string messageText)
        {
            var currentClient = GetCurrentClient();
            var message = new Message()
            {
                SenderId = currentClient.Id,
                RecipientId = recipientId,
                MessageText = messageText
            };

            var address = ClientConstants.SERVER_BASE_ADDRESS + "/api/Message";
            return await httpClient.PostAsync(address, new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json"));
        }
    }
}
