using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Memberships.Models
{
    public class UserViewModel
    {
        [Display(Name ="User ID")]
        public string userID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [StringLength(30,ErrorMessage ="No more than 30 characters")]
        [MinLength(2,ErrorMessage ="Must be at least 2 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(30, ErrorMessage = "No more than 30 characters")]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters.")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}