﻿using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users
{
    public interface IUserType
    {
        string Name { get; }
    }

    public sealed partial class UserType : AuditableEntity<UserTypeId>, IUserType
    {
        public string Name { get; private set; }

        private UserType() : base()
        {

        }
        private UserType(
            UserTypeId id,
            string name,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy) : base(id)
        {
            Name = name;
            CreatedTime = createdDateTime;
            CreatedByUserForeignKey = createdBy;
            LastModifiedTime = modifiedDateTime;
            LastModifiedByUserForeignKey = modifiedBy;
            DeletedTime = deletedDateTime;
            DeletedByUserForeignKey = deletedBy;
        }

        public static UserType Create(
            UserTypeId userTypeId,
            string name,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy)
        {
            UserType userType = new UserType(
                userTypeId,
                name,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy);

            userType.Raise(new DomainEvent(userTypeId));
            return userType;
        }
    }
    public sealed partial class UserType
    {
        public IReadOnlyCollection<User>? Users { get; }
    }
}
