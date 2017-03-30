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
    [RoutePrefix("api/Veiculos")]
    public class VeiculosController : ApiController
    {
        private LocadoraSisWebContext db = new LocadoraSisWebContext();

        // GET: api/Veiculos
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("")]
        public IQueryable<Veiculo> GetVeiculos()
        {
            return db.Veiculos.Include(x => x.Marca);
        }

        // GET: api/Veiculos/5
        [ResponseType(typeof(Veiculo))]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("get")]
        public async Task<IHttpActionResult> GetVeiculo(long id)
        {
            Veiculo veiculo = await db.Veiculos.Include(x => x.Marca).SingleOrDefaultAsync(y => y.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }

            return Ok(veiculo);
        }

        // GET: api/Veiculos
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVeiculosByMarca")]
        public IQueryable<Veiculo> GetVeiculosByMarca(long id)
        {
            return db.Veiculos.Include(x => x.Marca).Where(x => x.MarcaId == id && x.Alugado == false);
        }

        // PUT: api/Veiculos/5
        [ResponseType(typeof(void))]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("update")]
        public async Task<IHttpActionResult> PutVeiculo(long id, Veiculo veiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != veiculo.Id)
            {
                return BadRequest();
            }

            db.Entry(veiculo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoExists(id))
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

        // POST: api/Veiculos
        [ResponseType(typeof(Veiculo))]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("post")]
        public async Task<IHttpActionResult> PostVeiculo(Veiculo veiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            veiculo.Alugado = false;

            db.Veiculos.Add(veiculo);

            if (await db.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Veiculos/5
        [ResponseType(typeof(Veiculo))]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("delete")]
        public async Task<IHttpActionResult> DeleteVeiculo(long id)
        {
            Veiculo veiculo = await db.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            db.Veiculos.Remove(veiculo);
            await db.SaveChangesAsync();

            return Ok(veiculo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VeiculoExists(long id)
        {
            return db.Veiculos.Count(e => e.Id == id) > 0;
        }
    }
}