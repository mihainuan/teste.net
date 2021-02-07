using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using teste.net.Data;
using teste.net.Models;
using System;
using System.Linq;

namespace teste.net.Controllers
{

    [ApiController]
    [Route("v1/estados")]
    public class EstadosController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Estados>>> GetAll(
            [FromServices] DataContext context)
        {
             try
            {
                var estados = await context.Estados.ToListAsync();
                if (estados == null) return NotFound();
                else return estados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Estados>> GetEstadoById(
            [FromServices] DataContext context, 
            int id)
        {
            try
            {
                var result = await context.Estados.FindAsync(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("buscar/{nome}")]
        public async Task<ActionResult<List<Estados>>> GetByNome(
            [FromServices] DataContext context, 
            string nome)
        {
            try
            {
                var estados = await context.Estados
                .AsNoTracking()
                .Where(x => x.NomeEstado == nome)
                .ToListAsync();

                if (estados == null) return NotFound();
                else return estados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Estados>> Post(
            [FromServices] DataContext context,
            [FromBody] Estados model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    context.Estados.Add(model);
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

        [HttpPut("{IdEstado}")]
        [Route("")]
        public async Task<IActionResult> Put(
            [FromServices] DataContext context, 
            Estados estado,
            int IdEstado)
        {
            var item = await context.Estados.FindAsync(IdEstado);
            if (item == null)
            {
                return NotFound();
            }

            item.NomeEstado = estado.NomeEstado;
            item.UF = estado.UF;

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

        [HttpDelete("{IdEstado}")]
        [Route("")]
        public async Task<IActionResult> Delete(
            [FromServices] DataContext context, 
            int IdEstado)
        {
            try
            {
                var item = await context.Estados.FindAsync(IdEstado);                
                if (item == null)
                {
                    return NotFound();
                }

                context.Estados.Remove(item);
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