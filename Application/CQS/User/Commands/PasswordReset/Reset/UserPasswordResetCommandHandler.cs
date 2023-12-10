using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.User.Commands.PasswordReset.Reset
{
    /// <summary>
    /// Second and last step of password reset process
    /// </summary>
    internal sealed class UserPasswordResetCommandHandler : ICommandHandler<UserPasswordResetCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserPasswordResetCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(UserPasswordResetCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.PasswordResetToken == request.PasswordResetBase64Token && x.PasswordResetCode == request.PasswordResetCode);
            if(user==null)
            {
                return Result<bool>.Failure("user not found");
            }
            try
            {
                user.ResetPassword(
                    request.Password,
                    request.PasswordConfirm,
                    request.PasswordResetBase64Token,
                    request.PasswordResetCode);
            }
            catch(Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            _userRepository.PublishDomainEvents(user, mediator);
            return Result<bool>.Success(true);
        }
    }
}
