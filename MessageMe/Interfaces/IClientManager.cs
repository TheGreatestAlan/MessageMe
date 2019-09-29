using MessageMe.Model;

namespace MessageMe.Interfaces
{
    public interface IClientManager
    {
        bool CheckIn(Client client);

        string Register(Client client);

        bool IsOnline(string clientId);

        void SetOffline(string clientId);

        string GetClientAddress(string clientId);

    }
}
