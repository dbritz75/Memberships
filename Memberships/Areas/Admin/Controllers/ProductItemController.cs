using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Memberships.Entities;
using Memberships.Models;
using Memberships.Areas.Admin.Models;

namespace Memberships.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductItem
        public async Task<ActionResult> Index()
        {
            return View(await db.Products_Items.Convert(db));
        }



        // GET: Admin/ProductItem/Details/5
        public async Task<ActionResult> Details(int? itemID, int? productID)
        {
            if (itemID == null || productID==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Item product_Item = await GetProductItem(itemID, productID);
            if (product_Item == null)
            {
                return HttpNotFound();
            }
            return View(await product_Item.Convert(db));
        }

        // GET: Admin/ProductItem/Create
        public async Task<ActionResult> Create()
        {
            var model = new ProductItemModel
            {
                Items = await db.Items.ToListAsync(),
                Products = await db.Products.ToListAsync()
            };
            return View(model);
        }

        // POST: Admin/ProductItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,ItemID")] Product_Item product_Item)
        {
            if (ModelState.IsValid)
            {
                db.Products_Items.Add(product_Item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product_Item);
        }

        // GET: Admin/ProductItem/Edit/5
        public async Task<ActionResult> Edit(int? ItemID, int? ProductID)
        {
            if (ItemID == null || ProductID==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Item productItem = await GetProductItem(ItemID, ProductID);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(await productItem.Convert(db));
        }

        // POST: Admin/ProductItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,ItemID,OldProductID,OldItemID")] Product_Item product_Item)
        {
            if (ModelState.IsValid)
            {
                var canChange = await product_Item.CanChange(db);
                if (canChange) await product_Item.Change(db);
                return RedirectToAction("Index");
            }
            return View(product_Item);
        }

        // GET: Admin/ProductItem/Delete/5
        public async Task<ActionResult> Delete(int? itemID, int? productID)
        {
            if (itemID == null || productID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Item product_Item = await GetProductItem(itemID, productID);
            if (product_Item == null)
            {
                return HttpNotFound();
            }
            return View(await product_Item.Convert(db));
        }

        // POST: Admin/ProductItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int itemID, int productID)
        {
            Product_Item product_Item = await GetProductItem(itemID, productID);
            db.Products_Items.Remove(product_Item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        private async Task<Product_Item> GetProductItem(int? item, int? prod)
        {
            try
            {
                int itemID = 0, prodID = 0;
                int.TryParse(item.ToString(), out itemID);
                int.TryParse(prod.ToString(), out prodID);
                var productItem = await db.Products_Items.FirstOrDefaultAsync
                (
                    pi => pi.ProductID.Equals(prodID) && pi.ItemID.Equals(itemID)
                );
                return productItem;
            }
            catch {return null;}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
