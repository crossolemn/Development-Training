using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InventoriesController : Controller
    {
        private trdbEntities db = new trdbEntities();

        // GET: Inventories
        public ActionResult Index()
        {
            return View(db.Inventory.ToList());
        }

        // GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventory.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "商品番号,商品名,価格,個数,小計,最終更新")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Inventory.Add(inventory);

                inventory.商品番号 = (from Products in db.Product
                                           where Products.商品番号 == inventory.商品番号
                                           select Products.商品番号).Single();

                inventory.商品名 = (from Products in db.Product
                                         where Products.商品番号 == inventory.商品番号
                                         select Products.商品名).Single();

                inventory.価格 = (from Products in db.Product
                                          where Products.商品番号 == inventory.商品番号
                                          select Products.価格).Single();

                inventory.小計 = inventory.価格 * inventory.個数 ;

                //inventory.UpdateTime = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventory.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "商品番号,商品名,価格,個数,小計,最終更新")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;

                inventory.商品番号 = (from Products in db.Product
                                           where Products.商品番号 == inventory.商品番号
                                           select Products.商品番号).Single();

                inventory.商品名 = (from Products in db.Product
                                         where Products.商品番号 == inventory.商品番号
                                         select Products.商品名).Single();

                inventory.価格 = (from Products in db.Product
                                          where Products.商品番号 == inventory.商品番号
                                          select Products.価格).Single();

                inventory.小計 = inventory.価格 * inventory.個数;

                inventory.最終更新 = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventory.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventory.Find(id);
            db.Inventory.Remove(inventory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
