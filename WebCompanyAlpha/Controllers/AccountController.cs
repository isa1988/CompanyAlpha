using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CompanyAlpha.Contract;
using CompanyAlpha.DataInfo;
using WebCompanyAlpha.Filters;
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

        [AdminAccess]
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

        [AdminAccess]
        [HttpPost]
        public ActionResult Register(RegisterModel model, HttpPostedFileBase fileImport)
        {
            if (ModelState.IsValid)
            {
                RoleInfo role = dataProvider.Role.GetRole(model.RoleID);
                UserInfo user = new UserInfo
                {
                    Login = model.Login,
                    Password = model.Password,
                    PasswordReplay = model.PasswordReplay,
                    Name = model.Name,
                    SurName = model.SurName,
                    MiddleName = model.MiddleName,
                    RoleCur = role
                };
                if (fileImport != null)
                {
                    user.File = new byte[fileImport.ContentLength];
                    fileImport.InputStream.Read(user.File, 0, user.File.Length);
                }
                dataProvider.User.Register(user);
            }
            else
            {
                model.Roles = dataProvider.Role.GetRoles().Select(x =>
                    new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
                model.Error = "Неверно задали логин или пароль";
                return View(model);
            }
            return Redirect("/Admin/Index");
        }

        /// <summary>
        /// Личный кабинет 
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalArea()
        {
            UserInfo userInfo = dataProvider.User.GetUser(Cookies.Login);
            UserModel userModel = new UserModel();
            if (userInfo != null)
            {
                userModel.Login = userInfo.Login;
                userModel.RoleNane = dataProvider.Role.GetRole(userInfo.RoleID)?.Name ?? string.Empty;
                userModel.IsPhoto = userInfo.IsPhoto;
                userModel.SurName = userInfo.SurName;
                userModel.Name = userInfo.Name;
                userModel.MiddleName = userInfo.MiddleName;
            }

            return View(userModel);
        }

        /// <summary>
        /// Редактирование личнх данных
        /// </summary>
        public ActionResult EditPersonalArea()
        {
            UserInfo userInfo = dataProvider.User.GetUser(Cookies.Login);
            UserModel userModel = new UserModel();
            if (userInfo != null)
            {
                userModel.ID = userInfo.ID;
                userModel.Login = userInfo.Login;
                userModel.IsPhoto = userInfo.IsPhoto;
                userModel.SurName = userInfo.SurName;
                userModel.Name = userInfo.Name;
                userModel.MiddleName = userInfo.MiddleName;
            }
            return View(userModel);
        }

        /// <summary>
        /// Редактирование личнх данных
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditPersonalArea(UserModel model, HttpPostedFileBase fileImport)
        {
            UserInfo userInfo = new UserInfo
            {
                ID = model.ID,
                Login = model.Login,
                IsFileDelete = model.IsFileDelete,
                Password = model.Password,
                OldPassword = model.OldPassword,
                PasswordReplay = model.PasswordReplay,
                IsPaswordChange = model.IsPaswordChange,
                Name = model.Name,
                SurName = model.SurName,
                MiddleName = model.MiddleName
            };
            if (fileImport != null)
            {
                userInfo.File = new byte[fileImport.ContentLength];
                fileImport.InputStream.Read(userInfo.File, 0, userInfo.File.Length);
            }
            dataProvider.User.EditPersonalArea(userInfo);
            return Redirect("/Account/PersonalArea");
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}