using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;

namespace Application.CQS.Messenger.Chat.Command.AddChatMember
{
    internal sealed class AddChatMemberCommandHandler : ICommandHandler<AddChatMemberCommand, bool>
    {
        private readonly IUserTypeRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddChatMemberCommandHandler(
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


        public async Task<Result<bool>> Handle(AddChatMemberCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
