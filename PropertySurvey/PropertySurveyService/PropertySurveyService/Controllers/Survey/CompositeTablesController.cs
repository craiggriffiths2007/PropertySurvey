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
    public class CompositeTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public CompositeTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: CompositeTables
        public async Task<IActionResult> Index()
        {
              return _context.CompositeTable != null ? 
                          View(await _context.CompositeTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.CompositeTable'  is null.");
        }

        // GET: CompositeTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.CompositeTable == null)
            {
                return NotFound();
            }

            viewModel.Comp = await _context.CompositeTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Comp == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.Comp.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Comp.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: CompositeTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompositeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,isComplete,cause_of_damage,cause_of_damage_reason_different,door_make,opens,is_lock,frame_colour_inside,frame_colour_outside,door_colour_inside,door_colour_outside,door_design,glass_design,internal_width,internal_height,brick_width,brick_height,trickle_vents,addons,addons_height,addons_width,handle_colour,threshold_type,lever_pad_handles,glass_pattern,glass_type,spacer_thickness,spacer_colour,profile_type,room_location,special_glass,comments,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,docl,letteredit,letter_box_pos,pet_flap,pet_type,pet_magnetic,glaze,print_name,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,no_of_pics,no_of_photos,no_of_vids,bDifferentFromOriginal,lock_other_text,head_drip,ChangeItemTo,cills,door_wood,hinged_on,reason_not_repaired,lead_comments,parts_to_order,is_a_flat,point_of_entry,type_of_lockng_system_required,was_it_locked,fire_door,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,glass_complete,replace_glass,bRepair,fensa,WER_rating,gaskets,gaskets_text,handles_req,bHandleDrawingComplete,handles_text")] CompositeTable compositeTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compositeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compositeTable);
        }

        // GET: CompositeTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CompositeTable == null)
            {
                return NotFound();
            }

            var compositeTable = await _context.CompositeTable.FindAsync(id);
            if (compositeTable == null)
            {
                return NotFound();
            }
            return View(compositeTable);
        }

        // POST: CompositeTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,isComplete,cause_of_damage,cause_of_damage_reason_different,door_make,opens,is_lock,frame_colour_inside,frame_colour_outside,door_colour_inside,door_colour_outside,door_design,glass_design,internal_width,internal_height,brick_width,brick_height,trickle_vents,addons,addons_height,addons_width,handle_colour,threshold_type,lever_pad_handles,glass_pattern,glass_type,spacer_thickness,spacer_colour,profile_type,room_location,special_glass,comments,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,docl,letteredit,letter_box_pos,pet_flap,pet_type,pet_magnetic,glaze,print_name,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,no_of_pics,no_of_photos,no_of_vids,bDifferentFromOriginal,lock_other_text,head_drip,ChangeItemTo,cills,door_wood,hinged_on,reason_not_repaired,lead_comments,parts_to_order,is_a_flat,point_of_entry,type_of_lockng_system_required,was_it_locked,fire_door,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,glass_complete,replace_glass,bRepair,fensa,WER_rating,gaskets,gaskets_text,handles_req,bHandleDrawingComplete,handles_text")] CompositeTable compositeTable)
        {
            if (id != compositeTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compositeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompositeTableExists(compositeTable.Id))
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
            return View(compositeTable);
        }

        // GET: CompositeTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompositeTable == null)
            {
                return NotFound();
            }

            var compositeTable = await _context.CompositeTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compositeTable == null)
            {
                return NotFound();
            }

            return View(compositeTable);
        }

        // POST: CompositeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CompositeTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.CompositeTable'  is null.");
            }
            var compositeTable = await _context.CompositeTable.FindAsync(id);
            if (compositeTable != null)
            {
                _context.CompositeTable.Remove(compositeTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompositeTableExists(int id)
        {
          return (_context.CompositeTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
