using System;
using System.Transactions;
using CompanyAlpha.DataInfo;
using CompanyAlpha.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestCompanyAlpha
{
    [TestClass]
    public class UnitTestRoleRepository
    {
        [TestMethod]
        public void RoleAllOperations()
        {
            using (var dataProvider = new DataProvider())
            {
                var all = dataProvider.Role.GetRoles(); //получение всех ролей
                int totalItems = all.Count;
                Assert.IsNotNull(all);
                Assert.IsTrue(all.Count > 0);
                using (var tran = new TransactionScope())
                {
                    //все транзакции выполняются без комита в БД
                    totalItems++;
                    string name = "Тест";
                    //Создание статьи
                    int id = dataProvider.Role.Insert(name, false, false);

                    all = dataProvider.Role.GetRoles();
                    Assert.IsNotNull(all);
                    Assert.AreEqual(totalItems, all.Count);

                    GetIDAnCheckNameTest(id, name); //Проверка создалась запись в БД

                    name = "ТестПравка";
                    EditTest(id, name, true, true); // Редакттирование записи в БД

                    DeleteTest(id); // Удаление записи из БД
                }

                totalItems--;
                all = dataProvider.Role.GetRoles();
                Assert.IsNotNull(all);
                Assert.AreEqual(totalItems, all.Count);
            }
        }

        private void GetIDAnCheckNameTest(int id, string checkName)
        {
            using (var dataProvider = new DataProvider())
            {
                RoleInfo role = dataProvider.Role.GetRole(id);
                Assert.IsNotNull(role);
                Assert.AreEqual(checkName, role.Name);
            }
        }

        private void EditTest(int id, string name, bool isChangeRoom, bool isEditUser)
        {
            using (var dataProvider = new DataProvider())
            {
                dataProvider.Role.Edit(id, name, isChangeRoom, isEditUser);
                GetIDAnCheckNameTest(id, name);
            }
        }

        /// <summary>
        /// Тест удаления данных
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private void DeleteTest(int id)
        {
            using (var dataProvider = new DataProvider())
            {
                dataProvider.Role.Delete(id);
                RoleInfo articleInfo = dataProvider.Role.GetRole(id);
                Assert.IsNull(articleInfo);
            }
        }
    }
}
