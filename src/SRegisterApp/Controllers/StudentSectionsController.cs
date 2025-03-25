using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SRegisterApp.Data;
using SRegisterApp.Models;

namespace SRegisterApp.Controllers
{
    public class StudentSectionsController : Controller
    {
        private readonly SRegisterAppContext _context;

        public StudentSectionsController(SRegisterAppContext context)
        {
            _context = context;
        }

        // GET: StudentSections
        public async Task<IActionResult> Index()
        {
            var sRegisterAppContext = _context.StudentSection.Include(s => s.Section).Include(s => s.Student);
            return View(await sRegisterAppContext.ToListAsync());
        }

        // GET: StudentSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentSection = await _context.StudentSection
                .Include(s => s.Section)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentSection == null)
            {
                return NotFound();
            }

            return View(studentSection);
        }

        // GET: StudentSections/Create
        public IActionResult Create()
        {
            ViewData["SectionID"] = new SelectList(_context.Sections, "ID", "Nombre");
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: StudentSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StudentID,SectionID")] StudentSection studentSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionID"] = new SelectList(_context.Sections, "ID", "Nombre", studentSection.SectionID);
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Id", studentSection.StudentID);
            return View(studentSection);
        }

        // GET: StudentSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentSection = await _context.StudentSection.FindAsync(id);
            if (studentSection == null)
            {
                return NotFound();
            }
            ViewData["SectionID"] = new SelectList(_context.Sections, "ID", "Nombre", studentSection.SectionID);
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Id", studentSection.StudentID);
            return View(studentSection);
        }

        // POST: StudentSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StudentID,SectionID")] StudentSection studentSection)
        {
            if (id != studentSection.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentSectionExists(studentSection.ID))
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
            ViewData["SectionID"] = new SelectList(_context.Sections, "ID", "Nombre", studentSection.SectionID);
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Id", studentSection.StudentID);
            return View(studentSection);
        }

        // GET: StudentSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentSection = await _context.StudentSection
                .Include(s => s.Section)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studentSection == null)
            {
                return NotFound();
            }

            return View(studentSection);
        }

        // POST: StudentSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentSection = await _context.StudentSection.FindAsync(id);
            if (studentSection != null)
            {
                _context.StudentSection.Remove(studentSection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentSectionExists(int id)
        {
            return _context.StudentSection.Any(e => e.ID == id);
        }
    }
}
