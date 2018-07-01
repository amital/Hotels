using System.ComponentModel.DataAnnotations;
using Payoneer.ServicesInfra.DtoGeneration;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    public class RoomCI
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
    }
}
