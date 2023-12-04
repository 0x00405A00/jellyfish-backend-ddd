using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User.Exception;
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
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            this.mediator = mediator;
            this.roleRepository = roleRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Guid>>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new InvalidOperationException($"userId is {request.UserId}");
            }
            var user = await _userRepository.GetAsync(user => user.Uuid == request.UserId);
            if (user is null)
                throw new UserNotFoundException(request.UserId);

            var assignedByUser = await _userRepository.GetAsync(x => x.Uuid == request.AssignerId);

            var foundRoles = await roleRepository.ListAsync(role => request.RoleIds.Contains(role.Uuid));
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
                    roleIds.Add(role.Uuid.ToGuid());
                }
                catch (Exception ex)
                {

                }
            }
            if(!roleIds.Any())
            {
                return Result<List<Guid>>.Failure("no roles added");
            }
            _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            _userRepository.PublishDomainEvents(user, mediator);
            return Result<List<Guid>>.Success(roleIds);
        }
    }
}
