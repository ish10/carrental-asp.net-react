using CarRental.API.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Repository.IRepository
{
    public interface IBookingRepository
    {
        Task<ActionResult<string>> GetData(ProvinceNames city, DateTime startDate, DateTime endDate, CarModel model);

    }
}
