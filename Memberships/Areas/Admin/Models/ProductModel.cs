using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Memberships.Entities;

namespace Memberships.Areas.Admin.Models
{
    public class ProductModel
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        [MaxLength(1024)]
        [DisplayName("Image URL")]
        public string ImageURL { get; set; }

        public int ProductLinkTextID { get; set; }
        public int ProductTypeID { get; set; }

        public ICollection<ProductLinkText> ProductLinkTexts { get; set; }

        public ICollection<ProductType> ProductTypes { get; set; }

        public string ProductType
        {
            get
            {   //If there are no products return an empty string. Or else return the first product name found matching that product ID
                return ProductTypes == null || ProductTypes.Count.Equals(0) ?
                    String.Empty :
                    ProductTypes.First(pt => pt.ID.Equals(ProductTypeID)).Title;
            }
        }

        public string ProductLinkText
        {
            get
            {   //If there are no products return an empty string. Or else return the first product name found matching that product ID
                return ProductLinkTexts == null || ProductLinkTexts.Count.Equals(0) ?
                    String.Empty :
                    ProductLinkTexts.First(pt => pt.ID.Equals(ProductLinkTextID)).Title;
            }
        }

    }
}