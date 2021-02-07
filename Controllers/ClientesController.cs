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
    [Route("v1/clientes")]
    public class ClientesController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Clientes>>> GetAll(
            [FromServices] DataContext context)
        {
            try
            {                
                var cidades = await context.Clientes
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .ToListAsync();
                return cidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Clientes>> GetById(
            [FromServices] DataContext context,
            int id)
        {
            try
            {                
                var cidade = await context.Clientes
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdCliente == id);

                return cidade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpGet]
        [Route("buscar/{nome}")]
        public async Task<ActionResult<List<Clientes>>> GetByNome(
            [FromServices] DataContext context,
            string nome)
        {
            try
            {
                var cidade = await context.Clientes
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .AsNoTracking()
                .Where(x => x.NomeCompleto == nome)
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
        public async Task<ActionResult<List<Clientes>>> GetByEstado(
            [FromServices] DataContext context,
            int id)
        {
            try
            {                
                var estados = await context.Clientes
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .AsNoTracking()
                .Where(x => x.IdEstado == id)
                .ToListAsync();

                return estados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("cidade/{id:int}")]
        public async Task<ActionResult<List<Clientes>>> GetByCidade(
            [FromServices] DataContext context,
            int id)
        {
            try
            {                
                var cidades = await context.Clientes
                .Include(x => x.Cidade)
                .Include(x => x.Estado)
                .AsNoTracking()
                .Where(x => x.IdCidade == id)
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
        public async Task<ActionResult<Clientes>> Post(
            [FromServices] DataContext context,
            [FromBody] Clientes model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    context.Clientes.Add(model);
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

        [HttpPut("{IdCliente}")]
        [Route("")]
        public async Task<IActionResult> Put(
            [FromServices] DataContext context, 
            Clientes clientes,
            int IdCliente)
        {
            var item = await context.Clientes.FindAsync(IdCliente);
            if (item == null)
            {
                return NotFound();
            }

            item.NomeCompleto = clientes.NomeCompleto;
            item.Sexo = clientes.Sexo;
            item.DataNascimento = clientes.DataNascimento;
            item.Idade = clientes.Idade;
            item.IdCidade = clientes.IdCidade;
            item.Cidade = clientes.Cidade;
            item.IdEstado = clientes.IdEstado;
            item.Estado = clientes.Estado;

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

        [HttpDelete("{IdCliente}")]
        [Route("")]
        public async Task<IActionResult> Delete(
            [FromServices] DataContext context,
            int IdCliente)
        {
            try
            {                
                var item = await context.Clientes.FindAsync(IdCliente);

                if (item == null)
                {
                    return NotFound();
                }

                context.Clientes.Remove(item);
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