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
        /// <param name="user">Пользователь</param>
        /// <param name="room">Комната</param>
        /// <param name="status">Статус брони </param>
        /// <returns></returns>
        void Edit(int id, DateTime mainDate, DateTime start, DateTime end, UserInfo user, RoomInfo room, OrderRoomStatus status);

        /// <summary>
        /// Редактировать бронь на комнату
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="mainDate">Дата</param>
        /// <param name="start">Время начала</param>
        /// <param name="end">Конечное время</param>
        /// <param name="user">Пользователь</param>
        /// <param name="room">Комната</param>
        /// <returns></returns>
        void Edit(int id, DateTime mainDate, DateTime start, DateTime end, UserInfo user, RoomInfo room);

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
        /// Вернуть список комнат по фильтру
        /// </summary>
        /// <returns></returns>
        /// <param name="seatsCount">Количество комнат от и более</param>
        /// <param name="projector">Проектор</param>
        /// <param name="markerBoard">Маркерная доска</param>
        /// <param name="dateStart">Дата начала период</param>
        /// <param name="dateEnd">Дата окончания периода</param>
        /// <param name="statusFilter">Фильтр по статусу брони</param>
        List<OrderRoomInfo> GetRooms(int seatsCount, RoomIsProjector projector, RoomIsMarkerBoard markerBoard,
                            DateTime dateStart, DateTime dateEnd, OrderRoomStatusFilter statusFilter);
    }
}
