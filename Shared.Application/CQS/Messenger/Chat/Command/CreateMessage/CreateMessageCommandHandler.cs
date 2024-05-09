using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Messages;
using Domain.Errors;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.CreateMessage
{
    internal sealed class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, List<MessageDTO>>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageOutboxRepository messageOutboxRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        private readonly IAntiVirus antiVirus;
        private readonly IAzureAdultContentDetection azureAdultContentDetection;

        public CreateMessageCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IAntiVirus antiVirus,
            IAzureAdultContentDetection azureAdultContentDetection,
            IChatRepository chatRepository,
            IMessageRepository messageRepository,
            IMessageOutboxRepository messageOutboxRepository,
            IUserRepository userRepository,
            IMediaService mediaService)
        {
            this.mediator = mediator;
            _mapper = mapper;
            this.antiVirus = antiVirus;
            this.azureAdultContentDetection = azureAdultContentDetection;
            _chatRepository = chatRepository;
            this._messageRepository = messageRepository;
            this.messageOutboxRepository = messageOutboxRepository;
            _userRepository = userRepository;
            this.mediaService = mediaService;
        }


        public async Task<Result<List<MessageDTO>>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Chats.Chat chat = await _chatRepository.GetAsync(x=>x.Id == request.ChatId.ToIdentification<ChatId>());
            if(chat is null)
            {
                return Result<List<MessageDTO>>.Failure("chat is not existing", Error.ERROR_CODE.NotFound);
            }
            var userId = request.MessageCreatorId.ToIdentification<UserId>();
            if(!chat.IsChatMember(userId))
            {
                return Result<List<MessageDTO>>.Failure("you are not a member of the chat", Error.ERROR_CODE.NotFound);
            }
            List<Error> errors = new List<Error>();
            var chatMember = chat.GetChatMemberById(userId);
            List<Message> createdMessages = new List<Message>(request.Messages.Count);
            try
            {
                foreach(var message in request.Messages)
                {
                    try
                    {
                        var text = message.Text;
                        var location = message.Location;

                        //Prüfung des Content in 'UploadProfilePictureCommandValidator'
                        MediaContent mediaContent = null;
                        var messageId = Message.NewId();
                        if (message.HasBase64ContentSet)
                        {
                            var mimeType = message.BinaryContentMimeType;
                            var base64Content = message.BinaryContentBase64;
                            byte[] binaryData = Convert.FromBase64String(base64Content);

                            MediaContentDTO mediaContentDTO = MediaContentDTO.Parse(binaryData, antiVirus,azureAdultContentDetection);
                            var filePath = mediaService.CheckContent(mediaContentDTO, cancellationToken);
                            mediaContent = MediaContent.Parse(binaryData, mimeType);
                        }
                        var createdTime = (message.CreatedTime ?? DateTime.UtcNow).ToTypedDateTime();
                        Message messageEntity = Message.Create(
                            messageId,
                            chat.Id,
                            userId,
                            text,
                            mediaContent,
                            createdTime,//Prüfung des CreatedTime auf null-Wert in 'UploadProfilePictureCommandValidator'
                            userId,null,null,null,
                            null,
                            location);

                        var createMessageEntity = chat.AddMessage(messageEntity);
                        messageEntity.SetCreated(userId);
                        _messageRepository.Add(messageEntity);
                        foreach(var chatUser in chat.Members)
                        {
                            var messageOutBoxId = MessageOutbox.NewId();
                            var foreignChatMember = chatUser.UserForeignKey;
                            var messageOutbox = MessageOutbox.Create(
                                messageOutBoxId,
                                messageId,
                                foreignChatMember,
                                createdTime,
                                null,
                                null);
                            messageOutboxRepository.Add(messageOutbox);
                        }
                        createdMessages.Add(messageEntity); 
                    }
                    catch(Exception ex)
                    {
                        errors.Add(new Error(ex.Message, Error.ERROR_CODE.UnproccesableEntity));
                    }

                }
                //save nur wenn errs collected is empty
            }
            catch (Exception ex)
            {
                return Result<List<MessageDTO>>.Failure("cant save messages to chat", Error.ERROR_CODE.Exception);
            }

            var dto = _mapper.Map<List<MessageDTO>>(createdMessages);
            var result = Result<List<MessageDTO>>.Success(dto);
            if (errors.Any())
            {
                result.AddError(errors);
            }
            return result;
        }

    }
}
