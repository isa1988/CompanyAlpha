using CompanyAlpha.DataInfo;
using System;

namespace WebCompanyAlpha.Models
{
    public class OrderRoomModel : Page
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Датв
        /// </summary>
        public DateTime MainDate { get; set; }
        /// <summary>
        /// Начальная время периода
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Конечная время периода
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
        public OrderRoomStatus Status { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string UserCur { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        public string RoomCur { get; set; }
    }
}