using CompanyAlpha.DataInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCompanyAlpha.Models
{
    /// <summary>
    /// Заказы комнат модель для JSon
    /// </summary>
    public class OrderRoomJSonModel
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Полное наименваемк
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Начальная время периода
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// Конечная время периода
        /// </summary>
        public string End { get; set; }
        /// <summary>
        /// Начальная время периода
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Конечная время периода
        /// </summary>
        public DateTime EndDate { get; set; }

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

        public string StatusName { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string UserCur { get; set; }

        /// <summary>
        /// Комната
        /// </summary>
        public string RoomCur { get; set; }

        /// <summary>
        /// Фоновый цвет
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// Цвет рамки 
        /// </summary>
        public string BorderColor { get; set; }

        public void SetColor()
        {
            BackgroundColor = "#DCD4B2";
            BorderColor = "#000000";
            if (Status == OrderRoomStatus.ReservationApproved)
                BackgroundColor = "#9CEE91";
            else if (Status == OrderRoomStatus.ReservationDeclined)
                BackgroundColor = "#EE91AA";
        }
    }
}