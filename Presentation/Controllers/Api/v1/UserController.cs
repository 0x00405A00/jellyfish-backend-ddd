using Application.CQS.Messenger.User.Command.Friends.RemoveFriend;
using Application.CQS.Messenger.User.Command.FriendshipRequests.AcceptFriendshipRequest;
using Application.CQS.Messenger.User.Command.FriendshipRequests.CreateFriendshipRequest;
using Application.CQS.Messenger.User.Command.FriendshipRequests.RemoveFriendshipRequest;
using Application.CQS.User.Commands.CreateUser;
using Application.CQS.User.Commands.DeleteUser;
using Application.CQS.User.Commands.PasswordReset.Request;
using Application.CQS.User.Commands.PasswordReset.Reset;
using Application.CQS.User.Commands.RegisterUser.Activation;
using Application.CQS.User.Commands.RegisterUser.Register;
using Application.CQS.User.Commands.UpdateUser;
using Application.CQS.User.Queries.GetUserById;
using Application.DataTransferObject;
using Application.DataTransferObject.Messenger;
using Domain.Const;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using System.Net.Mime;
using WebApi.Abstractions;

namespace Presentation.Controllers.Api.v1
{
    [Authorize(Policy = AuthorizationConst.Policy.AdminPolicy)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ApiController
    {

        private readonly ILogger<UserController> _logger;

        public UserController(ISender sender, IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger) : base(sender, httpContextAccessor)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO registerUserDTO, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(registerUserDTO.UserName, registerUserDTO.Password, registerUserDTO.FirstName, registerUserDTO.LastName, registerUserDTO.Email, registerUserDTO.Phone, (DateTime)registerUserDTO.DateOfBirth);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("password/reset/request")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PasswordResetRequest([FromBody] PasswordResetRequestDTO passwordResetRequestDTO, CancellationToken cancellationToken)
        {
            var command = new UserPasswordResetRequestCommand(passwordResetRequestDTO.Email);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("password/reset/{base64Token}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> PasswordReset(string base64Token,[FromBody] PasswordResetDataTransferModel passwordResetDataTransferModel, CancellationToken cancellationToken)
        {
            UserPasswordResetCommand command= new UserPasswordResetCommand(passwordResetDataTransferModel.Password,passwordResetDataTransferModel.PasswordRepeat, passwordResetDataTransferModel.PasswordResetCode, base64Token);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("activation/{base64Token}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ActivateUser(string base64Token, [FromBody] UserActivationDataTransferModel userActivationDataTransferModel, CancellationToken cancellationToken)
        {
            var command = new UserActivationCommand(base64Token,userActivationDataTransferModel.ActivationCode);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new CreateUserCommand(userUuid,userDto.UserName, userDto.Password, userDto.FirstName, userDto.LastName, userDto.Email, userDto.Phone, (DateTime)userDto.DateOfBirth);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

        [HttpGet("{guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUser(Guid guid, CancellationToken cancellationToken)
        {
            var command = new Application.CQS.User.Queries.GetUserById.GetUserByIdQuery(guid);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

        [HttpGet()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var command = new Application.CQS.User.Queries.GetUsers.GetUsersQuery();

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet("messenger/{guid}")]
        public async Task<IActionResult> GetChatUser(Guid guid, CancellationToken cancellationToken)
        {
            var command = new Application.CQS.Messenger.User.Queries.GetUserById.GetUserByIdQuery(guid);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

        [HttpPut("{userId}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserDTO userDto, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new UpdateUserCommand(userId,
                                                userDto.UserName,
                                                userDto.Password,
                                                userDto.PasswordConfirm,
                                                userDto.FirstName,
                                                userDto.LastName,
                                                userDto.Email,
                                                userDto.Phone,
                                                userDto.DateOfBirth);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }
        [HttpDelete("{userId}")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new DeleteUserCommand(userUuid, userId);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPost("friend/request")]
        public async Task<IActionResult> CreateFriendshipRequest([FromBody] UserFriendshipRequestDTO userFriendshipRequestDTO, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new CreateFriendshipRequestCommand(userUuid, userFriendshipRequestDTO.UserUuid);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpDelete("friend/request")]
        public async Task<IActionResult> RemoveFriendshipRequest([FromBody] UserFriendshipRequestDTO userFriendshipRequestDTO, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new RemoveFriendshipRequestCommand(userUuid, userFriendshipRequestDTO.UserUuid);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet("friend/request")]
        public async Task<IActionResult> GetFriendshipRequests(CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new GetUserByIdQuery(userUuid);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value.FriendshipRequests) : BadRequest(result);
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPost("friend/request/accept")]
        public async Task<IActionResult> AcceptFriendshipRequest([FromBody] UserFriendshipRequestDTO userFriendshipRequestDTO, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new AcceptFriendshipRequestCommand(userUuid, userFriendshipRequestDTO.UserUuid);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet("friend")]
        public async Task<IActionResult> GetFriends(CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new GetUserByIdQuery(userUuid);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value.Friends) : BadRequest(result);
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpDelete("friend")]
        public async Task<IActionResult> RemoveFriend([FromBody] UserDTO friend, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new RemoveFriendCommand(userUuid, friend.Uuid);

            var result = await Sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result);
        }

    }
}