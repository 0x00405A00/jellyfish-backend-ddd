using Application.Abstractions.Messaging;
using Application.CQS.User.Commands.CreateUser;
using AutoMapper;
using Domain.Extension;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.RegisterUser.Register
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISender _sender;

        public RegisterUserCommandHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUserTypeRepository userTypeRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            ISender sender)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            this._sender = sender;
            _userTypeRepository = userTypeRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Result<UserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var systemUser = Domain.Entities.User.User.GetSystemUser();
            var command = new CreateUserCommand(
                systemUser.Uuid.ToGuid(),
                request.UserName,
                request.Password,
                request.PasswordRepeat,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                request.DateOfBirth);
            var result = await _sender.Send(command);
            return result;

        }
    }
}
