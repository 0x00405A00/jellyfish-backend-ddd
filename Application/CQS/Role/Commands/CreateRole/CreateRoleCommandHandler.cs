using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;

namespace Application.CQS.Role.Commands.CreateRole
{
    internal sealed class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, RoleDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMediator mediator;

        public CreateRoleCommandHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            this._mapper = mapper;
            this.userRepository = userRepository;
            this.mediator = mediator;
            _roleRepository = roleRepository;
        }

        public async Task<Result<RoleDTO>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExists = _roleRepository.GetAsync(x=>x.Name==request.Name);
            if(roleExists != null)
            {
                return Result<RoleDTO>.Failure("role already exists");
            }
            var createdByUser = await userRepository.GetAsync(x => x.Id == request.CreatedBy.ToIdentification<UserId>());

            Domain.Entities.Roles.Role role =null;
            try
            {
                var roleId = new RoleId(Guid.NewGuid());
                role = Domain.Entities.Roles.Role.Create(
                    roleId,
                    request.Name,
                    DateTime.Now.ToTypedDateTime(),
                    createdByUser.Id,
                    null,
                    null,
                    null,
                    null);
            }
            catch (Exception ex)
            {
                return Result<RoleDTO>.Failure(ex.Message);
            }
            try
            {
                _roleRepository.Add(role);
            }
            catch (Exception ex)
            {
                return Result<RoleDTO>.Failure(ex.Message);
            }
            var mapValue = _mapper.Map<RoleDTO>(role);
            return Result<RoleDTO>.Success(mapValue);
        }
    }
}
