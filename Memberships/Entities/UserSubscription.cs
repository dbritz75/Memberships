using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    [Table("User_Subscription")]
    public class User_Subscription
    {
        [Required]
        [Key,Column(Order =1)]
        public int SubscriptionID { get; set; }

        public string  FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [Key, Column(Order = 2)]
        [MaxLength(128)]
        public string UserID { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}