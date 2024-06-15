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
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.ToTable("procedures");

            builder.HasKey(e => e.Id);

            builder.Property(entity => entity.Id).ValueGeneratedOnAdd();

            builder.Property(entity => entity.CreatedOn).IsRequired();

            builder.Property(entity => entity.LastModifiedOn).IsRequired(false);

            builder.Property(procedure => procedure.CurrentProcedureStatus).IsRequired();

            builder
                .HasMany(p => p.Attachments)
                .WithOne()
                .HasForeignKey(a => a.ProcedureId)
                .IsRequired();

            builder
                .HasMany(p => p.StatusHistory)
                .WithOne()
                .HasForeignKey(a => a.ProcedureId)
                .IsRequired();
        }
    }
}
