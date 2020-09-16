using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memberships.Models
{
    public class UserSubscriptionModel
    {
        public int ID { get; set; }
        
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(20)]
        public string RegistrationCode { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}