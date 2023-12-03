using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User.Exception;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.AcceptFriendshipRequest
{
    internal sealed class AcceptFriendshipRequestCommandHandler : ICommandHandler<AcceptFriendshipRequestCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public AcceptFriendshipRequestCommandHandler(
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

        public async Task<Result<bool>> Handle(AcceptFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            var executor = await _userRepository.GetAsync(x => x.Uuid == request.ExecutorUserId);
            var target = await _userRepository.GetAsync(x => x.Uuid == request.TargetUserId);
            var requester = await _userRepository.GetAsync(x => x.Uuid == request.RequestUserId);

            bool isPermitted = executor == target;

            if (!isPermitted)
            {
                return Result<bool>.Failure("you cant accept these request", Domain.Error.Error.ERROR_CODE.Forbidden);
            }

            var receivedRequest = target.GetReceivedFriendshipRequests();
            try
            {
                var friendshipRequest = receivedRequest.Where(x => x.RequestUser == requester).Single();
                target.AcceptFriendshipRequest(friendshipRequest);
                _userRepository.UpdateAsync(target);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("cant accept request", Domain.Error.Error.ERROR_CODE.Exception);
            }
            _userRepository.PublishDomainEvents(target,mediator);
            return Result<bool>.Success(true);
        }
    }
}
