using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Chats.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.AssignChatAdmin
{
    internal sealed class AssignChatAdminCommandHandler : ICommandHandler<AssignChatAdminCommand, bool>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IMapper _mapper;
        private readonly IMediator mediator;

        public AssignChatAdminCommandHandler(
            IMapper mapper,
            IMediator mediator, 
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IMediaService mediaService)
        {
            _mapper = mapper;
            this.mediator = mediator;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            this.mediaService = mediaService;
        }


        public async Task<Result<bool>> Handle(AssignChatAdminCommand request, CancellationToken cancellationToken)
        {
            var executorUser = await _userRepository.GetAsync(x => x.Id == request.ActorId.ToIdentification<UserId>());
            if (executorUser is null)
            {
                return Result<bool>.Failure($"executor-user not found");
            }
            var targetUser = await _userRepository.GetAsync(x => x.Id == request.UserId.ToIdentification<UserId>());
            if (targetUser is null)
            {
                return Result<bool>.Failure($"target-user not found");
            }
            var chat = await _chatRepository.GetAsync(x => x.Id == request.ChatId.ToIdentification<ChatId>());
            if (chat is null)
            {
                return Result<bool>.Failure($"chat not found");
            }
            try
            {
                chat.AssignAdmin(executorUser, targetUser);
            }
            catch (ArgumentNullException ex)
            {
                return Result<bool>.Failure($"value is null ({ex.Message})");
            }
            catch (UserIsNoAdminInChatException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            catch (UserIsNoMemberInChatException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            catch (UserAlreadyAdminInChatException ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            _chatRepository.Update(chat);
            _chatRepository.PublishDomainEvents(chat, mediator);

            return Result<bool>.Success(true);
        }

    }
}
