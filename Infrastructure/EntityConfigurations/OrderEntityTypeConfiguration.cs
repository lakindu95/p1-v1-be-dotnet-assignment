using System;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : BaseEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property<Guid>("FlightId").IsRequired();
			builder.Property<Guid>("FlightRateId").IsRequired();

			builder.Property("Name").IsRequired();
            builder.Property("Status").IsRequired();
			builder.Property("NoOfSeats").IsRequired();

			builder.HasOne<Flight>()
			  .WithMany()
			  .IsRequired()
			  .HasForeignKey("FlightId");

			builder.HasOne<FlightRate>()
			  .WithMany()
			  .IsRequired()
			  .HasForeignKey("FlightRateId");
        }
    }
}