﻿using Application.Abstractions.Messaging;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            if (user is null)
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
            if (request.DateOfBirth != null)
            {
                try
                {
                    user.UpdateDateOfBirth(user,DateOnly.FromDateTime(request.DateOfBirth??DateTime.MinValue));
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

            _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(user.Uuid.ToGuid());
        }
    }
}
