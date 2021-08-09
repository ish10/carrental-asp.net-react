using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllCars()
        {

            IEnumerable<Car> carsList = await _unitOfWork.Car.GetAllAsync(null, null, nameof(Location));
            return new JsonResult(carsList);
        }

        [Route("FilterCarList")]
        [HttpGet]
        public async Task<IActionResult> GetFilterCars(ProvinceNames province ,string city,CarModel model)
        {

            // getting all car list 
            IEnumerable<Car> carList = await _unitOfWork.Car.GetAllAsync(null, null, nameof(Location));

            // return cars which mathces users selection of filter . 

            IEnumerable<Car> FilterList = carList.Where(c => c.IsRented == false).Select(c=>c);
            
            if (model >= 0)
            {
                FilterList = FilterList.Where(c=>c.Model == model).Select(s=>s);


                if (province >= 0)
                {
                    if (city != null)
                    {
                        FilterList = FilterList.Where(c => (c.Location.Province.Equals(province) && c.Location.City.Equals(city))).Select(c=>c);
                    }
                    else
                    {
                        FilterList = FilterList.Where(c => c.Location.Province.Equals(province)).Select(c => c);
                    }
                }
                // Did't check for only city as dropdown for city is only available once they select any province.  
            }
            else
            {
                if (province >= 0)
                {
                    if (city != null)
                    {
                        FilterList = FilterList.Where(c => (c.Location.Province.Equals(province) && c.Location.City.Equals(city))).Select(c => c);
                    }
                    else
                    {
                        FilterList = FilterList.Where(c => c.Location.Province.Equals(province)).Select(c => c);
                    }
                }
            }
            
            return new JsonResult(FilterList);
        }



    }
}
