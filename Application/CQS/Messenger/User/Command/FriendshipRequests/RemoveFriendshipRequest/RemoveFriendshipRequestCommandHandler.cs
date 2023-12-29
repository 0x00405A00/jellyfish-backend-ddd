using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Errors;
using Domain.Extension;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.RemoveFriendshipRequest
{
    /// <summary>
    /// Removes friendship request, executor must be targetuser or requestuser, both can decline/or revoke a friendship request
    /// </summary>
    internal sealed class RemoveFriendshipRequestCommandHandler : ICommandHandler<RemoveFriendshipRequestCommand, RemoveFriendshipRequestDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public RemoveFriendshipRequestCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<RemoveFriendshipRequestDTO>> Handle(RemoveFriendshipRequestCommand request, CancellationToken cancellationToken)
        {

            var executor = await _userRepository.GetAsync(x => x.Id.ToGuid() == request.ExecutorUserId);
            if (executor is null)
            {
                return Result<RemoveFriendshipRequestDTO>.Failure("executor not found", Error.ERROR_CODE.NotFound);
            }
            var target = await _userRepository.GetAsync(x => x.Id.ToGuid() == request.TargetUserId);
            if (target is null)
            {
                return Result<RemoveFriendshipRequestDTO>.Failure("target-user not found", Error.ERROR_CODE.NotFound);
            }
            var requester = await _userRepository.GetAsync(x => x.Id.ToGuid() == request.RequestUserId);
            if (requester is null)
            {
                return Result<RemoveFriendshipRequestDTO>.Failure("request-user not found", Error.ERROR_CODE.NotFound);
            }

            bool isPermitted = (executor == requester || executor == target);
            if(!isPermitted)
            {
                return Result<RemoveFriendshipRequestDTO>.Failure("you cant accept these request", Error.ERROR_CODE.Forbidden);
            }
            try
            {
                var friendshipRequest = executor.ReceivedFriendshipRequests.Where(x => x.TargetUser.Id.ToGuid() == request.TargetUserId&& x.RequestUser.Id.ToGuid() == request.RequestUserId).Single();
                if(friendshipRequest is null)
                {
                    return Result<RemoveFriendshipRequestDTO>.Failure("there is no matching friend ship request", Error.ERROR_CODE.Exception);
                }

                executor.RemoveFriendshipRequest(friendshipRequest);
                _userRepository.Update(requester);
            }
            catch (Exception ex)
            {
                return Result<RemoveFriendshipRequestDTO>.Failure("cant remove friendship request", Error.ERROR_CODE.Exception);
            }
            _userRepository.PublishDomainEvents(requester, mediator);

            var dto = new RemoveFriendshipRequestDTO { RequestUserUuid = request.RequestUserId, TargetUserUuid = request.TargetUserId };
            return Result<RemoveFriendshipRequestDTO>.Success(dto);
        }
    }
}
