using System;
using System.Linq;
using System.Collections.Generic;
using CompanyAlpha.Contract;
using CompanyAlpha.DataInfo;
using CompanyAlpha.DataModel;
using System.Configuration;
using System.Data.Entity;
using System.Web;
using System.Web.Hosting;
using System.Security.Cryptography;
using System.Text;
using CompanyAlpha.Media;
using System.IO;

namespace CompanyAlpha.Repository
{
    /// <summary>
    /// Операции над пользователями
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private DataContent dataContent = null;
        private User user = null;

        public object HttpContext { get; private set; }

        /// <summary>
        /// Операции над ролями
        /// </summary>
        /// <param name="mainContent">работа с базой</param>
        public UserRepository(object dataContent)
        {
            if (dataContent is DataContent) this.dataContent = (DataContent)dataContent;
        }
        /// <summary>
        /// Операции над ролями
        /// </summary>
        public UserRepository()
        {
            dataContent = new DataContent();
        }
        /// <summary>
        /// Дабавить нового пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="name">Имя</param>
        /// <param name="surName">Фамилия</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="role">Роль</param>
        /// <param name="file">Фото</param>
        public int Register(string login, string password, string name, 
                          string surName, string middleName, RoleInfo role, byte[] file)
        {
            Check(login, password, string.Empty, string.Empty, true, role);
            SetValue(login, true, password, string.Empty, string.Empty, role, surName, name,
                     middleName, file, false);
            return user.ID;
        }

