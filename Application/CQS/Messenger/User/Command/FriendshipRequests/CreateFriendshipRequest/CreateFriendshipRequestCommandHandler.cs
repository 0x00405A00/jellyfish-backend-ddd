using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.Messenger.User.Command.FriendshipRequests.CreateFriendshipRequest
{
    internal sealed class CreateFriendshipRequestCommandHandler : ICommandHandler<CreateFriendshipRequestCommand, FriendshipRequestDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateFriendshipRequestCommandHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<FriendshipRequestDTO>> Handle(CreateFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
