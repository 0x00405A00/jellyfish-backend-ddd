using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.Messenger.User.Command.Friends.RemoveFriend
{
    internal sealed class RemoveFriendCommandHandler : ICommandHandler<RemoveFriendCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public RemoveFriendCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetAsync(x => x.Uuid == request.ExecuteUserId);
            var targetUserThatShouldRemoved = await _userRepository.GetAsync(x => x.Uuid == request.TargetUserId);
            if(targetUserThatShouldRemoved is null)
            {
                return Result<Guid>.Failure("target user mot found", Domain.Error.Error.ERROR_CODE.NotFound);
            }
            if (!user.IsFriend(targetUserThatShouldRemoved))
            {
                return Result<Guid>.Failure("target user is not your friend", Domain.Error.Error.ERROR_CODE.NotFound);
            }

            try
            {
                user.RemoveFriend(user, targetUserThatShouldRemoved);
                _userRepository.UpdateAsync(user);  
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure("cant remove friend", Domain.Error.Error.ERROR_CODE.Exception);
            }
            _userRepository.PublishDomainEvents(user, mediator);
            return Result<Guid>.Success(request.TargetUserId);
        }
    }
}
