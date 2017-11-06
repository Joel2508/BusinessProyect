using System;
using System.Collections.Generic;
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
using API.Responses;

namespace API.Controllers
{
    [Authorize]
    public class TypeBusinessesController : ApiController
    {
        private ContextDomain db = new ContextDomain();

        // GET: api/TypeBusinesses
        public async Task<IHttpActionResult> GetTypeBusinesses()
        {
            var listTypeBusiness = await  db.TypeBusinesses.ToListAsync();

            var typeBusinnessResponse = new List<TypeBusinessResponse>();

            foreach (var typeBusiness in listTypeBusiness)
            {
                var businnessResponses = new List<BusinessResponse>();

                foreach (var businnessResponse in typeBusiness.Business.OrderBy(b=>b.Name))
                {
                    businnessResponses.Add(new BusinessResponse
                    {
                        BusinessId =businnessResponse.BusinessId,
                        Email = businnessResponse.Email,
                        Image = businnessResponse.Image,
                        Latitude = businnessResponse.Latitude,
                        Lengthe = businnessResponse.Lengthe,
                        Name = businnessResponse.Name,
                        PhoneBusiness = businnessResponse.PhoneBusiness,
                        RNC = businnessResponse.RNC,
                    });
                }

                typeBusinnessResponse.Add(new TypeBusinessResponse
                {
                    Business = businnessResponses,
                    Type= typeBusiness.Type,
                    TypeBusinessId=typeBusiness.TypeBusinessId,
                });
            }
            return Ok(typeBusinnessResponse);
        }

        // GET: api/TypeBusinesses/5
        [ResponseType(typeof(TypeBusiness))]
        public async Task<IHttpActionResult> GetTypeBusiness(int id)
        {
            TypeBusiness typeBusiness = await db.TypeBusinesses.FindAsync(id);
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