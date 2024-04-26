using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertySurveyService.Data;
using PropertySurveyService.Models;
using PropertySurveyService.ViewModels;

namespace PropertySurveyService.Controllers
{
    public class GreenTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public GreenTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: GreenTables
        public async Task<IActionResult> Index()
        {
              return _context.GreenTable != null ? 
                          View(await _context.GreenTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.GreenTable'  is null.");
        }

        // GET: GreenTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.GreenTable == null)
            {
                return NotFound();
            }

            viewModel.Green = await _context.GreenTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Green == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.Green.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Green.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: GreenTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GreenTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,isComplete,bDifferentFromOriginal,cause_of_damage,cause_of_damage_reason_different,rep_reason,material_type,colour,glaze_type,base_size,base_size_x,base_size_y,type_of_glass,door_opening_type,window_opening_type,roof_opening_lights,auto_or_manual,overall_height,summary,no_of_pics,no_of_photos,no_of_vids,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked,ChangeItemTo,print_name,glass_complete,replace_glass,repair_or_replace")] GreenTable greenTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(greenTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(greenTable);
        }

        // GET: GreenTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GreenTable == null)
            {
                return NotFound();
            }

            var greenTable = await _context.GreenTable.FindAsync(id);
            if (greenTable == null)
            {
                return NotFound();
            }
            return View(greenTable);
        }

        // POST: GreenTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,isComplete,bDifferentFromOriginal,cause_of_damage,cause_of_damage_reason_different,rep_reason,material_type,colour,glaze_type,base_size,base_size_x,base_size_y,type_of_glass,door_opening_type,window_opening_type,roof_opening_lights,auto_or_manual,overall_height,summary,no_of_pics,no_of_photos,no_of_vids,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked,ChangeItemTo,print_name,glass_complete,replace_glass,repair_or_replace")] GreenTable greenTable)
        {
            if (id != greenTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(greenTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GreenTableExists(greenTable.Id))
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
            return View(greenTable);
        }

        // GET: GreenTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GreenTable == null)
            {
                return NotFound();
            }

            var greenTable = await _context.GreenTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (greenTable == null)
            {
                return NotFound();
            }

            return View(greenTable);
        }

        // POST: GreenTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GreenTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.GreenTable'  is null.");
            }
            var greenTable = await _context.GreenTable.FindAsync(id);
            if (greenTable != null)
            {
                _context.GreenTable.Remove(greenTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GreenTableExists(int id)
        {
          return (_context.GreenTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
