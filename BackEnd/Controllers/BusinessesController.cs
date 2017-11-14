using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BackEnd.Models;
using Domain.Model;
using BackEnd.Models.ModelView;
using System.IO;
using BackEnd.Helper;

namespace BackEnd.Controllers
{
    public class BusinessesController : Controller
    {
        private ContextBackEnd db = new ContextBackEnd();

      

        // GET: Businesses
        public async Task<ActionResult> Index()
        {
            var businesses = db.Businesses.Include(b => b.TypeBusiness);
            return View(await businesses.ToListAsync());
        }

        // GET: Businesses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var business = await db.Businesses.FindAsync(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: Businesses/Create
        public ActionResult Create()
        {
            ViewBag.TypeBusinessId = new SelectList(db.TypeBusinesses, "TypeBusinessId", "Type");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BusinessView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Image";
                if(view.ImageView != null)
                {
                    pic = FileHelper.UpPhote(view.ImageView, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
                var business = ToViewBusiness(view);
                business.Image = pic;
                db.Businesses.Add(business);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TypeBusinessId = new SelectList(db.TypeBusinesses, "TypeBusinessId", "Type", view.TypeBusinessId);
            return View(view);
        }

        private Business ToViewBusiness(BusinessView view)
        {
            
            return new Business
            {
                Address = view.Address,
                BusinessId = view.BusinessId,
                Email = view.Email,
                Image = view.Image,
                Latituded =Convert.ToDecimal(view.LatitudView),
                Longitud = Convert.ToDecimal(view.LogintudView),
                Name = view.Name,
                TypeBusiness = view.TypeBusiness,
                TypeBusinessId = view.TypeBusinessId,
            };
            
        }

        // GET: Businesses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var business = await db.Businesses.FindAsync(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeBusinessId = new SelectList(db.TypeBusinesses, "TypeBusinessId", "Type", business.TypeBusinessId);
            var busines = ViewToBusiness(business);
            return View(busines);
        }

        private BusinessView ViewToBusiness(Business business)
        {
            return new BusinessView
            {
                Address = business.Address,
                BusinessId = business.BusinessId,
                Email = business.Email,
                Image = business.Image,    
                Name = business.Name,
                TypeBusiness = business.TypeBusiness,
                TypeBusinessId = business.TypeBusinessId,
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BusinessView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Image";

                if (view.ImageView != null)
                {
                    pic = FileHelper.UpPhote(view.ImageView, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var business = ToViewBusiness(view);
                business.Image = pic;
                db.Entry(business).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TypeBusinessId = new SelectList(db.TypeBusinesses, "TypeBusinessId", "Type", view.TypeBusinessId);
            return View(view);
        }

        // GET: Businesses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = await db.Businesses.FindAsync(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Businesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Business business = await db.Businesses.FindAsync(id);
            db.Businesses.Remove(business);
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
