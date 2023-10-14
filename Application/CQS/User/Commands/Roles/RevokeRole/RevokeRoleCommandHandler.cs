using Application.Abstractions.Messaging;
using Application.DataTransferObject;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.User.Commands.Roles.RevokeRole
{
    internal sealed class RevokeRoleCommandHandler : ICommandHandler<RevokeRoleCommand, RoleDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RevokeRoleCommandHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<RoleDTO>> Handle(RevokeRoleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();    
        }

    }
}
