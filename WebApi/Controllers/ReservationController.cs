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
    /// Example Manage Reservations
    /// </summary>
    [RoutePrefix("api/reservations")] //TODO [Template Init]: Update route
    [ValidationFilter]
    public class ReservationController : ApiController
    {
        private readonly IReservationService reservationService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reservationService"></param>
        [LogExceptions]
        public ReservationController(IReservationService reservationService)
        {
            this.reservationService  = reservationService;
        }

        /// <summary>
        /// Gets ... Reservation List
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAsync()
        {
            var result = await reservationService.GetAsync();
            if (result != null)
                return Ok(result.ToContract());
            return NotFound();
        }


        /// <summary>
        /// Gets ... Reservation List for a given date
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("{date}")]
        public async Task<IHttpActionResult> GetAsync(DateTime date)
        {
            var result = await reservationService.GetAsync(date);
            if (result != null)
                return Ok(result.ToContract());
            return NotFound();
        }

        /// <summary>
        /// Deletes ... a Reservation (only if not in use)
        /// </summary>
        /// <param name="id"></param>
        /// <response code="202">Accepted</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            await reservationService.DeleteAsync(id);
            return StatusCode(HttpStatusCode.Accepted);
        }

        /// <summary>
        /// Adds ... a new Reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddAsync(ReservationContract reservation)
        {
            await reservationService.AddAsync(reservation.ToModel());
            return Created(new Uri(Request.RequestUri, reservation.ReservationId.ToString()), reservation.ReservationId);
        }

        /// <summary>
        /// Updates ... a Reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <response code="202">Accepted</response>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateAsync(ReservationContract reservation)
        {
            try
            {
                await reservationService.UpdateAsync(reservation.ToModel());
                return Content(HttpStatusCode.Accepted, reservation.ReservationId);
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