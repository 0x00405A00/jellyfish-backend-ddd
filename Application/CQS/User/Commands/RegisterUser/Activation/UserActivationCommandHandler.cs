using Application.Abstractions.Messaging;
using Application.DataTransferObject;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.User.Commands.RegisterUser.Activation
{
    internal sealed class UserActivationCommandHandler : ICommandHandler<UserActivationCommand, UserDTO>
    {
        private readonly IMapper _mapper;
        private readonly IMailoutboxRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserActivationCommandHandler(
            IMapper mapper,
            IMailoutboxRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDTO>> Handle(UserActivationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
