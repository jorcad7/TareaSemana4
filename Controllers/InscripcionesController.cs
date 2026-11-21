
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Models;

public class InscripcionesController : Controller
{
    private readonly SistemaAcademicoContext _context;

    public InscripcionesController(SistemaAcademicoContext context)
    {
        _context = context;
    }

    // GET: INSCRIPCIONS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Inscripcions.ToListAsync());
    }

    // GET: INSCRIPCIONS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inscripcion = await _context.Inscripcions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (inscripcion == null)
        {
            return NotFound();
        }

        return View(inscripcion);
    }

    // GET: INSCRIPCIONS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: INSCRIPCIONS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FechaInscripcion,Estado,CursoId,EstudianteId,Certificado,Curso,Estudiante")] Inscripcion inscripcion)
    {
        if (ModelState.IsValid)
        {
            _context.Add(inscripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(inscripcion);
    }

    // GET: INSCRIPCIONS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inscripcion = await _context.Inscripcions.FindAsync(id);
        if (inscripcion == null)
        {
            return NotFound();
        }
        return View(inscripcion);
    }

    // POST: INSCRIPCIONS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,FechaInscripcion,Estado,CursoId,EstudianteId,Certificado,Curso,Estudiante")] Inscripcion inscripcion)
    {
        if (id != inscripcion.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(inscripcion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionExists(inscripcion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(inscripcion);
    }

    // GET: INSCRIPCIONS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var inscripcion = await _context.Inscripcions
            .FirstOrDefaultAsync(m => m.Id == id);
        if (inscripcion == null)
        {
            return NotFound();
        }

        return View(inscripcion);
    }

    // POST: INSCRIPCIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var inscripcion = await _context.Inscripcions.FindAsync(id);
        if (inscripcion != null)
        {
            _context.Inscripcions.Remove(inscripcion);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool InscripcionExists(int? id)
    {
        return _context.Inscripcions.Any(e => e.Id == id);
    }
}
