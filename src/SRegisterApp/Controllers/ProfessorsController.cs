using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SRegisterApp.Data;
using SRegisterApp.Data.entitis;

namespace SRegisterApp.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly SRegisterAppContext _context;

        public ProfessorsController(SRegisterAppContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Professors.ToListAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professors = await _context.Professors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professors == null)
            {
                return NotFound();
            }

            return View(professors);
        }

        // GET: Professors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Apellido,Email,Telefono")] Professors professors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professors);
        }

        // GET: Professors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professors = await _context.Professors.FindAsync(id);
            if (professors == null)
            {
                return NotFound();
            }
            return View(professors);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Apellido,Email,Telefono")] Professors professors)
        {
            if (id != professors.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorsExists(professors.ID))
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
            return View(professors);
        }

        // GET: Professors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professors = await _context.Professors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professors == null)
            {
                return NotFound();
            }

            return View(professors);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professors = await _context.Professors.FindAsync(id);
            if (professors != null)
            {
                _context.Professors.Remove(professors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorsExists(int id)
        {
            return _context.Professors.Any(e => e.ID == id);
        }
    }
}
