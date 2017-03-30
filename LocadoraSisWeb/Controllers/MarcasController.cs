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
using LocadoraSisWeb.Models;

namespace LocadoraSisWeb.Controllers
{
    [RoutePrefix("api/Marcas")]
    public class MarcasController : ApiController
    {
        private LocadoraSisWebContext db = new LocadoraSisWebContext();

        // GET: api/Marcas
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        public virtual IQueryable<Marca> GetMarcas()
        {
            return db.Marcas;
        }

        // GET: api/Marcas/5
        [ResponseType(typeof(Marca))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("get")]
        public virtual async Task<IHttpActionResult> GetMarca(long id)
        {
            Marca marca = await db.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }

            return Ok(marca);
        }

        // PUT: api/Marcas/5
        [ResponseType(typeof(void))]
        [System.Web.Http.HttpPost]
        [Route("update")]
        public virtual async Task<IHttpActionResult> PutMarca(long id, Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marca.Id)
            {
                return BadRequest();
            }

            db.Entry(marca).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcaExists(id))
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

        // POST: api/Marcas
        [ResponseType(typeof(Marca))]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("post")]
        public virtual async Task<IHttpActionResult> PostMarca(Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Marcas.Add(marca);

            if (await db.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Marcas/5
        [ResponseType(typeof(Marca))]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("delete")]
        public virtual async Task<IHttpActionResult> DeleteMarca(long id)
        {
            Marca marca = await db.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }

            db.Marcas.Remove(marca);
            await db.SaveChangesAsync();

            return Ok(marca);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MarcaExists(long id)
        {
            return db.Marcas.Count(e => e.Id == id) > 0;
        }
    }
}