using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User;
using Domain.Extension;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;
using Shared.MimePart;
using System.Buffers.Text;
using System.Text;

namespace Application.CQS.Messenger.Chat.Command.CreateMessage
{
    internal sealed class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, List<MessageDTO>>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly MediaService mediaService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public CreateMessageCommandHandler(
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


        public async Task<Result<List<MessageDTO>>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.GetAsync(x=>x.Uuid == request.ChatId);
            if(chat is null)
            {
                return Result<List<MessageDTO>>.Failure("chat is not existing", Domain.Error.Error.ERROR_CODE.NotFound);
            }
            var userId = request.MessageCreatorId.ToIdentification<UserId>();
            if(!chat.IsChatMember(userId))
            {
                return Result<List<MessageDTO>>.Failure("you are not a member of the chat", Domain.Error.Error.ERROR_CODE.NotFound);
            }
            var chatMember = chat.GetChatMemberById(userId);
            try
            {
                foreach(var message in request.Messages)
                {
                    var text = message.Text;
                    var mimeType = message.BinaryContentMimeType;
                    var base64Content = message.BinaryContentBase64;
                    byte[] binaryData = Convert.FromBase64String(base64Content);

                    var filePath = mediaService.CreateChatMessageAttachment(request.ChatId,, MimeExtension.GetFileExtension(mimeType), binaryData, cancellationToken);
                    var mediaContent = MediaContent.Parse(binaryData, filePath, mimeType);

                    var messageEntity = chat.AddMessage(chatMember.User, text, mediaContent);
                }
                _chatRepository.UpdateAsync(chat);
            }
            catch (Exception ex)
            {

            }

            var dto = _mapper.Map<List<MessageDTO>>(null);
            return Result<List<MessageDTO>>.Success(dto);
        }

    }
}
