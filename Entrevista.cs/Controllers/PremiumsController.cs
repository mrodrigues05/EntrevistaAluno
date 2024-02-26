using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entrevista.Models;
using Entrevista.cs.Data;

namespace Entrevista.cs.Controllers
{
    public class PremiumsController : Controller
    {
        private readonly EntrevistacsContext _context;

        public PremiumsController(EntrevistacsContext context)
        {
            _context = context;
        }

        // GET: Premiums
        public async Task<IActionResult> Index()
        {
            var entrevistacsContext = _context.Premium.Include(p => p.Student);
            return View(await entrevistacsContext.ToListAsync());
        }

        // GET: Premiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Premium == null)
            {
                return NotFound();
            }

            var premium = await _context.Premium
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premium == null)
            {
                return NotFound();
            }

            return View(premium);
        }

        // GET: Premiums/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Email");
            return View();
        }

        // POST: Premiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Título,StartDate,EndDate,StudentId")] Premium premium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(premium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Email", premium.StudentId);
            return View(premium);
        }

        // GET: Premiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Premium == null)
            {
                return NotFound();
            }

            var premium = await _context.Premium.FindAsync(id);
            if (premium == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Email", premium.StudentId);
            return View(premium);
        }

        // POST: Premiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Título,StartDate,EndDate,StudentId")] Premium premium)
        {
            if (id != premium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(premium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PremiumExists(premium.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Email", premium.StudentId);
            return View(premium);
        }

        // GET: Premiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Premium == null)
            {
                return NotFound();
            }

            var premium = await _context.Premium
                .Include(p => p.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (premium == null)
            {
                return NotFound();
            }

            return View(premium);
        }

        // POST: Premiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Premium == null)
            {
                return Problem("Entity set 'EntrevistacsContext.Premium'  is null.");
            }
            var premium = await _context.Premium.FindAsync(id);
            if (premium != null)
            {
                _context.Premium.Remove(premium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PremiumExists(int id)
        {
          return (_context.Premium?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
