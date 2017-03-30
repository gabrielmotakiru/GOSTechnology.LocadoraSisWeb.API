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
    [RoutePrefix("api/Opcionais")]
    public class OpcionaisController : ApiController
    {
        private LocadoraSisWebContext db = new LocadoraSisWebContext();

        // GET: api/Opcionais
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        public IQueryable<Opcional> GetOpcionais()
        {
            return db.Opcionais;
        }

        // GET: api/Opcionais/5
        [ResponseType(typeof(Opcional))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("get")]
        public async Task<IHttpActionResult> GetOpcional(long id)
        {
            Opcional opcional = await db.Opcionais.FindAsync(id);
            if (opcional == null)
            {
                return NotFound();
            }

            return Ok(opcional);
        }

        // PUT: api/Opcionais/5
        [ResponseType(typeof(void))]
        [System.Web.Http.HttpPost]
        [Route("update")]
        public virtual async Task<IHttpActionResult> PutOpcional(long id, Opcional opcional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != opcional.Id)
            {
                return BadRequest();
            }

            db.Entry(opcional).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpcionalExists(id))
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

        // POST: api/Opcionais
        [ResponseType(typeof(Opcional))]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("post")]
        public async Task<IHttpActionResult> PostOpcional(Opcional opcional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Opcionais.Add(opcional);

            if (await db.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Opcionais/5
        [ResponseType(typeof(Opcional))]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("delete")]
        public async Task<IHttpActionResult> DeleteOpcional(long id)
        {
            Opcional opcional = await db.Opcionais.FindAsync(id);
            if (opcional == null)
            {
                return NotFound();
            }

            db.Opcionais.Remove(opcional);
            await db.SaveChangesAsync();

            return Ok(opcional);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OpcionalExists(long id)
        {
            return db.Opcionais.Count(e => e.Id == id) > 0;
        }
    }
}