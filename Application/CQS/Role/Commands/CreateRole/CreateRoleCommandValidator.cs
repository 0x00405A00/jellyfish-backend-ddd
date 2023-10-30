using FluentValidation;

namespace Application.CQS.Role.Commands.CreateRole
{
    internal class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            //RuleFor(x => x.priv);
        }
    }
}
