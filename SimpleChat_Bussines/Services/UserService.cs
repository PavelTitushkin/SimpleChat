using SimpleChat_Core.Abstractions;
using SimpleChat_Core.DTO;
using SimpleChat_Data_Repositories.IRepositories;

namespace SimpleChat_Bussines.Services
{
    public class UserService : IUserService
    {
        private readonly IChatRepository _repository;

        public UserService(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDTO?> CreateUserAsync(string name, CancellationToken cancellationToken)
        {
            return await _repository.AddUserAsync(name, cancellationToken);
        }
    }
}
