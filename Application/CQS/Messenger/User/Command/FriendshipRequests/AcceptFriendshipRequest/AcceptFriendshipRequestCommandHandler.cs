using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.AcceptFriendshipRequest
{
    internal sealed class AcceptFriendshipRequestCommandHandler : ICommandHandler<AcceptFriendshipRequestCommand, bool>
    {
        private readonly IMailoutboxRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AcceptFriendshipRequestCommandHandler(
            IMapper mapper,
            IMailoutboxRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<bool>> Handle(AcceptFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
