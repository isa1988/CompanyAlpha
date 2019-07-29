using System;

namespace CompanyAlpha.DataInfo
{
    public class OrderRoomInfo
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
        public UserInfo UserCur { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        public RoomInfo RoomCur { get; set; }

        /// <summary>
        /// Полное наименование комнаты
        /// </summary>
        public string RoomFullName { get; set; }
    }

    /// <summary>
    /// Статусы брони
    /// </summary>
    public enum OrderRoomStatus
    {
        /// <summary>
        /// Подача брони
        /// </summary>
        FilingArmor = 0,
        
        /// <summary>
        /// Бронь одобрена
        /// </summary>
        ReservationApproved = 1,
        
        /// <summary>
        /// бронь отклонена
        /// </summary>
        ReservationDeclined = 2,
        
        /// <summary>
        /// бронь помечена на удаление менеджером
        /// </summary>
        ReservationMarkedForDeletionByManager = 3
    }


    /// <summary>
    /// Статусы брони фильтр
    /// </summary>
    public enum OrderRoomStatusFilter
    {
        /// <summary>
        /// Подача брони
        /// </summary>
        FilingArmor = 0,

        /// <summary>
        /// Бронь одобрена
        /// </summary>
        ReservationApproved = 1,

        /// <summary>
        /// бронь отклонена
        /// </summary>
        ReservationDeclined = 2,

        /// <summary>
        /// Все
        /// </summary>
        All = 3
    }
}
