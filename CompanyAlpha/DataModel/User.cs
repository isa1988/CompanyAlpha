using System.Collections.Generic;

namespace CompanyAlpha.DataModel
{
    /// <summary>
    /// Пользователь
    /// </summary>
    class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public virtual Role RoleCur { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Заблокировать
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        /// Занятые комнаты
        /// </summary>
        public virtual ICollection<OrderRoom> OrderRoomList { get; set; }
        
        /// <summary>
        /// Есть фотография у пользователя
        /// </summary>
        public bool IsPhoto { get; set; }
    }
}
