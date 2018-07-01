using System;
using System.ComponentModel.DataAnnotations;
using Payoneer.ServicesInfra.DtoGeneration;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    [GenerateDto]
    public class ExampleContract
    {
        // Use any type that can be a DB-PK here
        public Guid Id { get; set; }

        [Required]
        public string LookupName { get; set; }
    }
}
