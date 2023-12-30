using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Chats;
using Domain.Entities.Chats.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.AddChatMember
{
    internal sealed class AddChatMemberCommandHandler : ICommandHandler<AddChatMemberCommand, bool>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IMapper _mapper;
        private readonly IMediator mediator;

        public AddChatMemberCommandHandler(
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

        public async Task<Result<bool>> Handle(AddChatMemberCommand request, CancellationToken cancellationToken)
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

                var item = ChatRelationToUser.Create(
                    ChatRelationToUser.NewId(),
                    targetUser.Id,
                    chat.Id,
                    false,
                    DateTime.Now.ToTypedDateTime(),
                    executorUser.Id,
                    null,
                    null,
                    null,
                    null);

                chat.ChatRelationToUsers.Add(item);
                 
                chat.AddMember(executorUser, targetUser);
            }
            catch (Exception ex) when (ex is ArgumentNullException) 
            {
                return Result<bool>.Failure($"value is null ({ex.Message})");
            }
            catch (Exception ex) when (ex is UserIsNoAdminInChatException)
            {
                return Result<bool>.Failure(ex.Message);
            }
            catch (Exception ex) when (ex is UserIsNoMemberInChatException)
            {
                return Result<bool>.Failure(ex.Message);
            }
            catch (Exception ex) when (ex is UserAlreadyMemberInChatException)
            {
                return Result<bool>.Failure(ex.Message);
            }
            catch (Exception ex)  
            {
                return Result<bool>.Failure(ex.Message);
            }
            _chatRepository.Update(chat);
            
            return Result<bool>.Success(true);

        }

    }
}
