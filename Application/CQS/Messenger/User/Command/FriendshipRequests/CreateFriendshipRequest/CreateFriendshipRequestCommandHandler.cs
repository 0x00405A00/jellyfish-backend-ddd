using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Users;
using Domain.Errors;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObject;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.CreateFriendshipRequest
{
    internal sealed class CreateFriendshipRequestCommandHandler : ICommandHandler<CreateFriendshipRequestCommand, FriendshipRequestDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public CreateFriendshipRequestCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<FriendshipRequestDTO>> Handle(CreateFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.Id == request.RequesterUserId.ToIdentification<UserId>());
            if (user is null)
            {
                return Result<FriendshipRequestDTO>.Failure("request-user doesnt exists", Error.ERROR_CODE.NotFound);
            }
            var targetUser = await _userRepository.GetAsync(x => x.Id == request.PossibleNewFriendId.ToIdentification<UserId>());
            if(targetUser is null)
            {
                return Result<FriendshipRequestDTO>.Failure("target-user doesnt exists", Error.ERROR_CODE.NotFound);
            }

            var previousRequests = user.GetRequestedFriendshipRequests();
            if (previousRequests.Any(x=>x.TargetUser == targetUser))
            {
                return Result<FriendshipRequestDTO>.Failure("you sent the targetuser already a request", Error.ERROR_CODE.BadRequest);
            }
            FriendshipRequest friendshipRequest = null;
            try
            {
                friendshipRequest = FriendshipRequest.Create(
                    FriendshipRequest.NewId(),
                    request.Message,
                    user.Id,
                    targetUser.Id,
                    DateTime.Now.ToTypedDateTime(),
                    null,
                    null);

                user.AddFriendshipRequest(friendshipRequest);
                _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                return Result<FriendshipRequestDTO>.Failure("cant remove friendship request", Error.ERROR_CODE.Exception);
            }

            var dto = _mapper.Map<FriendshipRequestDTO>(friendshipRequest);
            return Result<FriendshipRequestDTO>.Success(dto);

        }
    }
}
