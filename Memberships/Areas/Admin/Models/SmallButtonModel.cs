
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    public class SmallButtonModel
    //This is for a generic "tiny button group" that will be used on index pages (Edit, Details, Delete, etc.)
    {
        public string Action { get; set; }
        public string Text { get; set; }
        public string Glyph { get; set; }
        public string ButtonType { get; set; }
        public int? ID { get; set; }
        public int? ItemID { get; set; }
        public int? ProductID { get; set; }
        public int? SubscriptionID { get; set; }
        public string UserID { get; set; }

        public string ActionParameters
        {
            get
            {
                var param = new StringBuilder("?");
                {
                    //If the ID is not null then "id=<ID>" will be appended to the parameter string.
                    if (ID != null && ID > 0)
                        param.Append(String.Format("{0}={1}&", "id", ID));

                    if (ItemID != null && ItemID > 0)
                        param.Append(String.Format("{0}={1}&", "itemId", ItemID));

                    if (ProductID != null && ProductID > 0)
                        param.Append(String.Format("{0}={1}&", "productId", ProductID));

                    if (SubscriptionID != null && SubscriptionID > 0)
                        param.Append(String.Format("{0}={1}&", "subscriptionId", SubscriptionID));

                    if (UserID != null && !UserID.Equals(string.Empty))
                        param.Append(string.Format("{0}={1}&", "userId", UserID));

                    return param.ToString().Substring(0, param.Length - 1); //Remove last "&"
                }
            }
        }
    }

}