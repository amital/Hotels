using System.ComponentModel.DataAnnotations;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    public class CustomerCI
    {
        public int CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
    }
}
