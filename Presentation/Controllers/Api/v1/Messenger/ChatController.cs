using Application.CQS.Messenger.Chat.Command.CreateChat;
using Application.CQS.Messenger.Chat.Command.DeleteChat;
using Application.CQS.Messenger.Chat.Command.UpdateChat;
using Application.CQS.Messenger.Chat.Queries.GetChatById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Extension;
using Presentation.WebResponse;
using Shared.Const;
using Shared.DataTransferObject.Messenger;
using System.Net.Mime;
using WebApi.Abstractions;

namespace Presentation.Controllers.Api.v1.Messenger
{
    [Authorize(Policy = AuthorizationConst.Policy.UserPolicy)]
    public class ChatController : ApiController
    {
        private readonly ILogger<ChatController> _logger;

        public ChatController(ISender sender, IHttpContextAccessor httpContextAccessor, ILogger<ChatController> logger)
            : base(sender, httpContextAccessor)
        {
            _logger = logger;
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPost()]
        public async Task<IActionResult> CreateChat([FromBody] ChatDTO chatDto, CancellationToken cancellationToken)
        {
            var membersUuidList = chatDto.Members.ToList();
            var adminsUuidList = chatDto.Members.ToList();
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var commandCreateChat = new CreateChatCommand(userUuid,
                                                          chatDto.ChatName,
                                                          chatDto.ChatDescription,
                                                            membersUuidList,
                                                          chatDto.PictureBase64,
                                                          chatDto.PictureMimeType);

            var result = await Sender.Send(commandCreateChat, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChat(Guid chatId, CancellationToken cancellationToken)
        {
            var command = new GetChatByIdQuery(chatId);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPut("{chatId}")]
        public async Task<IActionResult> UpdateChat(Guid chatId, [FromBody] ChatDTO chatDto, CancellationToken cancellationToken)
        {

            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new UpdateChatCommand(userUuid, 
                                                chatId,
                                                chatDto.ChatName,
                                                chatDto.ChatDescription,
                                                chatDto.PictureBase64,
                                                          chatDto.PictureMimeType);
            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        [Produces(MediaTypeNames.Application.Json)]
        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteChat(Guid chatId, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new DeleteChatCommand(userUuid, chatId);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

    }
}