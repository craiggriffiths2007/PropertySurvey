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
    public class ConsTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public ConsTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: ConsTables
        public async Task<IActionResult> Index()
        {
              return _context.ConsTable != null ? 
                          View(await _context.ConsTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.ConsTable'  is null.");
        }

        // GET: ConsTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.ConsTable == null)
            {
                return NotFound();
            }

            viewModel.Cons = await _context.ConsTable
               .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Cons == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.Cons.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Cons.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: ConsTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConsTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,isComplete,type,cause_of_damage,cause_of_damage_reason_different,material_type,sizeA,sizeB,sizeC,sizeD,sizeE,sizeF,sizeG,angle1,angle2,angle3,angle4,pitch_height,profile_section_size,sheet_width_1,sheet_height_1,sheet_width_2,sheet_height_2,sheet_width_3,sheet_height_3,sheet_width_4,sheet_height_4,sheet_width_5,sheet_height_5,sheet_width_6,sheet_height_6,sheet_width_7,sheet_height_7,sheet_width_8,sheet_height_8,sheet_width_9,sheet_height_9,sheet_width_10,sheet_height_10,flute_size,color,roof_color,new_firrings_req,new_gutters_req,roof_glazing_thickness,no_of_pics,no_of_photos,room_location,no_of_vids,bDifferentFromOriginal,ChangeItemTo,print_name,wall_pos,pitch_degree,long_comments,bDrawingsOnly,cons_roof_under_drawn,does_roof_fit_under,spars_line_up,roof_sheets_quantity_1,roof_sheets_quantity_2,roof_sheets_quantity_3,roof_sheets_quantity_4,roof_sheets_quantity_5,roof_sheets_quantity_6,roof_sheets_quantity_7,roof_sheets_quantity_8,roof_sheets_quantity_9,roof_sheets_quantity_10,good_conditions,ridge_length,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked,glass_complete,replace_glass,reason_not_repaired,bRepair,fensa,WER_rating,overall_length_of_sheet")] ConsTable consTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consTable);
        }

        // GET: ConsTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConsTable == null)
            {
                return NotFound();
            }

            var consTable = await _context.ConsTable.FindAsync(id);
            if (consTable == null)
            {
                return NotFound();
            }
            return View(consTable);
        }

        // POST: ConsTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,isComplete,type,cause_of_damage,cause_of_damage_reason_different,material_type,sizeA,sizeB,sizeC,sizeD,sizeE,sizeF,sizeG,angle1,angle2,angle3,angle4,pitch_height,profile_section_size,sheet_width_1,sheet_height_1,sheet_width_2,sheet_height_2,sheet_width_3,sheet_height_3,sheet_width_4,sheet_height_4,sheet_width_5,sheet_height_5,sheet_width_6,sheet_height_6,sheet_width_7,sheet_height_7,sheet_width_8,sheet_height_8,sheet_width_9,sheet_height_9,sheet_width_10,sheet_height_10,flute_size,color,roof_color,new_firrings_req,new_gutters_req,roof_glazing_thickness,no_of_pics,no_of_photos,room_location,no_of_vids,bDifferentFromOriginal,ChangeItemTo,print_name,wall_pos,pitch_degree,long_comments,bDrawingsOnly,cons_roof_under_drawn,does_roof_fit_under,spars_line_up,roof_sheets_quantity_1,roof_sheets_quantity_2,roof_sheets_quantity_3,roof_sheets_quantity_4,roof_sheets_quantity_5,roof_sheets_quantity_6,roof_sheets_quantity_7,roof_sheets_quantity_8,roof_sheets_quantity_9,roof_sheets_quantity_10,good_conditions,ridge_length,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked,glass_complete,replace_glass,reason_not_repaired,bRepair,fensa,WER_rating,overall_length_of_sheet")] ConsTable consTable)
        {
            if (id != consTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsTableExists(consTable.Id))
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
            return View(consTable);
        }

        // GET: ConsTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConsTable == null)
            {
                return NotFound();
            }

            var consTable = await _context.ConsTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consTable == null)
            {
                return NotFound();
            }

            return View(consTable);
        }

        // POST: ConsTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ConsTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.ConsTable'  is null.");
            }
            var consTable = await _context.ConsTable.FindAsync(id);
            if (consTable != null)
            {
                _context.ConsTable.Remove(consTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsTableExists(int id)
        {
          return (_context.ConsTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
