
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Models;

public class CategoriumsController : Controller
{
    private readonly SistemaAcademicoContext _context;

    public CategoriumsController(SistemaAcademicoContext context)
    {
        _context = context;
    }

    // GET: CATEGORIUMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Categoria.ToListAsync());
    }

    // GET: CATEGORIUMS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categorium = await _context.Categoria
            .FirstOrDefaultAsync(m => m.Id == id);
        if (categorium == null)
        {
            return NotFound();
        }

        return View(categorium);
    }

    // GET: CATEGORIUMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CATEGORIUMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Activo,FechaCreacion,Cursos")] Categorium categorium)
    {
        if (ModelState.IsValid)
        {
            _context.Add(categorium);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(categorium);
    }

    // GET: CATEGORIUMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categorium = await _context.Categoria.FindAsync(id);
        if (categorium == null)
        {
            return NotFound();
        }
        return View(categorium);
    }

    // POST: CATEGORIUMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Descripcion,Activo,FechaCreacion,Cursos")] Categorium categorium)
    {
        if (id != categorium.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(categorium);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriumExists(categorium.Id))
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
        return View(categorium);
    }

    // GET: CATEGORIUMS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categorium = await _context.Categoria
            .FirstOrDefaultAsync(m => m.Id == id);
        if (categorium == null)
        {
            return NotFound();
        }

        return View(categorium);
    }

    // POST: CATEGORIUMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var categorium = await _context.Categoria.FindAsync(id);
        if (categorium != null)
        {
            _context.Categoria.Remove(categorium);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoriumExists(int? id)
    {
        return _context.Categoria.Any(e => e.Id == id);
    }
}
