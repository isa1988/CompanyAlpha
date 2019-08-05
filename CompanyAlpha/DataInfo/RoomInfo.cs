using System.Collections.Generic;

namespace CompanyAlpha.DataInfo
{
    /// <summary>
    /// Комната
    /// </summary>
    public class RoomInfo
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество кресел
        /// </summary>
        public int SeatsCount { get; set; }

        /// <summary>
        /// Наличие проектора
        /// </summary>
        public bool IsProjector { get; set; }

        /// <summary>
        /// Наличие маркерной доски
        /// </summary>
        public bool IsMarkerBoard { get; set; }

        /// <summary>
        /// Комната недоступна
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        /// Занята комната на время
        /// </summary>
        public List<OrderRoomInfo> OrderRoomList { get; set; }
        
        public override string ToString()
        {
            string retVal = Name + " кресел " + SeatsCount.ToString();
            if (IsProjector) retVal += " проектор есть";
            if (IsMarkerBoard) retVal += " доска есть";
            return retVal;
        }
    }

    /// <summary>
    /// Фильтр по проекторам
    /// </summary>
    public enum RoomIsProjector
    {
        /// <summary>
        /// Все
        /// </summary>
        All = 0,
        
        /// <summary>
        /// Проектор есть
        /// </summary>
        Projector = 1,
        
        /// <summary>
        /// Проектора нет
        /// </summary>
        UnProjector = 2
    }

    /// <summary>
    /// Фильтр по маркерным доскам
    /// </summary>
    public enum RoomIsMarkerBoard
    {
        /// <summary>
        /// Все
        /// </summary>
        All = 0,

        /// <summary>
        /// Маркерная доска есть
        /// </summary>
        MarkerBoard = 1,

        /// <summary>
        /// Маркерной доски нет 
        /// </summary>
        UnMarkerBoard = 2
    }
}
