using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Chats.Exceptions;
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
            var executorUser = await _userRepository.GetAsync(x => x.Id == new Domain.Entities.User.UserId(request.ActorId));
            if (executorUser is null)
            {
                return Result<bool>.Failure($"executor-user not found");
            }
            var targetUser = await _userRepository.GetAsync(x => x.Id == new Domain.Entities.User.UserId(request.UserId));
            if (targetUser is null)
            {
                return Result<bool>.Failure($"target-user not found");
            }

            var chat = await _chatRepository.GetAsync(x => x.Id == new Domain.Entities.Chats.ChatId(request.ChatId));

            if (chat is null)
            {
                return Result<bool>.Failure($"chat not found");
            }
            try
            {

                /*
                 * 1.Test: Added state wird richtig von ChangeTracker erkannt
                 * var item = new ChatRelationToUser
                {
                    
                };
                chat.ChatRelationToUsers.Add(item);

                item.Uuid = Guid.NewGuid();
                item.UserUuid = targetUser.Uuid.ToGuid();
                item.ChatUuid = chat.Uuid;*/

                /*
                 * 2.Test: Added state wird richtig von ChangeTracker erkannt
                var item = new ChatRelationToUser
                {
                    Uuid = Guid.NewGuid(),
                    UserUuid = targetUser.Uuid.ToGuid(),
                    ChatUuid = chat.Uuid,
                    
                };
                chat.ChatRelationToUsers.Add(item);*/
                 /**/
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
            
            _chatRepository.PublishDomainEvents(chat, mediator);

            return Result<bool>.Success(true);

        }

    }
}
