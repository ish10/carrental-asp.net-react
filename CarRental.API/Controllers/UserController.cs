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

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (id < 0)
            {
                return BadRequest("Id cannot be less than zero.");
            }
            User userRecord = await _userRepository.GetFirstOrDefaultAsync(u => u.UserId == id);

            if (userRecord == null)
            {
                return NotFound("User profile does not exist");
            }

            return userRecord;
        }

        //PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId || id < 0)
            {
                return BadRequest("Id is not valid.");
            }

            _userRepository.UpdateUser(user);

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