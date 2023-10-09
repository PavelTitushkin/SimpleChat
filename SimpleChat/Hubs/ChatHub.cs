using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using SimpleChat_Core.DTO;
using SimpleChat_Data_Repositories.IRepositories;

namespace SimpleChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IServiceProvider _serviceProvider;

        public ChatHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IChatRepository>();
            var connectionId = Context.ConnectionId;
            repository.RemoveConnection(connectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SaveUserConnection(int userId)
        {
            var connectionId = Context.ConnectionId;
            var connectionDto = new ConnectionDTO
            {
                Id = 0,
                ConnectionId = connectionId,
                UserId = userId,
            };

            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IChatRepository>();

            await repository.CreateConnectionAsync(connectionDto);
        }

        public async Task JoinToChatAsync(string connectionId, string chatName)
        {
            await Groups.AddToGroupAsync(connectionId, chatName);
        }

        public async Task SendMessageToChatAsync(string message, string chatName)
        {
            await Clients.Group(chatName).SendAsync("Receive", message);
        }
    }
}
