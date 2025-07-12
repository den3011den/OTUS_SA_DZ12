using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository.IRepository
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        public Task<Feedback> GetFeedbackByOrderIdAndDishIdAsync(int orderId, int dishId);
    }
}
