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
using CarRental.API.UtilitiesObjects;

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

        public  ActionResult getFormData(string city, DateTime startDate, DateTime endDate, CarModel model) {

            var _Action = CheckData(startDate, endDate, city);

            if (_Action.GetType() == typeof(OkResult))
            {
                FormData bookingFormData = new FormData(city.ToLower(), startDate, endDate, model);
                List<CarSelectedObject> carSelected = _bookingRepository.GetData(bookingFormData);
                if (carSelected.Count == 0) return NoContent();
                return Ok(carSelected);
            }
            return _Action;
        }
        private ActionResult CheckData(DateTime startDate, DateTime endDate, string city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (city == null || city.Trim().Length == 0)
            {
                return BadRequest("City is required");
            }
            else if (startDate.ToString().Length == 0 || endDate.ToString().Length == 0)
            {
                return BadRequest("Date is required");
            }
            else if (startDate < DateTime.Today)
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
            return Ok();
        }


    }
}
