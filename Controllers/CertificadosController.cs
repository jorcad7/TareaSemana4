
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Models;

public class CertificadosController : Controller
{
    private readonly SistemaAcademicoContext _context;

    public CertificadosController(SistemaAcademicoContext context)
    {
        _context = context;
    }

    // GET: CERTIFICADOS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Certificados.ToListAsync());
    }

    // GET: CERTIFICADOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var certificado = await _context.Certificados
            .FirstOrDefaultAsync(m => m.Id == id);
        if (certificado == null)
        {
            return NotFound();
        }

        return View(certificado);
    }

    // GET: CERTIFICADOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CERTIFICADOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CodigoUnico,FechaEmision,CalificacionFinal,InscripcionId,Inscripcion")] Certificado certificado)
    {
        if (ModelState.IsValid)
        {
            _context.Add(certificado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(certificado);
    }

    // GET: CERTIFICADOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var certificado = await _context.Certificados.FindAsync(id);
        if (certificado == null)
        {
            return NotFound();
        }
        return View(certificado);
    }

    // POST: CERTIFICADOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,CodigoUnico,FechaEmision,CalificacionFinal,InscripcionId,Inscripcion")] Certificado certificado)
    {
        if (id != certificado.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(certificado);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificadoExists(certificado.Id))
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
        return View(certificado);
    }

    // GET: CERTIFICADOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var certificado = await _context.Certificados
            .FirstOrDefaultAsync(m => m.Id == id);
        if (certificado == null)
        {
            return NotFound();
        }

        return View(certificado);
    }

    // POST: CERTIFICADOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var certificado = await _context.Certificados.FindAsync(id);
        if (certificado != null)
        {
            _context.Certificados.Remove(certificado);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CertificadoExists(int? id)
    {
        return _context.Certificados.Any(e => e.Id == id);
    }
}
