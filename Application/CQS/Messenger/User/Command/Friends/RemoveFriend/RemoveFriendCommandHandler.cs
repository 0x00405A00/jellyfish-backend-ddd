using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.Messenger.User.Command.Friends.RemoveFriend
{
    internal sealed class RemoveFriendCommandHandler : ICommandHandler<RemoveFriendCommand, bool>
    {
        private readonly IMailoutboxRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RemoveFriendCommandHandler(
            IMapper mapper,
            IMailoutboxRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<bool>> Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
