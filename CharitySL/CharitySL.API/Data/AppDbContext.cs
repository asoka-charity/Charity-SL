using CharitySL.API.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CharitySL.API.Data
{
    public class AppDbContext : IdentityDbContext<User>
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(e => e.Guid)
					  .HasDefaultValueSql("NEWID()");

				entity.HasIndex(e => e.Guid)
					  .IsUnique();

				entity.Property(e => e.CreatedAt)
					  .HasDefaultValueSql("SYSDATETIMEOFFSET()");
			});

			modelBuilder.Entity<User>()
						.HasOne<UserType>(u => u.UserType)
						.WithMany()
						.HasForeignKey(u => u.UserTypeId)
						.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Project>()
						.HasOne<ProjectStatus>(p => p.ProjectStatus)
						.WithMany()
						.HasForeignKey(p => p.ProjectStatusId)
						.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<Project>()
						.HasOne<ProjectCategory>(p => p.ProjectCategory)
						.WithMany()
						.HasForeignKey(p => p.ProjectCategoryId)
						.OnDelete(DeleteBehavior.SetNull);

			modelBuilder.Entity<ProjectOwner>()
						.HasOne<Project>(u => u.Project)
						.WithMany()
						.HasForeignKey(u => u.ProjectId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProjectOwner>()
						.HasOne<User>(u => u.User)
						.WithMany()
						.HasForeignKey(u => u.UserId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProjectContribution>()
						.HasOne<ProjectOwner>(u => u.ProjectOwner)
						.WithMany()
						.HasForeignKey(u => u.ProjectOwnerId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProjectContribution>()
						.HasOne<PaymentMethod>(u => u.PaymentMethod)
						.WithMany()
						.HasForeignKey(u => u.PaymentMethodId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<ProjectContribution>()
						.HasOne<PaymentStatus>(u => u.PaymentStatus)
						.WithMany()
						.HasForeignKey(u => u.PaymentStatusId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Province>()
						.HasOne<Country>(u => u.Country)
						.WithMany()
						.HasForeignKey(u => u.CountryId)
						.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<UserGroup>()
						.HasOne(ug => ug.User)
						.WithMany(u => u.UserGroups)
						.HasForeignKey(ug => ug.UserId)
						.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<UserGroup>()
						.HasOne(ug => ug.Group)
						.WithMany(g => g.UserGroups)
						.HasForeignKey(ug => ug.GroupId)
						.OnDelete(DeleteBehavior.Cascade);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<UserType> UserTypes { get; set; }
		public DbSet<ProjectStatus> ProjectStatuses { get; set; }
		public DbSet<ProjectCategory> ProjectCategories { get; set; }
		public DbSet<EmailAudit> EmailAudits { get; set; }
		public DbSet<ProjectOwner> ProjectOwners { get; set; }
		public DbSet<ProjectContribution> ProjectContributions { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }
		public DbSet<PaymentStatus> PaymentStatuses { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DbSet<Province> Provinces { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<UserGroup> UserGroups { get; set; }
		public DbSet<TicketReservation> TicketReservation { get; set; }
		public DbSet<LookupContactedBy> LookupContactedBy { get; set; }
	}
}
