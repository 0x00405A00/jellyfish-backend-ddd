using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Queries.GetNotDeliveredMessagesByChat
{
    internal sealed class GetNotDeliveredMessagesByChatQueryHandler(IMapper mapper, IMessageOutboxRepository messageOutboxRepository) : IQueryHandler<GetNotDeliveredMessagesByChatQuery, ICollection<MessageDTO>>
    {
        public async Task<Result<ICollection<MessageDTO>>> Handle(GetNotDeliveredMessagesByChatQuery request, CancellationToken cancellationToken)
        {
            var notDeliveredMessages = await messageOutboxRepository.ListAsync(x=>x.UserForeignKey == request.UserId.ToIdentification<UserId>());
            if(!notDeliveredMessages.Any())
            {
                //no messages available to poll
                return Result<ICollection<MessageDTO>>.Failure("no messages in queue");
            }

            var messagesExtracted = notDeliveredMessages.Select(x=>x.Message).ToList();

            var response = mapper.Map < List<MessageDTO>> (messagesExtracted);
            return Result<ICollection<MessageDTO>>.Success(response);
        }
    }
}
