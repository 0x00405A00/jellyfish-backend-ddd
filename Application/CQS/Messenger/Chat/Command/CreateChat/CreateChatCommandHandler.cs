﻿using Application.Abstractions.Messaging;
using AutoMapper;
using Domain.Entities.Chats;
using Domain.Extension;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.FileSys;
using Shared.DataTransferObject.Messenger;
using Shared.MimePart;

namespace Application.CQS.Messenger.Chat.Command.CreateChat
{
    internal sealed class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, ChatDTO>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediaService mediaService;
        private readonly IMapper _mapper;
        public CreateChatCommandHandler(
            IMapper mapper,
            IChatRepository chatRepository,
            IUserRepository userRepository,
            IMediaService mediaService)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
            this._userRepository = userRepository;
            this.mediaService = mediaService;
        }


        public async Task<Result<ChatDTO>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var createdBy = await _userRepository.GetAsync(x => x.Id.Id == request.ChatOwnerUuid);
            ICollection<Domain.Entities.Users.User> members = null;
            ICollection<ChatMember> chatMembers = new List<ChatMember>();
            request.Members.Add(request.ChatOwnerUuid);
            members = await _userRepository.ListAsync(x => request.Members.Contains(x.Id.Id));
            Domain.Entities.Chats.Chat chat = null;
            try
            {
                foreach(var member in members)
                {
                    bool isAdmin = request.ChatOwnerUuid == member.Id.ToGuid();
                    var chatMember = ChatMember.Create(Guid.NewGuid(), member, isAdmin, DateTime.Now, null, null);
                    chatMembers.Add(chatMember);
                }
                var id = new Domain.Entities.Chats.ChatId(Guid.NewGuid());
                Picture? picture = null;
                if (!String.IsNullOrWhiteSpace(request.Picture))
                {
                    byte[] pictureBinary = Convert.FromBase64String(request.Picture);
                    var path = mediaService.CreateChatPicture(id.ToGuid(),MimeExtension.GetFileExtension(request.PictureMimeType), pictureBinary, cancellationToken);
                    picture = Picture.Parse(pictureBinary, path, request.PictureMimeType);
                }

                chat = Domain.Entities.Chats.Chat.Create(id,
                                                             createdBy,
                                                             null,
                                                             null,
                                                             request.ChatName,
                                                             request.ChatDescription,
                                                             picture,
                                                             chatMembers,
                                                             null,
                                                             DateTime.Now,
                                                             null,
                                                             null);
                chat.SetCreated(createdBy);

            }
            catch(Exception ex)
            {
                return Result<ChatDTO>.Failure("chat create failed");
            }
            _chatRepository.Add(chat);
            var dto = _mapper.Map<ChatDTO>(chat);
            return Result<ChatDTO>.Success(dto);
        }

    }
}
