using System.Data.Entity;
using Payoneer.ServicesInfra.Repositories;
using Payoneer.ServicesInfra.Repositories.EF;
// ReSharper disable UnusedAutoPropertyAccessor.Local -  db model readonly

namespace Payoneer.Payoneer.Hotels.Model.HotelsDomain
{
    public class HotelContext : CodeFirstEfContext, IHotelContext
    {
        // Use connection string named "MyContext" from app.config/web.config
        public HotelContext() : base(nameof(HotelContext))
        {
        }

        // Pass custom connection string e.g. LocalDB for integration tests
        public HotelContext(string connectionNameOrString)
            : base(connectionNameOrString)
        {
        }

        public IEntitySet<Hotel> Hotels { get; private set; }

        public IEntitySet<Customer> Customers { get; private set; }
  
        public IEntitySet<Room> Rooms { get; private set; }
  
        public IEntitySet<RoomBed> RoomBeds { get; private set; }

        public IEntitySet<Reservation> Reservaitions { get; private set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<HotelContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
