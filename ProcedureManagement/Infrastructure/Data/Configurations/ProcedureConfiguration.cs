﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<ProcedureConfiguration>
    {
        public void Configure(EntityTypeBuilder<ProcedureConfiguration> builder)
        {
            
        }
    }
}
