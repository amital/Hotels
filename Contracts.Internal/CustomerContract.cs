using System.ComponentModel.DataAnnotations;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    public class CustomerContract
    {
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
    }
}
