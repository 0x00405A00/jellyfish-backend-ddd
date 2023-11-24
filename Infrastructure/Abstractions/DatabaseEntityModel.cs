using Domain.Primitives;

namespace Infrastructure.Abstractions
{
    public abstract class DatabaseEntityModel
    {
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public DatabaseEntityModel()
        {
        }

        
    }
}
