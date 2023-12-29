using Application.Abstractions.Messaging;
using Domain.Entities.Chats.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.Messenger.Chat.Command.DeleteChat
{
    internal sealed class DeleteChatCommandHandler : ICommandHandler<DeleteChatCommand, Guid>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        public DeleteChatCommandHandler(
            IChatRepository chatRepository,
            IUserRepository userRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
        }


        public async Task<Result<Guid>> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x=>x.Id.Id == request.DeletedByUserId);
            if(user == null)
            {
                return Result<Guid>.Failure("execution user not found");
            }
            var chat = await _chatRepository.GetAsync(x=>x.Id.Id == request.ChatId);
            if(chat == null) 
            {
                return Result<Guid>.Failure("chat not found");
            }
            try
            {

                chat.Remove(user);
            }
            catch(UserIsNoAdminInChatException ex)
            {
                return Result<Guid>.Failure("execution user has no permissions");
            }
            _chatRepository.Remove(chat);
            return Result<Guid>.Success(request.ChatId);
        }
    }
}
