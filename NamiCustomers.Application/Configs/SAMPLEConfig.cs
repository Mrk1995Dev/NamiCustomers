namespace NamiCustomers.Application.Configs
{
    public class SAMPLEConfig : IEntityTypeConfiguration<SAMPLEEntity>
    {

        public void Configure(EntityTypeBuilder<SAMPLEEntity> builder)
        {
            builder.ToTable("T1_SAMPLEs", "PROJECTNAME");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("T1_SAMPLEs_Id");
            builder.Property(t => t.Field).HasColumnName("T1_SAMPLEs_Field");
        }
    }
}
