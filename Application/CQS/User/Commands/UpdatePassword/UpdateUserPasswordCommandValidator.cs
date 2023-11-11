using FluentValidation;

namespace Application.CQS.User.Commands.UpdatePassword
{
    internal class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
