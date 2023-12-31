using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Messages.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.DeleteMessage
{
    internal sealed class DeleteMessageCommandHandler : ICommandHandler<DeleteMessageCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        public DeleteMessageCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository,
            IMediaService mediaService,
            IMessageRepository messageRepository)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _userRepository = userRepository;
            this.mediaService = mediaService;
            _messageRepository = messageRepository;
        }

        public async Task<Result<Guid>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var deleteByUser = await _userRepository.GetAsync(x => x.Id == request.DeletedById.ToIdentification<UserId>());
            if(deleteByUser is null)
            {
                return Result<Guid>.Failure("operational user not found");
            }
            var message = await _messageRepository.GetAsync(x=>x.Id == request.MessageId.ToIdentification<MessageId>() && x.Chat.Id == request.ChatId.ToIdentification<ChatId>());
            if (message is null)
            {
                return Result<Guid>.Failure("message not found");
            }
            try
            {
                message.Delete(deleteByUser.Id);
            }
            catch (NotMessageOwnerException ex)
            {
                return Result<Guid>.Failure("you are not the owner of the message");
            }
            _messageRepository.Update(message);    

            return Result<Guid>.Success(request.MessageId);
        }

    }
}
