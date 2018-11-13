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
    public class SolicitudArticulosController : ControllerBase
    {
        private readonly labdispmovilesContext _context;

        public SolicitudArticulosController(labdispmovilesContext context)
        {
            _context = context;
        }

        // GET: api/SolicitudArticulos
        [HttpGet]
        public IEnumerable<SolicitudArticulos> GetSolicitudArticulos()
        {
            return _context.SolicitudArticulos;
        }

        // GET: api/SolicitudArticulos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSolicitudArticulos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var solicitudArticulos = await _context.SolicitudArticulos.FindAsync(id);

            if (solicitudArticulos == null)
            {
                return NotFound();
            }

            return Ok(solicitudArticulos);
        }

        // PUT: api/SolicitudArticulos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitudArticulos([FromRoute] int id, [FromBody] SolicitudArticulos solicitudArticulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solicitudArticulos.TranId)
            {
                return BadRequest();
            }

            _context.Entry(solicitudArticulos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudArticulosExists(id))
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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult>Collect([FromBody] string content) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            return CreatedAtAction("GetSolicitudArticulos", "");
        }
        // POST: api/SolicitudArticulos
        [HttpPost]
        public async Task<IActionResult> PostSolicitudArticulos([FromBody] SolicitudArticulos solicitudArticulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SolicitudArticulos.Add(solicitudArticulos);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SolicitudArticulosExists(solicitudArticulos.TranId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSolicitudArticulos", new { id = solicitudArticulos.TranId }, solicitudArticulos);
        }

        // DELETE: api/SolicitudArticulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitudArticulos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var solicitudArticulos = await _context.SolicitudArticulos.FindAsync(id);
            if (solicitudArticulos == null)
            {
                return NotFound();
            }

            _context.SolicitudArticulos.Remove(solicitudArticulos);
            await _context.SaveChangesAsync();

            return Ok(solicitudArticulos);
        }

        private bool SolicitudArticulosExists(int id)
        {
            return _context.SolicitudArticulos.Any(e => e.TranId == id);
        }
    }
}