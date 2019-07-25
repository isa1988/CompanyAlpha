using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCompanyAlpha.Models
{
    public static class Cookies
    {
        /// <summary>
        /// Доступ к исправлению переговорных
        /// </summary>
        public static bool IsChangeRoom
        {
            get
            {
                return true;
                HttpContext context = HttpContext.Current;
                return (bool)context.Session["IsChangeRoom"];
            }
            set
            {
                HttpContext context = HttpContext.Current;
                context.Session["IsChangeRoom"] = value;
            }
        }

        /// <summary>
        /// Доступ к добавлению, редактированию пользователей
        /// </summary>
        public static bool IsEditUser
        {
            get
            {
                return true;
                HttpContext context = HttpContext.Current;
                return (bool)context.Session["IsEditUser"];
            }
            set
            {
                HttpContext context = HttpContext.Current;
                context.Session["IsEditUser"] = value;
            }
        }
    }
}