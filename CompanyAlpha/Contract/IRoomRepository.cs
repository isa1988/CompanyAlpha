using CompanyAlpha.DataInfo;
using System.Collections.Generic;

namespace CompanyAlpha.Contract
{
    interface IRoomRepository
    {
        /// <summary>
        /// Добавить новую комнату
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="seatsCount">Количество кресел</param>
        /// <param name="isProjector">Наличие проектора</param>
        /// <param name="isMarkerBoard">Наличие маркерной доски</param>
        /// <returns></returns>
        int Insert(string name, int seatsCount, bool isProjector, bool isMarkerBoard);

        /// <summary>
        /// Добавить новую комнату
        /// </summary>
        /// <param name="room">Модель комнаты</param>
        /// <returns></returns>
        int Insert(RoomInfo room);
        
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
        void Edit(int id, string name, int seatsCount, bool isProjector, bool isMarkerBoard, bool isBlock);

        /// <summary>
        /// Редактирование комнаты
        /// </summary>
        /// <param name="room">Модель комнаты</param>
        /// <returns></returns>
        void Edit(RoomInfo room);

        /// <summary>
        /// Удаление комнаты
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void Delete(int id);

        /// <summary>
        /// Вернуть комнату 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        RoomInfo GetRoom(int id);

        /// <summary>
        /// Вернуть список комнат
        /// </summary>
        /// <returns></returns>
        List<RoomInfo> GetRooms();


        /// <summary>
        /// Вернуть список комнат по фильтру
        /// </summary>
        /// <returns></returns>
        /// <param name="seatsCount">Количество комнат от и более</param>
        /// <param name="projector">Проектор</param>
        /// <param name="markerBoard">Маркерная доска</param>
        List<RoomInfo> GetRooms(int seatsCount, RoomIsProjector projector, RoomIsMarkerBoard markerBoard);
    }
}
