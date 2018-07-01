using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;
using Payoneer.ServicesInfra.DependencyInjection.Resolving;
using PubComp.Caching.AopCaching;

namespace Payoneer.Payoneer.Hotels.Service
{
    public class HotelService : IHotelService
    {
        public async Task AddAsync(Hotel hotel)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                context.Hotels.Add(hotel);
                await context.ApplyChangesAsync();
            }
        }

        public async Task<IList<Hotel>> GetAsync()
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                return await context.Hotels.AsQueryable().ToListAsync();
            }
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                var existing = await context.Hotels.AsQueryable().SingleOrDefaultAsync(x => x.HotelId == hotel.HotelId);
                if (existing == null)
                {
                    throw new KeyNotFoundException($"No record found with {nameof(Hotel.HotelId)} {hotel.HotelId}");
                }

                existing.InjectFrom(hotel);
                await context.ApplyChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                context.Hotels.Delete(e => e.HotelId == id);
                await context.ApplyChangesAsync();
            }
        }

 /*
        /// <summary>
        /// An Example of how to read values by ID 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [CacheList("localCacheLRU", typeof(HotelKeyConverter))]
        private async Task<IList<Hotel>> Get(IList<Guid> ids)
        {
            List<Hotel> result;
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                result = await context.Hotels.AsQueryable().Where(ent => ids.Contains(ent.Id)).ToListAsync();
            }

            return result;
        }

        private class HotelKeyConverter : IDataKeyConverter<Guid, Hotel>
        {
            public Guid GetKey(Hotel data)
            {
                return data.Id;
            }
        }
        */
    }
}
