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
    [RoutePrefix("api/Locacoes")]
    public class LocacoesController : ApiController
    {
        private LocadoraSisWebContext db = new LocadoraSisWebContext();

        // GET: api/Locacoes
        [System.Web.Http.HttpGet]
        [Route("")]
        public IQueryable<Locacao> GetLocacoes()
        {
            return db.Locacoes.Include(x => x.Cliente).Include(x => x.Veiculo);
        }

        // GET: api/Locacoes/5
        [ResponseType(typeof(Locacao))]
        [System.Web.Http.HttpGet]
        [Route("get")]
        public async Task<IHttpActionResult> GetLocacao(long id)
        {
            Locacao locacao = await db.Locacoes.Include(x => x.Cliente).Include(x => x.Veiculo).SingleOrDefaultAsync(y => y.Id == id);
            if (locacao == null)
            {
                return NotFound();
            }

            return Ok(locacao);
        }

        // PUT: api/Locacoes/5
        [ResponseType(typeof(void))]
        [System.Web.Http.HttpPost]
        [Route("update")]
        public async Task<IHttpActionResult> PutLocacao(long id, Locacao locacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locacao.Id)
            {
                return BadRequest();
            }

            db.Entry(locacao).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocacaoExists(id))
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

        // POST: api/Locacoes
        [ResponseType(typeof(Locacao))]
        [System.Web.Http.HttpPost]
        [Route("post")]
        public async Task<IHttpActionResult> PostLocacao(Locacao locacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Veiculo veiculo = await db.Veiculos.Include(x => x.Marca).SingleOrDefaultAsync(y => y.Id == locacao.VeiculoId);
            veiculo.Alugado = true;

            locacao.Veiculo = veiculo;

            db.Locacoes.Add(locacao);
            if (await db.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        // POST: api/Locacoes
        [ResponseType(typeof(Locacao))]
        [System.Web.Http.HttpPost]
        [Route("postDevolverLocacao")]
        public async Task<IHttpActionResult> DevolverLocacao(Locacao locacao)
        {
            var flagLocacao = false;
            var flagVeiculo = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(locacao).State = EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                flagLocacao = true;
            }

            Veiculo veiculo = await db.Veiculos.Include(x => x.Marca).SingleOrDefaultAsync(y => y.Id == locacao.VeiculoId);
            veiculo.Alugado = false;

            db.Entry(veiculo).State = EntityState.Modified;

            if (await db.SaveChangesAsync() > 0)
            {
                flagVeiculo = true;
            }

            if (flagLocacao && flagVeiculo)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }

        // DELETE: api/Locacoes/5
        [ResponseType(typeof(Locacao))]
        [System.Web.Http.HttpPost]
        [Route("delete")]
        public async Task<IHttpActionResult> DeleteLocacao(long id)
        {
            Locacao locacao = await db.Locacoes.FindAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }

            db.Locacoes.Remove(locacao);
            await db.SaveChangesAsync();

            return Ok(locacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocacaoExists(long id)
        {
            return db.Locacoes.Count(e => e.Id == id) > 0;
        }
    }
}