using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.User.Queries.GetUsers
{
    internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<MessengerUserDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GetUsersQueryHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<MessengerUserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var data = await _userRepository.ListAsync(x => x.ActivationDatetime != null);

            var res = Result<List<MessengerUserDTO>>.Success();

            var t = _mapper.Map<List<MessengerUserDTO>>(data);
            res.Value = t;
            return res;
        }
    }
}
