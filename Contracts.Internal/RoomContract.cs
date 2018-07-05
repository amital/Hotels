using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    public class RoomContract
    {
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        [Required]
        public short FloorNumber { get; set; }
        [Required]
        public decimal SizeSqMtr { get; set; }
        [Required]
        public bool HasBalcony { get; set; }
        [Required]
        public bool PoolFacing { get; set; }
        public ICollection<RoomBedContract> RoomBeds { get; set; }
        public ICollection<ReservationContract> Reservations { get; set; }
    }
}
