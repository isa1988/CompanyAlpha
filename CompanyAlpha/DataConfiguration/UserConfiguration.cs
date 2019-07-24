using CompanyAlpha.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace CompanyAlpha.DataConfiguration
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(x => x.ID);
            Property(x => x.Login).HasMaxLength(100).IsRequired();
            Property(x => x.Password).HasMaxLength(100).IsRequired();
            Property(x => x.Name).HasMaxLength(100);
            Property(x => x.SurName).HasMaxLength(100);
            Property(x => x.MiddleName).HasMaxLength(100);
            HasRequired(x => x.RoleCur).WithMany(x => x.UserList).
                              HasForeignKey(x => x.RoleID).WillCascadeOnDelete(false);
        }
    }
}
