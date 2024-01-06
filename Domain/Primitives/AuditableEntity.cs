using Domain.Entities.Users;
using Domain.Primitives.Ids;

namespace Domain.Primitives
{
    public abstract class AuditableEntity<TEntityId> : Entity<TEntityId>, IAuditableEntity
        where TEntityId : Identification
    {
        public UserId? CreatedByUserForeignKey { get; set; }
        public UserId? LastModifiedByUserForeignKey { get; set; }
        public UserId? DeletedByUserForeignKey { get; set; }
        public User? CreatedByUser { get; set; }
        public User? LastModifiedByUser { get; set; }
        public User? DeletedByUser { get; set; }

        protected AuditableEntity() 
        {

        }
        public AuditableEntity(TEntityId entityId):base(entityId)
        {
            
        }

        public void SetLastModified(UserId modifiedBy)
        {
            LastModifiedTime = CustomDateTime.Now();
            LastModifiedByUserForeignKey = modifiedBy;
        }
        public void SetCreated(UserId createdBy)
        {
            CreatedTime = CustomDateTime.Now();
            CreatedByUserForeignKey = createdBy;
        }
        public void SetDeleted(UserId deletedBy)
        {
            DeletedTime = CustomDateTime.Now();
            DeletedByUserForeignKey = deletedBy;
        }
    }
}
