using Application.CQS.User.Commands.Roles.AssignRole;
using AutoMapper;
using Domain.Extension;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;
using FluentAssertions;
using Domain.Entities.Users;
using Domain.Entities.Roles;
using Domain.Primitives.Ids;

namespace Application.UnitTests.UseCase.User.Commands.Roles.AssignRole
{
    public class AssignRoleTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid UserAdminId = Guid.NewGuid();
        private static readonly AssignRoleCommand Command = new AssignRoleCommand(Guid.NewGuid(), Guid.NewGuid(), new List<Guid> { Guid.NewGuid() });

        private AssignRoleCommandHandler _handler;

        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserTypeRepository _userTypeRepositoryMock;
        private readonly IRoleRepository _roleRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;

        private static readonly Role RoleAdmin = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleAdminInstance();
        private static readonly UserHasRelationToRole UserRoleAdmin = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserRoleInstance(RoleAdmin, UserAdminId.ToIdentification<UserId>());
        private static readonly Role RoleUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetRoleUserInstance();
        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.Users.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserAdminId, UserRoleAdmin);

        public AssignRoleTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _userTypeRepositoryMock = Substitute.For<IUserTypeRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new AssignRoleCommandHandler(
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

            var command = Command with { AssignerId = UserAdminId, UserId = UserId, RoleIds = new List<Guid> { RoleAdmin.Id.ToGuid() } };

            _roleRepositoryMock.ListAsync(Arg.Any<Expression<Func<Domain.Entities.Roles.Role, bool>>>()).Returns(new List<Role>
            {
                RoleUser,
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
        public async Task Handle_InvalidCommand_RoleAlreadyAssigned()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Domain.Entities.Users.User, bool>>>()).Returns(UserAdminInstance);

            var command = Command with { AssignerId = UserAdminId, UserId = UserId, RoleIds = new List<Guid> { RoleAdmin.Id.ToGuid() } };

            _roleRepositoryMock.ListAsync(Arg.Any<Expression<Func<Domain.Entities.Roles.Role, bool>>>()).Returns(new List<Role>
            {
                RoleAdmin,
            });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);
            result.Value.Should().BeNull("RoleAdmin already assigned");
        }
    }
}
