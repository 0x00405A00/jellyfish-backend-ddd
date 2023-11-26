using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObject;
using System.Linq.Expressions;

namespace Application.CQS.User.Commands.UpdateUser
{
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserCommandHandler(
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new InvalidOperationException($"userId is {request.UserId}");
            }
            var user = await _userRepository.GetAsync(user => user.Uuid == request.UserId);
            if (user is null)
                throw new UserNotFoundException(request.UserId);

            var updatedByUser = await _userRepository.GetAsync(x => x.Uuid == request.UpdatedBy);

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

            _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var mapValue = _mapper.Map<UserDTO>(user);
            mapValue.Password = null;
            return Result<UserDTO>.Success(mapValue);
        }
    }
}
