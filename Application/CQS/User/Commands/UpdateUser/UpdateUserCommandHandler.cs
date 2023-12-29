using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Users.Exceptions;
using Domain.Exceptions;
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
            var user = await _userRepository.GetAsync(user => user.Id.Id == request.UserId);
            if (user is null)
            {
                return Result<UserDTO>.Failure("user not found");
            }

            var updatedByUser = await _userRepository.GetAsync(x => x.Id.Id == request.UpdatedBy);

            if (request.Phone != null)
            {
                PhoneNumber phoneNumber = null!;
                try
                {

                    phoneNumber = PhoneNumber.Parse(request.Phone);
                    user.UpdatePhone(updatedByUser, phoneNumber);
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
                    user.UpdateEmail(updatedByUser, email);
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
                    user.UpdateDateOfBirth(updatedByUser, DateOnly.FromDateTime(request.DateOfBirth??DateTime.MinValue));
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
                    user.UpdateFirstName(updatedByUser, request.FirstName);
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
                    user.UpdateLastName(updatedByUser, request.LastName);
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
                    user.UpdateUserName(updatedByUser, request.UserName);
                }
                catch(InvalidUserNameException ex)
                {
                    return Result<UserDTO>.Failure(ex.Message);
                }
            }

            _userRepository.Update(user);
            _userRepository.PublishDomainEvents(user,mediator);

            var mapValue = _mapper.Map<UserDTO>(user);
            return Result<UserDTO>.Success(mapValue);
        }
    }
}
