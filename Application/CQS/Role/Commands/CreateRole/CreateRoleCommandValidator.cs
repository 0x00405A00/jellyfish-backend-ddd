using FluentValidation;

namespace Application.CQS.Role.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(command => command.CreatedBy).NotEmpty().WithMessage("CreatedBy must not be empty.");
            RuleFor(command => command.Name).NotEmpty().MaximumLength(255).WithMessage("Name must not be empty and should not exceed 255 characters.");
        }
    }
}
