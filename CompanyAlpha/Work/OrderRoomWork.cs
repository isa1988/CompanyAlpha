using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CompanyAlpha.Contract;
using CompanyAlpha.DataInfo;
using CompanyAlpha.DataModel;

namespace CompanyAlpha.Work
{
    /// <summary>
    /// Операции над бронированием комнатам
    /// </summary>
    class OrderRoomWork : IOrderRoomRepository
    {
        private DataContent dataContent = null;
        private OrderRoom orderRoom = null;

        /// <summary>
        /// Операции над бронированием комнатам
        /// </summary>
        /// <param name="mainContent">работа с базой</param>
        public OrderRoomWork(object dataContent)
        {
            if (dataContent is DataContent) this.dataContent = (DataContent)dataContent;
        }

        /// <summary>
        /// Операции над бронированием комнатам
        /// </summary>
        public OrderRoomWork()
        {
            dataContent = new DataContent();
        }

        /// <summary>
        /// Дабавить бронь на комнату
        /// </summary>
        /// <param name="mainDate">Дата</param>
        /// <param name="start">Время начала</param>
        /// <param name="end">Конечное время</param>
        /// <param name="user">Пользователь</param>
        /// <param name="room">Комната</param>
        /// <returns></returns>
        public int Insert(DateTime mainDate, DateTime start, DateTime end, UserInfo user, RoomInfo room)
        {
            Check(mainDate, start, end, user, room, OrderRoomStatus.FilingArmor);
            SetValue(mainDate, start, end, user, room, OrderRoomStatus.FilingArmor);
            return orderRoom.ID;
        }

        /// <summary>
        /// Дабавить бронь на комнату
        /// </summary>
        /// <param name="orderRoom">Модель заказа комнаты</param>
        /// <returns></returns>
        public int Insert(OrderRoomInfo orderRoom)
        {
            if (orderRoom == null)
                throw new ArgumentException("Вы не указали объект");
            return Insert(orderRoom.MainDate, orderRoom.Start, orderRoom.End, orderRoom.UserCur, orderRoom.RoomCur);
        }

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
        public void Edit(int id, DateTime mainDate, DateTime start, DateTime end, UserInfo user, RoomInfo room, OrderRoomStatus status)
        {
            orderRoom = dataContent.OrderRooms.FirstOrDefault(x => x.ID == id);
            if (orderRoom == null)
                throw new ArgumentException("Не найден объект");
            Check(mainDate, start, end, user, room, status);
            SetValue(mainDate, start, end, user, room, status, false);
        }

        /// <summary>
        /// Редактировать бронь на комнату
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="mainDate">Дата</param>
        /// <param name="start">Время начала</param>
        /// <param name="end">Конечное время</param>
        /// <param name="userID">Пользователь</param>
        /// <param name="roomID">Комната</param>
        /// <returns></returns>
        public void Edit(int id, DateTime mainDate, DateTime start, DateTime end, UserInfo user, RoomInfo room)
        {
            orderRoom = dataContent.OrderRooms.FirstOrDefault(x => x.ID == id);
            if (orderRoom == null)
                throw new ArgumentException("Не найден объект");
            OrderRoomStatus status = OrderRoomStatus.FilingArmor;
            switch (orderRoom.Status)
            {
                case 1:
                {
                    status = OrderRoomStatus.ReservationApproved;
                    break;
                }
                case 2:
                {
                    status = OrderRoomStatus.ReservationDeclined;
                    break;
                }
                case 3:
                {
                    status = OrderRoomStatus.ReservationMarkedForDeletionByManager;
                    break;
                }
            }
            Check(mainDate, start, end, user, room, status);
            SetValue(mainDate, start, end, user, room, status, false);
        }

        /// <summary>
        /// Редактировать бронь на комнату
        /// </summary>
        /// <param name="orderRoom">Модель заказа комнаты</param>
        public void Edit(OrderRoomInfo orderRoom)
        {
            Edit(orderRoom.ID, orderRoom.MainDate, orderRoom.Start, 
                orderRoom.End, orderRoom.UserCur, orderRoom.RoomCur);
        }

        /// <summary>
        /// Проверка входных данных при создание/редактирование
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="seatsCount">Количество кресел</param>
        private void Check(DateTime mainDate, DateTime start, DateTime end, 
                           UserInfo user, RoomInfo room, OrderRoomStatus status)
        {
            if (mainDate == null)
                throw new ArgumentException("Не заполнена дата");
            if (start == null)
                throw new ArgumentException("Не заполнено начальное время");
            if (end == null)
                throw new ArgumentException("Не заполнено конечноеое время");
            if (user == null)
                throw new ArgumentException("Не указан пользователь");
            if (room == null)
                throw new ArgumentException("Не указана комната");
            if (status == null)
                throw new ArgumentException("Не указан статус");
            if (dataContent.OrderRooms.
                    Count(x => (x.Start <= start && x.End >= start && x.RoomID == room.ID && x.Status == 1) ||
                               (x.Start <= end && x.End >= end && x.RoomID == room.ID && x.Status == 1)) > 0)
                throw new ArgumentException("На текущее время уже есть одобренная бронь данной комнаты");
        }

