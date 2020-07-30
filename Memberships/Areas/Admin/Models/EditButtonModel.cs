using Memberships.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    public class EditButtonModel
    {
        public int ItemID { get; set; }
        public int ProductID { get; set; }
        public int SubscriptionID { get; set; }
        public string Link {
            get
            {
                var s = new StringBuilder("?");
                if (ItemID > 0)
                    s.Append(String.Format("{0}={1}&", "itemID", ItemID));
                    s.Append(String.Format("{0}={1}&", "productID", ProductID));
                    s.Append(String.Format("{0}={1}&", "subscriptionID", SubscriptionID));
                return s.ToString().Substring(0, s.Length - 1);
            }
        }
    }
}