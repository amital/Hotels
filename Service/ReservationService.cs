using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;
using Payoneer.ServicesInfra.DependencyInjection.Resolving;

namespace Payoneer.Payoneer.Hotels.Service
{
    public class ReservationService : IReservationService
    {
        public async Task AddAsync(Reservation reservation)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                context.Reservaitions.Add(reservation);
                await context.ApplyChangesAsync();
            }
        }

        public async Task<IList<Reservation>> GetAsync()
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                var rm = await context.Reservaitions.AsQueryable()
                    .ToListAsync();
                return rm;
            }
        }

        public async Task<IList<Reservation>> GetAsync(DateTime date)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                var rm = await context.Reservaitions.AsQueryable()
                    .Where(rv => rv.ReservedFrom <= date && rv.ReservedTo >= date)
                    .ToListAsync();

                return rm;
            }
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                var existing = await context.Reservaitions.AsQueryable().SingleOrDefaultAsync(x => x.ReservationId == reservation.ReservationId);
                if (existing == null)
                {
                    throw new KeyNotFoundException($"No record found with {nameof(Reservation.ReservationId)} {reservation.ReservationId}");
                }

                existing.InjectFrom(reservation);
                await context.ApplyChangesAsync();
            }
        }

        //private bool NameIsUnique(IHotelContext context, string name)
        //{
        //    var recs = context.Hotels.AsQueryable().Count(h => name.Trim().ToLower() == h.HotelName.Trim().ToLower());
        //    return recs == 0;
        //}

        public async Task DeleteAsync(int id)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                context.Reservaitions.Delete(e => e.ReservationId == id);
                await context.ApplyChangesAsync();
            }
        }

        /*
               /// <summary>
               /// An Example of how to read values by ID 
               /// </summary>
               /// <param name="ids"></param>
               /// <returns></returns>
               [CacheList("localCacheLRU", typeof(ReservationKeyConverter))]
               private async Task<IList<Reservation>> Get(IList<Guid> ids)
               {
                   List<Reservation> result;
                   using (var context = DiResolver.Resolve<IReservationContext>())
                   {
                       result = await context.Reservations.AsQueryable().Where(ent => ids.Contains(ent.Id)).ToListAsync();
                   }
       
                   return result;
               }
       
               private class ReservationKeyConverter : IDataKeyConverter<Guid, Reservation>
               {
                   public Guid GetKey(Reservation data)
                   {
                       return data.Id;
                   }
               }
               */
    }
}