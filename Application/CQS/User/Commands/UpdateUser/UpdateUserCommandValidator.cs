using FluentValidation;

namespace Application.CQS.User.Commands.UpdateUser
{
    internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
