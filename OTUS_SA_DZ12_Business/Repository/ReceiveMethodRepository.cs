using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_DataAccess;
using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository
{
    public class ReceiveMethodRepository : Repository<ReceiveMethod>, IReceiveMethodRepository
    {
        public ReceiveMethodRepository(ApplicationDbContext _db) : base(_db)
        {
        }
    }
}
