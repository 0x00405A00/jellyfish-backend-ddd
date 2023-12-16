using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User;
using Domain.Extension;
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
            var user = await _userRepository.GetAsync(x => x.Id.ToGuid() == request.RequesterUserId);
            if (user is null)
            {
                return Result<FriendshipRequestDTO>.Failure("request-user doesnt exists", Domain.Error.Error.ERROR_CODE.NotFound);
            }
            var targetUser = await _userRepository.GetAsync(x => x.Id.ToGuid() == request.PossibleNewFriendId);
            if(targetUser is null)
            {
                return Result<FriendshipRequestDTO>.Failure("target-user doesnt exists", Domain.Error.Error.ERROR_CODE.NotFound);
            }

            var previousRequests = user.GetRequestedFriendshipRequests();
            if (previousRequests.Any(x=>x.TargetUser == targetUser))
            {
                return Result<FriendshipRequestDTO>.Failure("you sent the targetuser already a request", Domain.Error.Error.ERROR_CODE.BadRequest);
            }
            FriendshipRequest friendshipRequest = null;
            try
            {
                friendshipRequest = FriendshipRequest.Create(FriendshipRequest.NewId(),request.Message, user, targetUser);
                user.AddFriendshipRequest(friendshipRequest);
                _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                return Result<FriendshipRequestDTO>.Failure("cant remove friendship request", Domain.Error.Error.ERROR_CODE.Exception);
            }

            _userRepository.PublishDomainEvents(user, mediator);
            var dto = _mapper.Map<FriendshipRequestDTO>(friendshipRequest);
            return Result<FriendshipRequestDTO>.Success(dto);

        }
    }
}
