using Domain.Const;
using Domain.Entities.Mails;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.DatabaseEntityConfiguration
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