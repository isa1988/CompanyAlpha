using System.Collections.Generic;

namespace CompanyAlpha.DataModel
{
    /// <summary>
    /// Комната
    /// </summary>
    class Room
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
        public virtual ICollection<OrderRoom> OrderRoomList { get; set; }
    }
}