        /// <summary>
        /// Дабавить нового пользователя
        /// </summary>
        /// <param name="userInfo">Модель пользователя</param>
        public int Register(UserInfo userInfo)
        {
            if (userInfo == null)
                throw new ArgumentException("Вы не указали объект");
            return Register(userInfo.Login, userInfo.Password, userInfo.Name, userInfo.SurName, 
                userInfo.MiddleName, userInfo.RoleCur, userInfo.File);
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="password">Новый пароль</param>
        /// <param name="passwordReplay">Повтор нового пароля</param>
        /// <param name="oldPassword">Старый пароль</param>
        /// <param name="isPaswordChange">Изменять пароль или нет</param>
        /// <param name="name">Имя</param>
        /// <param name="surName">Фамилия</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="role">Роль</param>
        /// <param name="file">Фото</param>
        /// <param name="isDeleteFile">Удалить фото, если true то да</param>
        public void EditPersonalArea(int id, string password, string passwordReplay, string oldPassword, bool isPaswordChange, 
                           string name, string surName, string middleName, byte[] file, bool isDeleteFile)
        {
            user = dataContent.Users.FirstOrDefault(x => x.ID == id);
            if (user == null)
                throw new ArgumentException("Не найден объект");
            CheckNotRole(user.Login, password, passwordReplay, oldPassword, isPaswordChange);
            SetValue(user.Login, isPaswordChange, password, passwordReplay, oldPassword, null, surName, name,
                middleName, file, isDeleteFile, false);
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="userInfo">Модель пользователя</param>
        public void EditPersonalArea(UserInfo userInfo)
        {
            if (userInfo == null)
                throw new ArgumentException("Вы не указали объект");
            EditPersonalArea(userInfo.ID, userInfo.Password, userInfo.PasswordReplay, userInfo.OldPassword,
                userInfo.IsPaswordChange,
                userInfo.Name, userInfo.SurName, userInfo.MiddleName, userInfo.File,
                userInfo.IsFileDelete);
        }

        /// <summary>
        /// Проверка входных данных при создание/редактирование
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="text">Текст статьи</param>
        /// <param name="author">Автор</param>
        /// <param name="headingID">Ссылка на рубрику</param>
        /// <param name="role">Роль</param>
        private void Check(string login, string password, string passwordReplay, 
                        string oldPassword, bool isPaswordChange, RoleInfo role)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("Не заполнен логин");
            if (role == null)
                throw new ArgumentException("Не заполнена роль");
            if (isPaswordChange)
            {
                if (user == null && string.IsNullOrEmpty(password))
                    throw new ArgumentException("Не заполнен пароль");
                if (user != null && user.Password != PasswordEncryption(oldPassword))
                    throw new ArgumentException("Неверно указан старый пароль");
                if (user != null && password != passwordReplay)
                    throw new ArgumentException("Неверно указан новый пароль не совподает с повторным");
            }
            if (user == null)
            {
                if (dataContent.Users.Any(x => x.Login.Equals(login, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException("Текущее логин уже используется");
            }
            else
            {
                if (dataContent.Users.Any(x => x.Login.Equals(login, StringComparison.OrdinalIgnoreCase) &&
                                                  x.ID != user.ID))
                    throw new ArgumentException("Текущее логин уже используется");
            }
        }


        /// <summary>
        /// Проверка входных данных при создание/редактирование
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="text">Текст статьи</param>
        /// <param name="author">Автор</param>
        /// <param name="headingID">Ссылка на рубрику</param>
        private void CheckNotRole(string login, string password, string passwordReplay,
            string oldPassword, bool isPaswordChange)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("Не заполнен логин");
            if (isPaswordChange)
            {
                if (user == null && string.IsNullOrEmpty(password))
                    throw new ArgumentException("Не заполнен пароль");
                if (user != null && user.Password != PasswordEncryption(oldPassword))
                    throw new ArgumentException("Неверно указан старый пароль");
                if (user != null && password != passwordReplay)
                    throw new ArgumentException("Неверно указан новый пароль не совподает с повторным");
            }
            if (user == null)
            {
                if (dataContent.Users.Any(x => x.Login.Equals(login, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException("Текущее логин уже используется");
            }
            else
            {
                if (dataContent.Users.Any(x => x.Login.Equals(login, StringComparison.OrdinalIgnoreCase) &&
                                               x.ID != user.ID))
                    throw new ArgumentException("Текущее логин уже используется");
            }
        }

        /// <summary>
        /// Дабавить/редактировать
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="isPaswordChange">Изменить пароль</param>
        /// <param name="password">пароль</param>
        /// <param name="passwordReplay">Повторный пароль для проверки при изменение</param>
        /// <param name="oldPassword">Старый пароль при изменение</param>
        /// <param name="role">Роль</param>
        /// <param name="surName">Фамилия</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="file">Файл</param>
        /// <param name="isDeleteFile">Удаление файла при редактирование</param>
        /// <param name="isNew">Новый</param>

        private void SetValue(string login, bool isPaswordChange, string password, string passwordReplay, 
             string oldPassword, RoleInfo role, string surName, string name, string middleName,
             byte[] file, bool isDeleteFile, bool isNew = true)
        {
            WorkForFiles workForFiles = WorkForFiles.New;
            if (isNew)
            {
                user = new User();
                workForFiles = WorkForFiles.New;
            }
            else
            {
                workForFiles = WorkForFiles.Edit;
            }

            user.Login = login.GetNotNull();
            if (isPaswordChange) user.Password = PasswordEncryption(password.GetNotNull());
            if (role != null) user.RoleID = role.ID;
            user.Name = name.GetNotNull();
            user.SurName = surName.GetNotNull();
            user.MiddleName = middleName.GetNotNull();

            if (isNew) dataContent.Users.Add(user);
            dataContent.SaveChanges();
            if (workForFiles == WorkForFiles.New)
            {
                user.IsPhoto = (file?.Length > 0);
                if (file?.Length > 0)
                    WorkForFile(login, file, isDeleteFile, WorkForFiles.New);
            }
            else
            {
                if (file?.Length > 0)
                {
                    user.IsPhoto = true;
                    dataContent.SaveChanges();
                }
                else if (isDeleteFile)
                {
                    user.IsPhoto = false;
                    dataContent.SaveChanges();
                }
                WorkForFile(login, file, isDeleteFile, WorkForFiles.Edit);
            }
        }

        /// <summary>
        /// Работа с файлами
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="file">Файл</param>
        /// <param name="isDelete">Удаление файла при редактирование</param>
        /// <param name="workForFile">Что нужно сделать</para>
        private void WorkForFile(string login, byte[] file, bool isDelete, WorkForFiles workForFile)
        {
            login += ".jpg";
            Configuration cfg = null;
            if (System.Web.HttpContext.Current != null)
            {
                cfg =
                    System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            }
            else
            {
                cfg =
                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            MediaFolderConfigSection section = (MediaFolderConfigSection)cfg.GetSection("MediaFolder");
            if (section == null) return;
            string pathDir = section.FolderItems[0].Path;
            pathDir = HostingEnvironment.MapPath(pathDir);

            switch (workForFile)
            {
                case WorkForFiles.New:
                    {
                        if (file?.Length > 0)
                        {
                            //File.WriteAllBytes(pathDir + login, file);
                            using (var stream = new FileStream(pathDir + login, FileMode.Create))
                            {
                                stream.Write(file, 0, file.Length);
                            }
                        }

                        break;
                    }
                case WorkForFiles.Edit:
                    {
                        if (isDelete)
                        {
                            File.Delete(pathDir + login);
                        }
                        else if (file?.Length > 0)
                        {
                            //if (login?.Length > 0)
                            //File.Delete(pathDir + login);
                            File.WriteAllBytes(pathDir + login, file);
                        }
                        break;
                    }
                case WorkForFiles.Delete:
                    {
                        File.Delete(pathDir + login);
                        break;
                    }
            }
        }

        /// <summary>
        /// Шифрование пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        string PasswordEncryption(string password)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Удадить пользователя
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void Delete(int id)
        {
            user = dataContent.Users.FirstOrDefault(x => x.ID == id);
            if (user == null)
                throw new ArgumentException("Не найден объект");
            List<OrderRoom> orderRooms = dataContent.OrderRooms.Where(x => x.UserID == user.ID).ToList();
            if (orderRooms?.Count > 0)
            {
                for (int i = 0; i < orderRooms.Count; i++)
                {
                    dataContent.OrderRooms.Remove(orderRooms[i]);
                }
            }

            dataContent.Users.Remove(user);
            dataContent.SaveChanges();
        }

        /// <summary>
        /// Вернуть параметры пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns></returns>
        public UserInfo GetUser(string login)
        {
            User userTemp = dataContent.Users.FirstOrDefault(x => x.Login == login);
            if (userTemp == null) return null;
            return new UserInfo
            {
                ID = userTemp.ID,
                Name = userTemp.Name,
                Password = userTemp.Password,
                Login = userTemp.Login,
                RoleID = userTemp.RoleID,
                IsBlock = userTemp.IsBlock,
                SurName = userTemp.SurName,
                MiddleName = userTemp.MiddleName,
                IsPhoto = userTemp.IsPhoto
            };
        }

        /// <summary>
        /// Вернуть параметры пользователя
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public UserInfo GetUser(int id)
        {
            User userTemp = dataContent.Users.FirstOrDefault(x => x.ID == id);
            if (userTemp == null) return null;
            return new UserInfo
            {
                ID = userTemp.ID,
                Name = userTemp.Name,
                Password = userTemp.Password,
                Login = userTemp.Login,
                RoleID = userTemp.RoleID,
                IsBlock = userTemp.IsBlock,
                SurName = userTemp.SurName,
                MiddleName = userTemp.MiddleName,
                IsPhoto = userTemp.IsPhoto
            };
        }

        /// <summary>
        /// Обнулить пароль до 123
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void PasswordReset(int id)
        {
            user = dataContent.Users.FirstOrDefault(x => x.ID == id);
            if (user == null)
                throw new ArgumentException("Не найден объект");
            user.Password = PasswordEncryption(string.Empty);
            dataContent.SaveChanges();
        }

        /// <summary>
        /// Изменить роль
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="role">Роль</param>
        public void ChangeRole(int id, RoleInfo role)
        {
            if (role == null)
                throw new ArgumentException("Не задана роль");
            user = dataContent.Users.FirstOrDefault(x => x.ID == id);
            if (user == null)
                throw new ArgumentException("Не найден объект");
            user.RoleID = role.ID;
            dataContent.SaveChanges();
        }

        // <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public UserInfo CheckLoginIn(string login, string password)
        {
            if (password == null) password = string.Empty;
            password = PasswordEncryption(password);
            User userTemp = dataContent.Users.FirstOrDefault(x => x.Login.ToLower() == login.ToLower() &&
                                                                  x.Password == password);
            if (userTemp == null) return null;
            RoleRepository roleTemp = new RoleRepository(dataContent);
            return new UserInfo
            {
                ID = userTemp.ID,
                Name = userTemp.Name,
                Password = userTemp.Password,
                Login = userTemp.Login,
                RoleID = userTemp.RoleID,
                IsBlock = userTemp.IsBlock,
                SurName = userTemp.SurName,
                MiddleName = userTemp.MiddleName,
                IsPhoto = userTemp.IsPhoto,
                RoleCur = roleTemp.GetRole(userTemp.RoleID)
            };
        }


        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetList()
        {
            return dataContent.Users.Select(x => new UserInfo
            {
                ID = x.ID,
                Name = x.Name,
                Password = x.Password,
                Login = x.Login,
                RoleID = x.RoleID,
                IsBlock = x.IsBlock,
                SurName = x.SurName,
                MiddleName = x.MiddleName,
                IsPhoto = x.IsPhoto,
            }).ToList();
        }
    }

    enum WorkForFiles
    {
        New = 0,
        Edit = 1,
        Delete = 2
    }
}
