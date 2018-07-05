using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;

namespace Payoneer.Payoneer.Hotels.Service
{
    public interface IRoomService
    {
        Task AddAsync(Room room);
        Task<IList<Room>> GetAsync();
        Task<IList<Room>> GetAsync(DateTime from, DateTime to);
        Task UpdateAsync(Room room);
        Task DeleteAsync(int id);
    }
}
