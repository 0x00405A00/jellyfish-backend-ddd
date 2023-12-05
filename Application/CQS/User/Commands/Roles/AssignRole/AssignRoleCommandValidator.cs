using FluentValidation;

namespace Application.CQS.User.Commands.Roles.AssignRole
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator()
        {
            RuleFor(command => command.AssignerId).NotEmpty().WithMessage("AssignerId must not be empty.");
            RuleFor(command => command.UserId).NotEmpty().WithMessage("UserId must not be empty.");
            RuleFor(command => command.RoleIds).NotEmpty().WithMessage("RoleIds list must not be empty.");
        }
    }
}
