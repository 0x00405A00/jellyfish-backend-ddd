using Application.DataTransferObject;
using AutoMapper;

namespace Application.Mapper
{
    public class DomainModelToDTOMappingProfile : Profile
    {
        public DomainModelToDTOMappingProfile()
        {
            CreateMap<Domain.ValueObjects.PhoneNumber, string>()
                .ConvertUsing(src => src.PhoneNumb);
            CreateMap<Domain.ValueObjects.Email, string>()
                .ConvertUsing(src => src.EmailValue);
            CreateMap<Domain.ValueObjects.Picture, string>()
                .ConvertUsing(src => Convert.ToBase64String(src.Data));

            CreateMap<Domain.Entities.Message.MessageId, Guid>()
                .ConvertUsing(src => src.Id);
            CreateMap<Guid, Domain.Entities.Message.MessageId>()
                .ConvertUsing(src => new Domain.Entities.Message.MessageId(src));
            CreateMap<Domain.Entities.Message.Message, Application.DataTransferObject.Messenger.MessageDTO>();

            CreateMap<Domain.Entities.Chats.ChatId, Guid>()
                .ConvertUsing(src => src.Id);
            CreateMap<Guid, Domain.Entities.Chats.ChatId>()
                .ConvertUsing(src => new Domain.Entities.Chats.ChatId(src));
            CreateMap<Domain.Entities.Chats.Chat, Application.DataTransferObject.Messenger.ChatDTO>();/*
                ForMember(src => src.PictureBase64, dst => dst.ConvertUsing(new ByteArrayToBase64StringConverter()));*/

            CreateMap<Domain.Entities.Role.RoleId, Guid>()
                .ConvertUsing(src => src.Id);
            CreateMap<Guid, Domain.Entities.Role.RoleId>()
                .ConvertUsing(src => new Domain.Entities.Role.RoleId(src));
            CreateMap<Domain.Entities.Role.Role, RoleDTO>();

            CreateMap<Domain.Entities.User.UserId, Guid>()
                .ConvertUsing(src => src.Id);
            CreateMap<Guid, Domain.Entities.User.UserId>()
                .ConvertUsing(src => new Domain.Entities.User.UserId(src));
            CreateMap<Domain.Entities.User.User, UserDTO>();
            CreateMap<Domain.Entities.User.User, Application.DataTransferObject.Messenger.MessengerUserDTO>();/*.
                ForMember(src => src.PictureBase64, dst => dst.ConvertUsing(new ByteArrayToBase64StringConverter())); */
            CreateMap<Domain.Entities.User.UserTypeId, Guid>()
                .ConvertUsing(src => src.Id);
            CreateMap<Guid, Domain.Entities.User.UserTypeId>()
                .ConvertUsing(src => new Domain.Entities.User.UserTypeId(src));
            CreateMap<Domain.Entities.User.UserType, UserTypeDTO>();
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
