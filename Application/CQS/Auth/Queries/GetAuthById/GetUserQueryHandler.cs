﻿using Application.Abstractions.Messaging;
using Application.DataTransferObject;
using AutoMapper;
using Domain.Entities.User.Exception;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

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
        public Task<Result<AuthDTO>> Handle(GetAuthByIdQuery request, CancellationToken cancellationToken)
        {
            var data = _authRepository.GetAsync(x => x.Uuid == request.Id);
            if (data == null)
            {
                throw new UserNotFoundException(request.Id);
            }
            var res = Result<AuthDTO>.Success();

            res.Value = _mapper.Map<AuthDTO>(data);
            return Task.FromResult(res);
        }
    }
}
