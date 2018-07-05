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
    public class RoomService : IRoomService
    {
        public async Task AddAsync(Room room)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                context.Rooms.Add(room);
                await context.ApplyChangesAsync();
            }
        }

        public async Task<IList<Room>> GetAsync()
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                var rm = await context.Rooms.AsQueryable()
                    .Include(b => b.RoomBeds)
                    .Include(b => b.Reservations)
                    .ToListAsync();
                return rm;
            }
        }

        public async Task<IList<Room>> GetAsync(DateTime from, DateTime to)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                var rm = await context.Rooms.AsQueryable()
                    .Include(b => b.RoomBeds)
                    .Include(b => b.Reservations)
                    .Where(r => (r.Reservations.Count == 0) ||
                                (!r.Reservations.Any(rv =>
                                    (rv.ReservedTo >= from && rv.ReservedFrom <= to))))
                    .ToListAsync();

                return rm;
            }
        }

        public async Task UpdateAsync(Room room)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                var existing = await context.Rooms.AsQueryable().SingleOrDefaultAsync(x => x.RoomId == room.RoomId);
                if (existing == null)
                {
                    throw new KeyNotFoundException($"No record found with {nameof(room.RoomId)} {room.RoomId}");
                }

                existing.InjectFrom(room);
                await context.ApplyChangesAsync();
            }
        }

        private bool NameIsUnique(IHotelContext context, string name)
        {
            var recs = context.Hotels.AsQueryable().Count(h => name.Trim().ToLower() == h.HotelName.Trim().ToLower());
            return recs == 0;
        }

        public async Task DeleteAsync(int id)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                context.Rooms.Delete(e => e.RoomId == id);
                await context.ApplyChangesAsync();
            }
        }

        /*
               /// <summary>
               /// An Example of how to read values by ID 
               /// </summary>
               /// <param name="ids"></param>
               /// <returns></returns>
               [CacheList("localCacheLRU", typeof(RoomKeyConverter))]
               private async Task<IList<Room>> Get(IList<Guid> ids)
               {
                   List<Room> result;
                   using (var context = DiResolver.Resolve<IRoomContext>())
                   {
                       result = await context.Rooms.AsQueryable().Where(ent => ids.Contains(ent.Id)).ToListAsync();
                   }
       
                   return result;
               }
       
               private class RoomKeyConverter : IDataKeyConverter<Guid, Room>
               {
                   public Guid GetKey(Room data)
                   {
                       return data.Id;
                   }
               }
               */
    }
}