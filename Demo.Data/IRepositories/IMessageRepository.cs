using Demo.Data.Models;


namespace Demo.Data.IRepositories;

public interface IMessageRepository : IRepository<Message>
{
    public Message? GetMessage(int id);

    IEnumerable<Message> GetMessagesForSender(string senderId);
    IEnumerable<Message> GetMessagesForReceiver(string receiverId);
}
