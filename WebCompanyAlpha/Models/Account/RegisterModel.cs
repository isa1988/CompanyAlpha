﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyAlpha.DataInfo;

namespace WebCompanyAlpha.Models.Account
{
    /// <summary>
    /// Регистрацмя
    /// </summary>
    public class RegisterModel : Page
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        [DisplayName("Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Повторный новый пароль для проверки
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Подтвердить пароль")]
        public string PasswordReplay { get; set; }


        /// <summary>
        /// Список ролей
        /// </summary>
        public List<SelectListItem> Roles { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        [DisplayName("Роль")]
        public int RoleID { get; set; }

        /// <summary>
        /// Роли
        /// </summary>
        public List<RoleInfo> RoleInfos { get; set; }

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
    }
}