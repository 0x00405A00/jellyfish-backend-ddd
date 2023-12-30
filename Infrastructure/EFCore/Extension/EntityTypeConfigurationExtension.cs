using Domain.Entities.Users;
using Domain.Primitives;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.Extension
{
    public static class EntityTypeConfigurationExtension
    {
        public static EntityTypeBuilder<TEntity> AddDefaultProperties<TEntity, TEntityId>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : Entity<TEntityId>
            where TEntityId : Identification
        {

            builder.HasKey(ut => ut.Id).HasName(DbContextExtension.IndexNameDefinitions.PrimaryKeyIndex);

            string tableName = DbContextExtension.GetTableName(typeof(TEntity));
            builder.ToTable(tableName);

            //Timestamps by action
            builder.Property(ut => ut.CreatedTime)
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValue(new CustomDateTime(DateTime.Now))
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.TimeSpecific.CreatedTime);
            builder.Property(ut => ut.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.TimeSpecific.ModifiedTime);
            builder.Property(ut => ut.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.TimeSpecific.DeletedTime);

            builder.Property(ut => ut.Id)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasConversion(toDb => toDb.Id, fromDb => (TEntityId)Activator.CreateInstance(typeof(TEntityId), new object[] { fromDb }))
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.UuidName);

            return builder;
        }
        public static EntityTypeBuilder<TEntity> AddAuditableProperties<TEntity, TEntityId>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : AuditableEntity<TEntityId>
            where TEntityId : Identification
        {

            builder.Property(ut => ut.CreatedByUserForeignKey)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                //.HasConversion(toDb => toDb.Uuid, fromDb => new UserId(fromDb))
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.UserSpecific.CreatedByUser);
            builder.Property(ut => ut.LastModifiedByUserForeignKey)
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                //.HasConversion(toDb => toDb.Uuid, fromDb => new UserId(fromDb))
                .IsRequired(false)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.UserSpecific.ModifiedByUser);
            builder.Property(ut => ut.DeletedByUserForeignKey)
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                //.HasConversion(toDb => toDb.Uuid, fromDb => new UserId(fromDb))
                .IsRequired(false)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.UserSpecific.DeletedByUser);
            return builder;
        }

        public static EntityTypeBuilder<TEntity> AddAuditableConstraints<TEntity, TEntityId>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : AuditableEntity<TEntityId>
            where TEntityId : Identification
        {
            var clrTypeName = typeof(TEntity).Name;
            var createByUserForeignPropertyName = $"Created{clrTypeName}s";
            var modifiedByUserForeignPropertyName = $"Modified{clrTypeName}s";
            var deletedByUserForeignPropertyName = $"Deleted{clrTypeName}s";

            var nameCollection = new string[3] { createByUserForeignPropertyName, modifiedByUserForeignPropertyName, deletedByUserForeignPropertyName };

            var props = typeof(User).GetProperties()
                .ToList()
                .FindAll(prop => nameCollection.Contains(prop.Name) && prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericArguments()[0] == typeof(TEntity));
            /*string tmp = null;
            foreach ( var prop in props )
            {
                var tDef = prop.PropertyType.GetGenericTypeDefinition();
                tmp += $"[{prop.Name}:{tDef.Name}|{tDef == typeof(ICollection<>)}],";
            }*/
            if (!props.Any() || props.Count != nameCollection.Count())
            {
                throw new InvalidOperationException($"cant find any of these foreign-properties (prop-type should be '{nameof(ICollection<TEntity>)}') '{createByUserForeignPropertyName},{modifiedByUserForeignPropertyName},{deletedByUserForeignPropertyName}' in entity {nameof(User)}");
            }

            var createdByUserConstraintName = DbContextExtension.GetForeignKeyName(typeof(TEntity).Name, nameof(AuditableEntity<TEntityId>.CreatedByUserForeignKey), nameof(User));
            builder.HasOne(u => u.CreatedByUser)
                .WithMany(createByUserForeignPropertyName)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(fk => fk.CreatedByUserForeignKey)
                .HasConstraintName(createdByUserConstraintName);

            var modifiedByUserConstraintName = DbContextExtension.GetForeignKeyName(typeof(TEntity).Name, nameof(AuditableEntity<TEntityId>.LastModifiedByUserForeignKey), nameof(User));
            builder.HasOne(u => u.LastModifiedByUser)
                .WithMany(modifiedByUserForeignPropertyName)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(fk => fk.LastModifiedByUserForeignKey)
                .HasConstraintName(modifiedByUserConstraintName);

            var deletedByUserConstraintName = DbContextExtension.GetForeignKeyName(typeof(TEntity).Name, nameof(AuditableEntity<TEntityId>.DeletedByUserForeignKey), nameof(User));
            builder.HasOne(u => u.DeletedByUser)
                .WithMany(deletedByUserForeignPropertyName)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(fk => fk.DeletedByUserForeignKey)
                .HasConstraintName(deletedByUserConstraintName);

            return builder;
        }
    }
}