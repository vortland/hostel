using Administration.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.DataAccessLayer.EntityTypeConfigurations
{
	class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
	{
		public void Configure(EntityTypeBuilder<Room> roomConfiguration)
		{
			roomConfiguration
				.ToTable("Rooms", AdministrationContext.DEFAULT_SCHEMA);

			roomConfiguration.HasKey(r => r.Id);
			roomConfiguration.Property(r => r.Id)
				.ValueGeneratedOnAdd();

			var navigation = roomConfiguration.Metadata.FindNavigation(nameof(Room.Visitors));
			navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

			roomConfiguration.Property(r => r.Capacity).IsRequired();

			roomConfiguration.Property(r => r.State).IsRequired();

			roomConfiguration.Property(r => r.Type).IsRequired();

			roomConfiguration.Property(r => r.Number).IsRequired();

			roomConfiguration.HasIndex(u => u.Number).IsUnique();
		}
	}
}
