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
using Microsoft.AspNetCore.Authorization;
using CarRental.API.Extensions;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            IEnumerable<User> userList = await _userRepository.GetAllAsync(null, null, null);
            return new JsonResult(userList);
        }

        // GET: api/User/5
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            var username = User.GetUsername();
            var userId = User.GetUserId();
            if (!email.Contains("@") || !email.Contains("."))
            {
                return BadRequest("Enter Valid Email Id.");
            }
            User userRecord = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == email);

            if (userRecord == null)
            {
                return NotFound("User profile does not exist");
            }

            return userRecord;
        }

        //PUT: api/User/5
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUser(string email, User user)
        {
            var username = User.GetUsername();
            if (email != user.Email || !email.Contains("@"))
            {
                return BadRequest("Email Id is not valid.");
            }

            _userRepository.UpdateUser(user);
            int id = user.UserId;

            try
            {
                await _userRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _userRepository.DoesRecordExist(id))
                {
                    return NotFound("User profile does not exist");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

    }
}
