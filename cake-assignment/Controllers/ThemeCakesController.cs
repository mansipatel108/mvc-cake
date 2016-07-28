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
    public class ThemeCakesController : Controller
    {
        private CakeModel db = new CakeModel();

        // GET: ThemeCakes
        public async Task<ActionResult> Index()
        {
            var themeCakes = db.ThemeCakes.Include(t => t.Cake);
            return View(await themeCakes.ToListAsync());
        }

        // GET: ThemeCakes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeCake themeCake = await db.ThemeCakes.FindAsync(id);
            if (themeCake == null)
            {
                return HttpNotFound();
            }
            return View(themeCake);
        }

        // GET: ThemeCakes/Create
        public ActionResult Create()
        {
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName");
            return View();
        }

        // POST: ThemeCakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] ThemeCake themeCake)
        {
            if (ModelState.IsValid)
            {
                db.ThemeCakes.Add(themeCake);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", themeCake.CakeID);
            return View(themeCake);
        }

        // GET: ThemeCakes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeCake themeCake = await db.ThemeCakes.FindAsync(id);
            if (themeCake == null)
            {
                return HttpNotFound();
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", themeCake.CakeID);
            return View(themeCake);
        }

        // POST: ThemeCakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] ThemeCake themeCake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(themeCake).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", themeCake.CakeID);
            return View(themeCake);
        }

        // GET: ThemeCakes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeCake themeCake = await db.ThemeCakes.FindAsync(id);
            if (themeCake == null)
            {
                return HttpNotFound();
            }
            return View(themeCake);
        }

        // POST: ThemeCakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ThemeCake themeCake = await db.ThemeCakes.FindAsync(id);
            db.ThemeCakes.Remove(themeCake);
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
