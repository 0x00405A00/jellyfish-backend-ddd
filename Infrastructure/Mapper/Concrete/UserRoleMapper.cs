﻿using Domain.Extension;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;

namespace Infrastructure.Mapper.Concrete
{
    internal class UserRoleMapper : AbstractMapper<UserRole, UserRelationToRole>
    {
        public override async Task<UserRelationToRole> MapToDatabaseEntity(UserRole entity, bool mapRelationObjects)
        {
            if (entity == null)
                return null;
            UserRelationToRole role = new UserRelationToRole();

            role.RoleUuid = entity.Role.Uuid.ToGuid();
            role.CreatedTime = entity.CreatedTime;  
            role.LastModifiedTime = entity.LastModifiedTime;    
            role.DeletedTime = entity.DeletedTime;
            return role;
        }

        public override async Task<UserRole> MapToDomainEntity(UserRelationToRole entity, bool withRelations)
        {
            if (entity == null)
                return null;
            var roleDomainEntity = await entity.RoleUu.MapToDomainEntity<Domain.Entities.Role.Role, Role>(false);
            return UserRole.Create(
                roleDomainEntity,
                entity.CreatedTime??DateTime.MinValue,
                entity.LastModifiedTime,
                entity.DeletedTime);
        }
    }
}
