using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObject;

namespace Application.CQS.Role.Commands.CreateRole
{
    internal sealed class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, RoleDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator mediator;

        public CreateRoleCommandHandler(
            IMapper mapper,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            this._mapper = mapper;
            _unitOfWork = unitOfWork;
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
            Domain.Entities.Role.Role role =null;
            try
            {
                var roleId = new Domain.Entities.Role.RoleId(Guid.NewGuid());
                role = Domain.Entities.Role.Role.Create(roleId, request.Name, request.Description, DateTime.Now, null, null);
            }
            catch (Exception ex)
            {
                return Result<RoleDTO>.Failure(ex.Message);
            }
            try
            {
                _roleRepository.Add(role);
                await _unitOfWork.SaveChangesAsync();
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
