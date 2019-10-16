using Administration.Core.Model;
using Administration.DataAccessLayer.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Administration.DataAccessLayer
{
	public class AdministrationContext : DbContext
	{
		public const string DEFAULT_SCHEMA = "Administration";
		public DbSet<Room> Rooms { get; set; }
		public DbSet<Visitor> Visitors { get; set; }
		public DbSet<User> Users { get; set; }

		public AdministrationContext(DbContextOptions<AdministrationContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new RoomEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new VisitorEntityTypeConfiguration());
			modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
		}
	}
}
