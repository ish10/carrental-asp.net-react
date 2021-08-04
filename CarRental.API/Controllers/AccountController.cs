using CarRental.API.Data;
using CarRental.API.Dtos;
using CarRental.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly CarRentalDbContext datacontext;

        public AccountController(CarRentalDbContext datacontext)
        {
           
            this.datacontext = datacontext;

        }
        [HttpPost]
        public async Task<ActionResult<UserDTO>> register(RegisterDTO registerDTO) {

            using var hmac = new HMACSHA512();
            var Appuser = new User
            {
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.PasswordHash)),

                FirstName = registerDTO.FirstName.ToLower(),
                LastName = registerDTO.LastName.ToLower(),
               Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber

            };
            datacontext.Users.Add(Appuser);
           await datacontext.SaveChangesAsync();
            return new UserDTO { 
            Username = registerDTO.FirstName + registerDTO.LastName,
            Token = "JBCJBVJKBVJABVJLADVBJLBVLAJKBVLKABKLABVBLKAKLVAKVL"
            };
        }
    }
}
