using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;

namespace Payoneer.Payoneer.Hotels.Service
{
    public interface IHotelService
    {
        Task AddAsync(Hotel hotel);
        Task<IList<Hotel>> GetAsync();
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(int id);
    }
}
