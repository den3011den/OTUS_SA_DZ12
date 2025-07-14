using Microsoft.EntityFrameworkCore;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_DataAccess;
using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext _db) : base(_db)
        {
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithNameSubstringFilterAsync(string nameSubstringFilter)
        {
            if (String.IsNullOrEmpty(nameSubstringFilter.Trim()))
            {
                var customersList = await _db.Customers.ToListAsync();
                return customersList;
            }
            else
            {
                var customersList = await _db.Customers.Where(u => u.FirstName.ToUpper().Contains(nameSubstringFilter.Trim().ToUpper())
                || u.LastName.ToUpper().Contains(nameSubstringFilter.Trim().ToUpper())
                || u.MiddleName.ToUpper().Contains(nameSubstringFilter.Trim().ToUpper())
                ).ToListAsync();
                return customersList;
            }
        }
    }
}
