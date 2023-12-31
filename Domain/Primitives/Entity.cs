using System.Linq.Expressions;

namespace Domain.Primitives
{
    public abstract class Entity : IEntityBase
    {
        protected readonly List<DomainEvent> _domainEvents = new();

        public CustomDateTime CreatedTime { get; set; }
        public CustomDateTime? LastModifiedTime { get; set; }
        public CustomDateTime? DeletedTime { get; set; }

        protected Entity()
        {

        }
        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        protected void SetValueInInstance<TEntity>(object instance,object value, Expression<Func<TEntity, object>> propertyExpression)
        {

            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (!(propertyExpression.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("Property expression must be a MemberExpression", nameof(propertyExpression));
            }

            var propertyInfo = memberExpression.Member as System.Reflection.PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("Expression must be a property access expression", nameof(propertyExpression));
            }

            propertyInfo.SetValue(instance, value);
        }
        protected virtual void Update<TEntity>(Expression<Func<TEntity, object>> propertyExpression, object value)
            where TEntity : Entity
        {
            SetValueInInstance(this, value, propertyExpression);
        }
        protected virtual void Update<TEntity>(params KeyValuePair<Expression<Func<TEntity, object>>, object>[] propertyExpression)
            where TEntity : Entity
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException($"expressions Array is null {propertyExpression}");
            }
            propertyExpression.ToList().ForEach(e => SetValueInInstance(this, e.Value, e.Key));
        }
        public List<DomainEvent> GetDomainEvents() => _domainEvents;
    }
    public abstract class Entity<TEntityId> : Entity, IEquatable<Entity<TEntityId>>, IEntityWithTypedId<TEntityId> 
        where TEntityId : Identification
        
    {
        public TEntityId Id { get; set; }

        protected Entity(TEntityId entityId)
        {
            Id = entityId;
        }

        protected Entity():base()
        {
            
        }
        public static TEntityId NewId() => (TEntityId)Activator.CreateInstance(typeof(TEntityId),new object[] { Guid.NewGuid() });

        public static bool operator !=(Entity<TEntityId>? first, Entity<TEntityId>? second)
        {
            if (ReferenceEquals(first, null) && ReferenceEquals(second, null)) return false;
            return !first.Equals(second);
        }
        public static bool operator ==(Entity<TEntityId>? first, Entity<TEntityId>? second)
        {
            var equalCheck = (ReferenceEquals(first, null) && ReferenceEquals(first, null)) ||(!ReferenceEquals(first, null) && !ReferenceEquals(second, null) && (first.Id == second.Id));
            return equalCheck;
        }
        public bool Equals(Entity<TEntityId>? other)
        {

            if (ReferenceEquals(other, null)) return false;
            if (other.GetType().BaseType != typeof(Entity<TEntityId>)) return false;
            return ((Entity<TEntityId>)other).Id == Id;
        }
        public override bool Equals(object? obj)
        {
            var bt = obj.GetType().BaseType;
            if (ReferenceEquals(obj, null)) return false;
            if(bt != typeof(Entity<TEntityId>) && bt?.BaseType != typeof(Entity<TEntityId>))
            {
                return false;
            }
            return ((Entity<TEntityId>)obj).Id == Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() * 41;
        }

        protected virtual new void Update<TEntity,Id>(Expression<Func<TEntity, object>> propertyExpression, object value)
            where TEntity : Entity<Id>
            where Id : Identification
        {
            SetValueInInstance(this, value, propertyExpression);
        }
        protected new virtual void Update<TEntity>(params KeyValuePair<Expression<Func<TEntity, object>>, object>[] propertyExpression)
            where TEntity : Entity<TEntityId>
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException($"expressions Array is null {propertyExpression}");
            }
            propertyExpression.ToList().ForEach(e => SetValueInInstance(this, e.Value, e.Key));
        }
    }
}
