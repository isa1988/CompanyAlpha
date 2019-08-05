using CompanyAlpha.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCompanyAlpha.Helper;
using WebCompanyAlpha.Models;

namespace WebCompanyAlpha.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Статьи контроллер
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public HomeController(IDataProvider dataProvider) : base(dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        // GET: Home
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && Cookies.IsEditUser && Cookies.IsChangeRoom)
                return Redirect("OrderRoom/Index");
            else if (User.Identity.IsAuthenticated && !Cookies.IsEditUser && !Cookies.IsChangeRoom)
                return Redirect("OrderRoom/Index");
            string result = "Вы не авторизованы 123";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }
            else
            {
                return Redirect("Account/Login");
            }

            HomeTest homeTest = new HomeTest {Text = result};
            return View("Index", homeTest);
        }


        public ActionResult Proba()
        {
            string result = "ПРивет";

            HomeTest homeTest = new HomeTest { Text = result };
            return View(homeTest);
        }
    }
}