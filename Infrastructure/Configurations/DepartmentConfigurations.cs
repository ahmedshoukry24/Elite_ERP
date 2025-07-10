using Core.Constants;
using Core.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments",Schema.HR);

            builder.HasKey(d => d.Id);

            builder.Property(d => d.NameAr)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.NameEn)
                .IsRequired()
                .HasMaxLength(50);
           
        }
    }
}
