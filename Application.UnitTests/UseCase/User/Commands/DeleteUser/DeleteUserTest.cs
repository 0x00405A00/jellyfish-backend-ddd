using Application.CQS.User.Commands.DeleteUser;
using AutoMapper;
using Domain.Entities.User;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.User.Commands.DeleteUser
{
    public class DeleteUserTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly DeleteUserCommand ValidCommand = new DeleteUserCommand(DeletedByUserId:Guid.NewGuid(), UserId);

        private readonly DeleteUserCommandHandler _handler;

        private readonly ISender _sender;
        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserTypeRepository _userTypeRepositoryMock;
        private readonly IRoleRepository _roleRepositoryMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);

        public DeleteUserTest()
        {

            _sender = Substitute.For<ISender>();
            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _userTypeRepositoryMock = Substitute.For<IUserTypeRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new DeleteUserCommandHandler(
                _sender,
                _mediatorMock,
                _userRepositoryMock);
        }


        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
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
        public async Task Handle_TargetUserNotFound_ReturnsFailureResult()
        {
            // Arrange
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                   .Returns(Task.FromResult<Domain.Entities.User.User>(null));

            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }

        [Fact]
        public async Task Handle_ExecutionUserNotFound_ReturnsFailureResult()
        {
            // Arrange
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));

            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }
    }
}
