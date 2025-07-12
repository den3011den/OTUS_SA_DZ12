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
    }
}
