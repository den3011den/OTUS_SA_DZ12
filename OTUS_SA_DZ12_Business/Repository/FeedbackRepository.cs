using Microsoft.EntityFrameworkCore;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_DataAccess;
using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(ApplicationDbContext _db) : base(_db)
        {

        }

        public async Task<Feedback> GetFeedbackByOrderIdAndDishIdAsync(int orderId, int dishId)
        {
            var foundFeedback = await _db.Feedbacks.FirstOrDefaultAsync(u => u.OrderId == orderId && u.DishId == dishId);
            return foundFeedback;
        }
    }
}
