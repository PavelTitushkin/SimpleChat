using SimpleChat_Core.DTO;

namespace SimpleChat_Core.Abstractions
{
    public interface IChatService
    {
        Task<ChatDTO?> CreateChatAsync(int userId, string chatName, CancellationToken cancellationToken);
        Task<ChatDTO?> SearchChatAsync(string chatName, CancellationToken cancellationToken);
        Task<int> DeleteChatAsync(int userId, int chatId, CancellationToken cancellationToken);
        Task<int> ConnectToChatAsync(int userId, int chatId, CancellationToken cancellationToken);
        Task<int> DisconnectToChatAsync(int userId, CancellationToken cancellationToken);
        Task<int> SendMessageToChatAsync(int userId, int chatId, string message, CancellationToken cancellationToken);
    }
}
