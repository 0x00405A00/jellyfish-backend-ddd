using AutoMapper;
using Domain.Entities.Chats;
using Domain.Entities.Mails;
using Domain.Entities.Messages;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives.Ids;
using Microsoft.Extensions.Configuration;
using Shared.Authentification.Claims;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;

namespace Application.Mapper
{
    public class DomainModelToDTOMappingProfile : Profile
    {
        public DomainModelToDTOMappingProfile(IConfiguration configuration)
        {
            var contentDeliverySection = configuration.GetSection("Infrastructure:ContentDelivery");
            var contentDeliveryUrl = contentDeliverySection.GetValue<string>("DeliveryUrl");

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

            CreateMap<MessageId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, MessageId>()
                .ConvertUsing(dst => new Domain.Entities.Message.MessageId(dst));
            CreateMap<Message, MessageDTO>()
                .ForMember(dst => dst.ChatId, dst => dst.MapFrom(x => x.Chat.ToGuid()))
                .ForMember(dst => dst.BinaryContentBase64, dst => dst.MapFrom(x => x.MediaContent.ToString()))
                .ForMember(dst => dst.BinaryContentMimeType, dst => dst.MapFrom(x => x.MediaContent.FileExtension))
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Id.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Id.ToGuid()))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Id.ToGuid()))
                .ForMember(dst => dst.OwnerUuid, dst => dst.MapFrom(x => x.Owner.Id.ToGuid()));

            CreateMap<ChatId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, ChatId>()
                .ConvertUsing(dst => new Domain.Entities.Chats.ChatId(dst));

            CreateMap<ChatMember, UserHasRelationToFriend>();
            CreateMap<Domain.Entities.Chats.Chat, ChatDTO>()
                .ForMember(dst => dst.Members, src => src.MapFrom(x => x.Members.Select(x => x.User.Id.ToGuid()).ToList()))
                .ForMember(dst => dst.Admins, src => src.MapFrom(x => x.Admins.Select(x => x.User.Id.ToGuid()).ToList()))
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Id.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Id.ToGuid()))
                .ForMember(dst => dst.PictureBase64, dst => dst.MapFrom(x => x.Picture.ToString()))
                .ForMember(dst => dst.PictureMimeType, dst => dst.MapFrom(x => x.Picture.FileExtension))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Id.ToGuid()))
                .ForMember(dst => dst.Messages, src => src.MapFrom(x => x.Messages));

            CreateMap<RoleId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, RoleId>()
                .ConvertUsing(dst => new Domain.Entities.Role.RoleId(dst));
            CreateMap<Role, RoleDTO>()
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Id.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Id.ToGuid()))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Id.ToGuid()));

            CreateMap<UserId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, UserId>()
                .ConvertUsing(dst => new Domain.Entities.User.UserId(dst));
            CreateMap<User, UserDTO>()
                .ForMember(dst => dst.Roles, dst => dst.MapFrom(x => x.UserRoles.Select(x => x.Role).ToList()))
                .ForMember(dst => dst.Friends, dst => dst.MapFrom(x => x.Friends.Select(x => x.Friend.Id.ToGuid()).ToList()))
                .ForMember(dst => dst.FriendshipRequests, dst => dst.MapFrom(x => x.RequestedFriendshipRequests))
                .ForMember(dst => dst.Phone, dst => dst.MapFrom(x => x.Phone.ToString()))
                .ForMember(dst => dst.Password, dst => dst.MapFrom(x => string.Empty))//securtiy: password shouldnt transfer over network
                .ForMember(dst => dst.PasswordConfirm, dst => dst.MapFrom(x => string.Empty))//securtiy: password shouldnt transfer over network
                .ForMember(dst => dst.UserTypeUuid, dst => dst.MapFrom(x => x.UserType.Id.ToGuid()))
                .ForMember(dst => dst.PictureUrl, dst => dst.MapFrom(x => Shared.Http.Extension.Create(contentDeliveryUrl + x.Picture.FilePath.ToString().Replace(@"\","/"))??null))
                .ForMember(dst => dst.PictureMimeType, dst => dst.MapFrom(x => x.Picture.FileExtension))
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Id.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Id.ToGuid()))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Id.ToGuid()))
                .ForMember(dst => dst.Email, dst => dst.MapFrom(x => x.Email.ToString()));
            CreateMap<User, MessengerUserDTO>()
                .ForMember(dst => dst.Friends, dst => dst.MapFrom(x => x.Friends.Select(x => x.Friend).ToList()))
                .ForMember(dst => dst.FriendshipRequests, dst => dst.MapFrom(x => x.RequestedFriendshipRequests))
                .ForMember(dst => dst.PictureBase64, dst => dst.MapFrom(x => x.Picture.ToString()));

            CreateMap<FriendshipRequest, FriendshipRequestDTO>();
            CreateMap<RoleDTO, UserHasRelationToRole>();
            CreateMap<UserDTO, UserHasRelationToFriend>();

            CreateMap<MailOutbox, MailOutboxDTO>();
            CreateMap<MailOutboxAttachment, MailOutboxAttachmentDTO>();
            CreateMap<MailOutboxRecipient, MailOutboxRecipientDTO>()
                .ForMember(dst => dst.EmailType, dst => dst.MapFrom(x => x.EmailType.Type));

            CreateMap<UserTypeId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, UserTypeId>()
                .ConvertUsing(dst => new Domain.Entities.User.UserTypeId(dst));
            CreateMap<UserType, UserTypeDTO>()
                .ForMember(dst => dst.DeletedByUserUuid, dst => dst.MapFrom(x => x.DeletedByUser.Id.ToGuid()))
                .ForMember(dst => dst.CreatedByUserUuid, dst => dst.MapFrom(x => x.CreatedByUser.Id.ToGuid()))
                .ForMember(dst => dst.LastModifiedByUserUuid, dst => dst.MapFrom(x => x.LastModifiedByUser.Id.ToGuid()));
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
