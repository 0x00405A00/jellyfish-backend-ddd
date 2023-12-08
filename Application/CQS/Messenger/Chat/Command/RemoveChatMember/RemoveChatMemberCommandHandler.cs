using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;

namespace Application.CQS.Messenger.Chat.Command.RemoveChatMember
{
    internal sealed class RemoveChatMemberCommandHandler : ICommandHandler<RemoveChatMemberCommand, bool>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RemoveChatMemberCommandHandler(
            IMapper mapper,
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IMediaService mediaService,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            this.mediaService = mediaService;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<bool>> Handle(RemoveChatMemberCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
