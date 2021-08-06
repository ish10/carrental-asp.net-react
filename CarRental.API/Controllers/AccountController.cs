using CarRental.API.Data;
using CarRental.API.Dtos;
using CarRental.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpPost("register")]
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
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> login(LoginDTO loginDTO) {
            var user = await datacontext.Users.SingleOrDefaultAsync(x => x.Email == loginDTO.Email);
            if (user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);

            return new UserDTO
            {
               // Username = registerDTO.FirstName + registerDTO.LastName,
                Token = "JBCJBVJKBVJABVJLADVBJLBVLAJKBVLKABKLABVBLKAKLVAKVL"
            };
        }
    }
}