        /// <summary>
        /// Дабавить/редактировать
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="seatsCount">Количество кресел</param>
        /// <param name="isProjector">Наличие проектора</param>
        /// <param name="isMarkerBoard">Наличие маркерной доски</param>
        /// <param name="isBlock">Заблокировать комнату</param>
        /// <param name="isNew">Новый</param>
        private void SetValue(DateTime mainDate, DateTime start, DateTime end,
                              UserInfo user, RoomInfo room, OrderRoomStatus status, bool isNew = true)
        {
            if (isNew)
            {
                orderRoom = new OrderRoom();
            }

            orderRoom.Start = new DateTime(mainDate.Year, mainDate.Month, mainDate.Day,
                                            start.Hour, start.Minute, start.Second);
            orderRoom.End = new DateTime(mainDate.Year, mainDate.Month, mainDate.Day,
                                            end.Hour, end.Minute, end.Second);
            orderRoom.UserID = user.ID;
            orderRoom.RoomID = room.ID;
            orderRoom.Status = (int)status;

            if (isNew) dataContent.OrderRooms.Add(orderRoom);
            dataContent.SaveChanges();
        }


        /// <summary>
        /// Удалить бронь на комнатц
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void Delete(int id)
        {
            orderRoom = dataContent.OrderRooms.FirstOrDefault(x => x.ID == id);
            if (orderRoom == null)
                throw new ArgumentException("Не найден объект");
            dataContent.OrderRooms.Remove(orderRoom);
            dataContent.SaveChanges();
        }

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
        public List<OrderRoomInfo> GetRooms(int seatsCount, RoomIsProjector projector, 
                RoomIsMarkerBoard markerBoard, DateTime dateStart, DateTime dateEnd, OrderRoomStatusFilter statusFilter)
        {
            RoomWork roomWork = new RoomWork(dataContent);
            List<RoomInfo> roomList = roomWork.GetRooms(seatsCount, projector, markerBoard);
            if (roomList == null || roomList.Count == 0) return new List<OrderRoomInfo>();
            List<OrderRoomInfo> orderRoomList = dataContent.OrderRooms
                .Where(x => x.Start >= dateStart && x.End <= dateEnd && roomList.Any(n => n.ID == x.ID)).
                Select(m => new OrderRoomInfo
                {
                    ID = m.ID,
                    Start = m.Start,
                    End = m.End,
                    RoomID = m.RoomID,
                    MainDate = m.Start,
                    UserID = m.UserID,
                    Status = (OrderRoomStatus)m.Status
                } ).ToList();
            return orderRoomList;
        }

        /// <summary>
        /// Подтвердить бронь
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void ReservationApproved(int id)
        {
            orderRoom = dataContent.OrderRooms.FirstOrDefault(x => x.ID == id);
            if (orderRoom == null)
                throw new ArgumentException("Не найден объект");
            orderRoom.Status = 1;
            List<OrderRoom> orderRoomList = dataContent.OrderRooms.Where(x =>
                    (x.Start <= orderRoom.Start && x.End >= orderRoom.Start && 
                     x.RoomID == orderRoom.RoomID && x.Status == 0) ||
                    (x.Start <= orderRoom.End && x.End >= orderRoom.End && 
                     x.RoomID == orderRoom.RoomID && x.Status == 0)).ToList();
            for (int i = 0; i < orderRoomList.Count; i++)
                orderRoomList[i].Status = 2;
            dataContent.SaveChanges();
        }

        /// <summary>
        /// Отклонить бронь
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void ReservationDeclined(int id)
        {
            orderRoom = dataContent.OrderRooms.FirstOrDefault(x => x.ID == id);
            if (orderRoom == null)
                throw new ArgumentException("Не найден объект");
            orderRoom.Status = 2;
            dataContent.SaveChanges();
        }

        /// <summary>
        /// Подтвердить бронь проверка на наличие есть ли еще на брони на время 
        /// вызывать перед ReservationApproved
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public List<OrderRoomInfo> ChekReservationApproved(int id)
        {
            return dataContent.OrderRooms.Where(x =>
                (x.Start <= orderRoom.Start && x.End >= orderRoom.Start &&
                 x.RoomID == orderRoom.RoomID && x.Status == 0) ||
                (x.Start <= orderRoom.End && x.End >= orderRoom.End &&
                 x.RoomID == orderRoom.RoomID && x.Status == 0)).
                Select(m => new OrderRoomInfo
                {
                    ID = m.ID,
                    Start = m.Start,
                    End = m.End,
                    RoomID = m.RoomID,
                    MainDate = m.Start,
                    UserID = m.UserID,
                    Status = (OrderRoomStatus)m.Status
                }).ToList();
        }
    }
}
