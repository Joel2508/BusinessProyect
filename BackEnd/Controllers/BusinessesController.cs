namespace BackEnd.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Domain.Context;
    using Domain.Model;
    using BackEnd.Models.ModelView;
    using BackEnd.Helper;
    using System;

    public class BusinessesController : Controller
    {
        private ContextDomain db = new ContextDomain();

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
            Business business = await db.Businesses.FindAsync(id);
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

                if(view != null)
                {
                    pic = FileHelper.UpPhote(view.ImageView, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var businesView = ToViewBusiness(view);
                businesView.Image = pic;
                db.Businesses.Add(businesView);
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
                BusinessId =view.BusinessId,
                Email=view.Email,
                Image = view.Image,
                Latitude = view.Latitude,
                Lengthe = view.Lengthe,
                Name = view.Name,
                PhoneBusiness = view.PhoneBusiness,
                RNC = view.RNC,
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
            var viewBusiness = ViewToBusiness(business);
            return View(viewBusiness);
        }

        private BusinessView ViewToBusiness(Business business)
        {
            return new BusinessView
            {
                 BusinessId =business.BusinessId,
                 Email = business.Email,
                 Image = business.Image,
                 Latitude = business.Latitude,
                 Lengthe = business.Lengthe,
                 Name = business.Name,
                 PhoneBusiness = business.PhoneBusiness,
                 RNC = business.RNC,
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

                var bussines = ToViewBusiness(view);
                bussines.Image = pic;
                db.Entry(bussines).State = EntityState.Modified;
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
            var business = await db.Businesses.FindAsync(id);
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
