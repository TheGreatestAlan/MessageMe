using MessageMe.Model;

namespace MessageMe.Interfaces
{
    public interface IMessageManager
    {
        bool EnqueueMessage(Message message);
    }
}
