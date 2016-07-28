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
    public class SpongeCakesController : Controller
    {
        private CakeModel db = new CakeModel();

        // GET: SpongeCakes
        public async Task<ActionResult> Index()
        {
            var spongeCakes = db.SpongeCakes.Include(s => s.Cake);
            return View(await spongeCakes.ToListAsync());
        }

        // GET: SpongeCakes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpongeCake spongeCake = await db.SpongeCakes.FindAsync(id);
            if (spongeCake == null)
            {
                return HttpNotFound();
            }
            return View(spongeCake);
        }

        // GET: SpongeCakes/Create
        public ActionResult Create()
        {
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName");
            return View();
        }

        // POST: SpongeCakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] SpongeCake spongeCake)
        {
            if (ModelState.IsValid)
            {
                db.SpongeCakes.Add(spongeCake);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", spongeCake.CakeID);
            return View(spongeCake);
        }

        // GET: SpongeCakes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpongeCake spongeCake = await db.SpongeCakes.FindAsync(id);
            if (spongeCake == null)
            {
                return HttpNotFound();
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", spongeCake.CakeID);
            return View(spongeCake);
        }

        // POST: SpongeCakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CakeID,CakeName,CakesDesc,Rate")] SpongeCake spongeCake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spongeCake).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CakeID = new SelectList(db.Cakes, "CakeID", "CakeName", spongeCake.CakeID);
            return View(spongeCake);
        }

        // GET: SpongeCakes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpongeCake spongeCake = await db.SpongeCakes.FindAsync(id);
            if (spongeCake == null)
            {
                return HttpNotFound();
            }
            return View(spongeCake);
        }

        // POST: SpongeCakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SpongeCake spongeCake = await db.SpongeCakes.FindAsync(id);
            db.SpongeCakes.Remove(spongeCake);
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
