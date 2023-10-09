using Microsoft.AspNetCore.Mvc;
using SimpleChat_Core.Abstractions;

namespace SimpleChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        [Route("ConnectToChat")]
        public async Task<IActionResult> ConnectToChat(int userId, int chatId, CancellationToken cancellationToken)
        {
            var result = await _chatService.ConnectToChatAsync(userId, chatId, cancellationToken);

            return result == 1 ? Ok(result) : BadRequest();
        }

        [HttpGet]
        [Route("DisconnectToChat")]
        public async Task<IActionResult> DisconnectToChat(int userId, CancellationToken cancellationToken)
        {
            var result = await _chatService.DisconnectToChatAsync(userId, cancellationToken);

            return result == 1 ? Ok(result) : BadRequest();
        }

        [HttpGet]
        [Route("SearchChat")]
        public async Task<IActionResult> SearchChat(string chatName, CancellationToken cancellationToken)
        {
            var dto = await _chatService.SearchChatAsync(chatName, cancellationToken);

            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpPost]
        [Route("CreateChat")]
        public async Task<IActionResult> CreateChat(int userId, string chatName, CancellationToken cancellationToken)
        {
            var dto = await _chatService.CreateChatAsync(userId, chatName, cancellationToken);

            return dto != null ? Ok(dto) : BadRequest();
        }

        [HttpPost]
        [Route("SendMessageToChat")]
        public async Task<IActionResult> SendMessageToChat(int userId, int chatId, string message, CancellationToken cancellationToken)
        {
            var result = await _chatService.SendMessageToChatAsync(userId, chatId, message, cancellationToken);

            return result == 1 ? Ok(result) : BadRequest();
        }


        [HttpDelete]
        [Route("DeleteChat")]
        public async Task<IActionResult> DeleteChat(int userId, int chatId, CancellationToken cancellationToken)
        {
            var result = await _chatService.DeleteChatAsync(userId, chatId, cancellationToken);

            return result == 1 ? Ok(result) : Forbid();
        }
    }
}
