﻿using Domain.Entities.Chats;
using Domain.Primitives.Ids;
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