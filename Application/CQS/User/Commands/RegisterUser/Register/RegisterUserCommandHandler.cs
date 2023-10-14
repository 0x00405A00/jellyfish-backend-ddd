using Application.Abstractions.Messaging;
using Application.DataTransferObject;
using AutoMapper;
using Domain.Const;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.User.Commands.RegisterUser.Register
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUserCommandHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUserTypeRepository userTypeRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            bool emailAlreadyInUseCheck = await _userRepository.IsEmailAlreadyInUse(request.Email);
            if (emailAlreadyInUseCheck)
            {
                return Result<UserDTO>.Failure("email address already in use");
            }
            PhoneNumber phoneNumber = null!;
            try
            {
                phoneNumber = PhoneNumber.Parse(request.Phone);
            }
            catch (NotValidPhoneNumberException ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }
            Email email = null!;
            try
            {
                email = Email.Parse(request.Email);
            }
            catch (NotValidEmailException ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }
            var userId = new Domain.Entities.User.UserId(Guid.NewGuid());
            var userType = await _userTypeRepository.GetAsync(x => x.Uuid == AuthorizationConst.UserType.UserTypeUuid);
            var userRole = await _roleRepository.GetAsync(x => x.Uuid == AuthorizationConst.Role.UserRoleUuid);
            var systemUser = Domain.Entities.User.User.GetSystemUser();
            var user = Domain.Entities.User.User.Create(
                userId,
                userType,
                request.UserName,
                request.Password,
                request.FirstName,
                request.LastName,
                email,
                phoneNumber,
                null,
                null,
                null,
                null,
                request.DateOfBirth,
                null,
                DateTime.Now,
                null,
                null,
                systemUser);
            user.AddRole(systemUser,userRole);

            _userRepository.Add(user);


            var result = Result<UserDTO>.Success();
            result.Value = _mapper.Map<UserDTO>(user);
            return result;
        }
    }
}
