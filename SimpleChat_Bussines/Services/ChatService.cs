using SimpleChat_Core.Abstractions;
using SimpleChat_Core.DTO;
using SimpleChat_Data_Repositories.IRepositories;

namespace SimpleChat_Bussines.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repository;

        public ChatService(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<ChatDTO?> CreateChatAsync(int userId, string chatName, CancellationToken cancellationToken)
        {
            return await _repository.CreateChatAsync(userId, chatName, cancellationToken);
        }

        public async Task<int> ConnectToChatAsync(int userId, int chatId, CancellationToken cancellationToken)
        {
            var connectionDto = await _repository.GetUserConnectionAsync(userId, cancellationToken);
            if (connectionDto != null)
            {
                return await _repository.AddChatIdToConnectionAsync(chatId, connectionDto, cancellationToken);
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> DisconnectToChatAsync(int userId, CancellationToken cancellationToken)
        {
            var connectionDto = await _repository.GetUserConnectionAsync(userId, cancellationToken);
            if (connectionDto != null)
            {
                return _repository.RemoveConnection(connectionDto.ConnectionId);
            }

            return 0;
        }

        public async Task<ChatDTO?> SearchChatAsync(string chatName, CancellationToken cancellationToken)
        {
            return await _repository.SearchChatByNameAsync(chatName, cancellationToken);
        }

        public async Task<int> DeleteChatAsync(int userId, int chatId, CancellationToken cancellationToken)
        {
            return await _repository.DeleteChatAsync(userId, chatId, cancellationToken);
        }

        public async Task<int> SendMessageToChatAsync(int userId, int chatId, string message, CancellationToken cancellationToken)
        {
            return await _repository.CreateMessageAsync(userId, chatId, message, cancellationToken);
        }
    }
}
