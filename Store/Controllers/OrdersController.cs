using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Store.DAL;
using Store.Models;

namespace Store.Controllers
{
    
    public class OrdersController : Controller
    {
        private StoreContext db = new StoreContext();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
             HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        public ActionResult ThankYou()
        {
            return View("ThankYou");
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Include(o => o.OrderLines).Where(o => o.OrderID == id).SingleOrDefault();

            if (order == null)
            {
                return HttpNotFound();
            }

            if (order.UserID == User.Identity.Name || User.IsInRole("Admin"))
            {
                return View(order);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
        }

        // GET: Orders/Review
        public async Task<ActionResult> Review()
        {
            Basket basket = Basket.GetBasket();
            Order order = new Order();

            order.UserID = User.Identity.Name;
            //ApplicationUser user = await UserManager.FindByNameAsync(order.UserID);
            order.OrderLines = new List<OrderLine>();
            foreach (var basketLine in basket.GetBasketLines())
            {
                OrderLine line = new OrderLine
                {
                    Product = basketLine.Product,
                    ProductID = basketLine.ProductID,
                    ProductName = basketLine.Product.Name,
                    Quantity = basketLine.Quantity,
                    UnitPrice = basketLine.Product.Price
                };
                order.OrderLines.Add(line);
            }
            order.TotalPrice = basket.GetTotalCost();
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();

                //add the orderlines to the database after creating the order
                Basket basket = Basket.GetBasket();
                order.TotalPrice = basket.CreateOrderLines(order.OrderID);
                db.SaveChanges();
                //return RedirectToAction("Details", new { id = order.OrderID });
                return RedirectToAction("ThankYou");
            }
            return RedirectToAction("Review");
           
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Include(o => o.OrderLines).Where(o => o.OrderID == id).SingleOrDefault();

            if (order == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("Admin"))
            {
                return View(order);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
