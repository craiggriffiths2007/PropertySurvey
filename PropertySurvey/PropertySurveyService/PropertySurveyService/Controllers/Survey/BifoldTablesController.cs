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
    public class BifoldTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public BifoldTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: BifoldTables
        public async Task<IActionResult> Index()
        {
              return _context.BifoldTable != null ? 
                          View(await _context.BifoldTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.BifoldTable'  is null.");
        }

        // GET: BifoldTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.BifoldTable == null)
            {
                return NotFound();
            }

            viewModel.Bifold = await _context.BifoldTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Bifold == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.Bifold.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Bifold.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: BifoldTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BifoldTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,ernal_width,ernal_height,overall_width,overall_height,opens,trickle_vents,hardware,color_ernal,color_external,threshold_type,no_of_pics,no_of_photos,no_of_vids,isComplete,comments,bifold_signed,number_of_doors,cause_of_damage,cause_of_damage_reason_different,door_type,glazing_options,number_of_doors_text,colour_of_doors,handle_colour,cill_type,knock_on,ernal_door_colour,s_spare12,parts_to_order,type_of_lockng_system_required,was_it_locked,point_of_entry,ChangeItemTo,pr_name,bDifferentFromOriginal,glass_complete,replace_glass,reason_not_repaired,bRepair,fensa,WER_rating,gaskets,gaskets_text,handles_req,bHandleDrawingComplete,handles_text,addons,addon_width,addon_height")] BifoldTable bifoldTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bifoldTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bifoldTable);
        }

        // GET: BifoldTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BifoldTable == null)
            {
                return NotFound();
            }

            var bifoldTable = await _context.BifoldTable.FindAsync(id);
            if (bifoldTable == null)
            {
                return NotFound();
            }
            return View(bifoldTable);
        }

        // POST: BifoldTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,ernal_width,ernal_height,overall_width,overall_height,opens,trickle_vents,hardware,color_ernal,color_external,threshold_type,no_of_pics,no_of_photos,no_of_vids,isComplete,comments,bifold_signed,number_of_doors,cause_of_damage,cause_of_damage_reason_different,door_type,glazing_options,number_of_doors_text,colour_of_doors,handle_colour,cill_type,knock_on,ernal_door_colour,s_spare12,parts_to_order,type_of_lockng_system_required,was_it_locked,point_of_entry,ChangeItemTo,pr_name,bDifferentFromOriginal,glass_complete,replace_glass,reason_not_repaired,bRepair,fensa,WER_rating,gaskets,gaskets_text,handles_req,bHandleDrawingComplete,handles_text,addons,addon_width,addon_height")] BifoldTable bifoldTable)
        {
            if (id != bifoldTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bifoldTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BifoldTableExists(bifoldTable.Id))
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
            return View(bifoldTable);
        }

        // GET: BifoldTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BifoldTable == null)
            {
                return NotFound();
            }

            var bifoldTable = await _context.BifoldTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bifoldTable == null)
            {
                return NotFound();
            }

            return View(bifoldTable);
        }

        // POST: BifoldTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BifoldTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.BifoldTable'  is null.");
            }
            var bifoldTable = await _context.BifoldTable.FindAsync(id);
            if (bifoldTable != null)
            {
                _context.BifoldTable.Remove(bifoldTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BifoldTableExists(int id)
        {
          return (_context.BifoldTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
