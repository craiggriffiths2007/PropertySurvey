using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PropertySurveyService.Data;
using PropertySurveyService.Models;
using PropertySurveyService.ViewModels;

namespace PropertySurveyService.Controllers
{
    public class JobsController : Controller
    {
        private readonly Data.AppDBContext _context;

        public static string GetLocalIPAddress()
        {
            return "";
        }

        public JobsController(Data.AppDBContext context)
        {
            DbInitializer.Initialize(context);
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index(int? Id, int? headerId)
        {
            var viewModel = new JobIndexViewModel();

            viewModel.Jobs = _context.Job.Include(j => j.Customer).Include(j => j.Surveyor);

            if(Id!=null)
            {
                ViewData["JobID"] = Id.Value;

                viewModel.Headers = _context.Header.Where(x => x.udi_cont == _context.Job.FirstOrDefault(j => j.Id == Id).ContractCode).ToList();
            }

            /*
            if(headerId!=null)
            {
                ViewData["HeaderID"] = headerId.Value;

                foreach (var n in Enum.GetValues(typeof(enum_item_type)))
                {
                    switch (n)
                    {
                        //case enum_item_type.upvc:
                        //    viewModel.SurveyItems += _context.Header.Where(x => x.udi_cont == _context.Header.FirstOrDefault(j => j.Id == headerId.Value).udi_cont).ToEnumeral();
                        case enum_item_type.panel: foreach (var p in _context.PanelTable.Where(x => x.HeaderId == headerId.Value))
                                                        viewModel.SurveyItems.Append(p.AsSurveyItem());
                                                        break;     
                            //case enum_item_type.glass: return "Glass";
                            //case enum_item_type.alum: return "Aluminium";
                            //case enum_item_type.garage: return "Garage";
                            //case enum_item_type.timber: return "Timber";
                            //case enum_item_type.bifold: return "Bifold";
                            //case enum_item_type.lockin: return "Lock-mech";
                            //case enum_item_type.green: return "Greenhouse";
                    }
                }
            }
            */
            return View(viewModel);
        }

        private void PopulateCustomersDropDownList(object selectedCustomer = null)
        {
            var customersQuery = from d in _context.Customer
                                 orderby d.Name
                                 select d;
            ViewBag.CustomerId = new SelectList(customersQuery.AsNoTracking(), "CustomerId", "Name", selectedCustomer);
        }

        private void PopulateSurveyorsDropDownList(object selectedSurveyor = null)
        {
            var customersQuery = from d in _context.Surveyor
                                 orderby d.Name
                                 select d;
            ViewBag.SurveyorId = new SelectList(customersQuery.AsNoTracking(), "SurveyorId", "SurveyorCode", selectedSurveyor);
        }


        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Job == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.Customer)
                .Include(j => j.Surveyor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            PopulateCustomersDropDownList();
            PopulateSurveyorsDropDownList();
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Time,DamageDesc,Instructions,CustomerId,SurveyorId")] Job job)
        {
            if (ModelState.IsValid)
            {
                job.ContractCode = (job.Id + 1000).ToString("00000000");
                _context.Add(job);
                await _context.SaveChangesAsync();
                job.ContractCode = (job.Id + 1000).ToString("00000000");
                _context.Update(job);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCustomersDropDownList(job.CustomerId);
            PopulateSurveyorsDropDownList(job.SurveyorId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Job == null)
            {
                return NotFound();
            }

            var job = await _context.Job.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }
            
            PopulateCustomersDropDownList(job.CustomerId);
            PopulateSurveyorsDropDownList(job.SurveyorId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContractCode,ContractId,Date,Time,DamageDesc,Instructions,CustomerId,SurveyorId")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    job.ContractCode = (job.Id + 1000).ToString("00000000");
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
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
            PopulateCustomersDropDownList(job.CustomerId);
            PopulateSurveyorsDropDownList(job.SurveyorId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Job == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.Customer)
                .Include(j => j.Surveyor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Job == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.Job'  is null.");
            }
            var job = await _context.Job.FindAsync(id);
            if (job != null)
            {
                _context.Job.Remove(job);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return (_context.Job?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
