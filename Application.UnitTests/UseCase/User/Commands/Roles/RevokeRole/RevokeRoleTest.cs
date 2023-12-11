using Application.CQS.User.Commands.Roles.RevokeRole;
using AutoMapper;
using Domain.Entities.User;
using Domain.Extension;
using FluentAssertions;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.User.Commands.Roles.RevokeRole
{
    public class RevokeRoleTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid UserAdminId = Guid.NewGuid();
        private static readonly RevokeRoleCommand Command = new RevokeRoleCommand(Guid.NewGuid(),Guid.NewGuid(),new List<Guid> { Guid.NewGuid() });

        private RevokeRoleCommandHandler _handler;

        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IRoleRepository _roleRepositoryMock;
        private readonly IMediator _mediatorMock;

        private static readonly Domain.Entities.Role.Role RoleAdmin = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetRoleInstance("Admin");
        private static readonly Domain.ValueObjects.UserRole UserRoleAdmin = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserRoleInstance(RoleAdmin);
        private static readonly Domain.Entities.Role.Role RoleUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetRoleInstance("User");
        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId, UserRoleAdmin);
        private static readonly Domain.Entities.User.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserAdminId, UserRoleAdmin);

        public RevokeRoleTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new RevokeRoleCommandHandler(
                _mapperMock,
                _mediatorMock,
                _roleRepositoryMock,
                _userRepositoryMock);
        }
        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            var command = Command with { RevokerId = UserAdminId, UserId = UserId, RoleIds = new List<Guid> { RoleAdmin.Uuid.ToGuid() } };

            _roleRepositoryMock.ListAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Role, bool>>>()).Returns(new List<Domain.Entities.Role.Role>
            {
                RoleAdmin,
            });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value);
            Assert.True(result.Value.Count == command.RoleIds.Count);
        }
        [Fact]
        public async Task Handle_InvalidCommand_RoleAlreadyRevoked()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            var command = Command with { RevokerId = UserAdminId, UserId = UserId, RoleIds = new List<Guid> { RoleUser.Uuid.ToGuid() } };

            _roleRepositoryMock.ListAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Role, bool>>>()).Returns(new List<Domain.Entities.Role.Role>
            {
            });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);
            result.Value.Should().BeNull("RoleUser already revoked");
        }
    }
}
