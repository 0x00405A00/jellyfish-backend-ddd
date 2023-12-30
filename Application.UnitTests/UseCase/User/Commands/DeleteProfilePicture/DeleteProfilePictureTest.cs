using Application.CQS.User.Commands.DeleteProfilePicture;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using NSubstitute.ExceptionExtensions;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.User.Commands.DeleteProfilePicture
{
    public class DeleteProfilePictureTest
    {
        private static readonly string ValidMimePart = "image/jpeg";
        private static readonly string InvalidMimePart = "images/jpeg";
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid DeletedByUserId = Guid.NewGuid();
        private static readonly DeleteProfilePictureCommand ValidCommand = new DeleteProfilePictureCommand(UserId, DeletedByUserId);

        private readonly DeleteProfilePictureCommandHandler _handler;

        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserTypeRepository _userTypeRepositoryMock;
        private readonly IRoleRepository _roleRepositoryMock;
        private readonly IMediator _mediatorMock;
        private readonly IMediaService _mediaServiceMock;

        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance(UserId);

        public DeleteProfilePictureTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _userTypeRepositoryMock = Substitute.For<IUserTypeRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _mediatorMock = Substitute.For<IMediator>();
            _mediaServiceMock = Substitute.For<IMediaService>();

            _handler = new DeleteProfilePictureCommandHandler(
                _mediatorMock,
                _userRepositoryMock,
                _mediaServiceMock); 
        }
        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var handler = _handler;

            UserInstance.UpdatePicture(UserInstance, Picture.Parse("", ValidMimePart));

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));

            _mediaServiceMock.DeleteProfilePicture(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns("mocked-file-path");

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

        [Fact]
        public async Task Handle_ProfilePictureNotSet_ReturnsFailureResult()
        {
            // Arrange
            var handler = _handler;

            UserInstance.UpdatePicture(UserInstance, null);

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));

            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }

        [Fact]
        public async Task Handle_MediaServiceThrowsException_ReturnsFailureResult()
        {
            // Arrange
            var handler = _handler;

            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                .Returns(Task.FromResult(UserInstance));

            _mediaServiceMock.DeleteProfilePicture(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Throws(new IOException("Mocked exception"));

            // Act
            var result = await handler.Handle(ValidCommand, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            // Add more assertions based on your specific failure conditions
        }

    }
}
