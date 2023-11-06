using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.User.Commands.PasswordReset.Request
{
    /// <summary>
    /// First step of Password reset process
    /// </summary>
    internal sealed class UserPasswordResetRequestCommandHandler : ICommandHandler<UserPasswordResetRequestCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserPasswordResetRequestCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UserPasswordResetRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.Email.ToLower() == request.Email.ToLower());
            if (user == null) {
                return Result<bool>.Failure("user not found");
            }
            try
            {
                user.ResetPasswordRequest();
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            try
            {
                _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
            user.DomainEvents.ToList().ForEach(e =>
            {
                mediator.Publish(e);
            });
            return Result<bool>.Success(true);
        }
    }
}
