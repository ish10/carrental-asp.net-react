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
using Moq;

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
            var compareto0 = BC.getFormData("Burnaby", new DateTime(2021, 8, 7), new DateTime(2021, 8, 7), (CarModel)2); //accepted but return Nocontent for model
            var compareto1 = BC.getFormData("klm", new DateTime(2021, 8, 9), new DateTime(2021, 8, 7), (CarModel)2); // accepted but return Nocontent for city
            var compareto2 = BC.getFormData("null", new DateTime(2021, 8, 9), new DateTime(2021, 8, 7), (CarModel)2); // accepted but return Nocontent for city
            var compareto3 = BC.getFormData("Burnaby", new DateTime(2021, 7 , 4), new DateTime(2021, 8, 7), (CarModel)3); // not accepted because startDate old date issue
            var compareto4 = BC.getFormData("Burnaby", new DateTime(2021, 8, 7), new DateTime(2021, 7, 4), (CarModel)3); // not accepted because endDate old date issue
            var compareto5 = BC.getFormData("Burnaby", new DateTime(2021, 8 ,9), new DateTime(2021, 8 , 7), (CarModel)3); // not accepted because start date comes after the end date issue
            var compareto6 = BC.getFormData("", new DateTime(2021, 8, 9), new DateTime(2021, 8, 7), (CarModel)2); // not accepted because empty string, city is required
            var compareto7 = BC.getFormData(null, new DateTime(2021, 8, 9), new DateTime(2021, 8, 7), (CarModel)2); // not accepted because null string, city is required
            var compareto8 = BC.getFormData("Burnaby", new DateTime(2021, 8, 7), new DateTime(2021, 8, 7), (CarModel)3); //accepted with JSON Array

            //Assert
            Assert.IsType<OkObjectResult>(compareto0);
            Assert.IsType<OkObjectResult>(compareto0);
            Assert.IsType<OkObjectResult>(compareto0);
            Assert.IsType<OkObjectResult>(compareto0);
            Assert.IsType<OkObjectResult>(compareto0);
            Assert.IsType<OkObjectResult>(compareto0);
            Assert.IsType<OkObjectResult>(compareto0);
            Assert.IsType<OkObjectResult>(compareto0);
            Assert.IsType<OkObjectResult>(compareto0);
            Assert.IsType<OkObjectResult>(compareto0);

        }
    }
}
