using Domain.Const;
using Domain.Entities.Mails;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Extension;
using Shared.Infrastructure.Extension;

namespace Shared.Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class EmailSendingTypeConfiguration : IEntityTypeConfiguration<EmailSendingType>
    {
        public void Configure(EntityTypeBuilder<EmailSendingType> builder)
        {
            builder.AddDefaultProperties<EmailSendingType, EmailTypeId>();

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Name);

        }
    }

}