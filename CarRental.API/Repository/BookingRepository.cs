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

        public async Task<Trip> ApplyBooking_and_confrimation(CarSelectedObject chosenCar)
        {
            int TotalAmount = getTotalAmount(chosenCar);

            //Insert the confirmed trip in the trip entity
            var trip = await InsertIntoTripsDB(chosenCar, TotalAmount);

            //Get Single car which need to be updated and change the isRented field to true.
            await UpdateCarsDB(chosenCar);

            return trip;
        }



        /*start of Class's dependent behaviours*/

            // Here we are comparing the sent JSON object with the entity, in order to check if any data is changed
            public object[] CheckPostCarValid(CarSelectedObject chosenCar)
                {
                    try
                    {
                        var car = (from carT in carRentalContext.Cars
                                   where carT.CarId.Equals(chosenCar.carId) && carT.Model.Equals(chosenCar.model)
                                         && carT.PricePerDay.Equals(chosenCar.pricePerDay) && carT.Image.Equals(chosenCar.image)
                                         && carT.NumberPlate.Equals(chosenCar.numberPlate)
                                   select carT).ToArray();
                        return car;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

            // Here we are checking that the user is in our db
            public object CheckUserValid(CarSelectedObject userObject)
            {
                try
                {
                    var user = carRentalContext.Users.Where(u => u.UserId == userObject.userId).Select(u => u);
                    return user;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            //select list of cars from db
            private List<CarSelectedObject> CarSet(string city, DateTime startDate, DateTime endDate, CarModel model)
            {
                List<CarSelectedObject> _result = new List<CarSelectedObject>();
                try
                {
                    var cars = (from carT in carRentalContext.Cars
                                join loc in (from loc in carRentalContext.Locations
                                             where loc.City.Equals(city)
                                             select loc.LocationId)
                                on carT.LocationId equals loc
                                where carT.Model.Equals(model) && carT.IsRented.Equals(false)
                                select new { carId = carT.CarId, model = carT.Model, pricaPerDay = carT.PricePerDay, plate = carT.NumberPlate, image = carT.Image }).ToArray();

                    foreach (var c in cars)
                    {
                        _result.Add(new CarSelectedObject(-1, c.carId, c.model, Enum.GetName(typeof(CarModel), c.model), c.pricaPerDay, c.image, c.plate, startDate, endDate));
                    }
                }
                catch (Exception error)
                {
                    _result = null;
                }
                return _result;
            }

            //Update a car's isRented filed
            private async Task UpdateCarsDB(CarSelectedObject chosenCar) {
                var car = carRentalContext.Cars
                            .Where(c => c.CarId == chosenCar.carId)
                            .FirstOrDefault();
                car.IsRented = true;
                await carRentalContext.SaveChangesAsync();
            }

            //Insert new trip in the trip entity
            private async Task<Trip> InsertIntoTripsDB(CarSelectedObject chosenCar, int total_amount)
            {
                var trip = new Trip()
                {
                    StartDate = chosenCar.startDate,
                    EndDate = chosenCar.endDate,
                    TotalAmount = total_amount,
                    CarId = chosenCar.carId,
                    UserId = chosenCar.userId
                };

                await carRentalContext.Trips.AddAsync(trip);
                await carRentalContext.SaveChangesAsync();
                return trip;
            }

            //calculate TotalAmount = totalDays * priceperDay 
            private int getTotalAmount(CarSelectedObject chosenCar)
            {
                int TotalDays = (int)(chosenCar.endDate.Date - chosenCar.startDate.Date).TotalDays;
                return (int)chosenCar.pricePerDay * TotalDays; //e.g 30 dollars/day * 3 days = 90 dollars.
            }

        /*end of Class's dependent behaviours*/
    }
}




