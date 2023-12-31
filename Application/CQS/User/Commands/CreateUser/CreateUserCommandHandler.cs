using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Const;
using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMediator mediator;

        public CreateUserCommandHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUserTypeRepository userTypeRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            this._mapper = mapper;
            _userRepository = userRepository;
            this.mediator = mediator;
            _userTypeRepository = userTypeRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Result<UserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
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
            var createdByUser = await _userRepository.GetAsync(x => x.Id == request.CreatedBy.ToIdentification<UserId>());

            bool emailAlreadyInUseCheck = await _userRepository.IsEmailAlreadyInUse(request.Email);
            bool phoneAlreadyInUseCheck = await _userRepository.IsPhoneAlreadyInUse(request.Phone);

            if (emailAlreadyInUseCheck)
            {
                return Result<UserDTO>.Failure("email address already in use");
            }
            if (phoneAlreadyInUseCheck)
            {
                return Result<UserDTO>.Failure("phonenumber already in use");
            }
            Domain.Entities.Users.User user;
            try
            {
                Domain.Entities.Users.User.CheckPasswordWithPolicy(request.Password, request.PasswordRepeat);

                var userId = Domain.Entities.Users.User.NewId();
                var userType = await _userTypeRepository.GetAsync(x => x.Id == UserTypeConst.UserTypeUuid.ToIdentification<UserTypeId>());
                var userRole = await _roleRepository.GetAsync(x => x.Id == RoleConst.UserRoleUuid.ToIdentification<RoleId>());
                user = Domain.Entities.Users.User.Create(
                    userId,
                    userType.Id,
                    request.UserName,
                    request.Password,
                    request.FirstName,
                    request.LastName,
                    null,
                    null,
                    null,
                    null,
                    null,
                    email,
                    phoneNumber,
                    null,
                    null,
                    request.DateOfBirth.ToTypedDateOnly(),
                    null,
                    DateTime.Now.ToTypedDateTime(),
                    createdByUser.Id,
                    null,
                    null,
                    null, 
                    null);

                user.AddRole(createdByUser.Id, userRole.Id);
                user.GenerateActivationToken();
                user.NewRegistered();
                user.SetCreated(createdByUser.Id);
                _userRepository.Add(user);
            }
            catch (Exception ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }
            var mapValue = _mapper.Map<UserDTO>(user);
            return Result<UserDTO>.Success(mapValue);
        }
    }
}
