﻿using Application.Abstractions.Messaging;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.User.Commands.DeleteUser
{
    internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Guid>
    {
        private readonly ISender sender;
        private readonly IMediator mediator;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(
            ISender sender,
            IMediator mediator, 
            IUserRepository userRepository)
        {
            this.sender = sender;
            this.mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(user => user.Id == request.UserId.ToIdentification<UserId>() && user.DeletedTime==null);
            if (user == null)
                return Result<Guid>.Failure("target user not found");
            var deletetByUser = await _userRepository.GetAsync(user => user.Id == request.DeletedByUserId.ToIdentification<UserId>());
            if (deletetByUser == null)
                return Result<Guid>.Failure("execution user not found");
            try
            {
                user.Remove(deletetByUser.Id);
            }
            catch(Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
            try
            {
                _userRepository.Update(user);
            }
            catch(Exception ex) {
                return Result<Guid>.Failure(ex.Message);
            }

            return Result<Guid>.Success(user.Id.ToGuid());
        }
    }
}
