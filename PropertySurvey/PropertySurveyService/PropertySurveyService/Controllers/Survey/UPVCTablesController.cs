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
    public class UPVCTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public UPVCTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: UPVCTables
        public async Task<IActionResult> Index()
        {
              return _context.UPVCTable != null ? 
                          View(await _context.UPVCTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.UPVCTable'  is null.");
        }

        // GET: UPVCTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.UPVCTable == null)
            {
                return NotFound();
            }

            viewModel.UPVC = await _context.UPVCTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.UPVC == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.UPVC.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.UPVC.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: UPVCTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UPVCTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,isComplete,bRepair,cosmetic_damage,additional_locks,gaskets,gaskets_text,handles_req,handles_text,replace_panel,replace_reason,replace_explain,upvc_item,cause_of_damage,cause_of_damage_reason_different,colour,cills,outer_section_size,internal_width,internal_height,brick_width,brick_height,midrail,addons,addon_width,addon_height,head_drip,handle_colour,locking_type,letter_box,letter_box_pos,pet_flap,pet_type,pet_magnetic,bead_type,opens,glaze,trickle_vents,spacer_thickness,spacer_colour,glass_type,glass_pattern,special_glass,double_tripple,internal_lock,bNewLockingMech,bLockComplete,bHandleDrawingComplete,no_of_pics,midrail_height,no_of_photos,frame_depth,docl,profile_type,room_location,no_of_vids,LPHandles,slide_position,threshold_type,bDifferentFromOriginal,ChangeItemTo,print_name,fensa,WER_Rating,long_comments,bDoorComplete,bWindowComplete,lead_sizeA,lead_sizeB,lead_sizeC,lead_sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,lead_bSGDesignComplete,lock_make,lock_codes,bPanelComplete,left_bolt,right_bolt,GearBox,hinge_colour,lead_comments,collect_and_copy,temporary,parts_to_order,is_a_flat,point_of_entry,type_of_lockng_system_required,was_it_locked,back_to_back_spacer_width,back_to_back_spacer_height,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,glass_complete,replace_glass")] UPVCTable uPVCTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uPVCTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uPVCTable);
        }

        // GET: UPVCTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UPVCTable == null)
            {
                return NotFound();
            }

            var uPVCTable = await _context.UPVCTable.FindAsync(id);
            if (uPVCTable == null)
            {
                return NotFound();
            }
            return View(uPVCTable);
        }

        // POST: UPVCTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,isComplete,bRepair,cosmetic_damage,additional_locks,gaskets,gaskets_text,handles_req,handles_text,replace_panel,replace_reason,replace_explain,upvc_item,cause_of_damage,cause_of_damage_reason_different,colour,cills,outer_section_size,internal_width,internal_height,brick_width,brick_height,midrail,addons,addon_width,addon_height,head_drip,handle_colour,locking_type,letter_box,letter_box_pos,pet_flap,pet_type,pet_magnetic,bead_type,opens,glaze,trickle_vents,spacer_thickness,spacer_colour,glass_type,glass_pattern,special_glass,double_tripple,internal_lock,bNewLockingMech,bLockComplete,bHandleDrawingComplete,no_of_pics,midrail_height,no_of_photos,frame_depth,docl,profile_type,room_location,no_of_vids,LPHandles,slide_position,threshold_type,bDifferentFromOriginal,ChangeItemTo,print_name,fensa,WER_Rating,long_comments,bDoorComplete,bWindowComplete,lead_sizeA,lead_sizeB,lead_sizeC,lead_sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,lead_bSGDesignComplete,lock_make,lock_codes,bPanelComplete,left_bolt,right_bolt,GearBox,hinge_colour,lead_comments,collect_and_copy,temporary,parts_to_order,is_a_flat,point_of_entry,type_of_lockng_system_required,was_it_locked,back_to_back_spacer_width,back_to_back_spacer_height,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,glass_complete,replace_glass")] UPVCTable uPVCTable)
        {
            if (id != uPVCTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uPVCTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UPVCTableExists(uPVCTable.Id))
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
            return View(uPVCTable);
        }

        // GET: UPVCTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UPVCTable == null)
            {
                return NotFound();
            }

            var uPVCTable = await _context.UPVCTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uPVCTable == null)
            {
                return NotFound();
            }

            return View(uPVCTable);
        }

        // POST: UPVCTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UPVCTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.UPVCTable'  is null.");
            }
            var uPVCTable = await _context.UPVCTable.FindAsync(id);
            if (uPVCTable != null)
            {
                _context.UPVCTable.Remove(uPVCTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UPVCTableExists(int id)
        {
          return (_context.UPVCTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
