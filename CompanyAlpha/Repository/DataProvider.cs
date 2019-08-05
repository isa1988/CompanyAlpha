using CompanyAlpha.Contract;
using CompanyAlpha.DataModel;

namespace CompanyAlpha.Repository
{
    /// <summary>
    /// Работа с данными
    /// </summary>
    public class DataProvider : IDataProvider
    {
        /// <summary>
        /// Работа с базой
        /// </summary>
        private DataContent dataContent;
        private RoleRepository role;
        private UserRepository user;
        private RoomRepository room;
        private OrderRoomRepository orderRoom;

        public DataProvider()
        {
            this.dataContent = new DataContent();
        }

        /// <summary>
        /// Роли
        /// </summary>
        public IRoleRepository Role
        {
            get { return role ?? (role = new RoleRepository(dataContent)); }
        }

        /// <summary>
        /// Пользователи
        /// </summary>
        public IUserRepository User
        {
            get { return user ?? (user = new UserRepository(dataContent)); }
        }

        /// <summary>
        /// Переговорные (комнаты)
        /// </summary>
        public IRoomRepository Room
        {
            get { return room ?? (room = new RoomRepository(dataContent)); }
        }

        /// <summary>
        /// Бронь переговорных
        /// </summary>
        public IOrderRoomRepository OrderRoom
        {
            get { return orderRoom ?? (orderRoom = new OrderRoomRepository(dataContent)); }
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
