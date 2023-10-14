using Domain.Entities.User;

namespace Domain.Primitives
{
    public interface IAuditibleModifiedEntity
    {
        DateTime? LastModifiedTime { get; }
        User? LastModifiedByUser { get; }
        void SetLastModified(User modifiedBy);
    }
    public interface IAuditibleCreateEntity
    {
        DateTime? CreatedTime { get; }
        User? CreatedByUser { get; }
        void SetCreated(User createdBy);
    }
    public interface IAuditibleDeleteEntity
    {
        DateTime? DeletedTime { get; }
        User? DeletedByUser { get; }
        void SetDeleted(User deletedBy);
    }
}
