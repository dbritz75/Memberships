using Memberships.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    public class SubscriptionProductModel
    {
        [DisplayName("Product ID")]
        public int ProductID { get; set; }
        [DisplayName("Subscription ID")]
        public int SubscriptionID { get; set; }
        [DisplayName("Product")]
        public string ProductTitle { get; set; }
        [DisplayName("Subscription")]
        public string SubscriptionTitle { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }

    }
}