using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;
using Payoneer.ServicesInfra.DependencyInjection.Resolving;

namespace Payoneer.Payoneer.Hotels.Service
{
    public class CustomerService : ICustomerService
    {
        public async Task AddAsync(Customer customer)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                context.Customers.Add(customer);
                await context.ApplyChangesAsync();
            }
        }

        public async Task<IList<Customer>> GetAsync()
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                return await context.Customers.AsQueryable().ToListAsync();
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                var existing = await context.Customers.AsQueryable().SingleOrDefaultAsync(x => x.CustomerId == customer.CustomerId);
                if (existing == null)
                {
                    throw new KeyNotFoundException($"No record found with {nameof(customer.CustomerId)} {customer.CustomerId}");
                }

                existing.InjectFrom(customer);
                await context.ApplyChangesAsync();
            }
        }

        //private bool NameIsUnique(IHotelContext context, string name)
        //{
        //    var recs =  context.Customers.AsQueryable().Count(h => name.Trim().ToLower() == h.CustomerName.Trim().ToLower());
        //    return recs == 0;
        //}
        public async Task DeleteAsync(int id)
        {
            using (var context = DiResolver.Resolve<IHotelContext>())
            {
                context.Customers.Delete(e => e.CustomerId == id);
                await context.ApplyChangesAsync();
            }
        }

 /*
        /// <summary>
        /// An Example of how to read values by ID 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [CacheList("localCacheLRU", typeof(customerKeyConverter))]
        private async Task<IList<customer>> Get(IList<Guid> ids)
        {
            List<customer> result;
            using (var context = DiResolver.Resolve<IcustomerContext>())
            {
                result = await context.customers.AsQueryable().Where(ent => ids.Contains(ent.Id)).ToListAsync();
            }

            return result;
        }

        private class customerKeyConverter : IDataKeyConverter<Guid, customer>
        {
            public Guid GetKey(customer data)
            {
                return data.Id;
            }
        }
        */
    }
}
