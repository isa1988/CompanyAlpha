
using CompanyAlpha.DataConfiguration;
using System.Data.Entity;

namespace CompanyAlpha.DataModel
{
    class DataContent : DbContext
    {

        public DataContent() :
            base("CompanyAlpha")
        { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<OrderRoom> OrderRooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoomConfiguration());
            modelBuilder.Configurations.Add(new OrderRoomConfiguration());
        }
    }
}
