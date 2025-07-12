using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<State?> GetOrderStateByOrderIdAsync(int id);
    }
}
