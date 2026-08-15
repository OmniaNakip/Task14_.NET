
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Management_System.Models;
using Movie_Management_System.Data;

public class CinemasController : Controller
{
    private readonly ApplicationDbContext _context;

    public CinemasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()    
    {
        return View(await _context.Cinemas.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinema = await _context.Cinemas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cinema == null)
        {
            return NotFound();
        }

        return View(cinema);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cinema cinema)
    {
        if (ModelState.IsValid)
        {
            _context.Cinemas.Add(cinema);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(cinema);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema == null)
        {
            return NotFound();
        }
        return View(cinema);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Description,Image,Movies")] Cinema cinema)
    {
        if (id != cinema.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cinema);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExists(cinema.Id))
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
        return View(cinema);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinema = await _context.Cinemas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cinema == null)
        {
            return NotFound();
        }

        return View(cinema);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema != null)
        {
            _context.Cinemas.Remove(cinema);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CinemaExists(int? id)
    {
        return _context.Cinemas.Any(e => e.Id == id);
    }
}
