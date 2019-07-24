using System;
using System.Collections.Generic;

namespace CompanyAlpha.DataModel
{
    /// <summary>
    /// Роли
    /// </summary>
    class Role
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

        public virtual ICollection<User> UserList { get; set; }
    }
}
