using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
//using System.Web.Http;
using Payoneer.Payoneer.Hotels.Contracts;
using Payoneer.Payoneer.Hotels.Service;
using Payoneer.Payoneer.Hotels.WebApi.ContractModelMapping;
using Payoneer.ServicesInfra.WebAPI.Validation;
using PubComp.Aspects.Monitoring;

namespace Payoneer.Payoneer.Hotels.WebApi.Controllers
{
    /// <summary>
    /// Example TODO: [NewApp] Update this
    /// </summary>
    [RoutePrefix("api/example/v1")] //TODO [Template Init]: Update route
    [ValidationFilter]
    public class ExampleV1Controller : ApiController
    {
        private readonly IExampleService exampleService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exampleService"></param>
        [LogExceptions]
        public ExampleV1Controller(IExampleService exampleService)
        {
            this.exampleService = exampleService;
        }

        /// <summary>
        /// Gets ... TODO: [NewApp] Update this
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var result = await exampleService.GetAsync(id);
            if (result != null)
                return Ok(result.ToContract());
            return NotFound();
        }

        /// <summary>
        /// Deletes ... TODO: [NewApp] Update this
        /// </summary>
        /// <param name="id"></param>
        /// <response code="202">Accepted</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await exampleService.DeleteAsync(id);
            return StatusCode(HttpStatusCode.Accepted);
        }

        /// <summary>
        /// Adds ... TODO: [NewApp] Update this
        /// </summary>
        /// <param name="example"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddAsync(ExampleContract example)
        {
            await exampleService.AddAsync(example.ToModel());
            return Created(new Uri(Request.RequestUri, example.Id.ToString()),example.Id);
        }

        /// <summary>
        /// Updates ... TODO: [NewApp] Update this
        /// </summary>
        /// <param name="example"></param>
        /// <response code="202">Accepted</response>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateAsync(ExampleContract example)
        {
            try
            {
                await exampleService.UpdateAsync(example.ToModel());
                return Content(HttpStatusCode.Accepted, example.Id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// TODO: [NewApp] Remove this method, its only here to demonstrate the validation 
        /// </summary>
        [HttpPost]
        [Route("Validate")]
        public string Validate(
            [Required] string param1, [MaxLength(2)] string param2,
            ExampleContract param3)
        {
            if (param1 == null)
                return $"This should not happen {nameof(param1)} is null";

            if (param2?.Length > 2)
                return $"This should not happen {nameof(param2)}.{nameof(string.Length)} > 2";

            return "OK";
        }
    }
}
