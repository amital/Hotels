﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
//using System.Web.Http;
using Payoneer.Payoneer.Hotels.Contracts;
using Payoneer.Payoneer.Hotels.Model.HotelsDomain;
using Payoneer.Payoneer.Hotels.Service;
using Payoneer.ServicesInfra.WebAPI.Validation;
using PubComp.Aspects.Monitoring;
using System.Web.Http;

namespace Payoneer.Payoneer.Hotels.WebApi.Controllers
{
    /// <summary>
    /// Example TODO: [NewApp] Update this
    /// </summary>
    [RoutePrefix("api/hotels/")] //TODO [Template Init]: Update route
    [ValidationFilter]
    public class HotelController : ApiController
    {
        private readonly IHotelService hotelService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hotelService"></param>
        [LogExceptions]
        public HotelController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
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
            var result = await hotelService.GetAsync();
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
            await HotelService.DeleteAsync(id);
            return StatusCode(HttpStatusCode.Accepted);
        }

        /// <summary>
        /// Adds ... TODO: [NewApp] Update this
        /// </summary>
        /// <param name="hotel"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> AddAsync(Hotel hotel)
        {
            await HotelService.AddAsync(hotel.ToModel());
            return Created(new Uri(Request.RequestUri, hotel.HotelId.ToString()), hotel.HotelId);
        }

        /// <summary>
        /// Updates ... TODO: [NewApp] Update this
        /// </summary>
        /// <param name="hotel"></param>
        /// <response code="202">Accepted</response>
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateAsync(HotelContract hotel)
        {
            try
            {
                await hotelService.UpdateAsync(hotel.ToModel());
                return Content(HttpStatusCode.Accepted, hotel.Id);
            }
            catch (KeyNotFoundException)
            {
                return httpNotFound();
            }
        }

        /// <summary>
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
    }
}