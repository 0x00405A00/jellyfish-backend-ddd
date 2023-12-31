using Application.CQS.User.Commands.Roles.RevokeRole;
using AutoMapper;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives.Ids;
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

        private static readonly Role RoleAdmin = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleAdminInstance();
        private static readonly UserHasRelationToRole UserRoleAdmin = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserRoleInstance(RoleAdmin, UserAdminId.ToIdentification<UserId>());
        private static readonly Role RoleUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleUserInstance();
        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserId, UserRoleAdmin);
        private static readonly Domain.Entities.Users.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserAdminId, UserRoleAdmin);

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
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>()).Returns(UserInstance);

            var command = Command with { RevokerId = UserAdminId, UserId = UserId, RoleIds = new List<Guid> { RoleAdmin.Id.ToGuid() } };

            _roleRepositoryMock.ListAsync(Arg.Any<Expression<Func<Domain.Entities.Roles.Role, bool>>>()).Returns(new List<Role>
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
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>()).Returns(UserInstance);

            var command = Command with { RevokerId = UserAdminId, UserId = UserId, RoleIds = new List<Guid> { RoleUser.Id.ToGuid() } };

            _roleRepositoryMock.ListAsync(Arg.Any<Expression<Func<Domain.Entities.Roles.Role, bool>>>()).Returns(new List<Role>
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
