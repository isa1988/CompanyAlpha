namespace CompanyAlpha.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CompanyAlpha.DataModel.DataContent>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CompanyAlpha.DataModel.DataContent context)
        {
            context.Roles.AddOrUpdate(x => x.ID,
                new DataModel.Role() { ID = 1, Name = "Офис менеджер", IsChangeRoom = true, IsEditUser = true },
                new DataModel.Role() { ID = 2, Name = "Сотрудник", IsChangeRoom = false, IsEditUser = false });
            context.Users.AddOrUpdate(x => x.ID,
                new DataModel.User() { ID = 1, Login = "admin", Password = "8CB2237D0679CA88DB6464EAC60DA96345513964",
                                      RoleID = 1, SurName = "Админов", Name = "Админ", MiddleName = "Админович"});
        }
    }
}
