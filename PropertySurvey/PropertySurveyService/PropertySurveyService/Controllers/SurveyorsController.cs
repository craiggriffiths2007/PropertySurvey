using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertySurveyService.Data;
using PropertySurveyService.Models;

namespace PropertySurveyService.Controllers
{
    public class SurveyorsController : Controller
    {
        private readonly Data.AppDBContext _context;

        public SurveyorsController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: Surveyors
        public async Task<IActionResult> Index()
        {
              return _context.Surveyor != null ? 
                          View(await _context.Surveyor.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.Surveyor'  is null.");
        }

        // GET: Surveyors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Surveyor == null)
            {
                return NotFound();
            }

            var surveyor = await _context.Surveyor
                .FirstOrDefaultAsync(m => m.SurveyorId == id);
            if (surveyor == null)
            {
                return NotFound();
            }

            return View(surveyor);
        }

        // GET: Surveyors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surveyors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SurveyorId,SurveyorCode,Name")] Surveyor surveyor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surveyor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surveyor);
        }

        // GET: Surveyors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Surveyor == null)
            {
                return NotFound();
            }

            var surveyor = await _context.Surveyor.FindAsync(id);
            if (surveyor == null)
            {
                return NotFound();
            }
            return View(surveyor);
        }

        // POST: Surveyors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SurveyorId,SurveyorCode,Name")] Surveyor surveyor)
        {
            if (id != surveyor.SurveyorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surveyor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyorExists(surveyor.SurveyorId))
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
            return View(surveyor);
        }

        // GET: Surveyors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Surveyor == null)
            {
                return NotFound();
            }

            var surveyor = await _context.Surveyor
                .FirstOrDefaultAsync(m => m.SurveyorId == id);
            if (surveyor == null)
            {
                return NotFound();
            }

            return View(surveyor);
        }

        // POST: Surveyors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Surveyor == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.Surveyor'  is null.");
            }
            var surveyor = await _context.Surveyor.FindAsync(id);
            if (surveyor != null)
            {
                _context.Surveyor.Remove(surveyor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyorExists(int id)
        {
          return (_context.Surveyor?.Any(e => e.SurveyorId == id)).GetValueOrDefault();
        }
    }
}
