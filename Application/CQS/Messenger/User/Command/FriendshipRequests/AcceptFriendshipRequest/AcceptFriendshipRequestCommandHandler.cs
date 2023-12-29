using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Errors;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.AcceptFriendshipRequest
{
    internal sealed class AcceptFriendshipRequestCommandHandler : ICommandHandler<AcceptFriendshipRequestCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public AcceptFriendshipRequestCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(AcceptFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            var executor = await _userRepository.GetAsync(x => x.Id.Id == request.ExecutorUserId);
            if (executor is null)
            {
                return Result<bool>.Failure("executor not found", Error.ERROR_CODE.NotFound);
            }
            var target = await _userRepository.GetAsync(x => x.Id.Id == request.TargetUserId);
            if (target is null)
            {
                return Result<bool>.Failure("target-user not found", Error.ERROR_CODE.NotFound);
            }
            var requester = await _userRepository.GetAsync(x => x.Id.Id == request.RequestUserId);
            if (requester is null)
            {
                return Result<bool>.Failure("request-user not found", Error.ERROR_CODE.NotFound);
            }

            bool isPermitted = executor == target;

            if (!isPermitted)
            {
                return Result<bool>.Failure("you cant accept these request", Error.ERROR_CODE.Forbidden);
            }

            var receivedRequest = target.GetReceivedFriendshipRequests();
            if(receivedRequest is null)
            {
                return Result<bool>.Failure("you have not open friendship requests", Error.ERROR_CODE.NotFound);
            }
            if(!receivedRequest.Any(x=>x.RequestUser == requester))
            {

                return Result<bool>.Failure($"you have not open friendship from {requester.UserName}", Error.ERROR_CODE.NotFound);
            }
            try
            {
                var friendshipRequest = receivedRequest.Where(x => x.RequestUser == requester).Single();
                target.AcceptFriendshipRequest(friendshipRequest);
                _userRepository.Update(target);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("cant accept request", Error.ERROR_CODE.Exception);
            }
            _userRepository.PublishDomainEvents(target,mediator);
            return Result<bool>.Success(true);
        }
    }
}
