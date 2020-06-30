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
using System.Web.UI.WebControls;

//This apparently has something to do with providing items for dropdowns. Not sure why.

namespace Memberships.Areas.Admin.Models
{
    public static class ConversionExtensions
    {
        //Convert list of product
        public static async Task<IEnumerable<Areas.Admin.Models.ProductModel>> Convert
            (this IEnumerable<Product> products, ApplicationDbContext db)
        {
            if (products.Count().Equals(0)) return new List<ProductModel>();
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

        //Convert product and its product link text.
        public static async Task<ProductModel> Convert
        (this Product product, ApplicationDbContext db)
        {
            var text = await db.ProductLinkTexts.FirstOrDefaultAsync
                (p => p.ID.Equals(product.ProductLinkTextID));

            var type = await db.ProductTypes.FirstOrDefaultAsync
                (p => p.ID.Equals(product.ProductTypeID));

            var model = new ProductModel
            {
                ID = product.ID,
                Title = product.Title,
                Description = product.Description,
                ImageURL = product.ImageURL,
                ProductLinkTextID = product.ProductLinkTextID,
                ProductTypeID = product.ProductTypeID,
                ProductLinkTexts = new List<ProductLinkText>(),
                ProductTypes = new List<ProductType>()
            };
            model.ProductLinkTexts.Add(text);
            model.ProductTypes.Add(type);
            return model;

        }
    }




}
