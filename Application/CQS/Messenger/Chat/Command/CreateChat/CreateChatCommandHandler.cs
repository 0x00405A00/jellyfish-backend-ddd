using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.CreateChat
{
    internal sealed class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, ChatDTO>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateChatCommandHandler(
            IMapper mapper,
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            this._userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<ChatDTO>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            byte[] pictureBinary = null;
            if(!String.IsNullOrWhiteSpace(request.Picture))
            {
                pictureBinary = Convert.FromBase64String(request.Picture);
            }
            var picture = Picture.Parse(pictureBinary);
            var createdBy = await _userRepository.GetAsync(x => x.Uuid == request.ChatOwnerUuid);
            ICollection<Domain.Entities.User.User> members = null;
            ICollection<ChatMember> chatMembers = new List<ChatMember>();
            request.Members.Add(request.ChatOwnerUuid);
            members = await _userRepository.ListAsync(x => request.Members.Contains(x.Uuid));
            Domain.Entities.Chats.Chat chat = null;
            try
            {
                members.ToList().ForEach(x => {
                    bool isAdmin = request.ChatOwnerUuid == x.Uuid.ToGuid();
                    var chatMember = ChatMember.Create(x, isAdmin, DateTime.Now, x.LastModifiedTime, null);
                    chatMembers.Add(chatMember);
                });
                chat = Domain.Entities.Chats.Chat.Create(new Domain.Entities.Chats.ChatId(Guid.NewGuid()),
                                                             createdBy,
                                                             request.ChatName,
                                                             request.ChatDescription,
                                                             picture,
                                                             chatMembers,
                                                             null,
                                                             DateTime.Now,
                                                             null,
                                                             null);

            }
            catch(Exception ex)
            {
                return Result<ChatDTO>.Failure("chat create failed");
            }

            _chatRepository.Add(chat);
            var result = Result<ChatDTO>.Success();
            var mapValue = _mapper.Map<ChatDTO>(chat);
            result.Value = mapValue;
            return result;
        }

    }
}
