using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository.IRepository
{
    public interface IDishRepository : IRepository<Dish>
    {
        public Task<IEnumerable<Dish>> GetDishesWithNameSubstringFilterAsync(string nameSubstringFilter);
    }
}
