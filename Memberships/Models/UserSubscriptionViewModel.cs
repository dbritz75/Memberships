using Memberships.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Models
{
    public class UserSubscriptionViewModel
    {
        public ICollection<Subscription> Subscriptions { get; set; } //Display available subscriptions in dropdown
        public ICollection<UserSubscriptionModel> UserSubscriptions { get; set; }
        public bool disableDropdown { get; set; }
        public string UserID { get; set; }
        public string UserFName { get; set; }
        public string UserLName { get; set; }
        public int SubscriptionID { get; set; }
    }
}