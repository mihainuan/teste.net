using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using teste.net.Data;
using teste.net.Models;

namespace teste.net.Controllers
{
    [ApiController]
    [Route("v1/cidades")]
    public class CidadesController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Cidades>>> GetAll(
            [FromServices] DataContext context)
        {
            try
            {                
                var cidades = await context.Cidades.Include(x => x.Estado).ToListAsync();
                return cidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Cidades>> GetById(
            [FromServices] DataContext context, 
            int id)
        {
            try
            {                
                var cidade = await context.Cidades
                .Include(x => x.Estado)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdCidade == id);

                return cidade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet]
        [Route("buscar/{nome}")]
        public async Task<ActionResult<List<Cidades>>> GetByNome(
            [FromServices] DataContext context, 
            string nome)
        {
            try
            {
                var cidade = await context.Cidades
                .Include(x => x.Estado)                
                .AsNoTracking()
                .Where(x => x.NomeCidade == nome)
                .ToListAsync();
                if (cidade == null) return NotFound();
                else return cidade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("estado/{id:int}")]
        public async Task<ActionResult<List<Cidades>>> GetByEstado(
            [FromServices] DataContext context,
            int id)
        {
            try
            {                
                var cidades = await context.Cidades
                .Include(x => x.Estado)
                .AsNoTracking()
                .Where(x => x.IdEstado == id)
                .ToListAsync();

                return cidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Cidades>> Post(
            [FromServices] DataContext context,
            [FromBody] Cidades model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    context.Cidades.Add(model);
                    await context.SaveChangesAsync();
                    return model;
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{IdCidade}")]
        [Route("")]
        public async Task<IActionResult> Put(
            [FromServices] DataContext context,
            Cidades cidades,
            int IdCidade)
        {
            var item = await context.Cidades.FindAsync(IdCidade);
            if (item == null)
            {
                return NotFound();
            }

            item.NomeCidade = cidades.NomeCidade;
            item.Estado = cidades.Estado;

            try
            {
                await context.SaveChangesAsync();
            }
          catch (DbUpdateConcurrencyException dbex)
            {
                throw dbex;
            }

            return Ok(item);
        }
        
        [HttpDelete("{IdCidade}")]
        [Route("")]
        public async Task<IActionResult> Delete(
            [FromServices] DataContext context,
            int IdCidade)
        {
            try
            {                
                var item = await context.Cidades.FindAsync(IdCidade);

                if (item == null)
                {
                    return NotFound();
                }

                context.Cidades.Remove(item);
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}