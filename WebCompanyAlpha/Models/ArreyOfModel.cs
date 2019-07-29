
using System.Collections.Generic;
using WebCompanyAlpha.Models.Account;

namespace WebCompanyAlpha.Models
{
    /// <summary>
    /// Обощающий класс для списков
    /// </summary>
    public class ArreyOfModel : Page
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        public List<UserModel> UserModels { get; set; }

        /// <summary>
        /// Переговорные
        /// </summary>
        public List<RoomModel> RoomModels { get; set; }
    }
}