using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCompanyAlpha.Models
{
    /// <summary>
    /// Роли
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Доступ к исправлению переговорных
        /// </summary>
        public bool IsChangeRoom { get; set; }

        /// <summary>
        /// Доступ к добавлению, редактированию пользователей
        /// </summary>
        public bool IsEditUser { get; set; }

        //public List<UserInfo> UserList { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}