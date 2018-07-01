using System.ComponentModel.DataAnnotations;
using Payoneer.ServicesInfra.DtoGeneration;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    public class ReservationCI
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        [Required]
        public System.DateTime ReservedFrom { get; set; }
        [Required]
        public System.DateTime ReservedTo { get; set; }
        public int CustomerId { get; set; }
    }
}
