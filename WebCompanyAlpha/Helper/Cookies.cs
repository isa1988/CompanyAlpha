using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCompanyAlpha.Helper
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
                try
                {
                    HttpContext context = HttpContext.Current;
                    return (bool)context.Session["IsChangeRoom"];
                }
                catch (Exception e)
                {
                    return false;
                }
                //return false;
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
                //return false;
                try
                {
                    HttpContext context = HttpContext.Current;
                    return (bool)context.Session["IsEditUser"];

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                HttpContext context = HttpContext.Current;
                context.Session["IsEditUser"] = value;
            }
        }


        /// <summary>
        /// Логин
        /// </summary>
        public static string Login
        {
            get
            {
                //return "user";
                try
                {
                    HttpContext context = HttpContext.Current;
                    return (string)context.Session["LoginUser"];
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }
                
            }
            set
            {
                HttpContext context = HttpContext.Current;
                context.Session["LoginUser"] = value;
            }
        }
    }
}