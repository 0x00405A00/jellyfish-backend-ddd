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

        private ICollection<User> _users = new List<User>();
        public ICollection<User> Users { get => _users?.ToList(); }

        private UserType(
            UserTypeId id,
            string name,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime,
            User createdByUser,
            User? modifiedByUser,
            User? deletedByUser) : base(id)
        {
            Id = id;
            Name = name;

            LastModifiedByUser = modifiedByUser;
            LastModifiedTime = lastModifiedTime;
            DeletedByUser = deletedByUser;
            DeletedTime = deletedTime;
            CreatedByUser = createdByUser;
            CreatedTime = createdTime;
        }

        public static UserType Create(
            UserTypeId userTypeId,
            string name,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime,
            User createdByUser,
            User? modifiedByUser,
            User? deletedByUser)
        {
            UserType userType = new UserType(
                userTypeId,
                name,
                createdTime,
                lastModifiedTime,
                deletedTime,
                createdByUser,
                modifiedByUser,
                deletedByUser);

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
