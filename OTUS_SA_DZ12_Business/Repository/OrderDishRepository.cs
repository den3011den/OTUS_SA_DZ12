using Microsoft.EntityFrameworkCore;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_DataAccess;
using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository
{
    public class OrderDishRepository : Repository<OrderDish>, IOrderDishRepository
    {
        public OrderDishRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<OrderDish> GetOrderDishByOrderIdAndDishIdAsync(int orderId, int dishId)
        {
            var foundOrderDish = await _db.OrdersDishes.FirstOrDefaultAsync(u => u.OrderId == orderId && u.DishId == dishId);
            return foundOrderDish;
        }
    }
}
