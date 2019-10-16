using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Administration.DataAccessLayer
{
	public class AppIdentityContext : IdentityDbContext
	{

		public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
