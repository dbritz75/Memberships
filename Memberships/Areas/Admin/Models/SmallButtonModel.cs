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
        public string ActionParameters //Build a string containing any id paramaters which are passed in.
        {
            get 
            {
                var param = new StringBuilder("?");
                if(ID != null && ID>0)
                {   //If the ID is not null then "id=<ID>" will be appended to the parameter string.
                    param.Append(String.Format("{0}={1}&","id",ID));
                }
                if ((ItemID ?? 0) > 0)
                {   //If the ID is not null then "id=<ID>" will be appended to the parameter string.
                    param.Append(String.Format("{0}={1}&", "ItemID", ItemID));
                }
                if ((ProductID ?? 0) > 0)
                {   //If the ID is not null then "id=<ID>" will be appended to the parameter string.
                    param.Append(String.Format("{0}={1}&", "ProductID", ProductID));
                }
                if ((SubscriptionID ?? 0) > 0)
                {   //If the ID is not null then "id=<ID>" will be appended to the parameter string.
                    param.Append(String.Format("{0}={1}&", "SubscriptionID", SubscriptionID));
                }
                return param.ToString().Substring(0,param.Length-1); //Remove last "&"
            }
        }
    }
}