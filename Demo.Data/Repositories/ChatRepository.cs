using Demo.Data.IRepositories;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data.Repositories;

public class ChatRepository : Repository<Chat>, IChatRepository
{
    public ChatRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<Chat>? ChatsForUser(string userId)
    {
        return _dbContext.Chats.Include(c => c.User1).Include(c=> c.User2).Where(c => c.Talker2 == userId || c.Talker1 == userId);
    }

    public Chat? Get(int id)
    {
        return _dbContext.Chats.Find(id);
    }

    public Chat? GetWithMessages(int id)
    {
        return _dbContext.Chats.Include(x => x.Messages).FirstOrDefault(x => x.Id == id);
    }

    public Chat? GetWithTalkers(int id)
    {
        return _dbContext.Chats.Include(c => c.User1).Include(c => c.User2).FirstOrDefault(c => c.Id == id);
    }
}
