using CompanyAlpha.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCompanyAlpha.Filters;

namespace WebCompanyAlpha.Controllers
{
    [AdminAccess]
    public class AdminController : BaseController
    {
        /// <summary>
        /// супер пользователь
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public AdminController(IDataProvider dataProvider) : base(dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}