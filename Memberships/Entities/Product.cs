using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Memberships.Entities
{
    [Table("Product")]
    public class Product
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        [MaxLength(1024)]
        public string ImageURL { get; set; }

        public int ProductLinkTextID { get; set; }
        public int ProductTypeID { get; set; }

    }
}