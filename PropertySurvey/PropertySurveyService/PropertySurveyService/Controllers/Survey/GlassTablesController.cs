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
    public class GlassTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public GlassTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: GlassTables
        public async Task<IActionResult> Index()
        {
              return _context.GlassTable != null ? 
                          View(await _context.GlassTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.GlassTable'  is null.");
        }

        // GET: GlassTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.GlassTable == null)
            {
                return NotFound();
            }

            viewModel.Glass = await _context.GlassTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Glass == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.Glass.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Glass.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: GlassTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GlassTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,isComplete,cause_of_damage,cause_of_damage_reason_different,units_required,glass_width,glass_height,glass_width2,glass_height2,glass_width3,glass_height3,glass_width4,glass_height4,glass_width5,glass_height5,glass_width6,glass_height6,glass_width7,glass_height7,glass_width8,glass_height8,stepped_unit,int_width,int_height,single_or_double,glass_type,sizeA,sizeB,sizeC,sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,glass_pattern,spacer_color,spacer_thickness,special_glass,no_of_pics,docl_old,no_of_photos,gb_trim,docl,room_location,no_of_vids,bDifferentFromOriginal,ChangeItemTo,print_name,ProductInto,glazing_type,long_comments,lead_posX,lead_posY,TapeorGasket,glaze,lead_comments,collect_and_copy,temporary,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked,back_to_back_spacer_width,back_to_back_spacer_height,lead_CWidthf,lead_CHeightf,sizeAf,sizeBf,sizeCf,sizeDf,lead_CWidths,lead_CHeights,parent_item")] GlassTable glassTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(glassTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(glassTable);
        }

        // GET: GlassTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GlassTable == null)
            {
                return NotFound();
            }

            var glassTable = await _context.GlassTable.FindAsync(id);
            if (glassTable == null)
            {
                return NotFound();
            }
            return View(glassTable);
        }

        // POST: GlassTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,isComplete,cause_of_damage,cause_of_damage_reason_different,units_required,glass_width,glass_height,glass_width2,glass_height2,glass_width3,glass_height3,glass_width4,glass_height4,glass_width5,glass_height5,glass_width6,glass_height6,glass_width7,glass_height7,glass_width8,glass_height8,stepped_unit,int_width,int_height,single_or_double,glass_type,sizeA,sizeB,sizeC,sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,glass_pattern,spacer_color,spacer_thickness,special_glass,no_of_pics,docl_old,no_of_photos,gb_trim,docl,room_location,no_of_vids,bDifferentFromOriginal,ChangeItemTo,print_name,ProductInto,glazing_type,long_comments,lead_posX,lead_posY,TapeorGasket,glaze,lead_comments,collect_and_copy,temporary,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked,back_to_back_spacer_width,back_to_back_spacer_height,lead_CWidthf,lead_CHeightf,sizeAf,sizeBf,sizeCf,sizeDf,lead_CWidths,lead_CHeights,parent_item")] GlassTable glassTable)
        {
            if (id != glassTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glassTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlassTableExists(glassTable.Id))
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
            return View(glassTable);
        }

        // GET: GlassTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GlassTable == null)
            {
                return NotFound();
            }

            var glassTable = await _context.GlassTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (glassTable == null)
            {
                return NotFound();
            }

            return View(glassTable);
        }

        // POST: GlassTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GlassTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.GlassTable'  is null.");
            }
            var glassTable = await _context.GlassTable.FindAsync(id);
            if (glassTable != null)
            {
                _context.GlassTable.Remove(glassTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlassTableExists(int id)
        {
          return (_context.GlassTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
