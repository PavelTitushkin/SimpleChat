using SimpleChat.Hubs;
using SimpleChat.SubscribeTableDependencies.Contracts;
using SimpleChat_Data_Repositories.IRepositories;
using SimpleChat_Db.Entities;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SimpleChat.SubscribeTableDependencies
{
    public class SubscribeConnectionTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Connection> tableDependency;
        ChatHub _chatHub;
        private readonly IServiceProvider _serviceProvider;

        public SubscribeConnectionTableDependency(ChatHub chatHub, IServiceProvider serviceProvider)
        {
            _chatHub = chatHub;
            _serviceProvider = serviceProvider;
        }

        public void SubscribeTable(string connectionString)
        {
            tableDependency = new SqlTableDependency<Connection>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Connection> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var connection = e.Entity;
                if (connection.ChatId != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IChatRepository>();

                    var chatName = await repository.GetChatNameAsync(connection.ChatId);
                    await _chatHub.JoinToChatAsync(connection.ConnectionId, chatName);
                }
            }
        }
        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{e.Message}");
        }
    }
}
