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
    public class CoffeeCakesController : Controller
    {
        private CakeModel db = new CakeModel();

        // GET: CoffeeCakes
        public async Task<ActionResult> Index()
        {
            var coffeeCakes = db.CoffeeCakes.Include(c => c.Cake);
            return View(await coffeeCakes.ToListAsync());
        }

        // GET: CoffeeCakes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeCake coffeeCake = await db.CoffeeCakes.FindAsync(id);
            if (coffeeCake == null)
            {
                return HttpNotFound();
            }
            return View(coffeeCake);
        }

        // GET: CoffeeCakes/Create
        public ActionResult Create()
        {
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName");
            return View();
        }

        // POST: CoffeeCakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] CoffeeCake coffeeCake)
        {
            if (ModelState.IsValid)
            {
                db.CoffeeCakes.Add(coffeeCake);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", coffeeCake.CakeID);
            return View(coffeeCake);
        }

        // GET: CoffeeCakes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeCake coffeeCake = await db.CoffeeCakes.FindAsync(id);
            if (coffeeCake == null)
            {
                return HttpNotFound();
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", coffeeCake.CakeID);
            return View(coffeeCake);
        }

        // POST: CoffeeCakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] CoffeeCake coffeeCake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coffeeCake).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", coffeeCake.CakeID);
            return View(coffeeCake);
        }

        // GET: CoffeeCakes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeCake coffeeCake = await db.CoffeeCakes.FindAsync(id);
            if (coffeeCake == null)
            {
                return HttpNotFound();
            }
            return View(coffeeCake);
        }

        // POST: CoffeeCakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CoffeeCake coffeeCake = await db.CoffeeCakes.FindAsync(id);
            db.CoffeeCakes.Remove(coffeeCake);
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
