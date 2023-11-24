using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User.Exception;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.RegisterUser.Activation
{
    internal sealed class UserActivationCommandHandler : ICommandHandler<UserActivationCommand, UserDTO>
    {
        private readonly IServiceProvider serviceProvider;

        public UserActivationCommandHandler(
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<Result<UserDTO>> Handle(UserActivationCommand request, CancellationToken cancellationToken)
        {

            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                var mapper = scope.ServiceProvider.GetService<IMapper>();

                var user = await userRepository.GetAsync(user => user.ActivationToken == request.Base64Token);
                if (user == null)
                    throw new UserNotFoundException(request.Base64Token);

                try
                {
                    user.Activate(request.ActivationCode, request.Base64Token);
                }
                catch (InvalidActivationTry ex)
                {
                    return Result<UserDTO>.Failure(ex.Message);
                }

                userRepository.UpdateAsync(user);
                userRepository.PublishDomainEvents(user, mediator);

                var mapValue = mapper.Map<UserDTO>(user);
                mapValue.Password = null;
                return Result<UserDTO>.Success(mapValue);
            }
        }
    }
}
