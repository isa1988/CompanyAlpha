using CompanyAlpha.DataInfo;
using System.Collections.Generic;

namespace CompanyAlpha.Contract
{
    /// <summary>
    /// Операции над ролями
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Добавление новой роли 
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="isChangeRoom">Редактирование комнат</param>
        /// <param name="isEditUser">Добавление новых пользователей</param>
        int Insert(string name, bool isChangeRoom, bool isEditUser);

        /// <summary>
        /// Добавление новой роли 
        /// </summary>
        /// <param name="roleInfo">Модель роли</param>
        int Insert(RoleInfo roleInfo);
        
        /// <summary>
        /// Редактирование роли 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наименование</param>
        /// <param name="isChangeRoom">Редактирование комнат</param>
        /// <param name="isEditUser">Добавление новых пользователей</param>
        void Edit(int id, string name, bool isChangeRoom, bool isEditUser);

        /// <summary>
        /// Редактирование роли 
        /// </summary>
        /// <param name="roleInfo">Модель роли</param>
        void Edit(RoleInfo roleInfo);

        /// <summary>
        /// Удалить роль
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void Delete(int id);

        /// <summary>
        /// Вернуть роль
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        RoleInfo GetRole(int id);

        /// <summary>
        /// Вернуть все роли
        /// </summary>
        /// <returns></returns>
        List<RoleInfo> GetRoles();
    }
}
