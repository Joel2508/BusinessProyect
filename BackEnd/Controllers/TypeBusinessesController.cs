using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Context;
using Domain.Model;

namespace BackEnd.Controllers
{
    public class TypeBusinessesController : Controller
    {
        private ContextDomain db = new ContextDomain();

        // GET: TypeBusinesses
        public async Task<ActionResult> Index()
        {
            return View(await db.TypeBusinesses.ToListAsync());
        }

        // GET: TypeBusinesses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeBusiness typeBusiness = await db.TypeBusinesses.FindAsync(id);
            if (typeBusiness == null)
            {
                return HttpNotFound();
            }
            return View(typeBusiness);
        }

        // GET: TypeBusinesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeBusinesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TypeBusinessId,Type")] TypeBusiness typeBusiness)
        {
            if (ModelState.IsValid)
            {
                db.TypeBusinesses.Add(typeBusiness);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeBusiness);
        }

        // GET: TypeBusinesses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeBusiness typeBusiness = await db.TypeBusinesses.FindAsync(id);
            if (typeBusiness == null)
            {
                return HttpNotFound();
            }
            return View(typeBusiness);
        }

        // POST: TypeBusinesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TypeBusinessId,Type")] TypeBusiness typeBusiness)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeBusiness).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typeBusiness);
        }

        // GET: TypeBusinesses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeBusiness typeBusiness = await db.TypeBusinesses.FindAsync(id);
            if (typeBusiness == null)
            {
                return HttpNotFound();
            }
            return View(typeBusiness);
        }

        // POST: TypeBusinesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypeBusiness typeBusiness = await db.TypeBusinesses.FindAsync(id);
            db.TypeBusinesses.Remove(typeBusiness);
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
