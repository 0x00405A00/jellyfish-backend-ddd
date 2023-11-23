using AutoMapper;
using Domain.Primitives;
using Domain.ValueObjects;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;
using System.Security.Cryptography;

namespace Application.Mapper
{
    public class DomainModelToDTOMappingProfile : Profile
    {
        public DomainModelToDTOMappingProfile()
        {
            CreateMap<DateOnly, DateTime>()
                .ConvertUsing(dst => new DateTime(dst.Year, dst.Month, dst.Day));
            CreateMap<DateTime, DateOnly>()
                .ConvertUsing(dst => DateOnly.FromDateTime(dst));

            CreateMap<Domain.ValueObjects.PhoneNumber, string>()
                .ConvertUsing(dst => dst.PhoneNumb);
            CreateMap<Domain.ValueObjects.Email, string>()
                .ConvertUsing(dst => dst.EmailValue);
            CreateMap<Domain.ValueObjects.Picture, string>()
                .ConvertUsing(dst => Convert.ToBase64String(dst.Data));

            CreateMap<Domain.Entities.Message.MessageId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, Domain.Entities.Message.MessageId>()
                .ConvertUsing(dst => new Domain.Entities.Message.MessageId(dst));
            CreateMap<Domain.Entities.Message.Message, MessageDTO>()
                .ForMember(dst => dst.ChatId, dst => dst.MapFrom(x => x.Chat.ToGuid()))
                .ForMember(dst => dst.BinaryContentBase64, dst => dst.MapFrom(x => x.MediaContent.ToString()))
                .ForMember(dst => dst.BinaryContentMimeType, dst => dst.MapFrom(x => x.MediaContent.FileExtension))
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.OwnerUuid, dst => dst.MapFrom(x => x.Owner.Uuid.ToGuid()));

            CreateMap<Domain.Entities.Chats.ChatId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, Domain.Entities.Chats.ChatId>()
                .ConvertUsing(dst => new Domain.Entities.Chats.ChatId(dst));

            CreateMap<ChatMember, Domain.ValueObjects.UserFriend>();
            CreateMap<Domain.Entities.Chats.Chat, ChatDTO>()
                .ForMember(dst => dst.Members, src => src.MapFrom(x => x.Members.Select(x => x.User.Uuid.ToGuid()).ToList()))
                .ForMember(dst => dst.Admins, src => src.MapFrom(x => x.Admins.Select(x => x.User.Uuid.ToGuid()).ToList()))
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.PictureBase64, dst => dst.MapFrom(x => x.Picture.ToString()))
                .ForMember(dst => dst.PictureMimeType, dst => dst.MapFrom(x => x.Picture.FileExtension))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.Messages, src => src.MapFrom(x => x.Messages));

            CreateMap<Domain.Entities.Role.RoleId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, Domain.Entities.Role.RoleId>()
                .ConvertUsing(dst => new Domain.Entities.Role.RoleId(dst));
            CreateMap<Domain.Entities.Role.Role, RoleDTO>()
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Uuid.ToGuid()));

            CreateMap<Domain.Entities.User.UserId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, Domain.Entities.User.UserId>()
                .ConvertUsing(dst => new Domain.Entities.User.UserId(dst));
            CreateMap<Domain.Entities.User.User, UserDTO>()
                .ForMember(dst => dst.Roles, dst => dst.MapFrom(x => x.Roles.Select(x => x.Role).ToList()))
                .ForMember(dst => dst.Friends, dst => dst.MapFrom(x => x.Friends.Select(x => x.Friend.Uuid.ToGuid()).ToList()))
                .ForMember(dst => dst.FriendshipRequests, dst => dst.MapFrom(x => x.FriendshipRequests))
                .ForMember(dst => dst.Phone, dst => dst.MapFrom(x => x.Phone.ToString()))
                .ForMember(dst => dst.Password, dst => dst.MapFrom(x => string.Empty))//securtiy: password shouldnt transfer over network
                .ForMember(dst => dst.PasswordConfirm, dst => dst.MapFrom(x => string.Empty))//securtiy: password shouldnt transfer over network
                .ForMember(dst => dst.UserTypeUuid, dst => dst.MapFrom(x => x.UserType.Uuid.ToGuid()))
                .ForMember(dst => dst.PictureBase64, dst => dst.MapFrom(x => x.Picture.ToString()))
                .ForMember(dst => dst.PictureMimeType, dst => dst.MapFrom(x => x.Picture.FileExtension))
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.Email, dst => dst.MapFrom(x => x.Email.ToString()));
            CreateMap<Domain.Entities.User.User, MessengerUserDTO>()
                .ForMember(dst => dst.Friends, dst => dst.MapFrom(x => x.Friends.Select(x => x.Friend).ToList()))
                .ForMember(dst => dst.FriendshipRequests, dst => dst.MapFrom(x => x.FriendshipRequests))
                .ForMember(dst => dst.PictureBase64, dst => dst.MapFrom(x => x.Picture.ToString()));

            /*CreateMap<Domain.ValueObjects.UserRole, RoleDTO>()
                .ForMember(src => src, dst => dst.MapFrom(x => x.Role));
            CreateMap<Domain.ValueObjects.FriendshipRequest, UserFriendshipRequestDTO>();
            CreateMap<Domain.ValueObjects.UserFriend, UserDTO>();*/

            CreateMap<UserFriendshipRequestDTO, Domain.ValueObjects.FriendshipRequest>();
            CreateMap<RoleDTO, Domain.ValueObjects.UserRole>();
            CreateMap<UserDTO, Domain.ValueObjects.UserFriend>();


            CreateMap<Infrastructure.DatabaseEntity.MailOutbox, MailOutboxDTO>();
            CreateMap<Infrastructure.DatabaseEntity.MailOutboxAttachment, MailOutboxAttachmentDTO>();
            CreateMap<Infrastructure.DatabaseEntity.MailOutboxRecipient, MailOutboxRecipientDTO>()
                .ForMember(dst => dst.EmailType, dst => dst.MapFrom(x => x.EmailTypeUu.Type));

            CreateMap<Domain.Entities.User.UserTypeId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, Domain.Entities.User.UserTypeId>()
                .ConvertUsing(dst => new Domain.Entities.User.UserTypeId(dst));
            CreateMap<Domain.Entities.User.UserType, UserTypeDTO>()
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Uuid.ToGuid()))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Uuid.ToGuid()));
        }
    }
    public class ByteArrayToBase64StringConverter : IValueConverter<byte[], string>
    {
        public string Convert(byte[] sourceMember, ResolutionContext context)
        {
            if (sourceMember != null)
            {
                return System.Convert.ToBase64String(sourceMember);
            }
            return null;
        }
    }
}
