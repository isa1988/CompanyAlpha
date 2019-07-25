using System.Collections.Generic;

namespace CompanyAlpha.DataInfo
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserInfo
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
        /// Повторный новый пароль для проверки
        /// </summary>
        public string PasswordReplay { get; set; }

        /// <summary>
        /// Старый пароль
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Редактировать пароль
        /// </summary>
        public bool IsPaswordChange { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public RoleInfo RoleCur { get; set; }

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
        public List<OrderRoomInfo> OrderRoomList { get; set; }

        /// <summary>
        /// Есть фотография у пользователя
        /// </summary>
        public bool IsPhoto { get; set; }
        /// <summary>
        /// Удалить фото
        /// </summary>
        public bool IsFileDelete { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public byte[] File { get; set; }

        public override string ToString()
        {
            if (SurName != string.Empty || Name != string.Empty || MiddleName != string.Empty)
                return SurName + " " + Name + " " + MiddleName;
            else
                return Login;
        }
    }
}
