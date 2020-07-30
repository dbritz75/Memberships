﻿using Memberships.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    public class ProductItemModel
    {
        [DisplayName("Product ID")]
        public int ProductID { get; set; }
        [DisplayName("Item ID")]
        public int ItemID { get; set; }
        [DisplayName("Product")]
        public string ProductTitle { get; set; }
        [DisplayName("Item")]
        public string ItemTitle { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Item> Items { get; set; }

    }
}