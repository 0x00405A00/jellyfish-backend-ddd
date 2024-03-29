﻿using Application.Abstractions.Messaging;
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
            var data = await _chatRepository.GetAsync(x => x.Id == request.Id.ToIdentification<ChatId>());
            if (data == null)
            {
                return Result<ChatDTO>.Failure("chat not found");
            }

            var result = _mapper.Map<ChatDTO>(data);
            return Result<ChatDTO>.Success(result);
        }
    }
}
