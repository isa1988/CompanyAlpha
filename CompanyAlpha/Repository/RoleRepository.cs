using System;
using System.Linq;
using System.Collections.Generic;
using CompanyAlpha.Contract;
using CompanyAlpha.DataInfo;
using CompanyAlpha.DataModel;

namespace CompanyAlpha.Repository
{
    /// <summary>
    /// Операции над ролями
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private DataContent dataContent = null;
        private Role role = null;
        /// <summary>
        /// Операции над ролями
        /// </summary>
        /// <param name="mainContent">работа с базой</param>
        public RoleRepository(object dataContent)
        {
            if (dataContent is DataContent) this.dataContent = (DataContent)dataContent;
        }
        /// <summary>
        /// Операции над ролями
        /// </summary>
        public RoleRepository()
        {
            dataContent = new DataContent();
        }
        /// <summary>
        /// Добавление новой роли 
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="isChangeRoom">Редактирование комнат</param>
        /// <param name="isEditUser">Добавление новых пользователей</param>
        public int Insert(string name, bool isChangeRoom, bool isEditUser)
        {
            Check(name, isChangeRoom, isEditUser);
            SetValue(name, isChangeRoom, isEditUser);
            return role.ID;
        }

        /// <summary>
        /// Добавление новой роли 
        /// </summary>
        /// <param name="roleInfo">Модель роли</param>
        public int Insert(RoleInfo roleInfo)
        {
            if (roleInfo == null)
                throw new ArgumentException("Вы не указали объект");
            return Insert(roleInfo.Name, roleInfo.IsChangeRoom, roleInfo.IsEditUser);
        }

        /// <summary>
        /// Редактирование роли 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наименование</param>
        /// <param name="isChangeRoom">Редактирование комнат</param>
        /// <param name="isEditUser">Добавление новых пользователей</param>
        public void Edit(int id, string name, bool isChangeRoom, bool isEditUser)
        {
            role = dataContent.Roles.FirstOrDefault(x => x.ID == id);
            if (role == null)
                throw new ArgumentException("Не найден объект");
            Check(name, isChangeRoom, isEditUser);
            SetValue(name, isChangeRoom, isEditUser, false);
        }

        /// <summary>
        /// Редактирование роли 
        /// </summary>
        /// <param name="roleInfo">Модель роли</param>
        public void Edit(RoleInfo roleInfo)
        {
            Edit(roleInfo.ID, roleInfo.Name, roleInfo.IsChangeRoom, roleInfo.IsEditUser);
        }

        /// <summary>
        /// Проверка входных данных при создание/редактирование
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="isChangeRoom">Редактирование комнат</param>
        /// <param name="isEditUser">Добавление новых пользователей</param>
        private void Check(string name, bool isChangeRoom, bool isEditUser)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Не заполнено наименование");
            if (role == null)
            {
                if (dataContent.Roles.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException("Текущее наименование уже используется");
            }
            else
            {
                if (dataContent.Roles.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                                               x.ID != role.ID))
                    throw new ArgumentException("Текущее наименование уже используется");
            }
        }

        /// <summary>
        /// Дабавить/редактировать
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="isChangeRoom">Редактирование комнат</param>
        /// <param name="isEditUser">Добавление новых пользователей</param>
        /// <param name="isNew">Новый</param>
        private void SetValue(string name, bool isChangeRoom, bool isEditUser, bool isNew = true)
        {
            if (isNew)
            {
                role = new Role();
            }

            role.Name = name.GetNotNull();
            role.IsChangeRoom = isChangeRoom;
            role.IsEditUser = isEditUser;

            if (isNew) dataContent.Roles.Add(role);
            dataContent.SaveChanges();
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void Delete(int id)
        {
            role = dataContent.Roles.FirstOrDefault(x => x.ID == id);
            if (role == null)
                throw new ArgumentException("Не найден объект");
            dataContent.Roles.Remove(role);
            dataContent.SaveChanges();
        }

        public RoleInfo GetRole(int id)
        {
            Role roleTemp = dataContent.Roles.FirstOrDefault(x => x.ID == id);
            if (roleTemp == null) return null;
            return new RoleInfo
            {
                ID = roleTemp.ID,
                Name = roleTemp.Name,
                IsChangeRoom = roleTemp.IsChangeRoom,
                IsEditUser = roleTemp.IsEditUser
            };
        }

        public List<RoleInfo> GetRoles()
        {
            return dataContent.Roles.Select(x => new RoleInfo
            {
                ID = x.ID,
                Name = x.Name,
                IsChangeRoom = x.IsChangeRoom,
                IsEditUser = x.IsEditUser
            }).ToList();
        }

    }
}
