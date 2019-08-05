using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CompanyAlpha.Contract;
using CompanyAlpha.DataInfo;
using WebCompanyAlpha.Filters;
using WebCompanyAlpha.Helper;
using WebCompanyAlpha.Models;
using WebCompanyAlpha.Models.Account;

namespace WebCompanyAlpha.Controllers
{
    public class AccountController : BaseController
    {
        /// <summary>
        /// Работа с пользователями контроллер
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public AccountController(IDataProvider dataProvider) : base(dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            model.Title = "Авторизация";
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                UserInfo user = dataProvider.User.CheckLoginIn(model.Login, model.Password);
                if (user != null)
                {
                    user.RoleCur = dataProvider.Role.GetRole(user.RoleID);
                    FormsAuthentication.SetAuthCookie(user.ToString(), true);
                    Cookies.IsChangeRoom = user.RoleCur.IsChangeRoom;
                    Cookies.IsEditUser = user.RoleCur.IsEditUser;
                    Cookies.Login = user.Login;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.Error = "Неверно задали логин или пароль";
                }
            }
            model.Title = "Авторизация";
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
                MiddleName = string.Empty,
                Title = "Регистрация"
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
                model.Title = "Регистрация";
                return View(model);
            }
            return RedirectToAction("ListUser");
        }

        /// <summary>
        /// Изменение роли
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        [AdminAccess]
        public ActionResult ChangeRole(int id)
        {
            UserInfo userInfo = dataProvider.User.GetUser(id);
            if (userInfo == null) return View();
            UserModel userModel = new UserModel
            {
                ID = userInfo.ID,
                RoleID = userInfo.RoleID,
                Name = userInfo.ToString(),
                Title = "Изменение роли"
            };
            userModel.Roles = dataProvider.Role.GetRoles().Select(x =>
                new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View(userModel);
        }

        /// <summary>
        /// Изменение роли
        /// </summary>
        /// <param name="modal">Модель полььзоватеоя</param>
        /// <returns></returns>
        [AdminAccess]
        [HttpPost]
        public ActionResult ChangeRole(UserModel modal)
        {
            try
            {
                RoleInfo role = dataProvider.Role.GetRole(modal.RoleID);
                dataProvider.User.ChangeRole(modal.ID, role);
                return RedirectToAction("ListUser");
                
            }
            catch
            {
                modal.Title = "Изменение роли";
                return View(modal);
            }
        }

        /// <summary>
        /// Обнуление пороля
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        [AdminAccess]
        public ActionResult PasswordReset(int id)
        {
            UserInfo userInfo = dataProvider.User.GetUser(id);
            if (userInfo == null) return View();
            UserModel userModel = new UserModel
            {
                ID = userInfo.ID,
                Title = "Сбросить пароль",
                RoleID = userInfo.RoleID,
                Name = userInfo.ToString()
            };
            userModel.Roles = dataProvider.Role.GetRoles().Select(x =>
                new SelectListItem { Text = x.Name, Value = x.ID.ToString() }).ToList();
            return View(userModel);
        }

        /// <summary>
        /// Обнуление пароля
        /// </summary>
        /// <param name="modal">Модель полььзоватеоя</param>
        /// <returns></returns>
        [AdminAccess]
        [HttpPost]
        public ActionResult PasswordReset(UserModel modal)
        {
            try
            {
                RoleInfo role = dataProvider.Role.GetRole(modal.RoleID);
                dataProvider.User.ChangeRole(modal.ID, role);
                return RedirectToAction("ListUser");
            }
            catch
            {
                modal.Title = "Сбросить пароль";
                return View(modal);
            }
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
                userModel.Title = "Личный кабинет";
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
                userModel.Title = "Редактирование личнх данных";
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
            return View("PersonalArea", model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: Room/Delete/5
        [AdminAccess]
        public ActionResult Delete(int id)
        {
            UserInfo userInfo = dataProvider.User.GetUser(id);
            if (userInfo == null)
                return View();
            UserModel userModel = new UserModel
            {
                ID = userInfo.ID,
                Name = userInfo.Name,
                Password = userInfo.Password,
                Login = userInfo.Login,
                RoleID = userInfo.RoleID,
                IsBlock = userInfo.IsBlock,
                SurName = userInfo.SurName,
                MiddleName = userInfo.MiddleName,
                IsPhoto = userInfo.IsPhoto,
                Title = "Удаление",
                

            };
            userModel.OrderRoomModels = dataProvider.OrderRoom.GetPreDeleteUser(userInfo).Select(x => new OrderRoomModel
            {
                ID = x.ID,
                StartDT = x.Start,
                EndDT = x.End,
                RoomID = x.RoomID,
            }).ToList();

            if (userModel.OrderRoomModels?.Count > 0)
            {
                List<RoomInfo> roomInfos = dataProvider.Room.GetRooms(0, RoomIsProjector.All, RoomIsMarkerBoard.All);
                for (int i = 0; i < userModel.OrderRoomModels.Count; i++)
                {
                    userModel.OrderRoomModels[i].RoomCur =
                        roomInfos.FirstOrDefault(x => x.ID == userModel.OrderRoomModels[i].RoomID).ToString();
                }
                return View("DeleteDetails", userModel);
            }
            else
            {
                return View(userModel);
            }
        }

        // POST: Room/Delete/5
        [AdminAccess]
        [HttpPost]
        public ActionResult Delete(UserModel modal)
        {
            try
            {
                dataProvider.User.Delete(modal.ID);
                return Redirect("/Account/PersonalArea");
            }
            catch
            {
                return View(modal);
            }
        }

        // GET: Room/Delete/5
        [AdminAccess]
        public ActionResult DeleteDetails(UserModel model)
        {
            model.Title = "Удаление";
            return View(model);
        }

        // POST: Room/Delete/5
        [AdminAccess]
        [HttpPost]
        public ActionResult DeleteDetails(RoomModel model, int id)
        {
            try
            {
                dataProvider.User.Delete(model.ID);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(id);
            }
        }

        /// <summary>
        /// Список всех пользователей
        /// </summary>
        /// <returns></returns>
        [AdminAccess]
        public ActionResult ListUser()
        {
            List<RoleInfo> rooms = dataProvider.Role.GetRoles();
            ArreyOfModel arreyOfModel = new ArreyOfModel();
            
            List<UserModel> userModels = dataProvider.User.GetList().Select(x => new UserModel
            {
                ID = x.ID,
                Name = x.Name,
                Password = x.Password,
                Login = x.Login,
                RoleID = x.RoleID,
                RoleNane = rooms.FirstOrDefault(n => n.ID == x.RoleID)?.Name ?? string.Empty,
                IsBlock = x.IsBlock,
                SurName = x.SurName,
                MiddleName = x.MiddleName,
                IsPhoto = x.IsPhoto
            }).ToList();
            arreyOfModel.Title = "Пользователм";
            arreyOfModel.UserModels = userModels;
            return View(arreyOfModel);
        }
    }
}