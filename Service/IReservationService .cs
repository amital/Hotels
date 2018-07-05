using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;

namespace Payoneer.Payoneer.Hotels.Service
{
    public interface IReservationService
    {
        Task AddAsync(Reservation reservation);
        Task<IList<Reservation>> GetAsync();
        Task<IList<Reservation>> GetAsync(DateTime date);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(int id);
    }
}
