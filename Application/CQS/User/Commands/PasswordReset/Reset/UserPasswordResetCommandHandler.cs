using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.User.Commands.PasswordReset.Reset
{
    internal sealed class UserPasswordResetCommandHandler : ICommandHandler<UserPasswordResetCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IMailoutboxRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserPasswordResetCommandHandler(
            IMapper mapper,
            IMailoutboxRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UserPasswordResetCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
