using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.UpdateMessage
{
    internal sealed class UpdateMessageCommandHandler : ICommandHandler<UpdateMessageCommand, MessageDTO>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public UpdateMessageCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            IMediaService mediaService)
        {
            this.mediator = mediator;
            _mapper = mapper;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            this.mediaService = mediaService;
        }


        public async Task<Result<MessageDTO>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {

            var modifiedByUser = await _userRepository.GetAsync(x => x.Id == request.ModifiedById.ToIdentification<UserId>());
            if (modifiedByUser is null)
            {
                return Result<MessageDTO>.Failure($"modifiedby-user not found");
            }
            var message = await _messageRepository.GetAsync(x => x.Id == request.MessageId.ToIdentification<MessageId>() && request.ChatId.ToIdentification<ChatId>() == x.Chat.Id);
            if (message is null)
            {
                return Result<MessageDTO>.Failure($"message not found");
            }

            var newMessage = request.messageDTOs;
            if (newMessage.HasBase64ContentSet)
            {
                MediaContent mediaContent = null;
                try
                {

                    mediaContent = MediaContent.ParseBase64(newMessage.BinaryContentBase64, newMessage.BinaryContentMimeType);
                }
                catch (Exception ex)
                {
                    return Result<MessageDTO>.Failure($"cant processing the media content");
                }
                message.UpdateMediaContent(modifiedByUser.Id, mediaContent);
            }
            if (!String.IsNullOrWhiteSpace(newMessage.Text))
            {
                message.UpdateText(modifiedByUser.Id, newMessage.Text);    
            }

            _messageRepository.Update(message);

            var dto = _mapper.Map<MessageDTO>(message);
            return Result<MessageDTO>.Success(dto);
        }

    }
}
