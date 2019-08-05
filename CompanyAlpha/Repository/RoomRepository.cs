using System;
using System.Linq;
using System.Collections.Generic;
using CompanyAlpha.Contract;
using CompanyAlpha.DataInfo;
using CompanyAlpha.DataModel;

namespace CompanyAlpha.Repository
{
    /// <summary>
    /// Операции над комнатами
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private DataContent dataContent = null;
        private Room room = null;

        /// <summary>
        /// Операции над комнатами
        /// </summary>
        /// <param name="mainContent">работа с базой</param>
        public RoomRepository(object dataContent)
        {
            if (dataContent is DataContent) this.dataContent = (DataContent) dataContent;
        }

        /// <summary>
        /// Операции над комнатами
        /// </summary>
        public RoomRepository()
        {
            dataContent = new DataContent();
        }

        /// <summary>
        /// Добавить новую комнату
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="seatsCount">Количество кресел</param>
        /// <param name="isProjector">Наличие проектора</param>
        /// <param name="isMarkerBoard">Наличие маркерной доски</param>
        /// <returns></returns>
        public int Insert(string name, int seatsCount, bool isProjector, bool isMarkerBoard)
        {
            Check(name, seatsCount);
            SetValue(name, seatsCount, isProjector, isMarkerBoard, false);
            return room.ID;
        }

        /// <summary>
        /// Добавить новую комнату
        /// </summary>
        /// <param name="room">Модель комнаты</param>
        /// <returns></returns>
        public int Insert(RoomInfo room)
        {
            if (room == null)
                throw new ArgumentException("Вы не указали объект");
            return Insert(room.Name, room.SeatsCount, room.IsProjector, room.IsMarkerBoard);
        }

        /// <summary>
        /// Редактирование комнаты
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наименование</param>
        /// <param name="seatsCount">Количество кресел</param>
        /// <param name="isProjector">Наличие проектора</param>
        /// <param name="isMarkerBoard">Наличие маркерной доски</param>
        /// <param name="isBlock">Комната недоступна</param>
        /// <returns></returns>
        public void Edit(int id, string name, int seatsCount, bool isProjector, bool isMarkerBoard, bool isBlock)
        {
            room = dataContent.Rooms.FirstOrDefault(x => x.ID == id);
            if (room == null)
                throw new ArgumentException("Не найден объект");
            Check(name, seatsCount);
            SetValue(name, seatsCount, isProjector, isMarkerBoard, isBlock, false);
        }

        /// <summary>
        /// Редактирование комнаты
        /// </summary>
        /// <param name="room">Модель комнаты</param>
        /// <returns></returns>
        public void Edit(RoomInfo room)
        {
            if (room == null)
                throw new ArgumentException("Вы не указали объект");
            Edit(room.ID, room.Name, room.SeatsCount, room.IsProjector, room.IsMarkerBoard, room.IsBlock);
        }

        /// <summary>
        /// Проверка входных данных при создание/редактирование
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="seatsCount">Количество кресел</param>
        private void Check(string name, int seatsCount)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Не заполнено наименование");
            if (seatsCount <= 0)
                throw new ArgumentException("Не заполнено количество кресел");

            if (room == null)
            {
                if (dataContent.Roles.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException("Текущее наименование уже используется");
            }
            else
            {
                if (dataContent.Roles.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                                               x.ID != room.ID))
                    throw new ArgumentException("Текущее наименование уже используется");
            }
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
        private void SetValue(string name, int seatsCount, bool isProjector,
            bool isMarkerBoard, bool isBlock, bool isNew = true)
        {
            if (isNew)
            {
                room = new Room();
            }

            room.Name = name.GetNotNull();
            room.SeatsCount = seatsCount;
            room.IsProjector = isProjector;
            room.IsMarkerBoard = isMarkerBoard;
            room.IsBlock = isBlock;

            if (isNew) dataContent.Rooms.Add(room);
            dataContent.SaveChanges();
        }

        /// <summary>
        /// Удаление комнаты
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void Delete(int id)
        {
            room = dataContent.Rooms.FirstOrDefault(x => x.ID == id);
            if (room == null)
                throw new ArgumentException("Не найден объект");

            List<OrderRoom> orderRooms = dataContent.OrderRooms.Where(x => x.UserID == room.ID).ToList();
            if (orderRooms?.Count > 0)
            {
                for (int i = 0; i < orderRooms.Count; i++)
                {
                    dataContent.OrderRooms.Remove(orderRooms[i]);
                }
            }
            dataContent.Rooms.Remove(room);
            dataContent.SaveChanges();
        }

