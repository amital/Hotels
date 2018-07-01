using System.Data.Entity;
using Payoneer.ServicesInfra.Repositories;
using Payoneer.ServicesInfra.Repositories.EF;

namespace Payoneer.Payoneer.Hotels.Model.ExampleDomain
{
    public class ExampleDomainContext : CodeFirstEfContext, IExampleDomainContext
    {
        // Use connection string named "MyContext" from app.config/web.config
        public ExampleDomainContext() : base(nameof(ExampleDomainContext))
        {
        }

        // Pass custom connection string e.g. LocalDB for integration tests
        public ExampleDomainContext(string connectionNameOrString)
            : base(connectionNameOrString)
        {
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public IEntitySet<ExampleModel> Examples { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public IEntitySet<LookupData> LookupData { get; private set; }

        // ReSharper disable once RedundantOverriddenMember
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TODO: Any additional mappings your model needs
        }

        // TODO: [NewApp] call your SPs like so

        //public void DoSomething(int param1, string param2)
        //{
        //    var rowsChanged = this.ExecuteStoredProcedureNonQuery(
        //        "[dbo].[DoSomething]",
        //        new ParameterDefinition("param1", param1),
        //        new ParameterDefinition("param2", param2));

        //    if (rowsChanged == 0)
        //        throw new DataAccessFailure("[dbo].[DoSomething] had no effect");
        //}

        //public int CountSomething(int param1, string param2)
        //{
        //    var result = this.ExecuteStoredProcedureScalar<int>(
        //        "[dbo].[CountSomething]",
        //        new ParameterDefinition("param1", param1),
        //        new ParameterDefinition("param2", param2));

        //    return result;
        //}

        //public List<ExampleModel> GetSomething(int param1, string param2)
        //{
        //    var results = this.ExecuteStoredProcedureQuery<ExampleModel>(
        //        "[dbo].[GetSomething]",
        //        new ParameterDefinition("param1", param1),
        //        new ParameterDefinition("param2", param2));

        //    return results;
        //}
    }
}
