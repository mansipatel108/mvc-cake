using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cake_assignment.Models;

namespace cake_assignment.Controllers
{
    public class FruitCakesController : Controller
    {
        private CakeModel db = new CakeModel();

        // GET: FruitCakes
        public async Task<ActionResult> Index()
        {
            var fruitCakes = db.FruitCakes.Include(f => f.Cake);
            return View(await fruitCakes.ToListAsync());
        }

        // GET: FruitCakes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FruitCake fruitCake = await db.FruitCakes.FindAsync(id);
            if (fruitCake == null)
            {
                return HttpNotFound();
            }
            return View(fruitCake);
        }

        // GET: FruitCakes/Create
        public ActionResult Create()
        {
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName");
            return View();
        }

        // POST: FruitCakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] FruitCake fruitCake)
        {
            if (ModelState.IsValid)
            {
                db.FruitCakes.Add(fruitCake);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", fruitCake.CakeID);
            return View(fruitCake);
        }

        // GET: FruitCakes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FruitCake fruitCake = await db.FruitCakes.FindAsync(id);
            if (fruitCake == null)
            {
                return HttpNotFound();
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", fruitCake.CakeID);
            return View(fruitCake);
        }

        // POST: FruitCakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] FruitCake fruitCake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fruitCake).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", fruitCake.CakeID);
            return View(fruitCake);
        }

        // GET: FruitCakes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FruitCake fruitCake = await db.FruitCakes.FindAsync(id);
            if (fruitCake == null)
            {
                return HttpNotFound();
            }
            return View(fruitCake);
        }

        // POST: FruitCakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FruitCake fruitCake = await db.FruitCakes.FindAsync(id);
            db.FruitCakes.Remove(fruitCake);
            await db.SaveChangesAsync();
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
