﻿using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Queries.GetChatById
{
    internal sealed class GetChatQueryHandler : IQueryHandler<GetChatByIdQuery, ChatDTO>
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GetChatQueryHandler(
            IMapper mapper,
            IChatRepository chatRepository,
            IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<ChatDTO>> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _chatRepository.GetAsync(x => x.Uuid == request.Id);
            if (data == null)
            {
                throw new UserNotFoundException(request.Id);
            }

            var result = _mapper.Map<ChatDTO>(data);
            return Result<ChatDTO>.Success(result);
        }
    }
}
