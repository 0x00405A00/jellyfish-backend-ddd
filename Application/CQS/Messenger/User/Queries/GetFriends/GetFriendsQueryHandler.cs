using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Errors;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.User.Queries.GetFriends
{
    internal sealed class GetFriendsQueryHandler : IQueryHandler<GetFriendsQuery, List<MessengerUserDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GetFriendsQueryHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<MessengerUserDTO>>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x=> x.Id == request.UserId.ToIdentification<UserId>());
            if(!user.HasFriends())
            {
                return Result<List<MessengerUserDTO>>.Failure("you have no friends", Error.ERROR_CODE.NotFound);
            }
            var friends = user.Friends.ToList();

            var dto = _mapper.Map<List<MessengerUserDTO>>(friends);
            return Result<List<MessengerUserDTO>>.Success(dto);
        }
    }
}
