using Administration.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.DataAccessLayer.EntityTypeConfigurations
{
	class VisitorEntityTypeConfiguration : IEntityTypeConfiguration<Visitor>
	{
		public void Configure(EntityTypeBuilder<Visitor> visitorConfiguration)
		{
			visitorConfiguration
				.ToTable("Visitors", AdministrationContext.DEFAULT_SCHEMA);

			visitorConfiguration.HasKey(v => v.Id);
			visitorConfiguration.Property(v => v.Id)
				.ValueGeneratedOnAdd();

			visitorConfiguration.Property(v => v.CheckInDate).IsRequired();

			visitorConfiguration.Property(v => v.CheckOutDate).IsRequired(false);

			visitorConfiguration.Property(v => v.FullName).IsRequired();

			visitorConfiguration.Property(v => v.RoomId).IsRequired();
		}
	}
}
