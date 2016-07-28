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
    public class CakesController : Controller
    {
        private CakeModel db = new CakeModel();

        // GET: Cakes
        public async Task<ActionResult> Index()
        {
            return View(await db.Cakes.ToListAsync());
        }

        // GET: Cakes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cake cake = await db.Cakes.FindAsync(id);
            if (cake == null)
            {
                return HttpNotFound();
            }
            return View(cake);
        }

        // GET: Cakes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CakeID,CakeName")] Cake cake)
        {
            if (ModelState.IsValid)
            {
                db.Cakes.Add(cake);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cake);
        }

        // GET: Cakes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cake cake = await db.Cakes.FindAsync(id);
            if (cake == null)
            {
                return HttpNotFound();
            }
            return View(cake);
        }

        // POST: Cakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CakeID,CakeName")] Cake cake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cake).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cake);
        }

        // GET: Cakes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cake cake = await db.Cakes.FindAsync(id);
            if (cake == null)
            {
                return HttpNotFound();
            }
            return View(cake);
        }

        // POST: Cakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cake cake = await db.Cakes.FindAsync(id);
            db.Cakes.Remove(cake);
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
