using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.User.Queries.GetUserType
{
    internal sealed class GetUserTypeQueryHandler : IQueryHandler<GetUserTypeQuery, List<UserTypeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserTypeRepository _userTypeRepository;
        public GetUserTypeQueryHandler(
            IMapper mapper,
            IUserTypeRepository userTypeRepository)
        {
            _mapper = mapper;
            _userTypeRepository = userTypeRepository;
        }
        public async Task<Result<List<UserTypeDTO>>> Handle(GetUserTypeQuery request, CancellationToken cancellationToken)
        {
            var data = await _userTypeRepository.ListAsync();

            var t = _mapper.Map<List<UserTypeDTO>>(data);
            return Result<List<UserTypeDTO>>.Success(t);
        }
    }
}
