﻿using SimpleChat.Hubs;
using SimpleChat.SubscribeTableDependencies.Contracts;
using SimpleChat_Data_Repositories.IRepositories;
using SimpleChat_Db.Entities;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SimpleChat.SubscribeTableDependencies
{
    public class SubscribeMessageTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Message> tableDependency;
        ChatHub _chatHub;
        private readonly IServiceProvider _serviceProvider;

        public SubscribeMessageTableDependency(ChatHub chatHub, IServiceProvider serviceProvider)
        {
            _chatHub = chatHub;
            _serviceProvider = serviceProvider;
        }

        public void SubscribeTable(string connectionString)
        {
            tableDependency = new SqlTableDependency<Message>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<Message> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var message = e.Entity;
                using var scope = _serviceProvider.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IChatRepository>();
                var chatName = await repository.GetChatNameAsync(message.ChatId);
                await _chatHub.SendMessageToChatAsync(message.Content, chatName);
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            //loger
            Console.WriteLine($"{e.Message}");
        }
    }
}
