using Application.CQS.User.Commands.RegisterUser.Activation;
using AutoMapper;
using Domain.Entities.Users.Exceptions;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Application.UnitTests.UseCase.User.Commands.RegisterUser.Activation
{
    public class ActivationTest
    {
        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly UserActivationCommand ValidCommand = new UserActivationCommand(
            Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
            "123456");

        private readonly UserActivationCommandHandler _handler;

        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScope _serviceScopeFactory;
        private readonly IUserRepository _userRepositoryMock;
        private readonly IMediator _mediatorMock;
        private readonly IMapper _mapperMock;

        private static readonly Domain.Entities.Users.User UserInstance = SharedTest.DomainTestInstance.Entity.User.InstancingHelper.GetUserInstance(UserId);

        public ActivationTest()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _mediatorMock = Substitute.For<IMediator>();
            _mapperMock = Substitute.For<IMapper>();
            
            var serviceScopeFactorySubstitute = Substitute.For<IServiceScopeFactory>();

            // Setze das Verhalten für CreateScope
            serviceScopeFactorySubstitute.CreateScope().Returns(x =>
            {
                var serviceScopeSubstitute = Substitute.For<IServiceScope>();
                // Hier kannst du das Verhalten für IServiceScope konfigurieren, wenn es notwendig ist.

                return serviceScopeSubstitute;
            });

            // Füge das Substitute zu deinem DI-Container hinzu
            _serviceProvider = new ServiceCollection()
                .AddScoped<IServiceScopeFactory>(_ => serviceScopeFactorySubstitute)
                .AddScoped<IUserRepository>(_ => _userRepositoryMock)
                .AddScoped<IMediator>(_ => _mediatorMock)
                .AddScoped<IMapper>(_ => _mapperMock)
                .BuildServiceProvider();

            _handler = new UserActivationCommandHandler(
                _serviceProvider);
        }

        [Fact]
        public async Task Handle_ValidActivation_ReturnsSuccessResult()
        {
            // Arrange
            
            UserInstance.GenerateActivationToken(); 
            var command = ValidCommand with { ActivationCode = UserInstance.ActivationCode, Base64Token = UserInstance.ActivationToken };
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>()).Returns(UserInstance);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _userRepositoryMock.Received(1).UpdateAsync(UserInstance);
            _userRepositoryMock.Received(1).PublishDomainEvents(UserInstance, _mediatorMock);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_InvalidActivation_ThrowsException()
        {
            // Arrange
            _userRepositoryMock.GetAsync(Arg.Any<Expression<Func<Infrastructure.DatabaseEntity.User, bool>>>())
                   .Returns(Task.FromResult<Domain.Entities.Users.User>(null));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(() => _handler.Handle(ValidCommand, CancellationToken.None));
            Assert.True(exception is  UserNotFoundException);
        }
    }
}
