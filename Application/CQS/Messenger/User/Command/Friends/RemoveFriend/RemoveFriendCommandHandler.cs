using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Errors;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.Messenger.User.Command.Friends.RemoveFriend
{
    internal sealed class RemoveFriendCommandHandler : ICommandHandler<RemoveFriendCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public RemoveFriendCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetAsync(x => x.Id == request.ExecuteUserId.ToIdentification<UserId>());
            var targetUserThatShouldRemoved = await _userRepository.GetAsync(x => x.Id == request.TargetUserId.ToIdentification<UserId>());
            if(targetUserThatShouldRemoved is null)
            {
                return Result<Guid>.Failure("target user mot found", Error.ERROR_CODE.NotFound);
            }
            if (!user.IsFriend(targetUserThatShouldRemoved))
            {
                return Result<Guid>.Failure("target user is not your friend", Error.ERROR_CODE.NotFound);
            }

            try
            {
                user.RemoveFriend(user, targetUserThatShouldRemoved);
                _userRepository.Update(user);  
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure("cant remove friend", Error.ERROR_CODE.Exception);
            }
            _userRepository.PublishDomainEvents(user, mediator);
            return Result<Guid>.Success(request.TargetUserId);
        }
    }
}
