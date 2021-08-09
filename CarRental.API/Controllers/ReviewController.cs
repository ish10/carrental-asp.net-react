using CarRental.API.Dtos;
using CarRental.API.Model;
using CarRental.API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Review
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetReview()
        {
            IEnumerable<Review> ReviewList = await _unitOfWork.Review.GetAllAsync(null, null, nameof(Location));
            return new JsonResult(ReviewList);
        }


        [HttpPost]
        public async Task<ActionResult<Car>> CreateReview([FromBody]ReviewDTO reviewDTO)
        {
            if(reviewDTO == null)
            {
                return BadRequest(ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Review reviewobj = new Review()
            {
                starValue = reviewDTO.starValue,
                Title = reviewDTO.Title,
                Description = reviewDTO.Description,
                PostTime = DateTime.Now
            };

            _unitOfWork.Review.AddReview(reviewobj);

            
            return Ok();
        }

        //PUT :api/Review/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id ,Review review)
        {
            if(id <= 0 || review.FeedbackId != id)
            {
                return BadRequest("Id is not valid or Feedback not found");
            }


            _unitOfWork.Review.Update(review);


            try
            {
                await _unitOfWork.Review.Save();    //Seve the review 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.Review.DoesRecordExist(review.FeedbackId))
                {
                    return NotFound("review record does not exist or Not authorise to change");
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }


        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id cannot be less than 0");
            }

            Review review = await _unitOfWork.Review.GetAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            await _unitOfWork.Review.RemoveAsync(review);
            await _unitOfWork.Review.Save();

            return NoContent();
        }





    }
}
