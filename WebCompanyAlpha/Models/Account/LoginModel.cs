using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCompanyAlpha.Models.Account
{
    /// <summary>
    /// Авторизация
    /// </summary>
    public class LoginModel : Page
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        [DisplayName("Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}