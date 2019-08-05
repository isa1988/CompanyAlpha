using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCompanyAlpha.Models
{
    /// <summary>
    /// Общая страница
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Натменование
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст ошибки
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Доступ к исправлению переговорных
        /// </summary>
        public bool IsChangeRoom { get; set; }

        /// <summary>
        /// Доступ к добавлению, редактированию пользователей
        /// </summary>
        public bool IsEditUser { get; set; }

        /// <summary>
        /// Страничка
        /// </summary>
        public string Layout { get; set; }
    }
}