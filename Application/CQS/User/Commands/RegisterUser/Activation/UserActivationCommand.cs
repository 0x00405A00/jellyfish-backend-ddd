using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.RegisterUser.Activation
{
    public record UserActivationCommand : ICommand<UserDTO>
    {
        public string Base64Token { get; }
        public string ActivationCode { get; }

        public UserActivationCommand(string Base64Token, string ActivationCode)
        {
            this.Base64Token = Base64Token;
            this.ActivationCode = ActivationCode;
        }

    }
}