using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using Payoneer.Payoneer.Hotels.Model.ExampleDomain;
using Payoneer.ServicesInfra.DependencyInjection.Resolving;
using PubComp.Caching.AopCaching;

namespace Payoneer.Payoneer.Hotels.Service
{
    public class ExampleService : IExampleService
    {
        public async Task AddAsync(ExampleModel exampleModel)
        {
            using (var context = DiResolver.Resolve<IExampleDomainContext>())
            {
                context.Examples.Add(exampleModel);
                await context.ApplyChangesAsync();
            }
        }

        public async Task<ExampleModel> GetAsync(Guid id)
        {
            return (await Get(new [] { id })).FirstOrDefault();
        }

        public async Task UpdateAsync(ExampleModel exampleModel)
        {
            using (var context = DiResolver.Resolve<IExampleDomainContext>())
            {
                var existing = await context.Examples.AsQueryable().SingleOrDefaultAsync(x => x.Id == exampleModel.Id);
                if (existing == null)
                {
                    throw new KeyNotFoundException($"No record found with {nameof(exampleModel.Id)} {exampleModel.Id}");
                }

                existing.InjectFrom(exampleModel);
                await context.ApplyChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var context = DiResolver.Resolve<IExampleDomainContext>())
            {
                context.Examples.Delete(e => e.Id == id);
                await context.ApplyChangesAsync();
            }
        }

        /// <summary>
        /// An example of how to read values by ID 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [CacheList("localCacheLRU", typeof(ExampleModelKeyConverter))]
        private async Task<IList<ExampleModel>> Get(IList<Guid> ids)
        {
            List<ExampleModel> result;
            using (var context = DiResolver.Resolve<IExampleDomainContext>())
            {
                result = await context.Examples.AsQueryable().Where(ent => ids.Contains(ent.Id)).ToListAsync();
            }

            return result;
        }

        private class ExampleModelKeyConverter : IDataKeyConverter<Guid, ExampleModel>
        {
            public Guid GetKey(ExampleModel data)
            {
                return data.Id;
            }
        }
    }
}
