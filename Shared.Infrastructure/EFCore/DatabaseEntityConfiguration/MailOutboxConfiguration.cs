﻿using Domain.Entities.Mails;
using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Extension;
using Shared.Infrastructure.Extension;

namespace Shared.Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class MailOutboxConfiguration : IEntityTypeConfiguration<MailOutbox>
    {
        public void Configure(EntityTypeBuilder<MailOutbox> builder)
        {
            builder.AddDefaultProperties<MailOutbox, MailOutboxId>();

            builder.Property(ut => ut.From)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.EmailAddrLength)
                .HasColumnName("from");

            builder.Property(ut => ut.Subject)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.EmailSubject)
                .HasColumnName("subject");

            builder.Property(ut => ut.Body)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("body");

            builder.Property(ut => ut.IsBodyHtml)
                .IsRequired(false)
                .HasColumnType(DbContextExtension.ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("boolean value to describe if email contain html content")
                .HasColumnName("is_body_html");
        }
    }
}