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
                PasswordSalt = hmac.Key,
                FirstName = registerDTO.FirstName.ToLower(),
                LastName = registerDTO.LastName.ToLower(),
               Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserType="User"

            };
            datacontext.Users.Add(Appuser);
           await datacontext.SaveChangesAsync();
            return new UserDTO { 
            Email = Appuser.Email,
            UserType= Appuser.UserType,
            Token = "JBCJBVJKBVJABVJLADVBJLBVLAJKBVLKABKLABVBLKAKLVAKVL"
            };
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> login(LoginDTO loginDTO) {
            var user = await datacontext.Users.SingleOrDefaultAsync(x => x.Email == loginDTO.Email);
            if (user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.PasswordHash));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDTO
            {
                Email = user.Email,
                UserType = user.UserType,
                Token = "JBCJBVJKBVJABVJLADVBJLBVLAJKBVLKABKLABVBLKAKLVAKVL"
            };
        }
    }
}
