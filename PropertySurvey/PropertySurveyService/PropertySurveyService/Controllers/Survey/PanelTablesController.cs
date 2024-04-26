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
    public class PanelTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public PanelTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: PanelTables
        public async Task<IActionResult> Index()
        {
              return _context.PanelTable != null ? 
                          View(await _context.PanelTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.PanelTable'  is null.");
        }

        // GET: PanelTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();


            if (id == null || _context.PanelTable == null)
            {
                return NotFound();
            }

            viewModel.Panel = await _context.PanelTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Panel == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0,8) == viewModel.Panel.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Panel.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: PanelTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PanelTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,isComplete,cause_of_damage,cause_of_damage_reason_different,knockedit,knocoledit,letteredit,letter_box_pos,wedit,hedit,typeedit,thickedit,backgedit,coledit,gltext,spaccoloedit,pet_flap,pet_type,pet_magnetic,no_of_pics,no_of_photos,no_of_vids,room_location,bDifferentFromOriginal,ChangeItemTo,print_name,long_sptext,upvc_item_number,alum_item_number,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked")] PanelTable panelTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panelTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(panelTable);
        }

        // GET: PanelTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PanelTable == null)
            {
                return NotFound();
            }

            var panelTable = await _context.PanelTable.FindAsync(id);
            if (panelTable == null)
            {
                return NotFound();
            }
            return View(panelTable);
        }

        // POST: PanelTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,isComplete,cause_of_damage,cause_of_damage_reason_different,knockedit,knocoledit,letteredit,letter_box_pos,wedit,hedit,typeedit,thickedit,backgedit,coledit,gltext,spaccoloedit,pet_flap,pet_type,pet_magnetic,no_of_pics,no_of_photos,no_of_vids,room_location,bDifferentFromOriginal,ChangeItemTo,print_name,long_sptext,upvc_item_number,alum_item_number,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked")] PanelTable panelTable)
        {
            if (id != panelTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panelTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanelTableExists(panelTable.Id))
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
            return View(panelTable);
        }

        // GET: PanelTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PanelTable == null)
            {
                return NotFound();
            }

            var panelTable = await _context.PanelTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (panelTable == null)
            {
                return NotFound();
            }

            return View(panelTable);
        }

        // POST: PanelTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PanelTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.PanelTable'  is null.");
            }
            var panelTable = await _context.PanelTable.FindAsync(id);
            if (panelTable != null)
            {
                _context.PanelTable.Remove(panelTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanelTableExists(int id)
        {
          return (_context.PanelTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
