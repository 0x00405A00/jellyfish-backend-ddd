using Application.CQS.User.Commands.CreateUser;
using Application.CQS.User.Commands.RegisterUser.Register;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObject;

namespace Application.UnitTests.UseCase.User.Commands.RegisterUser.Register
{
    public class RegisterTest
    {

        private static readonly RegisterUserCommand Command = new RegisterUserCommand(
                                                                            UserName: "john_doe",
                                                                            Password: "password123",
                                                                            PasswordRepeat: "password123",
                                                                            FirstName: "John",
                                                                            LastName: "Doe",
                                                                            Email: "john.doe@example.com",
                                                                            Phone: "1234567890",
                                                                            DateOfBirth: new DateTime(1990, 1, 1));

        private readonly RegisterUserCommandHandler _handler;

        private readonly ISender _sender;
        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserTypeRepository _userTypeRepositoryMock;
        private readonly IRoleRepository _roleRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;

        public RegisterTest()
        {
            _sender = Substitute.For<ISender>();
            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _userTypeRepositoryMock = Substitute.For<IUserTypeRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new RegisterUserCommandHandler(
                _mapperMock,
                _userRepositoryMock,
                _userTypeRepositoryMock,
                _roleRepositoryMock,
                _unitOfWorkMock,
                _sender) ;
        }
        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccess()
        {
            // Arrange
            _sender.Send(Arg.Any<CreateUserCommand>()).Returns(Result<UserDTO>.Success(new UserDTO()));

            // Act
            var result = await _handler.Handle(Command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task Handle_CreateUserFails_ReturnsFailure()
        {
            // Arrange
            _sender.Send(Arg.Any<CreateUserCommand>()).Returns(Result<UserDTO>.Failure("User creation failed"));

            // Act
            var result = await _handler.Handle(Command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User creation failed", result.Error.Message);
        }
    }
}
