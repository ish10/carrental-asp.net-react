using CarRental.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.UtilitiesObjects
{
    public class CarSelectedObject
    {
        public int CarId { get; private set; }
        public CarModel Model { get; private set; }
        public string ModelValue { get; private set; }
        public Double PricePerDay { get; private set; }

        public string Image { get; private set; }
        public string NumberPlate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public CarSelectedObject(int carID, CarModel model, string modelValue, Double price , string Image, string plate, DateTime startDate, DateTime endDate)
        {
            this.CarId = carID;
            this.Model = model;
            this.ModelValue = modelValue;
            this.PricePerDay = price;
            this.Image = Image;
            this.NumberPlate = plate;
            this.StartDate = startDate;
            this.EndDate = endDate;
                
        }
    }
}
