using Administration.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.DataAccessLayer.EntityTypeConfigurations
{
	class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> userConfiguration)
		{
			userConfiguration
				.ToTable("Users", AdministrationContext.DEFAULT_SCHEMA);

			userConfiguration.HasKey(v => v.Id);
			userConfiguration.Property(v => v.Id)
				.ValueGeneratedOnAdd();

			userConfiguration.Property(v => v.Login).IsRequired();

			userConfiguration.HasIndex(u => u.Login).IsUnique();

			userConfiguration.Property(v => v.PasswordHash).IsRequired();
		}
	}
}
