using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Messages;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.AcknowledgeMessageDeliveryByChat
{
    internal sealed class AcknowledgeMessageDeliveryByChatCommandHandler : ICommandHandler<AcknowledgeMessageDeliveryByChatCommand, MessageDTO>
    {
        private readonly IMapper mapper;
        private readonly IMessageOutboxRepository messageOutboxRepository;

        public AcknowledgeMessageDeliveryByChatCommandHandler(IMapper mapper, IMessageOutboxRepository messageOutboxRepository)
        {
            this.mapper = mapper;
            this.messageOutboxRepository = messageOutboxRepository;
        }
        public async Task<Result<MessageDTO>> Handle(AcknowledgeMessageDeliveryByChatCommand request, CancellationToken cancellationToken)
        {
            var requestUserId = request.UserId.ToIdentification<UserId>();
            var messageOutboxId = request.MessageId.ToIdentification<MessageId>();

            var messageOutbox = await messageOutboxRepository.GetAsync(x=>x.UserForeignKey == requestUserId && x.MessageForeignKey == messageOutboxId);
            if(messageOutbox == null)
            {
                //messageoutbox is not existing for requesting user
                return Result<MessageDTO>.Failure("no message to acknowledge");
            }
            
            messageOutboxRepository.Remove(messageOutbox);

            var result = mapper.Map<MessageDTO>(messageOutbox.Message);
            return Result<MessageDTO>.Success(result);
        }
    }
}
