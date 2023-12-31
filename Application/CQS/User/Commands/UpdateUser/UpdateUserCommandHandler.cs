using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Users.Exceptions;
using Domain.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.UpdateUser
{
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this.mediator = mediator;
            this._mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                return Result<UserDTO>.Failure("user not found");
            }
            var user = await _userRepository.GetAsync(user => user.Id == request.UserId.ToIdentification<UserId>());
            if (user is null)
            {
                return Result<UserDTO>.Failure("user not found");
            }

            var updatedByUser = await _userRepository.GetAsync(x => x.Id == request.UpdatedBy.ToIdentification<UserId>());

            if (request.Phone != null)
            {
                PhoneNumber phoneNumber = null!;
                try
                {

                    phoneNumber = PhoneNumber.Parse(request.Phone);
                    user.UpdatePhone(updatedByUser.Id, phoneNumber);
                }
                catch (NotValidPhoneNumberException ex)
                {
                    return Result<UserDTO>.Failure(ex.Message);
                }
            }
            if (request.Email != null)
            {

                Email email = null!;
                try
                {

                    email = Email.Parse(request.Email);
                    user.UpdateEmail(updatedByUser.Id, email);
                }
                catch (NotValidEmailException ex)
                {
                    return Result<UserDTO>.Failure(ex.Message);
                }
            }
            if (request.DateOfBirth != null)
            {
                try
                {
                    user.UpdateDateOfBirth(updatedByUser.Id, (request.DateOfBirth??DateTime.MinValue).ToTypedDateOnly());
                }
                catch (InvalidDateOfBirthException ex)
                {
                    return Result<UserDTO>.Failure(ex.Message);
                }
            }
            if (request.FirstName != null)
            {
                try
                {
                    user.UpdateFirstName(updatedByUser.Id, request.FirstName);
                }
                catch (InvalidFirstNameException ex)
                {
                    return Result<UserDTO>.Failure(ex.Message);
                }
            }
            if (request.LastName != null)
            {
                try
                {
                    user.UpdateLastName(updatedByUser.Id, request.LastName);
                }
                catch (InvalidLastNameException ex)
                {
                    return Result<UserDTO>.Failure(ex.Message);
                }
            }
            if (request.UserName != null)
            {
                try
                {
                    user.UpdateUserName(updatedByUser.Id, request.UserName);
                }
                catch(InvalidUserNameException ex)
                {
                    return Result<UserDTO>.Failure(ex.Message);
                }
            }

            _userRepository.Update(user);

            var mapValue = _mapper.Map<UserDTO>(user);
            return Result<UserDTO>.Success(mapValue);
        }
    }
}
