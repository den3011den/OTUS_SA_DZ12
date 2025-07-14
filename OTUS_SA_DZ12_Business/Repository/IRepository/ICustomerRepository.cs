using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_Business.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task<IEnumerable<Customer>> GetCustomersWithNameSubstringFilterAsync(string nameSubstringFilter);
    }
}
