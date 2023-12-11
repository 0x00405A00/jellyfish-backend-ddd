using Application.CQS.User.Commands.UpdateUser;
using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.User.Commands.UpdateUser
{
    public class UpdateUserTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly UpdateUserCommand ValidCommand = new UpdateUserCommand(
            Guid.NewGuid(),
            UserId,
            UserName: "john_doe",
            Password: "password123",
            PasswordConfirm: "password123",
            FirstName: "John",
            LastName: "Doe",
            Email: "john.doe@example.com",
            Phone: "1234567890",
            DateOfBirth: new DateTime(1990, 1, 1));

        private static readonly UpdateUserCommand InvalidCommand = ValidCommand with { Phone = "abcd iam not a valid phonenumber" };

        private readonly UpdateUserCommandHandler _handler;

        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);

        public UpdateUserTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new UpdateUserCommandHandler(
                _mediatorMock,
                _mapperMock,
                _userRepositoryMock);
        }

        [Fact]
        public async Task Handle_ValidUpdateCommand_Success()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidPhoneNumber_Failure()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            // Act
            var result = await _handler.Handle(InvalidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

    }
}
