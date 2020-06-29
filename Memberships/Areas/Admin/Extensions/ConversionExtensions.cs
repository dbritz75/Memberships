using Memberships;
using Memberships.Entities;
using Memberships.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    public static class ConversionExtensions
    {
        public static async Task<IEnumerable<Areas.Admin.Models.ProductModel>> Convert
            (this IEnumerable<Product> products, ApplicationDbContext db)
        {
            var texts = await db.ProductLinkTexts.ToListAsync();
            var types = await db.ProductTypes.ToListAsync();

            return from p in products
                   select new ProductModel
                   {
                       ID = p.ID,
                       Title = p.Title,
                       Description = p.Description,
                       ImageURL = p.ImageURL,
                       ProductLinkTextID = p.ProductLinkTextID,
                       ProductTypeID = p.ProductTypeID,
                       ProductLinkTexts = texts,
                       ProductTypes = types
                   };
        }
    }
}