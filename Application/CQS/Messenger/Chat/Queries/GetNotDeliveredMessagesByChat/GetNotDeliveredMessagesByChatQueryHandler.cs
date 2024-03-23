using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Queries.GetNotDeliveredMessagesByChat
{
    internal sealed class GetNotDeliveredMessagesByChatQueryHandler(IMapper mapper, IChatRepository chatRepository, IMessageOutboxRepository messageOutboxRepository) : IQueryHandler<GetNotDeliveredMessagesByChatQuery, ICollection<MessageDTO>>
    {
        public async Task<Result<ICollection<MessageDTO>>> Handle(GetNotDeliveredMessagesByChatQuery request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetAsync(x=>x.Id == request.ChatId.ToIdentification<ChatId>());
            if(chat is null)
            {
                //chat doesnt exists
                return Result<ICollection<MessageDTO>>.Failure("chat doesnt exists");
            }
            var requestUserId = request.UserId.ToIdentification<UserId>();
            if(!chat.IsChatMember(requestUserId))
            {
                //user is not a chatmember
                return Result<ICollection<MessageDTO>>.Failure("you are not member in this chat");
            }

            var notDeliveredMessages = await messageOutboxRepository.ListAsync(x=>x.Message.ChatForeignKey == chat.Id && x.UserForeignKey == requestUserId);
            var messagesExtracted = notDeliveredMessages.Select(x=>x.Message).ToList();

            var response = mapper.Map < List<MessageDTO>> (messagesExtracted);
            return Result<ICollection<MessageDTO>>.Success(response);
        }
    }
}
