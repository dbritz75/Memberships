using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    [Table("Subscription_Product")]
    public class Subscription_Product
    {
        //Product ID and Subscription ID is a composite primary key, thus the "Key(Column...)" syntax.

        [Required]
        [Key, Column(Order = 1)]
        public int SubscriptionID { get; set; }
        [Required]
        [Key,Column(Order =2)]
        public int ProductID { get; set; }

        [NotMapped]
        public int OldProductID { get; set; }

        [NotMapped]
        public int OldSubscriptionID { get; set; }
    }
}