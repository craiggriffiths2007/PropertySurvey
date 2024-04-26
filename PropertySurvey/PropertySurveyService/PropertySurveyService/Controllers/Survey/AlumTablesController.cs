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
    public class AlumTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public AlumTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: AlumTables
        public async Task<IActionResult> Index()
        {
              return _context.AlumTable != null ? 
                          View(await _context.AlumTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.AlumTable'  is null.");
        }

        // GET: AlumTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.AlumTable == null)
            {
                return NotFound();
            }

            viewModel.Alum = await _context.AlumTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Alum == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.Alum.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Alum.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: AlumTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AlumTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,bRepair,cosmetic_damage,additional_locks,gaskets,gaskets_text,handles_req,handles_text,replace_panel,replace_reason,replace_explain,type,cause_of_damage,cause_of_damage_reason_different,section_type,new_timber_sub_frame,sub_frame_depth,item_frame_width,item_frame_height,brick_width,brick_height,internal_width,internal_height,frame_type,cill,drip,night_vent,midrail_type,item_color,locking_type,letter_box,letter_box_pos,pet_flap,pet_type,pet_magnetic,opens,handle_color,spacer_thickness,spacer_color,glass_type,glass_pattern,special_glass,sub_frame_color,bNewLockingMech,bLockComplete,bHandleDrawingComplete,no_of_pics,midrail_height,no_of_photos,docl,room_location,no_of_vids,LPHandles,threshold_type,bDifferentFromOriginal,ChangeItemTo,print_name,bFencer,FecerRating,long_comments,bDoorComplete,bWindowComplete,lead_sizeA,lead_sizeB,lead_sizeC,lead_sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,lead_bSGDesignComplete,lock_make,lock_codes,bPanelComplete,GearBox,left_bolt,right_bolt,isComplete,cill_on_subframe,cill_type,i_spare3,collect_and_copy,temporary,glazed,bead_type,outer_section_width,outer_section_height,parts_to_order,lead_comments,is_a_flat,point_of_entry,type_of_lockng_system_required,was_it_locked,back_to_back_spacer_width,back_to_back_spacer_height,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,glass_complete,replace_glass")] AluminiumTable alumTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumTable);
        }

        // GET: AlumTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AlumTable == null)
            {
                return NotFound();
            }

            var alumTable = await _context.AlumTable.FindAsync(id);
            if (alumTable == null)
            {
                return NotFound();
            }
            return View(alumTable);
        }

        // POST: AlumTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,bRepair,cosmetic_damage,additional_locks,gaskets,gaskets_text,handles_req,handles_text,replace_panel,replace_reason,replace_explain,type,cause_of_damage,cause_of_damage_reason_different,section_type,new_timber_sub_frame,sub_frame_depth,item_frame_width,item_frame_height,brick_width,brick_height,internal_width,internal_height,frame_type,cill,drip,night_vent,midrail_type,item_color,locking_type,letter_box,letter_box_pos,pet_flap,pet_type,pet_magnetic,opens,handle_color,spacer_thickness,spacer_color,glass_type,glass_pattern,special_glass,sub_frame_color,bNewLockingMech,bLockComplete,bHandleDrawingComplete,no_of_pics,midrail_height,no_of_photos,docl,room_location,no_of_vids,LPHandles,threshold_type,bDifferentFromOriginal,ChangeItemTo,print_name,bFencer,FecerRating,long_comments,bDoorComplete,bWindowComplete,lead_sizeA,lead_sizeB,lead_sizeC,lead_sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,lead_bSGDesignComplete,lock_make,lock_codes,bPanelComplete,GearBox,left_bolt,right_bolt,isComplete,cill_on_subframe,cill_type,i_spare3,collect_and_copy,temporary,glazed,bead_type,outer_section_width,outer_section_height,parts_to_order,lead_comments,is_a_flat,point_of_entry,type_of_lockng_system_required,was_it_locked,back_to_back_spacer_width,back_to_back_spacer_height,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,glass_complete,replace_glass")] AluminiumTable alumTable)
        {
            if (id != alumTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumTableExists(alumTable.Id))
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
            return View(alumTable);
        }

        // GET: AlumTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AlumTable == null)
            {
                return NotFound();
            }

            var alumTable = await _context.AlumTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumTable == null)
            {
                return NotFound();
            }

            return View(alumTable);
        }

        // POST: AlumTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AlumTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.AlumTable'  is null.");
            }
            var alumTable = await _context.AlumTable.FindAsync(id);
            if (alumTable != null)
            {
                _context.AlumTable.Remove(alumTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumTableExists(int id)
        {
          return (_context.AlumTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
