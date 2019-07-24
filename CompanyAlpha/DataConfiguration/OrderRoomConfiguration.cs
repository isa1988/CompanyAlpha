
using CompanyAlpha.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace CompanyAlpha.DataConfiguration
{
    class OrderRoomConfiguration : EntityTypeConfiguration<OrderRoom>
    {
        public OrderRoomConfiguration()
        {
            HasKey(x => x.ID);
            Property(x => x.Start).IsRequired();
            Property(x => x.End).IsRequired();
            HasRequired(x => x.RoomCur).WithMany(x => x.OrderRoomList).
                              HasForeignKey(x => x.RoomID).WillCascadeOnDelete(false);
            HasRequired(x => x.UserCur).WithMany(x => x.OrderRoomList).
                              HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);
        }
    }
}
