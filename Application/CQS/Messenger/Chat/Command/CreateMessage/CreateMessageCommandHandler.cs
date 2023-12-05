using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.User;
using Domain.Error;
using Domain.Extension;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using MediatR;
using Shared.DataTransferObject.Messenger;
using Shared.MimePart;

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
        private readonly IAntiVirus antiVirus;
        private readonly IAzureAdultContentDetection azureAdultContentDetection;

        public CreateMessageCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IAntiVirus antiVirus,
            IAzureAdultContentDetection azureAdultContentDetection,
            IChatRepository chatRepository,
            IUserRepository userRepository,
            MediaService mediaService,
            IUnitOfWork unitOfWork)
        {
            this.mediator = mediator;
            _mapper = mapper;
            this.antiVirus = antiVirus;
            this.azureAdultContentDetection = azureAdultContentDetection;
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
            List<Domain.Error.Error> errors = new List<Domain.Error.Error>();
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
                        var messageId = new Domain.Entities.Message.MessageId(Guid.NewGuid());
                        if (message.HasBase64ContentSet)
                        {
                            var mimeType = message.BinaryContentMimeType;
                            var base64Content = message.BinaryContentBase64;
                            byte[] binaryData = Convert.FromBase64String(base64Content);

                            MediaContentDTO mediaContentDTO = MediaContentDTO.Parse(binaryData, antiVirus,azureAdultContentDetection);
                            var filePath = mediaService.CheckContent(mediaContentDTO, cancellationToken);
                            mediaContent = MediaContent.Parse(binaryData, mimeType);
                        }

                        Domain.Entities.Message.Message messageEntity = Domain.Entities.Message.Message.Create(
                            messageId,
                            chat.Uuid,
                            chatMember.User,
                            text,
                            mediaContent,
                            message.CreatedTime ?? DateTime.Now,//Prüfung des CreatedTime auf null-Wert in 'UploadProfilePictureCommandValidator'
                            null,
                            null);
                        var createMessageEntity = chat.AddMessage(messageEntity);
                    }
                    catch(Exception ex)
                    {
                        errors.Add(new Error(ex.Message, Error.ERROR_CODE.UnproccesableEntity));
                    }

                }
                //save nur wenn errs collected is empty
                _chatRepository.UpdateAsync(chat);
                _chatRepository.PublishDomainEvents(chat, mediator);
            }
            catch (Exception ex)
            {
                return Result<List<MessageDTO>>.Failure("cant save messages to chat", Domain.Error.Error.ERROR_CODE.Exception);
            }

            var dto = _mapper.Map<List<MessageDTO>>(null);
            var result = Result<List<MessageDTO>>.Success(dto);
            result.Error = errors.Any()?errors.First():null;  
            return result;
        }

    }
}
