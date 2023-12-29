using Application.CQS.User.Commands.PasswordReset.Request;
using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.User.Commands.PasswordReset.Request
{
    public class RequestTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly string TestEmail = "test@example.com";
        private static readonly UserPasswordResetRequestCommand ValidCommand = new UserPasswordResetRequestCommand(TestEmail);

        private readonly UserPasswordResetRequestCommandHandler _handler;

        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);

        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediator _mediatorMock;

        public RequestTest()
        {

            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediatorMock = Substitute.For<IMediator>();
            _handler = new UserPasswordResetRequestCommandHandler(
                _mediatorMock,
                _mapperMock,
                _userRepositoryMock);
        }


        [Fact]
        public async Task Handle_UserFound_ReturnsSuccessResult()
        {
            // Arrange
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));
            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            // Add more assertions based on your specific success conditions
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsFailureResult()
        {
            // Arrange
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult<Domain.Entities.Users.User>(null));

            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }

    }
}
