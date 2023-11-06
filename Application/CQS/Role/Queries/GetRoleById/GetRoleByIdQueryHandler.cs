using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject;

namespace Application.CQS.Role.Queries.GetRoleById
{
    internal sealed class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        public GetRoleByIdQueryHandler(
            IMapper mapper,
            IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }
        public async Task<Result<RoleDTO>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetAsync(x=>x.DeletedTime==null);
            if(role == null)
            {
                return Result<RoleDTO>.Failure("role doenst exists");
            }

            var mapValue = _mapper.Map<RoleDTO>(role);
            return Result<RoleDTO>.Success(mapValue);
        }
    }
}
