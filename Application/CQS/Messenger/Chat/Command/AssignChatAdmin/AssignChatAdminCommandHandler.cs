using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;

namespace Application.CQS.Messenger.Chat.Command.AssignChatAdmin
{
    internal sealed class AssignChatAdminCommandHandler : ICommandHandler<AssignChatAdminCommand, bool>
    {
        private readonly IUserTypeRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AssignChatAdminCommandHandler(
            IMapper mapper,
            IUserTypeRepository chatRepository,
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


        public async Task<Result<bool>> Handle(AssignChatAdminCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
