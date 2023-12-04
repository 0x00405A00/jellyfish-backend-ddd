using Domain.Extension;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;

namespace Infrastructure.Mapper.Concrete
{
    internal class RoleMapper : AbstractMapper<Domain.Entities.Role.Role, Role>
    {
        public override async Task<Role> MapToDatabaseEntity(Domain.Entities.Role.Role entity, bool mapRelationObjects)
        {
            if (entity == null)
                return null;
            Role role = new Role();

            role.Uuid =entity.Uuid.ToGuid();
            role.Name = entity.Name;
            role.CreatedTime = entity.CreatedTime;  
            role.LastModifiedTime = entity.LastModifiedTime;    
            role.DeletedTime = entity.DeletedTime;
            role.Description = entity.Name;
            return role;
        }

        public override async Task<Domain.Entities.Role.Role> MapToDomainEntity(Role entity, bool withRelations)
        {
            if (entity == null)
                return null;
            var roleId = entity.Uuid.ToIdentification<Domain.Entities.Role.RoleId>();

            return Domain.Entities.Role.Role.Create(
                roleId,
                entity.Name,
                entity.Description,
                entity.CreatedTime??DateTime.MinValue,
                entity.LastModifiedTime,
                entity.DeletedTime);
        }
    }
}
