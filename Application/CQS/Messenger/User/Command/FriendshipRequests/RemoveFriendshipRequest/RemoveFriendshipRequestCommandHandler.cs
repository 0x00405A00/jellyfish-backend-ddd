using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Org.BouncyCastle.Asn1.X509;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.RemoveFriendshipRequest
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class RemoveFriendshipRequestCommandHandler : ICommandHandler<RemoveFriendshipRequestCommand, RemoveFriendshipRequestDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public RemoveFriendshipRequestCommandHandler(
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

        public async Task<Result<RemoveFriendshipRequestDTO>> Handle(RemoveFriendshipRequestCommand request, CancellationToken cancellationToken)
        {

            var executorUser = await _userRepository.GetAsync(x => x.Uuid == request.ExecutorUserId);
            var requestUser = await _userRepository.GetAsync(x => x.Uuid == request.RequestUserId);
            var targetUser = await _userRepository.GetAsync(x => x.Uuid == request.TargetUserId);

            bool isPermitted = (executorUser == requestUser || executorUser== targetUser);
            if(!isPermitted)
            {
                return Result<RemoveFriendshipRequestDTO>.Failure("you cant accept these request", Domain.Error.Error.ERROR_CODE.Forbidden);
            }
            try
            {
                var friendshipRequest = requestUser.FriendshipRequests.Where(x => x.TargetUser.Uuid.ToGuid() == request.TargetUserId).Single();
                if(friendshipRequest is null)
                {
                    return Result<RemoveFriendshipRequestDTO>.Failure("there is no matching friend ship request", Domain.Error.Error.ERROR_CODE.Exception);
                }

                requestUser.RemoveFriendshipRequest(friendshipRequest);
                _userRepository.UpdateAsync(requestUser);
            }
            catch (Exception ex)
            {
                return Result<RemoveFriendshipRequestDTO>.Failure("cant remove friendship request", Domain.Error.Error.ERROR_CODE.Exception);
            }
            _userRepository.PublishDomainEvents(requestUser, mediator);

            var dto = new RemoveFriendshipRequestDTO { RequestUserUuid = request.RequestUserId, TargetUserUuid = request.TargetUserId };
            return Result<RemoveFriendshipRequestDTO>.Success(dto);
        }
    }
}
