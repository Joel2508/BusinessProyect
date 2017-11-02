using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Domain.Context;
using Domain.Model;
using API.Respons;
using System.Collections.Generic;

namespace API.Controllers
{
    public class TypeBusinessesController : ApiController
    {
        private ContextDomain db = new ContextDomain();

        // GET: api/TypeBusinesses
        public async Task<IHttpActionResult> GetTypeBusinesses()
        {
            var typeBusinesses = await db.TypeBusinesses.ToListAsync();

            var typeResponse = new List<TypeResponse>();

            foreach (var typeBusinesse in typeBusinesses)
            {
                var businessResponse = new List<BusinesResponse>();

                foreach (var list in typeBusinesse.Business)
                {
                    businessResponse.Add(new BusinesResponse
                    {
                        BusinessId = list.BusinessId,
                        Email = list.Email,
                        Image = list.Image,
                        Latitude = list.Latitude,
                        Lengthe = list.Lengthe,
                        Name = list.Name,
                        PhoneBusiness = list.PhoneBusiness,
                        RNC = list.RNC,
                        TypeBusinessId = list.TypeBusinessId,
                    });
                    typeResponse.Add(new TypeResponse
                    {
                        Type = typeBusinesse.Type,
                        TypeBusinessId = typeBusinesse.TypeBusinessId,                       
                    });
                }
               
            }
            return Ok(typeResponse);       
        }



        // GET: api/TypeBusinesses/5
        [ResponseType(typeof(TypeBusiness))]
        public async Task<IHttpActionResult> GetTypeBusiness(int id)
        {
            var typeBusiness = await db.TypeBusinesses.FindAsync(id);
            if (typeBusiness == null)
            {
                return NotFound();
            }

            return Ok(typeBusiness);
        }

        // PUT: api/TypeBusinesses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTypeBusiness(int id, TypeBusiness typeBusiness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeBusiness.TypeBusinessId)
            {
                return BadRequest();
            }

            db.Entry(typeBusiness).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeBusinessExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TypeBusinesses
        [ResponseType(typeof(TypeBusiness))]
        public async Task<IHttpActionResult> PostTypeBusiness(TypeBusiness typeBusiness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeBusinesses.Add(typeBusiness);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = typeBusiness.TypeBusinessId }, typeBusiness);
        }

        // DELETE: api/TypeBusinesses/5
        [ResponseType(typeof(TypeBusiness))]
        public async Task<IHttpActionResult> DeleteTypeBusiness(int id)
        {
            TypeBusiness typeBusiness = await db.TypeBusinesses.FindAsync(id);
            if (typeBusiness == null)
            {
                return NotFound();
            }

            db.TypeBusinesses.Remove(typeBusiness);
            await db.SaveChangesAsync();

            return Ok(typeBusiness);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeBusinessExists(int id)
        {
            return db.TypeBusinesses.Count(e => e.TypeBusinessId == id) > 0;
        }
    }
}