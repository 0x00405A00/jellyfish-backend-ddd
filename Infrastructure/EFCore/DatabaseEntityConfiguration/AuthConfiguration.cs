﻿using Domain.Entities.Auths;
using Domain.Entities.Users;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.ValueObjects.Ids;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class AuthConfiguration : IEntityTypeConfiguration<Auth>
    {
        public void Configure(EntityTypeBuilder<Auth> builder)
        {
            builder.AddDefaultProperties<Auth, AuthId>();

            builder.Property(ut => ut.UserId)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("user_uuid");

            builder.Property(ut => ut.RemoteIp)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("remote_ip");

            builder.Property(ut => ut.RemoteIpPort)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("remote_port");

            builder.Property(ut => ut.LocalIp)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("local_ip");

            builder.Property(ut => ut.LocalIpPort)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("local_port");

            builder.Property(ut => ut.Token)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Base64)
                .HasColumnName("token");

            builder.Property(ut => ut.TokenExpiresIn)
                .IsRequired()
                .HasColumnType("datetime")
                .HasColumnName("token_expires_in");

            builder.Property(ut => ut.UserAgent)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName("browser_user_agent");

            builder.Property(ut => ut.RefreshToken)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Base64)
                .HasColumnName("refresh_token");

            builder.Property(ut => ut.RefreshTokenExpiresIn)
                .IsRequired()
                .HasColumnType("datetime")
                .HasColumnName("refresh_token_expires_in");

            builder.Property(ut => ut.LogoutTime)
                .IsRequired(false)
                .HasColumnType("datetime")
                .HasColumnName("logout_time");

            string authConstraintName = DbContextExtension.GetForeignKeyName(nameof(Auth), nameof(Auth.UserId), nameof(User));
            builder.HasOne(u => u.User)
                .WithMany(x => x.Auths)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName(authConstraintName);
        }
    }

}