using CompanyAlpha.DataInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        [DisplayName("Число")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MainDate { get; set; }

        /// <summary>
        /// Начальная время периода
        /// </summary>
        [DisplayName("Время начала")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDT { get; set; }

        /// <summary>
        /// Конечная время периода
        /// </summary>
        [DisplayName("Время окончания")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDT { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [DisplayName("Пользователь")]
        public int UserID { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        [DisplayName("Комната")]
        public int RoomID { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        [DisplayName("Комната")]
        public List<SelectListItem> Rooms { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public int MainUserID { get; set; }

        /// <summary>
        /// Статус комнаты
        /// 0 - подача брони
        /// 1 - бронь одобрена
        /// 2 - бронь отклонена
        /// 3 - бронь помечана на удаление менеджером
        /// </summary>
        public OrderRoomStatus Status { get; set; }

        [DisplayName("Статус")]
        public string StatusName { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [DisplayName("Пользователь")]
        public string UserCur { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        [DisplayName("Комната")]
        public string RoomCur { get; set; }

        public List<OrderRoomModel> ReservationApprovedList { get; set; }
    }
}