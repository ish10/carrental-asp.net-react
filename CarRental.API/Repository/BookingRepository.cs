using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using CarRental.API.UtilitiesObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CarRental.API.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CarRentalDbContext carRentalContext;

        public BookingRepository(CarRentalDbContext carRentalContext)
        {
            this.carRentalContext = carRentalContext;
        }

        public List<CarSelectedObject> GetData(FormData formdata)
        {
            var result = CarSet(formdata._cityName, formdata._startDate, formdata._endDate, formdata._Model);
            return result;
        }

        private List<CarSelectedObject> CarSet(string city, DateTime startDate, DateTime endDate, CarModel model)
        {
            List<CarSelectedObject> _result = new List<CarSelectedObject>();

            var cars = (from carT in carRentalContext.Cars
                        join loc in (from loc in carRentalContext.Locations
                                     where loc.City.Equals(city)
                                     select loc.LocationId)
                        on carT.LocationId equals loc
                        where carT.Model.Equals(model) && carT.IsRented.Equals(false)
                        select new { carId = carT.CarId, model = carT.Model, pricaPerDay = carT.PricePerDay, plate = carT.NumberPlate, image = carT.Image }).ToArray();

            foreach (var c in cars)
            {
                _result.Add(new CarSelectedObject(c.carId, c.model, Enum.GetName(typeof(CarModel), c.model), c.pricaPerDay, c.image, c.plate, startDate, endDate));
            }

            return _result;
        }

        public async Task ApplyBooking_and_confrimation(CarSelectedObject chosenCar)
        {
            int TotalAmount = getTotalAmount(chosenCar);
        }

        //calculate TotalAmount = totalDays * priceperDay 
        private int getTotalAmount(CarSelectedObject chosenCar)
        {
            int TotalDays = (int)(chosenCar.endDate.Date - chosenCar.startDate.Date).TotalDays;
            return (int)chosenCar.pricePerDay * TotalDays; //e.g 30 dollars/day * 3 days = 90 dollars.
        }

        public object[] CheckPostValid(CarSelectedObject chosenCar)
        {
            var car = (from carT in carRentalContext.Cars
                       where carT.CarId.Equals(chosenCar.carId) && carT.Model.Equals(chosenCar.model)
                             && carT.PricePerDay.Equals(chosenCar.pricePerDay) && carT.Image.Equals(chosenCar.image)
                             && carT.NumberPlate.Equals(chosenCar.numberPlate)
                       select carT).ToArray();
            return car;
        }
    }
}




