using CompanyAlpha.DataModel;
using System.Data.Entity.ModelConfiguration;

namespace CompanyAlpha.DataConfiguration
{
    class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            HasKey(x => x.ID);
            Property(x => x.Name).HasMaxLength(100).IsRequired();
        }
    }
}
