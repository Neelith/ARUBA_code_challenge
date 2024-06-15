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
    public class ProcedureStatusHistoryRecordConfiguration : IEntityTypeConfiguration<ProcedureStatusHistoryRecord>
    {
        public void Configure(EntityTypeBuilder<ProcedureStatusHistoryRecord> builder)
        {
            builder.ToTable("procedureStatusHistory");

            builder.HasKey(e => e.Id);

            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();

            builder.Property(entity => entity.CreatedOn).IsRequired();

            builder.Property(entity => entity.LastModifiedOn).IsRequired(false);

            builder.HasOne<Procedure>()
                .WithMany(procedure => procedure.StatusHistory)
                .HasForeignKey(attachment => attachment.ProcedureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
