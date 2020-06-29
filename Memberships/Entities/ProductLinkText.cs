using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    [Table("ProductLinkText")]
    public class ProductLinkText
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; } 
        
        [MaxLength(25)]
        [Required]
        public string Title { get; set; }
    }
}