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
    }
}