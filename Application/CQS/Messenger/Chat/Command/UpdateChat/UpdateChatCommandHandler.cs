using Application.Abstractions.Messaging;
using Domain.Entities.Chats.Exception;
using Domain.Exceptions;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.Messenger.Chat.Command.UpdateChat
{
    internal sealed class UpdateChatCommandHandler : ICommandHandler<UpdateChatCommand, Guid>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateChatCommandHandler(
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            var updatedByUser = await _userRepository.GetAsync(x=>x.Uuid == request.UpdatedByUserId);
            if (updatedByUser == null)
            {
                return Result<Guid>.Failure("execution user not found");
            }
            var chat = await _chatRepository.GetAsync(x=>x.Uuid == request.ChatId);
            if(chat == null)
            {
                return Result<Guid>.Failure("chat not found");
            }
            if (!String.IsNullOrEmpty(request.ChatName))
            {
                try
                {
                    chat.UpdateName(updatedByUser, request.ChatName);
                }
                catch (UserIsNoAdminInChatException ex)
                {
                    return Result<Guid>.Failure("execution user has not permissions");
                }
                catch (NotValidChatDescriptionException ex)
                {
                    return Result<Guid>.Failure(ex.Message);
                }
                finally
                {

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

                    return Result<Guid>.Failure(ex.Message);
                }
                catch (NotValidChatDescriptionException ex)
                {

                    return Result<Guid>.Failure(ex.Message);
                }
            }
            if (!String.IsNullOrEmpty(request.Picture))
            {
                Picture picture = null;
                try
                {
                    byte[] data = Convert.FromBase64String(request.Picture);
                    picture = Picture.Parse(data);
                    chat.UpdatePicture(updatedByUser, picture);
                }
                catch(NotValidMediaContentException ex)
                {
                    return Result<Guid>.Failure(ex.Message);
                }
            }

            _chatRepository.Update(chat);
            return Result<Guid>.Success(chat.Uuid.ToGuid());
        }
    }
}
