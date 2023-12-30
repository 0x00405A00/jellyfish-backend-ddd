using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Chats;
using Domain.Entities.Chats.Exceptions;
using Domain.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.UpdateChat
{
    internal sealed class UpdateChatCommandHandler : ICommandHandler<UpdateChatCommand, ChatDTO>
    {
        private readonly IMapper mapper;
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        public UpdateChatCommandHandler(
            IMapper mapper,
            IChatRepository chatRepository,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this.mapper = mapper;
            _chatRepository = chatRepository;
        }

        public async Task<Result<ChatDTO>> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.GetAsync(x=>x.Id == request.ChatId.ToIdentification<ChatId>());
            if(chat == null)
            {
                return Result<ChatDTO>.Failure("chat not found");
            }
            ChatMember updatedByMember = null;
            try
            {

                updatedByMember = chat.GetChatMemberById(new UserId(request.UpdatedByUserId));
            }
            catch(Exception ex)
            {
                return Result<ChatDTO>.Failure("you are not a member in this chat");
            }
            var updatedByUser = updatedByMember.User;
            if (!String.IsNullOrEmpty(request.ChatName))
            {
                try
                {
                    chat.UpdateName(updatedByUser, request.ChatName);
                }
                catch (UserIsNoAdminInChatException ex)
                {
                    return Result<ChatDTO>.Failure("execution user has not permissions");
                }
                catch (NotValidChatDescriptionException ex)
                {
                    return Result<ChatDTO>.Failure(ex.Message);
                }
            }
            if (!String.IsNullOrEmpty(request.ChatDescription))
            {
                try
                {
                    chat.UpdateDescription(updatedByUser, request.ChatDescription);
                }
                catch (UserIsNoAdminInChatException ex)
                {
                    return Result<ChatDTO>.Failure(ex.Message);
                }
                catch (NotValidChatDescriptionException ex)
                {
                    return Result<ChatDTO>.Failure(ex.Message);
                }
            }
            if (!String.IsNullOrEmpty(request.Picture))
            {
                Picture picture = null;
                try
                {
                    byte[] data = Convert.FromBase64String(request.Picture);
                    picture = Picture.Parse(data,request.Picture,request.PictureMimeType);
                    chat.UpdatePicture(updatedByUser, picture);
                }
                catch(NotValidMediaContentException ex)
                {
                    return Result<ChatDTO>.Failure(ex.Message);
                }
            }

            _chatRepository.Update(chat);
            var dto = mapper.Map<ChatDTO>(chat);
            return Result<ChatDTO>.Success(dto);
        }
    }
}
