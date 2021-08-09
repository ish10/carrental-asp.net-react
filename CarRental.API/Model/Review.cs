using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.API.Model
{
    public class Review
    {
        [Key]
        [Required]
        public int FeedbackId { get; set; }

        public int starValue { get; set; }

        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        
        public string Description { get; set; }

        public DateTime PostTime { get; set; }

        [Display(Name = "User")]
        public virtual int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
