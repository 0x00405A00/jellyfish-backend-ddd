using AutoMapper;
using Domain.Entities.Chats;
using Domain.Entities.Mails;
using Domain.Entities.Messages;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;
using Shared.Http;

namespace Application.Shared.Mapper
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

            CreateMap<CustomDateTime, DateTime>()
                .ConvertUsing(dst => dst.DateTime);

            CreateMap<Domain.ValueObjects.PhoneNumber, string>()
                .ConvertUsing(dst => dst.PhoneNumb);
            CreateMap<Domain.ValueObjects.Email, string>()
                .ConvertUsing(dst => dst.EmailValue);
            CreateMap<Domain.ValueObjects.Picture, string>()
                .ConvertUsing(dst => Convert.ToBase64String(dst.Data));

            CreateMap<Guid, AuthId>()
                .ConvertUsing(dst => new AuthId(dst));
            CreateMap<AuthId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<MessageId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, MessageId>()
                .ConvertUsing(dst => new MessageId(dst));
            CreateMap<ChatId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, ChatId>()
                .ConvertUsing(dst => new ChatId(dst));
            CreateMap<RoleId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, RoleId>()
                .ConvertUsing(dst => new RoleId(dst));
            CreateMap<Identification, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<UserId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, UserId>()
                .ConvertUsing(dst => new UserId(dst));
            CreateMap<UserTypeId, Guid>()
                .ConvertUsing(dst => dst.Id);
            CreateMap<Guid, UserTypeId>()
                .ConvertUsing(dst => new UserTypeId(dst));

            CreateMap<Message, MessageDTO>()
                .ForMember(dst => dst.ChatId, dst => dst.MapFrom(x => x.ChatForeignKey))
                .ForMember(dst => dst.BinaryContentBase64, dst => dst.MapFrom(x => x.MediaContent.ToString()))
                .ForMember(dst => dst.BinaryContentMimeType, dst => dst.MapFrom(x => x.MediaContent.FileExtension))
                .ForMember(dst => dst.OwnerUuid, dst => dst.MapFrom(x => x.CreatedByUserForeignKey))
                .ForMember(dst => dst.OwnerName, dst => dst.MapFrom(x => x.CreatedByUser.UserName))
                .ForMember(dst => dst.DeletedByUserForeignKey, dst => dst.MapFrom(x => x.DeletedByUserForeignKey))
                .ForMember(dst => dst.CreatedByUserForeignKey, dst => dst.MapFrom(x => x.CreatedByUserForeignKey))
                .ForMember(dst => dst.LastModifiedByUserForeignKey, dst => dst.MapFrom(x => x.LastModifiedByUserForeignKey))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Chat, ChatDTO>()
                .ForMember(dst => dst.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dst => dst.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dst => dst.MembersAsDTO, src => src.MapFrom(x => x.Members.Select(x => x.User).ToList()))//Chat.ChatRelationToUser.User to MessengerUserDTO
                .ForMember(dst => dst.MemberIds, src => src.MapFrom(x => x.Members.Select(x => x.UserForeignKey.ToGuid()).ToList()))
                .ForMember(dst => dst.AdminIds, src => src.MapFrom(x => x.Admins.Select(x => x.UserForeignKey.ToGuid()).ToList()))
                .ForMember(dst => dst.MembersAsUsernames, src => src.MapFrom(x => x.Members.Select(x => x.User.UserName).ToList()))
                .ForMember(dst => dst.AdminsAsUsernames, src => src.MapFrom(x => x.Admins.Select(x => x.User.UserName).ToList()))
                .ForMember(dst => dst.PictureBase64, dst => dst.MapFrom(x => x.Picture.ToString()))
                .ForMember(dst => dst.PictureMimeType, dst => dst.MapFrom(x => x.Picture.FileExtension))
                .ForMember(dst => dst.Messages, src => src.MapFrom(x => x.Messages))
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .ForMember(dst => dst.DeletedByUserForeignKey, dst => dst.MapFrom(x => x.DeletedByUserForeignKey))
                .ForMember(dst => dst.CreatedByUserForeignKey, dst => dst.MapFrom(x => x.CreatedByUserForeignKey))
                .ForMember(dst => dst.LastModifiedByUserForeignKey, dst => dst.MapFrom(x => x.LastModifiedByUserForeignKey))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<Role, RoleDTO>()
                .ForMember(dst => dst.Name, dst => dst.MapFrom(x => x.Name))
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .ForMember(dst => dst.DeletedByUserForeignKey, dst => dst.MapFrom(x => x.DeletedByUserForeignKey))
                .ForMember(dst => dst.CreatedByUserForeignKey, dst => dst.MapFrom(x => x.CreatedByUserForeignKey))
                .ForMember(dst => dst.LastModifiedByUserForeignKey, dst => dst.MapFrom(x => x.LastModifiedByUserForeignKey))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<User, UserDTO>()
                .ForMember(dst => dst.DateOfBirth, dst => dst.MapFrom(x => x.DateOfBirth))
                .ForMember(dst => dst.Roles, dst => dst.MapFrom(x => x.UserHasRelationToRoles.Select(x => x.Role).ToList()))
                .ForMember(dst => dst.Friends, dst => dst.MapFrom(x => x.GetFriends().Select(x => x.UserFriendForeignKey.ToGuid()).ToList()))
                .ForMember(dst => dst.FriendshipRequests, dst => dst.MapFrom(x => x.GetFriendshipRequests()))
                .ForMember(dst => dst.Phone, dst => dst.MapFrom(x => x.Phone.ToString()))
                .ForMember(dst => dst.Password, dst => dst.MapFrom(x => string.Empty))//securtiy: password shouldnt transfer over network
                .ForMember(dst => dst.PasswordConfirm, dst => dst.MapFrom(x => string.Empty))//securtiy: password shouldnt transfer over network
                .ForMember(dst => dst.UserTypeUuid, dst => dst.MapFrom(x => x.UserType.Id.ToGuid()))
                .ForMember(dst => dst.PictureUrl, dst => dst.MapFrom(x => Extension.Create(contentDeliveryUrl + x.Id.ToString()+"/"+"profile-img")))
                .ForMember(dst => dst.PictureMimeType, dst => dst.MapFrom(x => x.Picture.FileExtension))
                .ForMember(dst => dst.Email, dst => dst.MapFrom(x => x.Email.ToString()))
                .ForMember(dst => dst.Roles, dst=> dst.MapFrom(x=>x.UserHasRelationToRoles.ToList()))
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .ForMember(dst => dst.DeletedByUserForeignKey, dst => dst.MapFrom(x => x.DeletedByUserForeignKey))
                .ForMember(dst => dst.CreatedByUserForeignKey, dst => dst.MapFrom(x => x.CreatedByUserForeignKey))
                .ForMember(dst => dst.LastModifiedByUserForeignKey, dst => dst.MapFrom(x => x.LastModifiedByUserForeignKey))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<User, MessengerUserDTO>()
                .ForMember(dst => dst.DateOfBirth, dst => dst.MapFrom(x => x.DateOfBirth))
                .ForMember(dst => dst.Friends, dst => dst.MapFrom(x => x.GetFriends().Select(x => x.UserFriendForeignKey.ToGuid()).ToList()))
                .ForMember(dst => dst.FriendshipRequests, dst => dst.MapFrom(x => x.GetFriendshipRequests()))
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .ForMember(dst => dst.DeletedByUserForeignKey, dst => dst.MapFrom(x => x.DeletedByUserForeignKey))
                .ForMember(dst => dst.CreatedByUserForeignKey, dst => dst.MapFrom(x => x.CreatedByUserForeignKey))
                .ForMember(dst => dst.LastModifiedByUserForeignKey, dst => dst.MapFrom(x => x.LastModifiedByUserForeignKey))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<FriendshipRequest, FriendshipRequestDTO>()
                .ForMember(dst => dst.RequestUserForeignKey, dst => dst.MapFrom(x => x.RequestUserForeignKey))
                .ForMember(dst => dst.TargetUserForeignKey, dst => dst.MapFrom(x => x.TargetUserForeignKey))
                .ForMember(dst => dst.TargetUserRequestMessage, dst => dst.MapFrom(x => x.TargetUserRequestMessage))
                .ForMember(dst => dst.TargetUserName, dst => dst.MapFrom(x => x.TargetUser.UserName))
                .ForMember(dst => dst.RequesterUserName, dst => dst.MapFrom(x => x.RequesterUser.UserName))
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<UserHasRelationToRole, RoleDTO>()
                .ForMember(dst=>dst.Name,src=>src.MapFrom(x=>x.Role.Name))
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .ForMember(dst => dst.DeletedByUserForeignKey, dst => dst.MapFrom(x => x.DeletedByUserForeignKey))
                .ForMember(dst => dst.CreatedByUserForeignKey, dst => dst.MapFrom(x => x.CreatedByUserForeignKey))
                .ForMember(dst => dst.LastModifiedByUserForeignKey, dst => dst.MapFrom(x => x.LastModifiedByUserForeignKey))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<UserDTO, UserHasRelationToFriend>()
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .ForMember(dst => dst.DeletedByUserForeignKey, dst => dst.MapFrom(x => x.DeletedByUserForeignKey))
                .ForMember(dst => dst.CreatedByUserForeignKey, dst => dst.MapFrom(x => x.CreatedByUserForeignKey))
                .ForMember(dst => dst.LastModifiedByUserForeignKey, dst => dst.MapFrom(x => x.LastModifiedByUserForeignKey))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MailOutbox, MailOutboxDTO>()
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MailOutboxAttachment, MailOutboxAttachmentDTO>()
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<MailOutboxRecipient, MailOutboxRecipientDTO>()
                .ForMember(dst => dst.EmailType, dst => dst.MapFrom(x => x.SendingType.Name))
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<UserType, UserTypeDTO>()
                .ForMember(dst => dst.CreatedTime, dst => dst.MapFrom(x => x.CreatedTime))
                .ForMember(dst => dst.LastModifiedTime, dst => dst.MapFrom(x => x.LastModifiedTime))
                .ForMember(dst => dst.DeletedTime, dst => dst.MapFrom(x => x.DeletedTime))
                .ForMember(dst => dst.DeletedByUserForeignKey, dst => dst.MapFrom(x => x.DeletedByUserForeignKey))
                .ForMember(dst => dst.CreatedByUserForeignKey, dst => dst.MapFrom(x => x.CreatedByUserForeignKey))
                .ForMember(dst => dst.LastModifiedByUserForeignKey, dst => dst.MapFrom(x => x.LastModifiedByUserForeignKey))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
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
