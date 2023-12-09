using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Chats.Exception;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.RemoveChatMember
{
    internal sealed class RemoveChatMemberCommandHandler : ICommandHandler<RemoveChatMemberCommand, bool>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator mediator;

        public RemoveChatMemberCommandHandler(
            IMapper mapper,
            IMediator mediator,
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IMediaService mediaService,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            this.mediator = mediator;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            this.mediaService = mediaService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RemoveChatMemberCommand request, CancellationToken cancellationToken)
        {
            var executorUser = await _userRepository.GetAsync(x => x.Uuid == request.ActorId);
            if (executorUser is null)
            {
                return Result<bool>.Failure($"executor-user not found");
            }
            var targetUser = await _userRepository.GetAsync(x => x.Uuid == request.UserId);
            if (targetUser is null)
            {
                return Result<bool>.Failure($"target-user not found");
            }
            var chat = await _chatRepository.GetAsync(x=>x.Uuid == request.ChatId);
            if(chat is null)
            {
                return Result<bool>.Failure($"chat not found");
            }
            try
            {
                chat.RemoveMember(executorUser, targetUser);
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
            _chatRepository.UpdateAsync(chat);
            await _unitOfWork.SaveChangesAsync();
            _chatRepository.PublishDomainEvents(chat, mediator);

            return Result<bool>.Success(true);
        }

    }
}
