using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Org.BouncyCastle.Asn1.X509;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.RemoveFriendshipRequest
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class RemoveFriendshipRequestCommandHandler : ICommandHandler<RemoveFriendshipRequestCommand, Guid>
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

        public async Task<Result<Guid>> Handle(RemoveFriendshipRequestCommand request, CancellationToken cancellationToken)
        {

            var executorUser = await _userRepository.GetAsync(x => x.Uuid == request.ExecutorUserId);
            var requestUser = await _userRepository.GetAsync(x => x.Uuid == request.RequestUserId);

            bool isDecline = (executorUser != requestUser);

            try
            {
                var friendshipRequest = requestUser.FriendshipRequests.Where(x => x.TargetUser.Uuid.ToGuid() == request.TargetUserId).Single();
                if(friendshipRequest is null)
                {
                    return Result<Guid>.Failure("there is no matching friend ship request", Domain.Error.Error.ERROR_CODE.Exception);
                }

                requestUser.RemoveFriendshipRequest(friendshipRequest);
                _userRepository.UpdateAsync(requestUser);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure("cant remove friendship request", Domain.Error.Error.ERROR_CODE.Exception);
            }
            _userRepository.PublishDomainEvents(requestUser, mediator);
            return Result<Guid>.Success(request.TargetUserId);
        }
    }
}
