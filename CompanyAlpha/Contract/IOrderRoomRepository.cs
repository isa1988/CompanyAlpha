using CompanyAlpha.DataInfo;
using System;
using System.Collections.Generic;

namespace CompanyAlpha.Contract
{
    /// <summary>
    /// Операции над заказами переговорных
    /// </summary>
    public interface IOrderRoomRepository
    {
        /// <summary>
        /// Дабавить бронь на комнату
        /// </summary>
        /// <param name="mainDate">Дата</param>
        /// <param name="start">Время начала</param>
        /// <param name="end">Конечное время</param>
        /// <param name="user">Пользователь</param>
        /// <param name="room">Комната</param>
        /// <returns></returns>
        int Insert(DateTime mainDate, DateTime start, DateTime end, UserInfo user, RoomInfo room);

        /// <summary>
        /// Дабавить бронь на комнату
        /// </summary>
        /// <param name="orderRoom">Модель заказа комнаты</param>
        /// <returns></returns>
        int Insert(OrderRoomInfo orderRoom);

        /// <summary>
        /// Редактировать бронь на комнату
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="mainDate">Дата</param>
        /// <param name="start">Время начала</param>
        /// <param name="end">Конечное время</param>
        /// <param name="room">Комната</param>
        /// <param name="status">Статус брони </param>
        /// <returns></returns>
        void Edit(int id, DateTime mainDate, DateTime start, DateTime end, RoomInfo room, OrderRoomStatus status);

        /// <summary>
        /// Редактировать бронь на комнату
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="mainDate">Дата</param>
        /// <param name="start">Время начала</param>
        /// <param name="end">Конечное время</param>
        /// <param name="room">Комната</param>
        /// <returns></returns>
        void Edit(int id, DateTime mainDate, DateTime start, DateTime end, RoomInfo room);

        /// <summary>
        /// Редактировать бронь на комнату
        /// </summary>
        /// <param name="orderRoom">Модель заказа комнаты</param>
        void Edit(OrderRoomInfo orderRoom);

        /// <summary>
        /// Удалить бронь на комнатц
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void Delete(int id);

        /// <summary>
        /// Подтвердить бронь
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void ReservationApproved(int id);

        /// <summary>
        /// Подтвердить бронь проверка на наличие есть ли еще на брони на время 
        /// вызывать перед ReservationApproved
        /// </summary>
        /// <param name="id">Идентификатор</param>
        List<OrderRoomInfo> ChekReservationApproved(int id);

        /// <summary>
        /// Отклонить бронь
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void ReservationDeclined(int id);

        /// <summary>
        /// Информация при удаление переговорной (комнаты) о бронировании данной комнаты
        /// </summary>
        /// <param name="roomId">Идентификатор переговрной</param>
        /// <returns>Идентификатор переговорной</returns>
        List<OrderRoomInfo> GetPreDeleteRoomInfos(int roomId);

        /// <summary>
        /// Возратить все брони
        /// </summary>
        /// <returns></returns>
        List<OrderRoomInfo> GetOrderRoomInfos();

        /// <summary>
        /// Возратить все брони за период
        /// </summary>
        /// <returns></returns>
        /// <param name="dtStsrt">Дата начала</param>
        /// <param name="dtEnd">Дата окончания</param>
        List<OrderRoomInfo> GetOrderRoomInfos(DateTime dtStsrt, DateTime dtEnd);

        /// <summary>
        /// Данные о брони
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        OrderRoomInfo GetOrderRoom(int id);


        /// <summary>
        /// Метод для информации до удаления Пользователя
        /// </summary>
        /// <param name="userInfo">Пользователь которого собираются удалить</param>
        /// <returns></returns>
        List<OrderRoomInfo> GetPreDeleteUser(UserInfo userInfo);
    }
}
