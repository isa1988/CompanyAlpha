using CompanyAlpha.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCompanyAlpha.Controllers
{
    public class BaseController : Controller
    {
        protected IDataProvider dataProvider;
        /// <summary>
        /// контроллер
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public BaseController(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dataProvider != null)
                {
                    dataProvider.Dispose();
                    dataProvider = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}