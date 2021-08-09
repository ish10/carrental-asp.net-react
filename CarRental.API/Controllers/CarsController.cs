using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.API.Data;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using System.Net.Http;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<IEnumerable<Car>> GetCars()
        {
            IEnumerable<Car> carsList = await _unitOfWork.Car.GetAllAsync(null, null, nameof(Location));
            return carsList;
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id cannot be less than 0");
            }

            Car carRecord = await _unitOfWork.Car.GetFirstOrDefaultAsync(m => m.CarId == id, nameof(Location));
            //var car = await _context.Cars.FindAsync(id);

            if (carRecord == null)
            {
                return NotFound("Car record does not exist");
            }

            return carRecord;
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.CarId || id <= 0)
            {
                return BadRequest("Id is not valid");
            }
            _unitOfWork.Car.Update(car);

            try
            {
                await _unitOfWork.Car.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Car.DoesRecordExist(id))
                {
                    return NotFound("Car record does not exist");
                }
                else
                {
                    throw;
                }
            }
            var test = await GetCar(id);
            return NoContent();
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            car.CarId = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            if (car.CarId < 0 || car.LocationId < 0)
            {
                return BadRequest("CarId should not be less than 0 and location Id Should not be less than 0");
            }

            Location location = await _unitOfWork.Location.GetFirstOrDefaultAsync(l => l.City == car.Location.City && l.Province == car.Location.Province);
            //Location location = await _unitOfWork.Location.GetAsync(car.LocationId);

            if (location != null)
            {

                //await _unitOfWork.Location.AddAsync(car.Location);
                //await _unitOfWork.Location.Save();
                //Location locationInserted = await _unitOfWork.Location.GetFirstOrDefaultAsync(l => l.City == car.Location.City && l.Province == car.Location.Province);
                //car.LocationId = locationInserted.LocationId;
                //car.Location.LocationId = locationInserted.LocationId;
                //car.Location.Province = locationInserted.Province;
                //car.Location.City = locationInserted.City;

                car.LocationId = location.LocationId;
                car.Location = null;
            }

            await _unitOfWork.Car.AddAsync(car);
            await _unitOfWork.Car.Save();

            return await GetCar(car.CarId);

            //return CreatedAtAction("GetCar", new { id = car.CarId }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id cannot be less than 0");
            }

            Car car = await _unitOfWork.Car.GetAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            await _unitOfWork.Car.RemoveAsync(car);
            await _unitOfWork.Car.Save();

            return NoContent();
        }
    }
}
