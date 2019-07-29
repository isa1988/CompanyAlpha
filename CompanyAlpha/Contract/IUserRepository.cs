﻿using System.Collections.Generic;
using CompanyAlpha.DataInfo;

namespace CompanyAlpha.Contract
{
    /// <summary>
    /// Операции над пользователями
    /// </summary>
    public interface IUserRepository
    {
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
        int Register(string login, string password, string name, string surName, string middleName,
                    RoleInfo role, byte[] file);

        /// <summary>
        /// Дабавить нового пользователя
        /// </summary>
        /// <param name="userInfo">Модель пользователя</param>
        int Register(UserInfo userInfo);

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
        void EditPersonalArea(int id, string password, string passwordReplay, string oldPassword, bool isPaswordChange,
                  string name, string surName, string middleName, byte[] file, bool isDeleteFile);

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="userInfo">Модель пользователя</param>
        void EditPersonalArea(UserInfo userInfo);

        /// <summary>
        /// Удадить пользователя
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void Delete(int id);

        /// <summary>
        /// Обнулить пароль до 123
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void PasswordReset(int id);

        /// <summary>
        /// Изменить роль
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="role">Роль</param>
        void ChangeRole(int id, RoleInfo role);

        /// <summary>
        /// Вернуть параметры пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <returns></returns>
        UserInfo GetUser(string login);

        /// <summary>
        /// Вернуть параметры пользователя
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        UserInfo GetUser(int id);

        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        UserInfo CheckLoginIn(string login, string password);

        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        List<UserInfo> GetList();
    }
}
