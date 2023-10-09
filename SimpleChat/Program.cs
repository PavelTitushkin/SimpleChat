using Microsoft.EntityFrameworkCore;
using SimpleChat.Extensions;
using SimpleChat.Hubs;
using SimpleChat.SubscribeTableDependencies;
using SimpleChat_Bussines.Services;
using SimpleChat_Core.Abstractions;
using SimpleChat_Data_Repositories.IRepositories;
using SimpleChat_Data_Repositories.Repositories;
using SimpleChat_Db;

namespace SimpleChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Connection Db
            var connectionString = builder.Configuration.GetConnectionString("SimpleChatDb");
            builder.Services.AddDbContext<SimpleChatContext>(optionsBuilder =>
            optionsBuilder.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IChatRepository, ChatRepository>();
            builder.Services.AddSingleton<ChatHub>();
            builder.Services.AddSingleton<SubscribeConnectionTableDependency>();
            builder.Services.AddSingleton<SubscribeMessageTableDependency>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddSignalR();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapHub<ChatHub>("/chathub");

            app.UseSqlTableDependency<SubscribeConnectionTableDependency>(connectionString);
            app.UseSqlTableDependency<SubscribeMessageTableDependency>(connectionString);

            app.Run();
        }
    }
}