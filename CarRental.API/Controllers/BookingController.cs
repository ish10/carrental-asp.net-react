using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.API.Repository;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using NSwag.Annotations;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")] // (/api/Booking)
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            this._bookingRepository = bookingRepository;
        }

        [Route("BookingReservation")]
        [HttpGet]

        public async Task<ActionResult> getAsync(ProvinceNames city, DateTime startDate, DateTime endDate, CarModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            } else if(startDate < DateTime.Today)
            {
                return BadRequest("Start Date can't be in the past");
            }
            else if (endDate < DateTime.Today)
            {
                return BadRequest("End Date can't be in the past");
            }
            else if (startDate > endDate)
            {
                return BadRequest("Start Date can't come after the End Date");
            }

            return Ok(await _bookingRepository.GetData(city, startDate, endDate, model));
        }
    }
}
