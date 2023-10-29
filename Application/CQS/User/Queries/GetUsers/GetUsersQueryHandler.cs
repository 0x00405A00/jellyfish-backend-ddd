using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.User.Queries.GetUsers
{
    internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<UserDTO>>
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
        public async Task<Result<List<UserDTO>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var data = await _userRepository.ListAsync(x => x.ActivationDatetime != null);

            var res = Result<List<UserDTO>>.Success();

            var t = _mapper.Map<List<UserDTO>>(data);
            res.Value = t;
            return res;
        }
    }
}
