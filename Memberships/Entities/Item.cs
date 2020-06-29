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
    [Table("Item")]
    public class Item
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
        [DisplayName("Item URL")]
        public string ItemURL { get; set; }


        public string HTMLShort
        { get { return HTML == null || HTML.Length < 50 ? HTML : HTML.Substring(50);}
          //Removed set block because we want this to be read-only.
        }

        [MaxLength(1024)]
        [DisplayName("Image URL")]
        public string ImageURL { get; set; }

        [AllowHtml]
        public string HTML { get; set; }

        [DefaultValue(0)]
        [DisplayName("Wait Days")]
        public int WaitDays { get; set; }

        [DisplayName("Item Type")]
        public int ItemTypeID { get; set; }
        [DisplayName("Product")]
        public int ProductID { get; set; }
        [DisplayName("Section")]
        public int SectionID { get; set; }
        [DisplayName("Part")]
        public int PartID { get; set; }
        [DisplayName("Free?")]
        public bool IsFree { get; set; }

        [DisplayName("Item Type")]
        public ICollection<ItemType> ItemTypes { get; set; }

        public ICollection<Section> Sections { get; set; }

        public ICollection<Part> Parts { get; set; }


    }
}