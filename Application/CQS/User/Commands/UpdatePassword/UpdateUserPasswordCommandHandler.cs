﻿using Application.Abstractions.Messaging;
using Domain.Entities.User.Exception;
using Domain.Extension;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.User.Commands.UpdatePassword
{
    internal sealed class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand, Guid>
    {
        private readonly IMediator mediator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserPasswordCommandHandler(
            IMediator mediator,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                return Result<Guid>.Failure("user not found");
            }
            var user = await _userRepository.GetAsync(user => user.Uuid == request.UserId);
            if (user == null)
            {
                return Result<Guid>.Failure("user not found");
            }

            try
            {
                user.UpdatePassword(user, request.Password, request.PasswordConfirm);
            }
            catch (InvalidPasswordException ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }

            _userRepository.Update(user);
            _userRepository.PublishDomainEvents(user, mediator);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(user.Uuid.ToGuid());
        }
    }
}
