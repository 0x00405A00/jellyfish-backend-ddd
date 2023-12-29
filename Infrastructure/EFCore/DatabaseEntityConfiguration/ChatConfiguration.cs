using Domain.Entities.Chats;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.ValueObjects.Ids;

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

            builder.Property(ut => ut.PicturePath)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.PathDescriptors)
                .HasColumnName("picture_path");

            builder.Property(ut => ut.PictureFileExtension)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.FileExtension)
                .HasColumnName("picture_path_file_extension");

            builder.AddAuditableConstraints<Chat, ChatId>();
        }
    }
}