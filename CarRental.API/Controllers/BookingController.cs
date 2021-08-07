﻿using Microsoft.AspNetCore.Http;
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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
        public ActionResult getFormData(string city, DateTime startDate, DateTime endDate, CarModel model)
        {
            FormData bookingFormData = new FormData(city.ToLower(), startDate, endDate, model);
            var _Action = CheckData(bookingFormData);

            if (_Action.GetType() == typeof(OkResult))
            {

                List<CarSelectedObject> carSelected = _bookingRepository.GetData(bookingFormData);
                if (carSelected.Count == 0) return NoContent();
                return Ok(carSelected);
            }
            return _Action;
        }

        [Route("ConfrimTrip")]
        [HttpPost]
        public async Task<ActionResult> ApplyBooking_and_Confirm([FromBody] CarSelectedObject selected_car)
        {
            /*error To check and Test: {
              "carId": 0,
              "model": 0,
              "modelValue": null,
              "pricePerDay": 0,
              "imge": null,
              "numberPlate": null,
              "startDate": "0001-01-01T00:00:00",
              "endDate": "0001-01-01T00:00:00"
            }*/
            ActionResult statusCode = checkPostData(selected_car);
            if (statusCode.GetType() == typeof(OkResult))
            {
                await _bookingRepository.ApplyBooking_and_confrimation(selected_car);
                return Ok(statusCode + " " + selected_car); //accepted but no content to return back
            }
            return statusCode;
        }

        private ActionResult checkPostData(CarSelectedObject chosenObject)
        {
            // check startDate and endDate in case the client didn't send it
            var statusCode = CheckData(new FormData("None", chosenObject.startDate, chosenObject.endDate, chosenObject.model));
            if (statusCode.GetType() == typeof(BadRequestObjectResult))
            {
                return BadRequest("Dates is requried and can't be in the past");
            }

            // check that the data send by client is valid by comparing them in db
            var queryCheck = _bookingRepository.CheckPostValid(chosenObject);
            if (queryCheck.Length == 0)
            {
                return BadRequest("Car information is correct, check your data values");
            }

            //var jsonStringnew = JsonConvert.SerializeObject(chosenObject).Length;
            return Ok();
        }
        private ActionResult CheckData(FormData form_data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (form_data._cityName == null || form_data._cityName.Trim().Length == 0)
            {
                return BadRequest("City is required");
            }
            else if (form_data._startDate.ToString().Length == 0 || form_data._endDate.ToString().Length == 0)
            {
                return BadRequest("Date is required");
            }
            else if (form_data._startDate < DateTime.Today)
            {
                return BadRequest("Start Date can't be in the past");
            }
            else if (form_data._endDate < DateTime.Today)
            {
                return BadRequest("End Date can't be in the past");
            }
            else if (form_data._startDate > form_data._endDate)
            {
                return BadRequest("Start Date can't come after the End Date");
            }
            return Ok();
        }

    }
}
