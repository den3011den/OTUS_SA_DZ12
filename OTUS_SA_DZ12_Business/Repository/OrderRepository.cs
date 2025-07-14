using Microsoft.EntityFrameworkCore;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_DataAccess;
using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        /// <summary>
        /// Получить Статус заказа по его ИД
        /// </summary>
        /// <param name="id">ИД заказа</param>
        /// <returns>Возвращает Статус закза - объект State</returns>
        public async Task<State?> GetOrderStateByOrderIdAsync(int id)
        {
            var order = _db.Orders
                    .Include("State")
                    .FirstOrDefault(u => u.Id == id);
            if (order == null)
            {
                return null;
            }
            else
            {
                return order.State;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByTimeIntervalOfOrderdateAsync(DateTime startTime, DateTime endTime)
        {
            DateTime startTimeLocal = DateTime.SpecifyKind(startTime, DateTimeKind.Local);
            //DateTime startTimeUTC = startTimeLocal.ToUniversalTime();

            DateTime endTimeLocal = DateTime.SpecifyKind(endTime, DateTimeKind.Local);
            //DateTime endTimeUTC = endTimeLocal.ToUniversalTime();

            var tagValueList = await _db.Orders
                    .Include("Customer")
                    .Include("State")
                    .Include("ReceiveMethod")
                    .Include("OrdersDishesList")
                    .Include("OrdersDishesList.Dish")
                    .Include("FeedbacksList")
                    .Include("FeedbacksList.Dish")
                    .Where(u => u.OrderDate >= startTimeLocal && u.OrderDate <= endTimeLocal)
                    .ToListAsync();
            return tagValueList;
        }
    }
}
