using System;

namespace CompanyAlpha.DataModel
{
    class OrderRoom
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Начальная дата периода
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Конечная дата периода
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        public int RoomID { get; set; }

        /// <summary>
        /// Статус комнаты
        /// 0 - подача брони
        /// 1 - бронь одобрена
        /// 2 - бронь отклонена
        /// 3 - бронь помечана на удаление менеджером
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual User UserCur { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        public virtual Room RoomCur { get; set; }
    }
}
