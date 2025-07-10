using Core.Constants;
using Core.Constants.Enums;
using Core.Entities.HR;
using Infrastructure.Customizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees",Schema.HR);
            builder.HasKey(e => e.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.HireDate)
                .HasConversion(new DateConverter()) 
                .HasColumnType("Date")
                .IsRequired();
            //builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.Status)
                .HasConversion(new EnumToStringConverter<EmployeeStatus>())
                .IsRequired();


            builder.HasOne(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => x.DepartmentId).IsRequired();
            builder.HasOne(u=>u.User).WithOne(e => e.Employee)
                .HasForeignKey<Employee>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
