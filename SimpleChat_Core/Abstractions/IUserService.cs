using SimpleChat_Core.DTO;

namespace SimpleChat_Core.Abstractions
{
    public interface IUserService
    {
        Task<UserDTO?> CreateUserAsync(string name, CancellationToken cancellationToken);
    }
}
