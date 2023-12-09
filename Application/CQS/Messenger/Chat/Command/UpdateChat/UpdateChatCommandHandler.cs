using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Chats.Exception;
using Domain.Exceptions;
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
        private readonly IUnitOfWork _unitOfWork;
        public UpdateChatCommandHandler(
            IMapper mapper,
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            this.mapper = mapper;
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ChatDTO>> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var updatedByUser = await _userRepository.GetAsync(x=>x.Uuid == request.UpdatedByUserId);
            if (updatedByUser == null)
            {
                return Result<ChatDTO>.Failure("execution user not found");
            }
            var chat = await _chatRepository.GetAsync(x=>x.Uuid == request.ChatId);
            if(chat == null)
            {
                return Result<ChatDTO>.Failure("chat not found");
            }
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

            _chatRepository.UpdateAsync(chat);
            var dto = mapper.Map<ChatDTO>(chat);
            return Result<ChatDTO>.Success(dto);
        }
    }
}
