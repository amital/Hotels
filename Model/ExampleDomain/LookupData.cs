using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payoneer.Payoneer.Hotels.Model.ExampleDomain
{
    public class LookupData
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id { get; set; }

        [MaxLength(100)][Index(IsUnique = true)][Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
