using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Messages;
using Domain.Errors;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.CreateMessage
{
    internal sealed class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, List<MessageDTO>>
    {
        private readonly IChatRepository _chatRepository;
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
            IUserRepository userRepository,
            IMediaService mediaService)
        {
            this.mediator = mediator;
            _mapper = mapper;
            this.antiVirus = antiVirus;
            this.azureAdultContentDetection = azureAdultContentDetection;
            _chatRepository = chatRepository;
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
            try
            {
                foreach(var message in request.Messages)
                {
                    try
                    {
                        var text = message.Text;

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

                        Message messageEntity = Message.Create(
                            messageId,
                            chat.Id,
                            chatMember.User,
                            null,
                            null,
                            text,
                            mediaContent,
                            message.CreatedTime ?? DateTime.Now,//Prüfung des CreatedTime auf null-Wert in 'UploadProfilePictureCommandValidator'
                            null,
                            null);
                        var createMessageEntity = chat.AddMessage(messageEntity);
                        messageEntity.SetCreated(chatMember.User);
                    }
                    catch(Exception ex)
                    {
                        errors.Add(new Error(ex.Message, Error.ERROR_CODE.UnproccesableEntity));
                    }

                }
                //save nur wenn errs collected is empty
                _chatRepository.Update(chat);
                _chatRepository.PublishDomainEvents(chat, mediator);
            }
            catch (Exception ex)
            {
                return Result<List<MessageDTO>>.Failure("cant save messages to chat", Error.ERROR_CODE.Exception);
            }

            var dto = _mapper.Map<List<MessageDTO>>(null);
            var result = Result<List<MessageDTO>>.Success(dto);
            result.Error = errors.Any()?errors.First():null;  
            return result;
        }

    }
}
