using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProveedoresFimeApi.Models;

namespace ProveedoresFimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionesController : ControllerBase
    {
        private readonly labdispmovilesContext _context;

        public CotizacionesController(labdispmovilesContext context)
        {
            _context = context;
        }

        // GET: api/Cotizaciones
        [HttpGet]
        public IEnumerable<Cotizaciones> GetCotizaciones()
        {
            return _context.Cotizaciones;
        }

        // GET: api/Cotizaciones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCotizaciones([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cotizaciones = await _context.Cotizaciones.FindAsync(id);

            if (cotizaciones == null)
            {
                return NotFound();
            }

            return Ok(cotizaciones);
        }

        // PUT: api/Cotizaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCotizaciones([FromRoute] int id, [FromBody] Cotizaciones cotizaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cotizaciones.CotizacionId)
            {
                return BadRequest();
            }

            _context.Entry(cotizaciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CotizacionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cotizaciones
        [HttpPost]
        public async Task<IActionResult> PostCotizaciones([FromBody] Cotizaciones cotizaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cotizaciones.Add(cotizaciones);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CotizacionesExists(cotizaciones.CotizacionId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCotizaciones", new { id = cotizaciones.CotizacionId }, cotizaciones);
        }

        // DELETE: api/Cotizaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCotizaciones([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cotizaciones = await _context.Cotizaciones.FindAsync(id);
            if (cotizaciones == null)
            {
                return NotFound();
            }

            _context.Cotizaciones.Remove(cotizaciones);
            await _context.SaveChangesAsync();

            return Ok(cotizaciones);
        }

        private bool CotizacionesExists(int id)
        {
            return _context.Cotizaciones.Any(e => e.CotizacionId == id);
        }
    }
}