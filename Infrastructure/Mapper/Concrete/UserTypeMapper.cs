using Domain.Extension;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;

namespace Infrastructure.Mapper.Concrete
{
    internal class UserTypeMapper : AbstractMapper<Domain.Entities.User.UserType, UserType>
    {
        public override async Task<UserType> MapToDatabaseEntity(Domain.Entities.User.UserType entity, bool mapRelationObjects)
        {
            if (entity == null)
                return null;
            UserType userType = new UserType();
            userType.Name = entity.Name;
            userType.CreatedTime = entity.CreatedTime;  
            userType.LastModifiedTime = entity.LastModifiedTime;
            userType.DeletedTime = entity.DeletedTime;
            return  userType;
        }

        public override async Task<Domain.Entities.User.UserType> MapToDomainEntity(UserType entity, bool withRelations)
        {
            if (entity == null)
                return null;
            var userTypeId = entity.Uuid.ToIdentification<Domain.Entities.User.UserTypeId>();
            return Domain.Entities.User.UserType.Create(
                userTypeId,
                entity.Name,
                (DateTime)entity.CreatedTime!,
                entity.LastModifiedTime,
                entity.DeletedTime,
                null,
                null,
                null);
        }
    }
}
