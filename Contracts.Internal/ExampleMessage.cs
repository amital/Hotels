using System;
using Payoneer.ServicesInfra.DtoGeneration;

namespace Payoneer.Payoneer.Hotels.Contracts
{
    [GenerateDto]
    public class ExampleMessage
    {
        // Use a Guid here
        public Guid Id { get; set; }
    }
}
