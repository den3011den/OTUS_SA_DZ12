using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository.IRepository
{
    public interface IOrderDishRepository : IRepository<OrderDish>
    {
        public Task<OrderDish> GetOrderDishByOrderIdAndDishIdAsync(int orderId, int dishId);
    }
}
