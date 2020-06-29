using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memberships.Entities
{
    [Table("Section")]
    public class Section
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; } 
        
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
    }
}