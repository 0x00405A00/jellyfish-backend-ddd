using FluentValidation;

namespace Application.CQS.User.Commands.DeleteUser
{
    internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
