using System.ComponentModel.DataAnnotations;
using Payoneer.ServicesInfra.DtoGeneration;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    [GenerateDto]
    public class HotelContract
    {
        public int HotelId { get; set; }
        [Required]
        public string HotelName { get; set; }
        [Required]
        public short NumberOfFloors { get; set; }
    }
}
