using System;
using System.Threading.Tasks;
using Payoneer.Payoneer.Hotels.Model.ExampleDomain;

namespace Payoneer.Payoneer.Hotels.Service
{
    public interface IExampleService
    {
        Task AddAsync(ExampleModel exampleModel);

        Task UpdateAsync(ExampleModel exampleModel);

        Task<ExampleModel> GetAsync(Guid id);

        Task DeleteAsync(Guid id);
    }
}
