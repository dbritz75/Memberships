using Memberships.Entities;
using Memberships.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TransactionScope = System.Transactions.TransactionScope;

namespace Memberships.Areas.Admin.Models
{
    public static class ConversionExtensions
    {
        #region Product

        //These below Convert methods have something to do with taking tables 
        //and converting them into things that will work with dropdowns (?????)
        // Take for example an Item and convert into an ItemModel for the ViewModel for the views 
        // and those views have dropdown boxes and things like that and somehow this makes 
        // the dropdown boxes magically happen. Not sure how or why, but they do.

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
        #endregion

        #region ProductItem

        public static async Task<ProductItemModel> Convert
        (this Product_Item productItem, ApplicationDbContext db, bool addListData = true)
        //Only fill collections if the parameter passed in is true
        {
            var model = new ProductItemModel
            {
                ItemID = productItem.ItemID,
                ProductID = productItem.ProductID,
                Items = addListData ? await db.Items.ToListAsync() : null,
                Products = addListData ? await db.Products.ToListAsync() : null,

                ItemTitle = (await db.Items.FirstOrDefaultAsync(i => i.ID.Equals(productItem.ItemID))).Title,
                ProductTitle = (await db.Products.FirstOrDefaultAsync(p => p.ID.Equals(productItem.ProductID))).Title
            };
            return model;

        }

        public static async Task<IEnumerable<Areas.Admin.Models.ProductItemModel>> Convert
            (this IQueryable<Product_Item> productItems, ApplicationDbContext db)
        {
            if (productItems.Count().Equals(0)) return new List<ProductItemModel>();

            return await
            (
                from pi in productItems
                select new ProductItemModel
                {
                    ItemID = pi.ItemID,
                    ProductID = pi.ProductID,
                    ItemTitle = db.Items.FirstOrDefault(i => i.ID.Equals(pi.ItemID)).Title,
                    ProductTitle = db.Products.FirstOrDefault(p => p.ID.Equals(pi.ProductID)).Title
                }
            ).ToListAsync();
        }



        public static async Task<bool> CanChange(this Product_Item proditem, ApplicationDbContext db)
        {
            //Check to see if old product and item exist - for the Edit view
            var oldProdItem = await db.Products_Items.CountAsync
                (pi => pi.ProductID.Equals(proditem.OldProductID) && pi.ItemID.Equals(proditem.OldItemID));

            var newProdItem = await db.Products_Items.CountAsync
                (pi => pi.ProductID.Equals(proditem.ProductID) && pi.ItemID.Equals(proditem.ItemID));

            //Return true if the old product and item id combination do exist and the new ones don't exist.
            return oldProdItem.Equals(1) && newProdItem.Equals(0);

        }

        public static async Task Change(this Product_Item productitem, ApplicationDbContext db)
        {
            var oldProductItem = await db.Products_Items.FirstOrDefaultAsync
                (pi => pi.ProductID.Equals(productitem.OldProductID) && pi.ItemID.Equals(productitem.OldItemID));

            var newProductItem = await db.Products_Items.FirstOrDefaultAsync
                (pi => pi.ProductID.Equals(productitem.ProductID) && pi.ItemID.Equals(productitem.ItemID));

            if (oldProductItem != null & newProductItem == null)
            {
                newProductItem = new Product_Item()
                {
                    ItemID = productitem.ItemID,
                    ProductID = productitem.ProductID
                };

                using (var transaction = new System.Transactions.TransactionScope(
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        db.Products_Items.Remove(oldProductItem);
                        db.Products_Items.Add(newProductItem);
                        await db.SaveChangesAsync();
                        transaction.Complete();
                    }
                    catch
                    {
                        transaction.Dispose();
                    }
                }
            }

        }

        #endregion

        #region SubscriptionProduct

        public static async Task<IEnumerable<SubscriptionProductModel>> Convert(
        this IQueryable<Subscription_Product> subscriptionProducts, ApplicationDbContext db)
        {
            if (subscriptionProducts.Count().Equals(0))
                return new List<SubscriptionProductModel>();

            return await (from pi in subscriptionProducts
                          select new SubscriptionProductModel
                          {
                              SubscriptionID = pi.SubscriptionID,
                              ProductID = pi.ProductID,
                              SubscriptionTitle = db.Subscriptions.FirstOrDefault(
                                  i => i.ID.Equals(pi.SubscriptionID)).Title,
                              ProductTitle = db.Products.FirstOrDefault(
                                  p => p.ID.Equals(pi.ProductID)).Title
                          }).ToListAsync();
        }

        public static async Task<SubscriptionProductModel> Convert(
        this Subscription_Product subscriptionProduct,
        ApplicationDbContext db, bool addListData = true)
        {
            var model = new SubscriptionProductModel
            {
                SubscriptionID = subscriptionProduct.SubscriptionID,
                ProductID = subscriptionProduct.ProductID,
                Subscriptions = addListData ? await db.Subscriptions.ToListAsync() : null,
                Products = addListData ? await db.Products.ToListAsync() : null,
                SubscriptionTitle = (await db.Subscriptions.FirstOrDefaultAsync(s =>
                   s.ID.Equals(subscriptionProduct.SubscriptionID))).Title,
                ProductTitle = (await db.Products.FirstOrDefaultAsync(p =>
                   p.ID.Equals(subscriptionProduct.ProductID))).Title
            };

            return model;
        }

        public static async Task<bool> CanChange(
            this Subscription_Product subscriptionProduct,
            ApplicationDbContext db)
        {
            var oldSP = await db.Subscriptions_Products.CountAsync(sp =>
                sp.ProductID.Equals(subscriptionProduct.OldProductID) &&
                sp.SubscriptionID.Equals(subscriptionProduct.OldSubscriptionID));

            var newSP = await db.Subscriptions_Products.CountAsync(sp =>
                sp.ProductID.Equals(subscriptionProduct.ProductID) &&
                sp.SubscriptionID.Equals(subscriptionProduct.SubscriptionID));

            return oldSP.Equals(1) && newSP.Equals(0);
        }

        public static async Task Change(this Subscription_Product subscriptionProduct, ApplicationDbContext db)
        {
            var oldSubscriptionProduct = await db.Subscriptions_Products.FirstOrDefaultAsync(
                    sp => sp.ProductID.Equals(subscriptionProduct.OldProductID) &&
                    sp.SubscriptionID.Equals(subscriptionProduct.OldSubscriptionID));

            var newSubscriptionProduct = await db.Subscriptions_Products.FirstOrDefaultAsync(
                sp => sp.ProductID.Equals(subscriptionProduct.ProductID) &&
                sp.SubscriptionID.Equals(subscriptionProduct.SubscriptionID));

            if (oldSubscriptionProduct != null && newSubscriptionProduct == null)
            {
                newSubscriptionProduct = new Subscription_Product
                {
                    SubscriptionID = subscriptionProduct.SubscriptionID,
                    ProductID = subscriptionProduct.ProductID
                };

                using (var transaction = new TransactionScope(
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    //try
                    //{

                    using (var context = new EntityContext())
                    {
                        context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                        db.Subscriptions_Products.Remove(oldSubscriptionProduct);
                        db.Subscriptions_Products.Add(newSubscriptionProduct);
                        await db.SaveChangesAsync();

                    }

                    
                        transaction.Complete();
                    //}
                    //catch { transaction.Dispose(); }
                }
            }
        }

        public class EntityContext : DbContext
        {
            public EntityContext() 
            {

            }
        }
    }
}

#endregion


