using Application.CQS.User.Commands.PasswordReset.Reset;
using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;
using System.Text;

namespace Application.UnitTests.UseCase.User.Commands.PasswordReset
{
    public class ResetTest
    {
        private static readonly Guid UserId = Guid.NewGuid();

        private static readonly UserPasswordResetCommand ValidCommand = new UserPasswordResetCommand(
            "newpassword$!123AV",
            "newpassword$!123AV",
            "resetcode",
            Convert.ToBase64String(Encoding.UTF8.GetBytes("base64token")));

        private readonly UserPasswordResetCommandHandler _handler;

        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserId);

        public ResetTest()
        {

            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediatorMock = Substitute.For<IMediator>();
            _handler = new UserPasswordResetCommandHandler(
                _mediatorMock,
                _mapperMock,
                _userRepositoryMock);
        }

        [Fact]
        public async Task Handle_InvalidResetRequest_ReturnsFailure()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>()).Returns((Domain.Entities.Users.User)null);

            // Act
            var result = await _handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
