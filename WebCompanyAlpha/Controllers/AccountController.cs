using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CompanyAlpha.Contract;
using CompanyAlpha.DataInfo;
using WebCompanyAlpha.Models;
using WebCompanyAlpha.Models.Account;

namespace WebCompanyAlpha.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// Статьи контроллер
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public AccountController(IDataProvider dataProvider) : base(dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                UserInfo user = dataProvider.User.CheckLoginIn(model.Login, model.Password);
                user.RoleCur = dataProvider.Role.GetRole(user.RoleID);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.ToString(), true);
                    Cookies.IsChangeRoom = user.RoleCur.IsChangeRoom;
                    Cookies.IsEditUser = user.RoleCur.IsEditUser;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.Error = "Неверно задали логин или пароль";
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            RegisterModel registerModel = new RegisterModel
            {
                Login = string.Empty,
                Password = string.Empty,
                PasswordReplay = string.Empty,
                Name = string.Empty,
                SurName = string.Empty,
                MiddleName = string.Empty
            };
            registerModel.Roles = dataProvider.Role.GetRoles().Select(x =>
                new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View(registerModel);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                RoleInfo role = dataProvider.Role.GetRole(model.RoleID);
                return View(model);
                dataProvider.User.Register(model.Login, model.Password, model.Name, model.SurName,
                                            model.MiddleName, role, model.File);
                UserInfo user = dataProvider.User.GetUser(model.Login);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                model.Error = "Неверно задали логин или пароль";
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}