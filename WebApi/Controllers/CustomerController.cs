using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
//using System.Web.Http;
using Payoneer.Payoneer.Hotels.Contracts;
using Payoneer.Payoneer.Hotels.Service;
using Payoneer.ServicesInfra.WebAPI.Validation;
using PubComp.Aspects.Monitoring;
using System.Web.Http;
using Payoneer.Payoneer.Hotels.WebApi.ContractModelMapping;

namespace Payoneer.Payoneer.Hotels.WebApi.Controllers
{
    /// <summary>
    /// Example Manage Hotels
    /// </summary>
    [RoutePrefix("api/customers")] //TODO [Template Init]: Update route
    [ValidationFilter]
    public class CustomerController : ApiController
    {
        private readonly ICustomerService customerService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerService"></param>
        [LogExceptions]
        public CustomerController(ICustomerService customerService)
        {
            this.customerService  = customerService;
        }

        /// <summary>
        /// Gets ... Customers List
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAsync()
        {
            var result = await customerService.GetAsync();
            if (result != null)
                return Ok(result.ToContract());
            return NotFound();
        }

        /// <summary>
        /// Deletes ... a Customer (only if not in use)
        /// </summary>
        /// <param name="id"></param>
        /// <response code="202">Accepted</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            await customerService.DeleteAsync(id);
            return StatusCode(HttpStatusCode.Accepted);
        }

        /// <summary>
        /// Adds ... a new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddAsync(CustomerCI customer)
        {
            await customerService.AddAsync(customer.ToModel());
            return Created(new Uri(Request.RequestUri, customer.CustomerId.ToString()), customer.CustomerId);
        }

        /// <summary>
        /// Updates ... a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <response code="202">Accepted</response>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateAsync(CustomerCI customer)
        {
            try
            {
                await customerService.UpdateAsync(customer.ToModel());
                return Content(HttpStatusCode.Accepted, customer.CustomerId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
/*        /// <summary>
        /// TODO: [NewApp] Remove this method, its only here to demonstrate the validation 
        /// </summary>
        [HttpPost]
        [Route("Validate")]
        public string Validate(
            [Required] string param1, [MaxLength(2)] string param2,
            HotelCI param3)
        {
            if (param1 == null)
                return $"This should not happen {nameof(param1)} is null";

            if (param2?.Length > 2)
                return $"This should not happen {nameof(param2)}.{nameof(string.Length)} > 2";

            return "OK";
        }
        */
    }
}