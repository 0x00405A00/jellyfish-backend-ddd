using Application.CQS.User.Commands.UpdatePassword;
using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.User.Commands.UpdatePassword
{
    public class UpdatePasswordTest
    {
        private static readonly Guid UserId = Guid.NewGuid();

        private static readonly UpdateUserPasswordCommand ValidCommand = new UpdateUserPasswordCommand(
            UpdatedBy: Guid.NewGuid(),
            UserId: UserId,
            Password: "newpassword1234!$ABCD",
            PasswordConfirm: "newpassword1234!$ABCD");
        private static readonly UpdateUserPasswordCommand InvalidCommandPasswordIsNotSecure = ValidCommand with { Password = "abcd", PasswordConfirm = "abcd" };
        private static readonly UpdateUserPasswordCommand InvalidCommandPasswordNotEqual = ValidCommand with { PasswordConfirm = "newpassword1234!$ABCDF" };

        private readonly UpdateUserPasswordCommandHandler _handler;

        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);

        public UpdatePasswordTest()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new UpdateUserPasswordCommandHandler(
                _mediatorMock,
                _userRepositoryMock);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEqual(Guid.Empty, result.Value);
        }
        [Fact]
        public async Task Handle_InvalidCommand_PasswordNotEqual_ReturnsFailure()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            // Act
            var result = await _handler.Handle(InvalidCommandPasswordNotEqual, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public async Task Handle_InvalidCommand_PasswordNotSecure_ReturnsFailure()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            // Act
            var result = await _handler.Handle(InvalidCommandPasswordIsNotSecure, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
