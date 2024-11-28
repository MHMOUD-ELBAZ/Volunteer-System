using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Repositories;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Message? GetMessage(int id)
    {
        return _dbContext.Messages.Find(id);
    }

    public IEnumerable<Message> GetMessagesForReceiver(string receiverId)
    {
        return _dbContext.Messages.Where(m => m.Receiver == receiverId);
    }

    public IEnumerable<Message> GetMessagesForSender(string senderId)
    {
        return _dbContext.Messages.Where(m => m.Sender == senderId);
    }


}

