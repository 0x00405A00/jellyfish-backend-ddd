﻿using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Extension;
using Shared.Infrastructure.Extension;

namespace Shared.Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.AddDefaultProperties<Message, MessageId>();
            builder.AddAuditableProperties<Message, MessageId>();

            builder.Property(ut => ut.UserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("user_id");

            builder.Property(ut => ut.ChatForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("chat_id");

            builder.Property(ut => ut.Text)
                .IsRequired(false)
                .HasColumnType("TEXT")
                .HasColumnName("text");

            builder.OwnsOne(ut => ut.MediaContent, navigationBuilder =>
            {
                navigationBuilder.Property(img => img.FilePath)
                    .IsRequired(false)
                    .HasMaxLength(DbContextExtension.ColumnLength.PathDescriptors)
                    .HasColumnName("binary_content_path");

                navigationBuilder.Property(img => img.FileExtension)
                    .IsRequired(false)
                    .HasMaxLength(DbContextExtension.ColumnLength.FileExtension)
                    .HasColumnName("binary_content_path_file_extension");

                navigationBuilder.Property(img => img.Data)
                    .IsRequired(false)
                    .HasColumnName("binary_content_base64");

                navigationBuilder.Property(img => img.FileExtension)
                    .IsRequired(false)
                    .HasMaxLength(DbContextExtension.ColumnLength.MimeTypes)
                    .HasColumnName("binary_content_base64_mime_type");
            });

            builder.OwnsOne(e => e.Location, locationBuilder =>
            {
                locationBuilder.Property(loc => loc.Latitude)
                .HasColumnName("latitude")
                .IsRequired(false);

                locationBuilder.Property(loc => loc.Longitude)
                .HasColumnName("longitude")
                .IsRequired(false);

                locationBuilder.Property(loc => loc.Timestamp)
                .HasColumnName("timestamp")
                .IsRequired(false);

                locationBuilder.Property(loc => loc.Altitude)
                .HasColumnName("altitude")
                .IsRequired(false);

                locationBuilder.Property(loc => loc.Accuracy)
                .HasColumnName("accuracy")
                .IsRequired(false);

                locationBuilder.Property(loc => loc.VerticalAccuracy)
                .HasColumnName("vertical_accuracy")
                .IsRequired(false);

                locationBuilder.Property(loc => loc.Speed)
                .HasColumnName("speed")
                .IsRequired(false);

                locationBuilder.Property(loc => loc.Course)
                .HasColumnName("course")
                .IsRequired(false);

                locationBuilder.Property(loc => loc.GeoReferenceSystem)
                .HasColumnName("georeference_system")
                .IsRequired(false);
            });

            string messageRelationToUserConstraintName = DbContextExtension.GetForeignKeyName(nameof(Message), nameof(Message.UserForeignKey), nameof(User));
            string messageRelationToChatConstraintName = DbContextExtension.GetForeignKeyName(nameof(Message), nameof(Message.ChatForeignKey), nameof(Chat));
            builder.HasOne(e => e.User)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.UserForeignKey)
                .HasConstraintName(messageRelationToUserConstraintName)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Chat)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.ChatForeignKey)
                .HasConstraintName(messageRelationToChatConstraintName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.AddAuditableConstraints<Message, MessageId>();
        }
    }
}