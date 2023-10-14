using FluentValidation;

namespace Application.CQS.User.Commands.Roles.RevokeRole
{
    internal class RevokeRoleCommandValidator : AbstractValidator<RevokeRoleCommand>
    {
        public RevokeRoleCommandValidator()
        {

        }
    }
}
