using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    [Table("Product_Item")]
    public class Product_Item
    {
        //Product ID and Item ID is a composite primary key, thus the "Key(Column...)" syntax.

        [Required]
        [Key,Column(Order =1)]
        public int ProductID { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int ItemID { get; set; }

        [NotMapped]
        public int OldProductID { get; set; }

        [NotMapped]
        public int OldItemID { get; set; }
    }
}