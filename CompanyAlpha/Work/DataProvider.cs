using CompanyAlpha.Contract;
using CompanyAlpha.DataModel;

namespace CompanyAlpha.Work
{
    /// <summary>
    /// Работа с данными
    /// </summary>
    class DataProvider : IDataProvider
    {
        /// <summary>
        /// Работа с базой
        /// </summary>
        private DataContent dataContent;
        private RoleWork role;
        private UserWork user;
        private RoomWork room;
        private OrderRoomWork orderRoom;

        /// <summary>
        /// Роли
        /// </summary>
        public IRoleRepository Role
        {
            get { return role ?? (role = new RoleWork(dataContent)); }
        }

        /// <summary>
        /// Пользователи
        /// </summary>
        public IUserRepository User
        {
            get { return user ?? (user = new UserWork(dataContent)); }
        }

        /// <summary>
        /// Переговорные (комнаты)
        /// </summary>
        public IRoomRepository Room
        {
            get { return room ?? (room = new RoomWork(dataContent)); }
        }

        /// <summary>
        /// Бронь переговорных
        /// </summary>
        public IOrderRoomRepository OrderRoom
        {
            get { return orderRoom ?? (orderRoom = new OrderRoomWork(dataContent)); }
        }

        /// <summary>
        /// Закрыть поток
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Диструктор
        /// </summary>
        ~DataProvider()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (dataContent != null)
            {
                dataContent.Dispose();
                dataContent = null;
            }
        }
    }
}