        /// <summary>
        /// Вернуть комнату 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public RoomInfo GetRoom(int id)
        {
            Room roomTemp = dataContent.Rooms.FirstOrDefault(x => x.ID == id);
            if (roomTemp == null) return null;
            return new RoomInfo
            {
                ID = roomTemp.ID,
                Name = roomTemp.Name,
                SeatsCount = roomTemp.SeatsCount,
                IsProjector = roomTemp.IsProjector,
                IsMarkerBoard = roomTemp.IsMarkerBoard,
                IsBlock = roomTemp.IsBlock
            };
        }

        /// <summary>
        /// Вернуть список комнат
        /// </summary>
        /// <returns></returns>
        public List<RoomInfo> GetRooms()
        {
            return dataContent.Rooms.Select(x => new RoomInfo
            {
                ID = x.ID,
                Name = x.Name,
                SeatsCount = x.SeatsCount,
                IsProjector = x.IsProjector,
                IsMarkerBoard = x.IsMarkerBoard,
                IsBlock = x.IsBlock
            }).ToList();
        }

        /// <summary>
        /// Вернуть список комнат по фильтру
        /// </summary>
        /// <returns></returns>
        /// <param name="seatsCount">Количество комнат от и более</param>
        /// <param name="projector">Проектор</param>
        /// <param name="markerBoard">Маркерная доска</param>
        public List<RoomInfo> GetRooms(int seatsCount, RoomIsProjector projector, RoomIsMarkerBoard markerBoard)
        {
            List<RoomInfo> roomList = dataContent.Rooms.Where(n => n.SeatsCount > seatsCount).Select(x => new RoomInfo
            {
                ID = x.ID,
                Name = x.Name,
                SeatsCount = x.SeatsCount,
                IsProjector = x.IsProjector,
                IsMarkerBoard = x.IsMarkerBoard,
                IsBlock = x.IsBlock
            }).ToList();
            if (roomList == null || roomList.Count == 0) return roomList;
            if (projector == RoomIsProjector.Projector)
                roomList = roomList.Where(x => x.IsProjector).ToList();
            else if (projector == RoomIsProjector.UnProjector)
                roomList = roomList.Where(x => !x.IsProjector).ToList();
            if (markerBoard == RoomIsMarkerBoard.MarkerBoard)
                roomList = roomList.Where(x => x.IsMarkerBoard).ToList();
            else if (markerBoard == RoomIsMarkerBoard.UnMarkerBoard)
                roomList = roomList.Where(x => !x.IsMarkerBoard).ToList();
            return roomList;
        }

        /// <summary>
        /// Вернуть список заказ переговорных по фильтру
        /// </summary>
        /// <returns></returns>
        /// <param name="seatsCount">Количество комнат от и более</param>
        /// <param name="projector">Проектор</param>
        /// <param name="markerBoard">Маркерная доска</param>
        /// <param name="dateStart">Дата начала период</param>
        /// <param name="dateEnd">Дата окончания периода</param>
        /// <param name="statusFilter">Фильтр по статусу брони</param>
        public List<OrderRoomInfo> GetRoomsOfFilters(int seatsCount, RoomIsProjector projector,
            RoomIsMarkerBoard markerBoard, DateTime dateStart, DateTime dateEnd, OrderRoomStatusFilter statusFilter)
        {
            RoomRepository roomRepository = new RoomRepository(dataContent);
            List<RoomInfo> roomList = roomRepository.GetRooms(seatsCount, projector, markerBoard);
            if (roomList == null || roomList.Count == 0) return new List<OrderRoomInfo>();
            List<OrderRoomInfo> orderRoomList = dataContent.OrderRooms
                .Where(x => x.Start >= dateStart && x.End <= dateEnd && roomList.Any(n => n.ID == x.ID)).Select(m =>
                    new OrderRoomInfo
                    {
                        ID = m.ID,
                        Start = m.Start,
                        End = m.End,
                        RoomID = m.RoomID,
                        MainDate = m.Start,
                        UserID = m.UserID,
                        Status = (OrderRoomStatus) m.Status
                    }).ToList();
            return orderRoomList;
        }
    }
}
