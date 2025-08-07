using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiNotas.Data;
using ApiNotas.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiNotas.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotasController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/notas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Nota>>> GetNotas()
    {
        return await _context.Notas
            .OrderByDescending(n => n.FechaCreacion)
            .ToListAsync();
    }

    // GET: api/notas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Nota>> GetNota(int id)
    {
        var nota = await _context.Notas.FindAsync(id);
        return nota == null ? NotFound() : nota;
    }

    // POST: api/notas
    [HttpPost]
    public async Task<ActionResult<Nota>> PostNota(Nota nota)
    {
        _context.Notas.Add(nota);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetNota), new { id = nota.Id }, nota);
    }

    // PUT: api/notas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNota(int id, Nota nota)
    {
        if (id != nota.Id) return BadRequest();

        _context.Entry(nota).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/notas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNota(int id)
    {
        var nota = await _context.Notas.FindAsync(id);
        if (nota == null) return NotFound();

        _context.Notas.Remove(nota);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // GET: api/notas/buscar?q=texto
    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<Nota>>> BuscarNotas([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
            return BadRequest("Debe proporcionar un término de búsqueda.");

        var resultados = await _context.Notas
            .Where(n => n.Titulo.ToLower().Contains(q.ToLower()))
            .OrderByDescending(n => n.FechaCreacion)
            .ToListAsync();

        if (!resultados.Any())
            return NotFound("No se encontraron notas que coincidan con la búsqueda.");

        return resultados;
    }
}    
