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
    [RoutePrefix("api/rooms")] //TODO [Template Init]: Update route
    [ValidationFilter]
    public class RoomController : ApiController
    {
        private readonly IRoomService roomService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roomService"></param>
        [LogExceptions]
        public RoomController(IRoomService roomService)
        {
            this.roomService  = roomService;
        }

        /// <summary>
        /// Gets ... Room List
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAsync()
        {
            var result = await roomService.GetAsync();
            if (result != null)
                return Ok(result.ToContract());
            return NotFound();
        }


        /// <summary>
        /// Gets ... Room List Available Between FromDate, ToDate
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("{fromDate}/{toDate}")]
        public async Task<IHttpActionResult> GetAsync(DateTime fromDate, DateTime toDate)
        {
            var result = await roomService.GetAsync(fromDate, toDate);
            if (result != null)
                return Ok(result.ToContract());
            return NotFound();
        }

        /// <summary>
        /// Deletes ... a Room (only if not in use)
        /// </summary>
        /// <param name="id"></param>
        /// <response code="202">Accepted</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            await roomService.DeleteAsync(id);
            return StatusCode(HttpStatusCode.Accepted);
        }

        /// <summary>
        /// Adds ... a new Room
        /// </summary>
        /// <param name="room"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddAsync(RoomContract room)
        {
            await roomService.AddAsync(room.ToModel());
            return Created(new Uri(Request.RequestUri, room.RoomId.ToString()), room.RoomId);
        }

        /// <summary>
        /// Updates ... a Room
        /// </summary>
        /// <param name="room"></param>
        /// <response code="202">Accepted</response>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateAsync(RoomContract room)
        {
            try
            {
                await roomService.UpdateAsync(room.ToModel());
                return Content(HttpStatusCode.Accepted, room.RoomId);
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
            HotelContract param3)
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