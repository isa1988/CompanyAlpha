using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCompanyAlpha.Models.Account
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserModel
    {
        public int ID { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        [DisplayName("Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [DisplayName("Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Повторный новый пароль для проверки
        /// </summary>
        [DisplayName("Подтверждающий пароль")]
        public string PasswordReplay { get; set; }

        /// <summary>
        /// Старый пароль
        /// </summary>
        [DisplayName("Старый пароль")]
        public string OldPassword { get; set; }

        /// <summary>
        /// Редактировать пароль
        /// </summary>
        [DisplayName("Изменить пароль")]
        public bool IsPaswordChange { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        [DisplayName("Роль")]
        public int RoleID { get; set; }

        /// <summary>
        /// Список ролей
        /// </summary>
        [DisplayName("Роль")]
        public List<SelectListItem> Roles { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [DisplayName("Имя")]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [DisplayName("Фамилия")]
        public string SurName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Заблокировать
        /// </summary>
        [DisplayName("Заблокировать")]
        public bool IsBlock { get; set; }

        /// <summary>
        /// Есть фотография у пользователя
        /// </summary>
        public bool IsPhoto { get; set; }

        /// <summary>
        /// Удалить фото
        /// </summary>
        [DisplayName("Удалить фото")]
        public bool IsFileDelete { get; set; }

        /// <summary>
        /// Наименование роли
        /// </summary>
        [DisplayName("Роль")]
        public string RoleNane { get; set; }

        /// <summary>
        /// Путь до шаблона
        /// </summary>
        public string LayoutPath { get; set; }


        public override string ToString()
        {
            if (SurName != string.Empty || Name != string.Empty || MiddleName != string.Empty)
                return SurName + " " + Name + " " + MiddleName;
            else
                return Login;
        }
    }
}