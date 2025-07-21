
using Microsoft.EntityFrameworkCore;
using NamiCustomers.Domain.Entities.Subscribers;

namespace NamiCustomers.Application.Configs;

public class VehicleModelConfig : IEntityTypeConfiguration<VehicleModel>
{
    public void Configure(EntityTypeBuilder<VehicleModel> builder)
    {
        //// For required relationship (non-nullable FK)
        //builder.HasOne(vm => vm.VehicleAttachment)
        //      .WithMany(va => va.VehicleModels)
        //      .HasPrincipalKey<VehicleAttachment>(vm => vm.VehicleModelIdSevenSoft)
        //      .HasForeignKey<VehicleModel>(va => va.VehicleModelIdSevenSoft)
        //      .IsRequired();
    }
}
