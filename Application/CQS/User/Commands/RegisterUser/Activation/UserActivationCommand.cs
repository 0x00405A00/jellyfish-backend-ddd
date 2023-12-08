using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.RegisterUser.Activation
{
    public record UserActivationCommand(string Base64Token, string ActivationCode) : ICommand<UserDTO>
    {

    }
}