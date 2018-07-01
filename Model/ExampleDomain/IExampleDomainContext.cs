using Payoneer.ServicesInfra.Repositories;

namespace Payoneer.Payoneer.Hotels.Model.ExampleDomain
{
    public interface IExampleDomainContext : IDomainContext
    {
        IEntitySet<ExampleModel> Examples { get; }

        IEntitySet<LookupData> LookupData { get; }

        // TODO: [NewApp] call your SPs like so

        //void DoSomething(int param1, string param2);

        //int CountSomething(int param1, string param2);

        //List<ExampleModel> GetSomething(int param1, string param2);
    }
}
