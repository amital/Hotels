using Payoneer.ServicesInfra.Repositories;

namespace Payoneer.Payoneer.Hotels.Model.HotelsDomain
{
    public interface IHotelContext : IDomainContext
    {
        IEntitySet<Hotel> Hotels { get; }

        IEntitySet<Customer> Customers { get; }

        IEntitySet<Room> Rooms { get; }

        IEntitySet<RoomBed> RoomBeds { get; }

        IEntitySet<Reservation> Reservaitions { get; }
    }
}