using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.Role.Queries.GetRoles
{
    internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, List<RoleDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        public GetRolesQueryHandler(
            IMapper mapper,
            IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }
        public async Task<Result<List<RoleDTO>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.ListAsync(x=>x.DeletedTime==null);

            var result = Result<List<RoleDTO>>.Success();
            var mapValue = _mapper.Map<List<RoleDTO>>(roles);
            result.Value = mapValue;
            return result;
        }
    }
}
