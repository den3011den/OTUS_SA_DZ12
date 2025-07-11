using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_DataAccess.DbInitializer
{
    /// <summary>
    /// Инициализация БД - создание и наполнение начальными данными
    /// </summary>
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор инициализации БД значениями по умолчанию
        /// </summary>
        /// <param name="db">Контекст БД приложения</param>
        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Метод наполения БД значениями по умолчанию
        /// </summary>
        public void InitializeDb()
        {
            //Console.WriteLine("Инициализация БД: Удаление БД ... ");
            //_db.Database.EnsureDeleted();
            //Console.WriteLine("Инициализация БД: Удаление БД - Выполнено");

            //Console.WriteLine("Инициализация БД: Создание БД ... ");
            //_db.Database.EnsureCreated();
            //Console.WriteLine("Инициализация БД: Создание БД - Выполнено");

            Console.WriteLine("Инициализация БД: Очистка таблицы Feedbacks ... ");
            _db.Feedbacks.RemoveRange(_db.Feedbacks);
            _db.SaveChanges();

            Console.WriteLine("Инициализация БД: Очистка таблицы OrdersDishes ... ");
            _db.OrdersDishes.RemoveRange(_db.OrdersDishes);
            _db.SaveChanges();

            Console.WriteLine("Инициализация БД: Очистка таблицы Orders ... ");
            _db.Orders.RemoveRange(_db.Orders);
            _db.SaveChanges();

            Console.WriteLine("Инициализация БД: Очистка таблицы ReceiveMethods ... ");
            _db.ReceiveMethods.RemoveRange(_db.ReceiveMethods);
            _db.SaveChanges();

            Console.WriteLine("Инициализация БД: Очистка таблицы States ... ");
            _db.States.RemoveRange(_db.States);
            _db.SaveChanges();

            Console.WriteLine("Инициализация БД: Очистка таблицы Dishes ... ");
            _db.Dishes.RemoveRange(_db.Dishes);
            _db.SaveChanges();

            Console.WriteLine("Инициализация БД: Очистка таблицы Customers ... ");
            _db.Customers.RemoveRange(_db.Customers);
            _db.SaveChanges();

            Console.WriteLine("Инициализация БД: Заполнение таблицы Customers ... ");
            FillTable<Customer>(InitialDataFactory.Customers);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Customers - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы Dishes ... ");
            FillTable<Dish>(InitialDataFactory.Dishes);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Dishes - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы States ... ");
            FillTable<State>(InitialDataFactory.States);
            Console.WriteLine("Инициализация БД: Заполнение таблицы States - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы ReceiveMethods ... ");
            FillTable<ReceiveMethod>(InitialDataFactory.ReceiveMethods);
            Console.WriteLine("Инициализация БД: Заполнение таблицы ReceiveMethods - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы Orders ... ");
            FillTable<Order>(InitialDataFactory.Orders);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Orders - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы OrdersDishes ... ");
            FillTable<OrderDish>(InitialDataFactory.OrdersDishes);
            Console.WriteLine("Инициализация БД: Заполнение таблицы OrdersDishes - Выполнено");

            Console.WriteLine("Инициализация БД: Заполнение таблицы Feedbacks ... ");
            FillTable<Feedback>(InitialDataFactory.Feedbacks);
            Console.WriteLine("Инициализация БД: Заполнение таблицы Feedbacks - Выполнено");

        }


        /// <summary>
        /// Метод заполнения таблицы БД для определённой сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <param name="tableList">Список данных для записи в БД</param>
        public void FillTable<T>(List<T> tableList)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in tableList)
                    {
                        _db.Add(item);
                    }
                    _db.SaveChanges();
                    transaction.Commit();

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }
    }
}
