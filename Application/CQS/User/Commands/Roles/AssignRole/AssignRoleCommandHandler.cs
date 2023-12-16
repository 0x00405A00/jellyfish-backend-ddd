using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Extension;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.User.Commands.Roles.AssignRole
{
    internal sealed class AssignRoleCommandHandler : ICommandHandler<AssignRoleCommand, List<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator mediator;
        private readonly IRoleRepository roleRepository;

        public AssignRoleCommandHandler(
            IMapper mapper,
            IMediator mediator,
            IRoleRepository roleRepository,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            this.mediator = mediator;
            this.roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<List<Guid>>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                return Result<List<Guid>>.Failure("user not found");
            }
            var user = await _userRepository.GetAsync(user => user.Id.Id == request.UserId);
            if (user is null)
            {
                return Result<List<Guid>>.Failure("user not found");
            }

            var assignedByUser = await _userRepository.GetAsync(x => x.Id.Id == request.AssignerId);

            var foundRoles = await roleRepository.ListAsync(role => request.RoleIds.Contains(role.Id.Id));
            if (!foundRoles.Any())
            {
                return Result<List<Guid>>.Failure("invalid roles");
            }
            List<Guid> roleIds = new List<Guid>();
            foreach (var role in foundRoles)
            {
                try
                {
                    user.AddRole(assignedByUser, role);
                    roleIds.Add(role.Id.ToGuid());
                }
                catch (Exception ex)
                {

                }
            }
            if(!roleIds.Any())
            {
                return Result<List<Guid>>.Failure("no roles added");
            }
            _userRepository.Update(user);
            _userRepository.PublishDomainEvents(user, mediator);
            return Result<List<Guid>>.Success(roleIds);
        }
    }
}
