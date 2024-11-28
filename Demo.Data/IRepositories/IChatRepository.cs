using Demo.Data.Models;


namespace Demo.Data.IRepositories;
public interface  IChatRepository: IRepository<Chat>
{
    Chat? Get(int id);

    Chat? GetWithTalkers(int id);
    Chat? GetWithMessages(int id);

    IEnumerable<Chat>? ChatsForUser(string userId);
}
