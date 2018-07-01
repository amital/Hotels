using System.ComponentModel.DataAnnotations;
using Payoneer.ServicesInfra.DtoGeneration;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    public class CustomerCI
    {
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
    }
}
