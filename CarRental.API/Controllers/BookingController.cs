using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.API.Repository;
using CarRental.API.Model;

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
        public async Task<ActionResult<string>> getAsync(ProvinceNames city, DateTime startDate, DateTime endDate, CarModel model) {
            
            return await _bookingRepository.GetData(city, startDate, endDate, model);
        }
    }
}
