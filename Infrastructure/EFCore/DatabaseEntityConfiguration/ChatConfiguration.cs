using Domain.Entities.Chats;
using Domain.Primitives.Ids;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.AddDefaultProperties<Chat, ChatId>();
            builder.AddAuditableProperties<Chat, ChatId>();

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Name);

            builder.Property(ut => ut.Description)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.Descriptions)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Description);

            builder.OwnsOne(ut => ut.Picture, navigationBuilder => {

                navigationBuilder.Property(img => img.FilePath)
                                             .HasColumnName("picture_path")
                                             .IsRequired(false);
                navigationBuilder.Property(img => img.FileExtension)
                                             .HasColumnName("picture_path_file_extension")
                                             .IsRequired(false);
            });

            builder.AddAuditableConstraints<Chat, ChatId>();
        }
    }
}