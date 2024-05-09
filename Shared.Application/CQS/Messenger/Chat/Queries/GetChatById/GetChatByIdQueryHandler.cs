using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Queries.GetChatById
{
    internal sealed class GetChatByIdQueryHandler : IQueryHandler<GetChatByIdQuery, ChatDTO>
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        public GetChatByIdQueryHandler(
            IMapper mapper,
            IChatRepository chatRepository)
        {
            this._mapper = mapper;
            _chatRepository = chatRepository;
        }
        public async Task<Result<ChatDTO>> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.GetAsync(x => x.Id == request.Id.ToIdentification<ChatId>());
            if (chat == null)
            {
                return Result<ChatDTO>.Failure("chat not found");
            }
            var userId = request.RequestUserId.ToIdentification<UserId>();
            if (!chat.Members.Where(x=>x.UserForeignKey == userId).Any())
            {
                return Result<ChatDTO>.Failure("not a chat member");
            }

            var result = _mapper.Map<ChatDTO>(chat);
            return Result<ChatDTO>.Success(result);
        }
    }
}
