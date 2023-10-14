using Domain.Primitives;

namespace Domain.Entities.User
{
    public sealed class UserType : Entity<UserTypeId>,IAuditibleCreateEntity,IAuditibleModifiedEntity,IAuditibleDeleteEntity
    {
        public string Name { get; set; }

        public DateTime? CreatedTime { get; private set; }
        public DateTime? LastModifiedTime { get; private set; }
        public DateTime? DeletedTime { get; private set; }
        public User? CreatedByUser { get; private set; }
        public User? LastModifiedByUser { get; private set; }
        public User? DeletedByUser { get; private set; }

        private UserType(
            UserTypeId id,
            string name,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime) : base(id)
        {
            Name = name;

            CreatedTime = createdTime;
            LastModifiedTime = lastModifiedTime;
            DeletedTime = deletedTime;
        }

        public static UserType Create(
            UserTypeId userTypeId,
            string name,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime)
        {
            UserType userType = new UserType(
                userTypeId,
                name,
                createdTime,
                lastModifiedTime,
                deletedTime);

            userType.Raise(new DomainEvent(userType));
            return userType;
        }

        public void SetLastModified(User modifiedBy)
        {
            this.LastModifiedTime = DateTime.Now;
            this.LastModifiedByUser = modifiedBy;
        }
        public void SetCreated(User createdBy)
        {
            this.CreatedTime = DateTime.Now;
            this.CreatedByUser = createdBy;
        }
        public void SetDeleted(User deletedBy)
        {
            this.DeletedTime = DateTime.Now;
            this.DeletedByUser = deletedBy;
        }

    }
}
