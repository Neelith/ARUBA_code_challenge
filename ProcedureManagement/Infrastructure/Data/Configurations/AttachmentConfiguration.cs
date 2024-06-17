using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("attachments");

            builder.HasKey(e => e.Id);

            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();

            builder.Property(entity => entity.CreatedOn).IsRequired();

            builder.Property(entity => entity.LastModifiedOn).IsRequired(false);

            builder.Property(attachment => attachment.Content).IsRequired();

            builder.HasOne<Procedure>()
                .WithMany(procedure => procedure.Attachments)
                .HasForeignKey(attachment => attachment.ProcedureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
