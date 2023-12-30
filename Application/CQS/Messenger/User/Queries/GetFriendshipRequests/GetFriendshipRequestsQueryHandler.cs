using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Errors;
using Domain.Extension;
using Domain.Primitives.Ids;
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
            var user = await _userRepository.GetAsync(x => x.Id == request.UserId.ToIdentification<UserId>());
            if (!user.HasFriendshipRequests())
            {
                return Result<List<FriendshipRequestDTO>>.Failure("you have no open friendship requests", Error.ERROR_CODE.NotFound);
            }
            var friends = user.FriendshipRequests.ToList();

            var dto = _mapper.Map<List<FriendshipRequestDTO>>(friends);
            return Result<List<FriendshipRequestDTO>>.Success(dto);
        }
    }
}
