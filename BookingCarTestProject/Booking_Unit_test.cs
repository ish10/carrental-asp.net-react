using CarRental.API.Controllers;
using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository;
using CarRental.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingCarTestProject
{
    public class UnitTest1
    {
        //Arrange (Remove that with MOQ testing Later)
        IBookingRepository IBR;
        DbContextOptions<CarRentalDbContext> options;
        CarRentalDbContext CR;
        BookingController BC;

        public UnitTest1()
        {
            options = new DbContextOptions<CarRentalDbContext>();
            CR = new CarRentalDbContext(options);
            IBR = new BookingRepository(CR);
            BC = new BookingController(IBR);
        }

        [Fact]
        public void BookingGetData()
        {

            //Act
            var compareto0 = BC.getAsync((ProvinceNames)0, new DateTime(2021, 8, 4), new DateTime(2021, 8, 4), (CarModel)2); //accepted
            var compareto1 = BC.getAsync((ProvinceNames)0, new DateTime(2021, 7 , 4), new DateTime(2021, 8, 4), (CarModel)2); // not accepted because old date issue
            var compareto2 = BC.getAsync((ProvinceNames)0, new DateTime(2021, 8, 4), new DateTime(2021, 7, 4), (CarModel)2); // not accepted because old date issue
            var compareto3 = BC.getAsync((ProvinceNames)0, new DateTime(2021, 8 ,7), new DateTime(2021, 8 , 4), (CarModel)2); // not accepted because start date comes after the end date issue

            //Assert
            Assert.IsType<OkObjectResult>(compareto0.Result);
            Assert.IsType<BadRequestObjectResult>(compareto1.Result);
            Assert.IsType<BadRequestObjectResult>(compareto2.Result);
            Assert.IsType<BadRequestObjectResult>(compareto3.Result);
            
        }
    }
}
