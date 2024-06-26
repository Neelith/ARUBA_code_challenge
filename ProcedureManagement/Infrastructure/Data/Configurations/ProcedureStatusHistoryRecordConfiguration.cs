﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasForeignKey(record => record.ProcedureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
