using CompanyAlpha.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace CompanyAlpha.DataConfiguration
{
    class RoomConfiguration : EntityTypeConfiguration<Room>
    {
        public RoomConfiguration()
        {
            HasKey(x => x.ID);
            Property(x => x.Name).HasMaxLength(100).IsRequired();
        }
    }
}
