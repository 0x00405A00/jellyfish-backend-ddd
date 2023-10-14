using Application.Abstractions.Messaging;
using Application.DataTransferObject;

namespace Application.CQS.User.Commands.RegisterUser.Activation
{
    public class UserActivationCommand : ICommand<UserDTO>
    {
        public UserActivationCommand(string Base64Token, string ActivationCode)
        {
            this.Base64Token = Base64Token;
            this.ActivationCode = ActivationCode;
        }

        public string Base64Token { get; }
        public string ActivationCode { get; }
    }
}