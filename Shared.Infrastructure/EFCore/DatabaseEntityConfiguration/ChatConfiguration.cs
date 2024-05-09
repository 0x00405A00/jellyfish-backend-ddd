using Domain.Entities.Chats;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Extension;
using Shared.Infrastructure.Extension;

namespace Shared.Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.AddDefaultProperties<Chat, ChatId>();
            builder.AddAuditableProperties<Chat, ChatId>();

            builder.Ignore(x => x.Admins);
            builder.Ignore(x => x.Members);
            builder.Ignore(x => x.Picture);

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Name);

            builder.Property(ut => ut.Description)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.Descriptions)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Description);

            builder.OwnsOne(ut => ut.Picture, navigationBuilder =>
            {
                navigationBuilder.Property(img => img.FilePath)
                    .IsRequired(false)
                    .HasMaxLength(DbContextExtension.ColumnLength.PathDescriptors)
                    .HasColumnName("picture_binary_content_path");

                navigationBuilder.Property(img => img.FileExtension)
                    .IsRequired(false)
                    .HasMaxLength(DbContextExtension.ColumnLength.FileExtension)
                    .HasColumnName("picture_binary_content_path_file_extension");

                navigationBuilder.Property(img => img.Data)
                    .IsRequired(false)
                    .HasColumnName("picture_binary_content_base64");

                navigationBuilder.Property(img => img.FileExtension)
                    .IsRequired(false)
                    .HasMaxLength(DbContextExtension.ColumnLength.MimeTypes)
                    .HasColumnName("picture_binary_content_base64_mime_type");

                
            });


            builder.AddAuditableConstraints<Chat, ChatId>();
        }
    }
}