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
    public class GarageTablesController : Controller
    {
        private readonly Data.AppDBContext _context;

        public GarageTablesController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: GarageTables
        public async Task<IActionResult> Index()
        {
              return _context.GarageTable != null ? 
                          View(await _context.GarageTable.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.GarageTable'  is null.");
        }

        // GET: GarageTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ItemIndexViewModel();

            if (id == null || _context.GarageTable == null)
            {
                return NotFound();
            }

            viewModel.Garage = await _context.GarageTable
               .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Garage == null)
            {
                return NotFound();
            }

            List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 8) == viewModel.Garage.udi_cont &&
            x.Filename.Substring(12, 3) == viewModel.Garage.item_number.ToString("000")).ToList();

            viewModel.Images = photoimages;

            return View(viewModel);
        }

        // GET: GarageTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GarageTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HeaderId,udi_cont,item_number,opening_direction,cause_of_damage,cause_of_damage_reason_different,door_fits_into,new_subframe_req,obstruction_outside_b,obstruction_outside,obstruction_inside_b,obstruction_inside,actual_door_width,actual_door_height,frame_fix_type,type_of_garage,new_electric_operator_req,side_size_A,side_size_B,side_size_C,side_size_D,side_size_E,side_size_F,side_size_G,side_timber_1,side_timber_2,plan_size_A,plan_size_B,plan_size_C1,plan_size_C2,plan_size_D,plan_timber_1,plan_timber_2,color,opening_type,finish,power_points,electric_door,handle_outside,other_access,need_safety_release,no_of_pics,no_of_photos,insulated,door_stuck_shut,motor_position,no_of_vids,bDifferentFromOriginal,ChangeItemTo,print_name,long_comments,isComplete,door_within_perimeter,socket_within_1m,wire_type,colour_match_roll_box,additional_drawn,roller_door_type,roller_box_type,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked,where_is_garage")] GarageTable garageTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(garageTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(garageTable);
        }

        // GET: GarageTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GarageTable == null)
            {
                return NotFound();
            }

            var garageTable = await _context.GarageTable.FindAsync(id);
            if (garageTable == null)
            {
                return NotFound();
            }
            return View(garageTable);
        }

        // POST: GarageTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HeaderId,udi_cont,item_number,opening_direction,cause_of_damage,cause_of_damage_reason_different,door_fits_into,new_subframe_req,obstruction_outside_b,obstruction_outside,obstruction_inside_b,obstruction_inside,actual_door_width,actual_door_height,frame_fix_type,type_of_garage,new_electric_operator_req,side_size_A,side_size_B,side_size_C,side_size_D,side_size_E,side_size_F,side_size_G,side_timber_1,side_timber_2,plan_size_A,plan_size_B,plan_size_C1,plan_size_C2,plan_size_D,plan_timber_1,plan_timber_2,color,opening_type,finish,power_points,electric_door,handle_outside,other_access,need_safety_release,no_of_pics,no_of_photos,insulated,door_stuck_shut,motor_position,no_of_vids,bDifferentFromOriginal,ChangeItemTo,print_name,long_comments,isComplete,door_within_perimeter,socket_within_1m,wire_type,colour_match_roll_box,additional_drawn,roller_door_type,roller_box_type,parts_to_order,point_of_entry,type_of_lockng_system_required,was_it_locked,where_is_garage")] GarageTable garageTable)
        {
            if (id != garageTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garageTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GarageTableExists(garageTable.Id))
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
            return View(garageTable);
        }

        // GET: GarageTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GarageTable == null)
            {
                return NotFound();
            }

            var garageTable = await _context.GarageTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (garageTable == null)
            {
                return NotFound();
            }

            return View(garageTable);
        }

        // POST: GarageTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GarageTable == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.GarageTable'  is null.");
            }
            var garageTable = await _context.GarageTable.FindAsync(id);
            if (garageTable != null)
            {
                _context.GarageTable.Remove(garageTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GarageTableExists(int id)
        {
          return (_context.GarageTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
