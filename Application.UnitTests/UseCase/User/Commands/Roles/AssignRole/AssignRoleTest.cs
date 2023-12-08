using Application.CQS.User.Commands.Roles.AssignRole;
using AutoMapper;
using Domain.Entities.User;
using Domain.Extension;
using Infrastructure.Abstractions;
using MediatR;
using System.Linq.Expressions;
using FluentAssertions;

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

        private static readonly Domain.Entities.Role.Role RoleAdmin = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetRoleInstance("Admin");
        private static readonly Domain.ValueObjects.UserRole UserRoleAdmin = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserRoleInstance(RoleAdmin);
        private static readonly Domain.Entities.Role.Role RoleUser = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetRoleInstance("User");
        private static readonly Domain.Entities.User.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);
        private static readonly Domain.Entities.User.User UserAdminInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserAdminId, UserRoleAdmin);

        public AssignRoleTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _userTypeRepositoryMock = Substitute.For<IUserTypeRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new AssignRoleCommandHandler(
                _mapperMock,
                _mediatorMock,
                _roleRepositoryMock,
                _userRepositoryMock,
                _unitOfWorkMock);

        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            var command = Command with { AssignerId = UserAdminId, UserId = UserId, RoleIds = new List<Guid> { RoleAdmin.Uuid.ToGuid() } };

            _roleRepositoryMock.ListAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Role, bool>>>()).Returns(new List<Domain.Entities.Role.Role>
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
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserAdminInstance);

            var command = Command with { AssignerId = UserAdminId, UserId = UserId, RoleIds = new List<Guid> { RoleAdmin.Uuid.ToGuid() } };

            _roleRepositoryMock.ListAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.Role, bool>>>()).Returns(new List<Domain.Entities.Role.Role>
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
