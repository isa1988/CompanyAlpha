using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAlpha.Contract
{
    /// <summary>
    /// Операции с данными
    /// </summary>
    public interface IDataProvider : IDisposable
    {
        /// <summary>
        /// Роли
        /// </summary>
        IRoleRepository Role { get; }

        /// <summary>
        /// Пользователи
        /// </summary>
        IUserRepository User { get; }

        /// <summary>
        /// Переговорные (комнаты)
        /// </summary>
        IRoomRepository Room { get; }

        /// <summary>
        /// Бронь переговорных
        /// </summary>
        IOrderRoomRepository OrderRoom { get; }
    }
}
