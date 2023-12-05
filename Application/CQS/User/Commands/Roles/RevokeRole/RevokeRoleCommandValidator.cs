using FluentValidation;

namespace Application.CQS.User.Commands.Roles.RevokeRole
{
    public class RevokeRoleCommandValidator : AbstractValidator<RevokeRoleCommand>
    {
        public RevokeRoleCommandValidator()
        {
            RuleFor(command => command.RevokerId).NotEmpty().WithMessage("RevokerId must not be empty.");
            RuleFor(command => command.UserId).NotEmpty().WithMessage("UserId must not be empty.");
            RuleFor(command => command.RoleIds).NotEmpty().WithMessage("RoleIds list must not be empty.");
        }
    }
}
