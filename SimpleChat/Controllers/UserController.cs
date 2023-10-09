using Microsoft.AspNetCore.Mvc;
using SimpleChat_Core.Abstractions;

namespace SimpleChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(string name, CancellationToken cancellationToken)
        {
            var dto = await _userService.CreateUserAsync(name, cancellationToken);

            return dto != null ? Ok(dto) : BadRequest();
        }

    }
}
