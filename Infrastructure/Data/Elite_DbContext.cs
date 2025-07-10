using Core.Constants;
using Core.Entities.Auth;
using Core.Entities.HR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Elite_DbContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public Elite_DbContext(DbContextOptions<Elite_DbContext> context):base(context) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            builder.Entity<IdentityRole<int>>().ToTable("Roles",Schema.Auth);
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims",Schema.Auth);
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles",Schema.Auth);
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins",Schema.Auth);
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims",Schema.Auth);
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", Schema.Auth);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
