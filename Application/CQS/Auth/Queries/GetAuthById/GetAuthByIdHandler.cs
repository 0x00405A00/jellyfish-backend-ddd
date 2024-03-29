﻿using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Users.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.Auth.Queries.GetAuthById
{
    internal sealed class GetAuthByIdHandler : IQueryHandler<GetAuthByIdQuery, AuthDTO>
    {
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GetAuthByIdHandler(
            IMapper mapper,
            IAuthRepository authRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<AuthDTO>> Handle(GetAuthByIdQuery request, CancellationToken cancellationToken)
        {
            var data = _authRepository.GetAsync(x => x.Id == request.Id.ToIdentification<AuthId>());
            if (data == null)
            {
                throw new UserNotFoundException(request.Id);
            }
            var result = _mapper.Map<AuthDTO>(data);
            return Result<AuthDTO>.Success(result);
        }
    }
}
