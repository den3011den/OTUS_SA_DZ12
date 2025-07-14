using Microsoft.EntityFrameworkCore;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_DataAccess;
using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        public DishRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<IEnumerable<Dish>> GetDishesWithNameSubstringFilterAsync(string nameSubstringFilter)
        {
            if (String.IsNullOrEmpty(nameSubstringFilter.Trim()))
            {
                var dishesList = await _db.Dishes.ToListAsync();
                return dishesList;
            }
            else
            {
                var dishesList = await _db.Dishes.Where(u => u.Name.ToUpper().Contains(nameSubstringFilter.Trim().ToUpper())
                ).ToListAsync();
                return dishesList;
            }
        }
    }
}
