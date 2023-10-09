using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleChat_Core.DTO;
using SimpleChat_Data_Repositories.IRepositories;
using SimpleChat_Db;
using SimpleChat_Db.Entities;

namespace SimpleChat_Data_Repositories.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly SimpleChatContext _dbContext;
        private readonly IMapper _mapper;

        public ChatRepository(SimpleChatContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ChatDTO?> CreateChatAsync(int userId, string chatName, CancellationToken cancellationToken)
        {
            var chat = new Chat
            {
                ChatName = chatName,
                MainUserId = userId,
            };
            await _dbContext.AddAsync(chat, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? _mapper.Map<ChatDTO>(chat) : null;
        }

        public async Task<int> AddChatIdToConnectionAsync(int chatId, ConnectionDTO ConnectionDTO, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Connection
                .Where(c => c.Id == ConnectionDTO.Id)
                .Select(dto => _mapper.Map<Connection>(dto))
                .FirstOrDefaultAsync(cancellationToken);

            if (entity != null)
            {
                entity.ChatId = chatId;
                _dbContext.Update(entity);

                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return 0;
            }
        }


        public Task<ChatDTO?> SearchChatByNameAsync(string chatName, CancellationToken cancellationToken)
        {
            return _dbContext.Chats
                .Where(c => c.ChatName.ToUpper() == chatName.ToUpper())
                .Select(entity => _mapper.Map<ChatDTO>(entity))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<string?> GetChatNameAsync(int? chatId)
        {
            var dto = await _dbContext.Chats
                .AsNoTracking()
                .Where(c => c.Id == chatId)
                .Select(entity => _mapper.Map<ChatDTO>(entity))
                .FirstOrDefaultAsync();

            return dto != null ? dto.ChatName : null;
        }

        public async Task<int> DeleteChatAsync(int userId, int chatId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Chats
                .Where(c => c.Id == chatId)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity?.MainUserId == userId && entity.Connections.IsNullOrEmpty())
            {
                _dbContext.Remove(entity);

                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return 0;
            }
        }

        public async Task CreateConnectionAsync(ConnectionDTO ConnectionDTO)
        {
            var entity = _mapper.Map<Connection>(ConnectionDTO);
            await _dbContext.Connection.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public int RemoveConnection(string connectionId)
        {
            var entity = _dbContext.Connection
                 .Where(c => c.ConnectionId == connectionId)
                 .FirstOrDefault();
            if (entity != null)
            {
                _dbContext.Remove(entity);
                return _dbContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public async Task<ConnectionDTO?> GetUserConnectionAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Connection
                .Where(c => c.userId == userId)
                .Select(ent => _mapper.Map<ConnectionDTO>(ent))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> CreateMessageAsync(int userId, int chatId, string message, CancellationToken cancellationToken)
        {
            var entity = new Message
            {
                Id = 0,
                UserId = userId,
                ChatId = chatId,
                Content = message
            };
            await _dbContext.Message.AddAsync(entity, cancellationToken);

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserDTO?> AddUserAsync(string name, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = 0,
                Name = name,
            };
            await _dbContext.Users.AddAsync(user, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            return result == 1 ? _mapper.Map<UserDTO>(user) : null;
        }
    }
}
