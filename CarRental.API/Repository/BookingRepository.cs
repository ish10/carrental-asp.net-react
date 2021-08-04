using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CarRentalDbContext carRentalContext;

        public BookingRepository(CarRentalDbContext carRentalContext)
        {
            this.carRentalContext = carRentalContext;
        }


        public async Task<ActionResult<string>> GetData(ProvinceNames city, DateTime startDate, DateTime endDate, CarModel model)
        {
           
            //if (startDate > endDate)
            //{
            //    return "Check your dates. The start date is after the end date";
            //}
            //else if(endDate < DateTime.Today)
            //{
            //    return "endDate must be today or after";
            //} else if (startDate < DateTime.Today)
            //{
            //    return "startDate must be today or after";
            //}
            return trial(city, startDate, endDate, model);
        }
        public string trial(ProvinceNames city, DateTime startDate, DateTime endDate, CarModel model)
        {
            return "city: " + city + " startDate: " + startDate + " endDate: " + endDate + " carModel:" + model;
        }
    }
}
