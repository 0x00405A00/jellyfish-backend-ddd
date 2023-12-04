using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;
using Shared.MimePart;

namespace Application.CQS.Messenger.Chat.Command.DeleteMessage
{
    internal sealed class DeleteMessageCommandHandler : ICommandHandler<DeleteMessageCommand, Guid>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly MediaService mediaService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public DeleteMessageCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IChatRepository chatRepository,
            IUserRepository userRepository,
            MediaService mediaService,
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
