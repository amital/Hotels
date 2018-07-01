using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payoneer.Payoneer.Hotels.Model.ExampleDomain
{
    public class ExampleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        public short? LookupDataId { get; set; }
    }
}
