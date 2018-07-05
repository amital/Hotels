using System.Collections.Generic;
using System.Threading.Tasks;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;

namespace Payoneer.Payoneer.Hotels.Service
{
    public interface ICustomerService
    {
        Task AddAsync(Customer customer);
        Task<IList<Customer>> GetAsync();
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
    }
}
