using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Const;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Shared.Const;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
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
            _unitOfWork = unitOfWork;
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
            var createdByUser = await _userRepository.GetAsync(x => x.Uuid == request.CreatedBy);

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
            Domain.Entities.User.User user;
            using(var transaction = await _unitOfWork.BeginTransaction())
            {

                try
                {
                    Domain.Entities.User.User.CheckPasswordWithPolicy(request.Password, request.PasswordRepeat);

                    var userId = new Domain.Entities.User.UserId(Guid.NewGuid());
                    var userType = await _userTypeRepository.GetAsync(x => x.Uuid == UserTypeConst.UserTypeUuid);
                    var userRole = await _roleRepository.GetAsync(x => x.Uuid == RoleConst.UserRoleUuid);
                    user = Domain.Entities.User.User.Create(
                        userId,
                        userType,
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
                        null,
                        null,
                        DateOnly.FromDateTime(request.DateOfBirth),
                        null,
                        DateTime.Now,
                        null,
                        null,
                        createdByUser);
                    user.AddRole(createdByUser, userRole);
                    user.GenerateActivationToken();
                    user.NewRegistered();
                    _userRepository.Add(user);
                    await transaction.CommitAsync();

                    _userRepository.PublishDomainEvents(user,mediator);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Result<UserDTO>.Failure(ex.Message);
                }
            }
            var mapValue = _mapper.Map<UserDTO>(user);
            mapValue.Password = null;
            return Result<UserDTO>.Success(mapValue);
        }
    }
}
