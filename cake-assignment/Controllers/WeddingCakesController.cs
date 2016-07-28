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
    public class WeddingCakesController : Controller
    {
        private CakeModel db = new CakeModel();

        // GET: WeddingCakes
        public async Task<ActionResult> Index()
        {
            var weddingCakes = db.WeddingCakes.Include(w => w.Cake);
            return View(await weddingCakes.ToListAsync());
        }

        // GET: WeddingCakes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeddingCake weddingCake = await db.WeddingCakes.FindAsync(id);
            if (weddingCake == null)
            {
                return HttpNotFound();
            }
            return View(weddingCake);
        }

        // GET: WeddingCakes/Create
        public ActionResult Create()
        {
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName");
            return View();
        }

        // POST: WeddingCakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] WeddingCake weddingCake)
        {
            if (ModelState.IsValid)
            {
                db.WeddingCakes.Add(weddingCake);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", weddingCake.CakeID);
            return View(weddingCake);
        }

        // GET: WeddingCakes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeddingCake weddingCake = await db.WeddingCakes.FindAsync(id);
            if (weddingCake == null)
            {
                return HttpNotFound();
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", weddingCake.CakeID);
            return View(weddingCake);
        }

        // POST: WeddingCakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] WeddingCake weddingCake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weddingCake).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", weddingCake.CakeID);
            return View(weddingCake);
        }

        // GET: WeddingCakes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeddingCake weddingCake = await db.WeddingCakes.FindAsync(id);
            if (weddingCake == null)
            {
                return HttpNotFound();
            }
            return View(weddingCake);
        }

        // POST: WeddingCakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WeddingCake weddingCake = await db.WeddingCakes.FindAsync(id);
            db.WeddingCakes.Remove(weddingCake);
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
