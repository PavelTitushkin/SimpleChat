using SimpleChat_Core.DTO;

namespace SimpleChat_Data_Repositories.IRepositories
{
    public interface IChatRepository
    {
        Task<UserDTO?> AddUserAsync(string name, CancellationToken cancellationToken);

        Task<ChatDTO?> CreateChatAsync(int userId, string chatName, CancellationToken cancellationToken);
        Task<string?> GetChatNameAsync(int? chatId);
        Task<ChatDTO?> SearchChatByNameAsync(string chatName, CancellationToken cancellationToken);
        Task<int> DeleteChatAsync(int userId, int chatId, CancellationToken cancellationToken);

        Task<int> AddChatIdToConnectionAsync(int chatId, ConnectionDTO ConnectionDTO, CancellationToken cancellationToken);
        Task CreateConnectionAsync(ConnectionDTO ConnectionDTO);
        int RemoveConnection(string connectionId);
        Task<ConnectionDTO?> GetUserConnectionAsync(int userId, CancellationToken cancellationToken);

        Task<int> CreateMessageAsync(int userId, int chatId, string message, CancellationToken cancellationToken);
    }
}
