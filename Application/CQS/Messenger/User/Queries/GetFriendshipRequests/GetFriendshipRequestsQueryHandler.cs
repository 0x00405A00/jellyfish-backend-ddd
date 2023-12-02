using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.Messenger.User.Queries.GetFriendshipRequests
{
    internal sealed class GetFriendshipRequestsQueryHandler : IQueryHandler<GetFriendshipRequestsQuery, List<FriendshipRequestDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GetFriendshipRequestsQueryHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<FriendshipRequestDTO>>> Handle(GetFriendshipRequestsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
