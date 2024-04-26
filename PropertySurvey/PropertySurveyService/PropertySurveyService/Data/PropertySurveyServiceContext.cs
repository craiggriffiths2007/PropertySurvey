using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PropertySurveyService.Models;


namespace PropertySurveyService.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<PropertySurveyService.Models.Job> Job { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.Customer> Customer { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.Surveyor> Surveyor { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.Header> Header { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.AluminiumTable> AlumTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.BifoldTable> BifoldTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.CompositeTable> CompositeTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.ConsTable> ConsTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.GarageTable> GarageTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.GlassTable> GlassTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.GreenTable> GreenTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.LockingTable> LockingTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.PanelTable> PanelTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.TimberTable> TimberTable { get; set; } = default!;
        public DbSet<PropertySurveyService.Models.UPVCTable> UPVCTable { get; set; } = default!;

        public DbSet<PropertySurveyService.Models.PhotoImage> Images { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            var decimalProps = builder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }


            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }
        public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
        {
            public void Configure(EntityTypeBuilder<AppUser> builder)
            {
                builder.Property(u => u.FirstName).HasMaxLength(255);
                builder.Property(u => u.LastName).HasMaxLength(255);
            }
        }

    }
}
