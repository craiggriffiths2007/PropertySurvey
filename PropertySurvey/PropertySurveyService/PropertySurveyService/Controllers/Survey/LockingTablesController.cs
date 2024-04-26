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
    public class LockingTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public LockingTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: LockingTables
        public async Task<IActionResult> Index()
        {
              return _context.LockingTable != null ? 
                          View(await _context.LockingTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.LockingTable'  is null.");
        }

        // GET: LockingTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.LockingTable == null)
            {
                return NotFound();
            }

            viewModel.Lockin = await _context.LockingTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Lockin == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.Lockin.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Lockin.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: LockingTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LockingTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,isComplete,comments,point_of_entry,type_of_lockng_system_required,was_it_locked,no_of_pics,no_of_photos,bMulti,item,locking_make,locking_codes,bDoorComplete,bWindowComplete,lock_colour,pagenum,bDifferentFromOriginal,ChangeItemTo,print_name,COD_Code,cause_of_damage,cause_of_damage_reason_different,GearBox,no_of_vids,left_bolt,right_bolt,parts_to_order,bLockComplete,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,long_comments")] LockingTable lockingTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lockingTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lockingTable);
        }

        // GET: LockingTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LockingTable == null)
            {
                return NotFound();
            }

            var lockingTable = await _context.LockingTable.FindAsync(id);
            if (lockingTable == null)
            {
                return NotFound();
            }
            return View(lockingTable);
        }

        // POST: LockingTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,isComplete,comments,point_of_entry,type_of_lockng_system_required,was_it_locked,no_of_pics,no_of_photos,bMulti,item,locking_make,locking_codes,bDoorComplete,bWindowComplete,lock_colour,pagenum,bDifferentFromOriginal,ChangeItemTo,print_name,COD_Code,cause_of_damage,cause_of_damage_reason_different,GearBox,no_of_vids,left_bolt,right_bolt,parts_to_order,bLockComplete,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,long_comments")] LockingTable lockingTable)
        {
            if (id != lockingTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lockingTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LockingTableExists(lockingTable.Id))
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
            return View(lockingTable);
        }

        // GET: LockingTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LockingTable == null)
            {
                return NotFound();
            }

            var lockingTable = await _context.LockingTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lockingTable == null)
            {
                return NotFound();
            }

            return View(lockingTable);
        }

        // POST: LockingTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LockingTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.LockingTable'  is null.");
            }
            var lockingTable = await _context.LockingTable.FindAsync(id);
            if (lockingTable != null)
            {
                _context.LockingTable.Remove(lockingTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LockingTableExists(int id)
        {
          return (_context.LockingTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
