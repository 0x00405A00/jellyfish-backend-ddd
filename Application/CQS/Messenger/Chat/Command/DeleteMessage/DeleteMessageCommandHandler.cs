using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;

namespace Application.CQS.Messenger.Chat.Command.DeleteMessage
{
    internal sealed class DeleteMessageCommandHandler : ICommandHandler<DeleteMessageCommand, Guid>
    {
        private readonly IUserTypeRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public DeleteMessageCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IUserTypeRepository chatRepository,
            IUserRepository userRepository,
            IMediaService mediaService,
            IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            this.mediaService = mediaService;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<Guid>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
