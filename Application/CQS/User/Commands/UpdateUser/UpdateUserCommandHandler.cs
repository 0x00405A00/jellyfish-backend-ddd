﻿using Application.Abstractions.Messaging;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Application.CQS.User.Commands.UpdateUser
{
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new InvalidOperationException($"userId is {request.UserId}");
            }
            var user = await _userRepository.GetAsync(user => user.Uuid == request.UserId);
            if (user == null)
                throw new UserNotFoundException(request.UserId);

            if (request.Phone != null)
            {
                PhoneNumber phoneNumber = null!;
                try
                {

                    phoneNumber = PhoneNumber.Parse(request.Phone);
                    user.UpdatePhone(user, phoneNumber);
                }
                catch (NotValidPhoneNumberException ex)
                {
                    return Result<Guid>.Failure(ex.Message);
                }
            }
            if (request.Email != null)
            {

                Email email = null!;
                try
                {

                    email = Email.Parse(request.Email);
                    user.UpdateEmail(user, email);
                }
                catch (NotValidEmailException ex)
                {
                    return Result<Guid>.Failure(ex.Message);
                }
            }
            if (request.Password != null)
            {
                try
                {
                    user.UpdatePassword(user,request.Password);
                }
                catch (InvalidPasswordException ex)
                {
                    return Result<Guid>.Failure(ex.Message);
                }
            }
            if (request.DateOfBirth != null)
            {
                try
                {
                    user.UpdateDateOfBirth(user, (DateOnly)request.DateOfBirth);
                }
                catch (InvalidDateOfBirthException ex)
                {
                    return Result<Guid>.Failure(ex.Message);
                }
            }
            if (request.FirstName != null)
            {
                try
                {
                    user.UpdateFirstName(user, request.FirstName);
                }
                catch (InvalidFirstNameException ex)
                {
                    return Result<Guid>.Failure(ex.Message);
                }
            }
            if (request.LastName != null)
            {
                try
                {
                    user.UpdateLastName(user, request.LastName);
                }
                catch (InvalidLastNameException ex)
                {
                    return Result<Guid>.Failure(ex.Message);
                }
            }
            if (request.UserName != null)
            {
                try
                {
                    user.UpdateUserName(user, request.UserName);
                }
                catch(InvalidUserNameException ex)
                {
                    return Result<Guid>.Failure(ex.Message);
                }
            }

            _userRepository.Update(user);

            return Result<Guid>.Success();
        }
    }
}
