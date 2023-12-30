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

        public void SetLastModified(User modifiedBy)
        {
            LastModifiedTime = new CustomDateTime(DateTime.Now);
            LastModifiedByUser = modifiedBy;
        }
        public void SetCreated(User createdBy)
        {
            CreatedTime = new CustomDateTime(DateTime.Now);
            CreatedByUser = createdBy;
        }
        public void SetDeleted(User deletedBy)
        {
            DeletedTime = new CustomDateTime(DateTime.Now);
            DeletedByUser = deletedBy;
        }
    }
}
