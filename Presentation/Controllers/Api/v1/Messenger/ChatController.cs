using Application.CQS.Messenger.Chat.Command.AcknowledgeMessageDeliveryByChat;
using Application.CQS.Messenger.Chat.Command.AddChatMember;
using Application.CQS.Messenger.Chat.Command.AssignChatAdmin;
using Application.CQS.Messenger.Chat.Command.CreateChat;
using Application.CQS.Messenger.Chat.Command.CreateMessage;
using Application.CQS.Messenger.Chat.Command.DeleteChat;
using Application.CQS.Messenger.Chat.Command.DeleteMessage;
using Application.CQS.Messenger.Chat.Command.RemoveChatMember;
using Application.CQS.Messenger.Chat.Command.RevokeChatAdmin;
using Application.CQS.Messenger.Chat.Command.UpdateChat;
using Application.CQS.Messenger.Chat.Command.UpdateMessage;
using Application.CQS.Messenger.Chat.Queries.GetChatById;
using Application.CQS.Messenger.Chat.Queries.GetNotDeliveredMessagesByChat;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extension;
using Presentation.WebResponse;
using Shared.ApiDataTransferObject;
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
        #region Chat

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<ChatDTO>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpPost()]
        public async Task<IActionResult> CreateChat([FromBody] ChatDTO chatDto, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var commandCreateChat = new CreateChatCommand(userUuid,
                                                          chatDto.Name,
                                                          chatDto.Description,
                                                          chatDto.Members,
                                                          chatDto.PictureBase64,
                                                          chatDto.PictureMimeType);

            var result = await Sender.Send(commandCreateChat, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<ChatDTO>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChat(Guid chatId, CancellationToken cancellationToken)
        {
            var command = new GetChatByIdQuery(chatId);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<List<MessageDTO>>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpGet("message/nack")]
        public async Task<IActionResult> GetNotDeliveredMessagesByChat(CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new GetNotDeliveredMessagesByChatQuery(userUuid);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<List<MessageDTO>>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpPost("message/ack/{messageId}")]
        public async Task<IActionResult> GetNotDeliveredMessagesByChat(Guid messageId, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new AcknowledgeMessageDeliveryByChatCommand(userUuid, messageId);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<Guid>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpPut("{chatId}")]
        public async Task<IActionResult> UpdateChat(Guid chatId, [FromBody] ChatDTO chatDto, CancellationToken cancellationToken)
        {

            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new UpdateChatCommand(userUuid, 
                                                chatId,
                                                chatDto.Name,
                                                chatDto.Description,
                                                chatDto.PictureBase64,
                                                          chatDto.PictureMimeType);
            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<Guid>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteChat(Guid chatId, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new DeleteChatCommand(userUuid, chatId);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        #endregion
        #region Message

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<List<MessageDTO>>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpPost("{chatId}/message")]
        public async Task<IActionResult> CreateMessage(Guid chatId, [FromBody] List<MessageDTO> messageDTOs, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var commandCreateChat = new CreateMessageCommand(chatId,userUuid,messageDTOs);

            var result = await Sender.Send(commandCreateChat, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<MessageDTO>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpPut("{chatId}/message/{messageId}")]
        public async Task<IActionResult> UpdateMessage(Guid chatId, Guid messageId, [FromBody] MessageDTO messageDTO, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var commandCreateChat = new UpdateMessageCommand(userUuid,chatId,messageId, messageDTO);

            var result = await Sender.Send(commandCreateChat, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<Guid>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpDelete("{chatId}/message/{messageId}")]
        public async Task<IActionResult> DeleteMessage(Guid chatId, Guid messageId, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var commandCreateChat = new DeleteMessageCommand(userUuid, chatId, messageId);

            var result = await Sender.Send(commandCreateChat, cancellationToken);
            return result.PrepareResponse();
        }

        #endregion

        #region Chat Member Actions

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<Guid>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpPut("{chatId}/member/{userId}")]
        public async Task<IActionResult> AddMember(Guid chatId, Guid userId, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var commandCreateChat = new AddChatMemberCommand(userUuid, chatId, userId);

            var result = await Sender.Send(commandCreateChat, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<Guid>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpDelete("{chatId}/member/{userId}")]
        public async Task<IActionResult> RemoveMember(Guid chatId, Guid userId, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var commandCreateChat = new RemoveChatMemberCommand(userUuid, chatId, userId);

            var result = await Sender.Send(commandCreateChat, cancellationToken);
            return result.PrepareResponse();
        }
        #endregion
        #region Chat Admin Actions

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<Guid>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpPut("{chatId}/admin/{userId}")]
        public async Task<IActionResult> AssignAdmin(Guid chatId, Guid userId, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var commandCreateChat = new AssignChatAdminCommand(userUuid, chatId, userId);

            var result = await Sender.Send(commandCreateChat, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<Guid>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 400)]
        [HttpDelete("{chatId}/admin/{userId}")]
        public async Task<IActionResult> RevokeAdmin(Guid chatId, Guid userId, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var commandCreateChat = new RevokeChatAdminCommand(userUuid, chatId, userId);

            var result = await Sender.Send(commandCreateChat, cancellationToken);
            return result.PrepareResponse();
        }
        #endregion

    }
}