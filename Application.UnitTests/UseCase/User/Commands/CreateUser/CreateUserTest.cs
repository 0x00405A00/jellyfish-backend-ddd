using Application.CQS.User.Commands.CreateUser;
using AutoMapper;
using FluentAssertions;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObject;

namespace Application.UnitTests.UseCase.User.Commands.CreateUser
{
    public class CreateUserTest
    {
        private static readonly CreateUserCommand Command = new CreateUserCommand(
                                                                            CreatedBy: Guid.NewGuid(),
                                                                            UserName: "john_doe",
                                                                            Password: "password123",
                                                                            PasswordRepeat: "password123",
                                                                            FirstName: "John",
                                                                            LastName: "Doe",
                                                                            Email: "john.doe@example.com",
                                                                            Phone: "1234567890",
                                                                            DateOfBirth: new DateTime(1990, 1, 1)
                                                                        );

        private readonly CreateUserCommandHandler _handler;

        private readonly IMapper _mapperMock;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUserTypeRepository _userTypeRepositoryMock;
        private readonly IRoleRepository _roleRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly IMediator _mediatorMock;

        public CreateUserTest()
        {
            _mapperMock = Substitute.For<IMapper>();
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _userTypeRepositoryMock = Substitute.For<IUserTypeRepository>();
            _roleRepositoryMock = Substitute.For<IRoleRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _mediatorMock = Substitute.For<IMediator>();

            _handler = new CreateUserCommandHandler(
                _mapperMock,
                _userRepositoryMock,
                _userTypeRepositoryMock,
                _roleRepositoryMock,
                _unitOfWorkMock,
                _mediatorMock);
        }

        [Fact]
        public async Task Handle_Should_ReturnFailure_WhenEmailIsInvalid()
        {
            //Arrange
            CreateUserCommand invalidCommand = Command with { Email = "blabla@xyz" };

            //Act
            Domain.ValueObjects.Result<UserDTO> result = await _handler.Handle(invalidCommand, default);

            //Assert
            result.Error.Should().NotBeNull();
        }

    }
}
