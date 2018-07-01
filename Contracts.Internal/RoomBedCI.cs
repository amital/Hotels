using System.ComponentModel.DataAnnotations;
using Payoneer.ServicesInfra.DtoGeneration;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    public class RoomBedCI
    {
        public int RoomId { get; set; }
        public int BedId { get; set; }
        [Required]
        public short BedType { get; set; }
    }
}
