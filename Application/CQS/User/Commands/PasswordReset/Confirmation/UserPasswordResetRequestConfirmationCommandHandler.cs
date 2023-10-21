using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.User.Commands.PasswordReset.Confirmation
{
    internal sealed class UserPasswordResetRequestConfirmationCommandHandler : ICommandHandler<UserPasswordResetRequestConfirmationCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IMailoutboxRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserPasswordResetRequestConfirmationCommandHandler(
            IMapper mapper,
            IMailoutboxRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UserPasswordResetRequestConfirmationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
