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
    public class TimberTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public TimberTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: TimberTables
        public async Task<IActionResult> Index()
        {
              return _context.TimberTable != null ? 
                          View(await _context.TimberTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.TimberTable'  is null.");
        }

        // GET: TimberTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.TimberTable == null)
            {
                return NotFound();
            }

            viewModel.Timber = await _context.TimberTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Timber == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.Timber.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Timber.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: TimberTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimberTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,isComplete,bRepair,cosmetic_damage,additional_locks,gaskets,gaskets_text,handles_req,handles_text,replace_reason,replace_explain,timber_item,cause_of_damage,cause_of_damage_reason_different,timber_wood,timber_frame_wood,timber_new_frame_req,brick_width,brick_height,internal_width,internal_height,repair_frame,door_thickness,door_width,door_height,opens,new_sash_required,head_drip,cills,draught_strip,pet_flap,pet_type,pet_magnetic,bDoorComplete,bWindowComplete,beading_type,thresher,single_double,trickle_vents,locks,hardware_color,door_color,frame_color,spacer_thickness,spacer_color,glass_type,glass_pattern,special_glass,bNewLockingMech,bLockComplete,bHandleDrawingComplete,no_of_pics,no_of_photos,no_of_vids,docl,bSashDrawn,bSectionDrawn,bMouldingDrawn,room_location,doc_l_compliant_reason,doc_l_compliant,door_color_out,frame_color_out,door_color_code,door_color_code_out,frame_color_code,frame_color_code_out,b_signed,slide_position,timber_glazed,bDifferentFromOriginal,ChangeItemTo,print_name,standard_sizes,reasonnonstandard,Fensa,WER_rating,long_timber_comments,lead_sizeA,lead_sizeB,lead_sizeC,lead_sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,lock_make,lock_codes,GearBox,left_bolt,right_bolt,letter_box,letter_box_pos,moulding,hinge_type,collect_and_copy,temporary,pre_glazed_door,lead_comments,weather_bar,parts_to_order,is_a_flat,point_of_entry,type_of_lockng_system_required,was_it_locked,back_to_back_spacer_width,back_to_back_spacer_height,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,glass_complete,replace_glass")] TimberTable timberTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timberTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timberTable);
        }

        // GET: TimberTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TimberTable == null)
            {
                return NotFound();
            }

            var timberTable = await _context.TimberTable.FindAsync(id);
            if (timberTable == null)
            {
                return NotFound();
            }
            return View(timberTable);
        }

        // POST: TimberTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,isComplete,bRepair,cosmetic_damage,additional_locks,gaskets,gaskets_text,handles_req,handles_text,replace_reason,replace_explain,timber_item,cause_of_damage,cause_of_damage_reason_different,timber_wood,timber_frame_wood,timber_new_frame_req,brick_width,brick_height,internal_width,internal_height,repair_frame,door_thickness,door_width,door_height,opens,new_sash_required,head_drip,cills,draught_strip,pet_flap,pet_type,pet_magnetic,bDoorComplete,bWindowComplete,beading_type,thresher,single_double,trickle_vents,locks,hardware_color,door_color,frame_color,spacer_thickness,spacer_color,glass_type,glass_pattern,special_glass,bNewLockingMech,bLockComplete,bHandleDrawingComplete,no_of_pics,no_of_photos,no_of_vids,docl,bSashDrawn,bSectionDrawn,bMouldingDrawn,room_location,doc_l_compliant_reason,doc_l_compliant,door_color_out,frame_color_out,door_color_code,door_color_code_out,frame_color_code,frame_color_code_out,b_signed,slide_position,timber_glazed,bDifferentFromOriginal,ChangeItemTo,print_name,standard_sizes,reasonnonstandard,Fensa,WER_rating,long_timber_comments,lead_sizeA,lead_sizeB,lead_sizeC,lead_sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,lock_make,lock_codes,GearBox,left_bolt,right_bolt,letter_box,letter_box_pos,moulding,hinge_type,collect_and_copy,temporary,pre_glazed_door,lead_comments,weather_bar,parts_to_order,is_a_flat,point_of_entry,type_of_lockng_system_required,was_it_locked,back_to_back_spacer_width,back_to_back_spacer_height,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,glass_complete,replace_glass")] TimberTable timberTable)
        {
            if (id != timberTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timberTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimberTableExists(timberTable.Id))
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
            return View(timberTable);
        }

        // GET: TimberTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TimberTable == null)
            {
                return NotFound();
            }

            var timberTable = await _context.TimberTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timberTable == null)
            {
                return NotFound();
            }

            return View(timberTable);
        }

        // POST: TimberTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TimberTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.TimberTable'  is null.");
            }
            var timberTable = await _context.TimberTable.FindAsync(id);
            if (timberTable != null)
            {
                _context.TimberTable.Remove(timberTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimberTableExists(int id)
        {
          return (_context.TimberTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
