using Application.Abstractions.Messaging;
using Application.DataTransferObject;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.User.Commands.PasswordReset.Request
{
    internal sealed class UserPasswordResetRequestCommandHandler : ICommandHandler<UserPasswordResetRequestCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IMailoutboxRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserPasswordResetRequestCommandHandler(
            IMapper mapper,
            IMailoutboxRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UserPasswordResetRequestCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
