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
    public class HeadersController : Controller
    {
        private readonly Data.AppDBContext _context;

        public HeadersController(Data.AppDBContext context)
        {
            _context = context;
        }

        // GET: Headers
        public async Task<IActionResult> Index()
        {
              return _context.Header != null ? 
                          View(await _context.Header.ToListAsync()) :
                          Problem("Entity set 'PropertySurveyServiceContext.Header'  is null.");
        }

        // GET: Headers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new HeaderIndexViewModel();

            if (id == null || _context.Header == null)
            {
                return NotFound();
            }

            viewModel.Header = await _context.Header
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viewModel.Header == null)
            {
                return NotFound();
            }
            else
            {
                List<SurveyItem> items = new List<SurveyItem>();
                //viewModel.SurveyItems = new INumerable<SurveyItem>();
                foreach (var n in Enum.GetValues(typeof(enum_item_type)))
                {
                    switch (n)
                    {
                        case enum_item_type.upvc:
                            foreach (var p in _context.UPVCTable.Where(x => x.HeaderId == viewModel.Header.Id)) items.Add(p.AsSurveyItem()); break;
                        case enum_item_type.panel:
                            foreach (var p in _context.PanelTable.Where(x => x.HeaderId == viewModel.Header.Id)) items.Add(p.AsSurveyItem()); break;
                        case enum_item_type.glass:
                            foreach (var p in _context.GlassTable.Where(x => x.HeaderId == viewModel.Header.Id)) items.Add(p.AsSurveyItem()); break;
                        case enum_item_type.alum:
                            foreach (var p in _context.AlumTable.Where(x => x.HeaderId == viewModel.Header.Id)) items.Add(p.AsSurveyItem()); break;
                        case enum_item_type.garage:
                            foreach (var p in _context.GarageTable.Where(x => x.HeaderId == viewModel.Header.Id)) items.Add(p.AsSurveyItem()); break;
                        case enum_item_type.timber:
                            foreach (var p in _context.TimberTable.Where(x => x.HeaderId == viewModel.Header.Id)) items.Add(p.AsSurveyItem()); break;
                        case enum_item_type.bifold:
                            foreach (var p in _context.BifoldTable.Where(x => x.HeaderId == viewModel.Header.Id)) items.Add(p.AsSurveyItem()); break;
                        case enum_item_type.lockin:
                            foreach (var p in _context.LockingTable.Where(x => x.HeaderId == viewModel.Header.Id)) items.Add(p.AsSurveyItem()); break;
                        case enum_item_type.green:
                            foreach (var p in _context.GreenTable.Where(x => x.HeaderId == viewModel.Header.Id)) items.Add(p.AsSurveyItem()); break;
                    }
                }
                viewModel.SurveyItems = items;

                List<PhotoImage> photoimages = _context.Images.Where(x => x.Filename.Substring(0, 12) == viewModel.Header.udi_cont+"_cAH").ToList();

                viewModel.Images = photoimages;
            }

            return View(viewModel);
        }

        // GET: Headers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Headers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RecID,bDone,bSent,iRecordType,typeA,typeB,fit_diary,fitters_instructions,fit_start,fit_fin,fitter_work,parts_used,claim_ref,fitter_comments,udi_cont,sn_name,uc_laname,uc_name,uc_add1,uc_add2,uc_add3,uc_add4,uc_postcode,uc_h_phone,uc_goahead,uc_inceden,spare2,uc_excess,bExcessCollected,udi_tlight,si_numitem,udi_inst,alarm_cont,ladder_req,height_res,sand_cemen,plaster,doorbell,genreq,architreq,acroreq,acrosboy,acc_text,obs_wires,obs_wires_text,loose_brick,loose_brick_text,easy_park,access_rear,rep_text,mop,card_cheq,expiry,issue_no,reason_excess_not_collected,paych,summ_text,code_text,imchup,job_grade,njs,photo,booked,nsn,udi_start,udi_fin,si_done,udi_date,si_bday1,si_mpay,si_cnum,si_inum,udi_jobtext,udi_staff,type,sub_type,old_date,cover_instructions,old_start,old_finish,add_comm,udi_estrem,r_fault,r_excess,rexcedit,r_comp,rno_hours,r_work_txt,readditimage,readdtxt,r_sigimage,f_add_txt,fmclrf,fmdate,funfincode,funfinoth,freuntxt,fpartreq,fjobfin,fname1,fname2,fexcess,fexcessoth,fmand,fmandoth,ftimearr,ftimeleft,faddpaid,faddmuch,commtxt,wkcartxt,parttxt,faddimage,fmanimage,fsigimage,bWorkInside,inst_height,bBothHands,ground_surface,type_of_equipment,risks_and_dangers,uc_desc,work_at_height,no_ladders,funfinished_code,freason_unfinished,fparts_required,ffitter_name1,ffitter_name2,fbexcess_paid,freason_excess_not_paid,fbmandate_signed,freason_mandate_not_signed,ftime_arrived,ftime_left,fbadditional_paid,fhow_mutch_additional_paid,bfitter_complete,fitter_info_done,fbunfinother,bcompletion_signed,bad_image_complete,remedial_number,r_bsigned,r_bcomp,r_sign_date,stimea,f1_or_s2,f_sign_date,distance,duration,no_of_photos,bClosest,Group,bProcessed,ind,inevitable_damage,fbstockusagecomplete,uc_h_phone2,uc_h_phone3,bSecuring,ins_board,ins_lock,ins_temp,ins_perm,int_num_of_locks,int_type_of_lock,parking_at_rear,work_on_public_footpath,add_long,b_mrk,bSurvey,items_above_roof,added_to_otherrisks,bMSFJob,securing_surveyor_required,policy_number,photo_front_of_house,asbestos_visible,asvizex,refmessage,uspot_fitter,uspot_trainee,uspot_date,uspot_customer,uspot_postcode,uspot_insuranceco,uspot_branch,uspot_repair,uspot_repair_arrived,uspot_repair_setup,uspot_repair_ongoing,uspot_repair_completed,uspot_replace,uspot_replace_arrived,uspot_replace_setup,uspot_replace_unitmoved,uspot_replace_completed,uspot_rev_door,uspot_rev_window,uspot_rev_garagedoor,uspot_rev_glass,uspot_rev_locks,uspot_rev_other,uspot_revb_upvc,uspot_revb_ali,uspot_revb_timber,uspot_revb_other,uspot_appearence,doc_l_compliant_reason,lintel_present,lintel_present_text,uspot_customersatisfaction,uspot_customersatisfaction_improvementsOld,uspot_otherobservationsOld,uspot_signed,uspot_signeddate,bSpotCheck,uspot_replace_fit,uspot_p1,uspot_p2,uspot_p3,uspot_p4,uspot_appearence_improvements,uspot_qualityofworks_improvements,uspot_customersatisfaction_improvements,uspot_otherobservations,idampassword_entered,fit_no_of_videos,doc_l_compliant,shop_front_work,fitter_videos,is_halifax,messagetoinsurer,COD_Code,COD_String,bDamTicked,bSSTicked,SSRequired,old_cover_instructions,rcodchanged,bcodchanged,goaheadstr,b_subcontract,subcontracttext,truecomm,truecommconf,reason_not_booked_in,bSurveyRequiredOnSecuring,requiring_load_bearing_jacks,bSRFin,bMOPFin,bRepFin,bSumFin,bHazFin,bAllPictures,bSubFin,total_upvc,total_panels,total_glass,total_alum,total_garage,total_timber,total_cons,total_lock,total_comp,total_green,total_bifold,incomplete_upvc,incomplete_panels,incomplete_glass,incomplete_alum,incomplete_garage,incomplete_timber,incomplete_cons,incomplete_lock,incomplete_comp,incomplete_green,incomplete_bifold,front_house_photos,time_to_complete,current_item_number,survey_complete,reason_not_complete,add_phone_1,add_phone_2,no_of_fitters,fname3,fname4,fname5,fname6,fname7,fname8,ownquote,survey_on_fit,i_spare1,i_spare2,i_spare3,s_spare1,s_spare2,s_spare3,new_ispare1,new_ispare2,new_ispare3,new_ispare4,new_ispare5,new_ispare6,new_ispare7,new_ispare8,new_ispare9,is_messagetoinsurer,new_sspare1,new_sspare2,new_sspare3,new_sspare4,new_sspare5,new_sspare6,new_sspare7,new_sspare8,new_sspare9,new_sspare10,bInfoSeen,ss_bIsSecuritySurvey,ss_bIsComplete,ss_nowindows,ss_nodoors,ss_gencondition,ss_gencondition_other,ss_matwindows,ss_matwindows_other,ss_matdoors,ss_matdoors_other,ss_lockwindows,ss_lockwindows_other,ss_lockdoors,ss_lockdoors_other,ss_add_window_security,ss_location_windows_other,ss_secwindows_other,ss_add_door_security,ss_location_doors_other,ss_secdoors_other,ss_time_required,ss_no_of_photos,door_type,model_type,unique_serial,door_size,door_manufacturer,powerered_operator_type,operator_manufacturer,site_address,decleration_by,on_behalf_of_person,on_behalf_of_company,decleration_received_by,date,print_name,date_cust,i_signed,i_signed_cust,directive_complete,branch,name,job,name1,safety_boots_worn1,safety_gloves_worn1,safety_googles_worn1,safety_helmet_worn1,wristguards_worn1,uniform_worn_complete1,id_card_available1,name2,safety_boots_worn2,safety_gloves_worn2,safety_googles_worn2,safety_helmet_worn2,wristguards_worn2,uniform_worn_complete2,id_card_available2,chemicals_stored_correctly,are_sheets_available,area_above_been_checked,obstructions_checked,lintel_ok,ladders_secure,safe_work_at_height,condition_of_ladders,tools_set_out_safely,fire_extinguisher_on_van,first_aid_kit_on_van,electrical_equipment_tested,safety_boots_worn1_s,safety_gloves_worn1_s,safety_googles_worn1_s,safety_helmet_worn1_s,wristguards_worn1_s,uniform_worn_complete1_s,id_card_available1_s,safety_boots_worn2_s,safety_gloves_worn2_s,safety_googles_worn2_s,safety_helmet_worn2_s,wristguards_worn2_s,uniform_worn_complete2_s,id_card_available2_s,chemicals_stored_correctly_s,are_sheets_available_s,area_above_been_checked_s,obstructions_checked_s,lintel_ok_s,ladders_secure_s,safe_work_at_height_s,condition_of_ladders_s,tools_set_out_safely_s,fire_extinguisher_on_van_s,first_aid_kit_on_van_s,electrical_equipment_tested_s,comments,uspot_qualityofworks,current_summary_num,uspot_replacement,garage_door_motor,isTowerScaff")] Header header)
        {
            if (ModelState.IsValid)
            {
                _context.Add(header);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(header);
        }

        // GET: Headers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Header == null)
            {
                return NotFound();
            }

            var header = await _context.Header.FindAsync(id);
            if (header == null)
            {
                return NotFound();
            }
            return View(header);
        }

        // POST: Headers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RecID,bDone,bSent,iRecordType,typeA,typeB,fit_diary,fitters_instructions,fit_start,fit_fin,fitter_work,parts_used,claim_ref,fitter_comments,udi_cont,sn_name,uc_laname,uc_name,uc_add1,uc_add2,uc_add3,uc_add4,uc_postcode,uc_h_phone,uc_goahead,uc_inceden,spare2,uc_excess,bExcessCollected,udi_tlight,si_numitem,udi_inst,alarm_cont,ladder_req,height_res,sand_cemen,plaster,doorbell,genreq,architreq,acroreq,acrosboy,acc_text,obs_wires,obs_wires_text,loose_brick,loose_brick_text,easy_park,access_rear,rep_text,mop,card_cheq,expiry,issue_no,reason_excess_not_collected,paych,summ_text,code_text,imchup,job_grade,njs,photo,booked,nsn,udi_start,udi_fin,si_done,udi_date,si_bday1,si_mpay,si_cnum,si_inum,udi_jobtext,udi_staff,type,sub_type,old_date,cover_instructions,old_start,old_finish,add_comm,udi_estrem,r_fault,r_excess,rexcedit,r_comp,rno_hours,r_work_txt,readditimage,readdtxt,r_sigimage,f_add_txt,fmclrf,fmdate,funfincode,funfinoth,freuntxt,fpartreq,fjobfin,fname1,fname2,fexcess,fexcessoth,fmand,fmandoth,ftimearr,ftimeleft,faddpaid,faddmuch,commtxt,wkcartxt,parttxt,faddimage,fmanimage,fsigimage,bWorkInside,inst_height,bBothHands,ground_surface,type_of_equipment,risks_and_dangers,uc_desc,work_at_height,no_ladders,funfinished_code,freason_unfinished,fparts_required,ffitter_name1,ffitter_name2,fbexcess_paid,freason_excess_not_paid,fbmandate_signed,freason_mandate_not_signed,ftime_arrived,ftime_left,fbadditional_paid,fhow_mutch_additional_paid,bfitter_complete,fitter_info_done,fbunfinother,bcompletion_signed,bad_image_complete,remedial_number,r_bsigned,r_bcomp,r_sign_date,stimea,f1_or_s2,f_sign_date,distance,duration,no_of_photos,bClosest,Group,bProcessed,ind,inevitable_damage,fbstockusagecomplete,uc_h_phone2,uc_h_phone3,bSecuring,ins_board,ins_lock,ins_temp,ins_perm,int_num_of_locks,int_type_of_lock,parking_at_rear,work_on_public_footpath,add_long,b_mrk,bSurvey,items_above_roof,added_to_otherrisks,bMSFJob,securing_surveyor_required,policy_number,photo_front_of_house,asbestos_visible,asvizex,refmessage,uspot_fitter,uspot_trainee,uspot_date,uspot_customer,uspot_postcode,uspot_insuranceco,uspot_branch,uspot_repair,uspot_repair_arrived,uspot_repair_setup,uspot_repair_ongoing,uspot_repair_completed,uspot_replace,uspot_replace_arrived,uspot_replace_setup,uspot_replace_unitmoved,uspot_replace_completed,uspot_rev_door,uspot_rev_window,uspot_rev_garagedoor,uspot_rev_glass,uspot_rev_locks,uspot_rev_other,uspot_revb_upvc,uspot_revb_ali,uspot_revb_timber,uspot_revb_other,uspot_appearence,doc_l_compliant_reason,lintel_present,lintel_present_text,uspot_customersatisfaction,uspot_customersatisfaction_improvementsOld,uspot_otherobservationsOld,uspot_signed,uspot_signeddate,bSpotCheck,uspot_replace_fit,uspot_p1,uspot_p2,uspot_p3,uspot_p4,uspot_appearence_improvements,uspot_qualityofworks_improvements,uspot_customersatisfaction_improvements,uspot_otherobservations,idampassword_entered,fit_no_of_videos,doc_l_compliant,shop_front_work,fitter_videos,is_halifax,messagetoinsurer,COD_Code,COD_String,bDamTicked,bSSTicked,SSRequired,old_cover_instructions,rcodchanged,bcodchanged,goaheadstr,b_subcontract,subcontracttext,truecomm,truecommconf,reason_not_booked_in,bSurveyRequiredOnSecuring,requiring_load_bearing_jacks,bSRFin,bMOPFin,bRepFin,bSumFin,bHazFin,bAllPictures,bSubFin,total_upvc,total_panels,total_glass,total_alum,total_garage,total_timber,total_cons,total_lock,total_comp,total_green,total_bifold,incomplete_upvc,incomplete_panels,incomplete_glass,incomplete_alum,incomplete_garage,incomplete_timber,incomplete_cons,incomplete_lock,incomplete_comp,incomplete_green,incomplete_bifold,front_house_photos,time_to_complete,current_item_number,survey_complete,reason_not_complete,add_phone_1,add_phone_2,no_of_fitters,fname3,fname4,fname5,fname6,fname7,fname8,ownquote,survey_on_fit,i_spare1,i_spare2,i_spare3,s_spare1,s_spare2,s_spare3,new_ispare1,new_ispare2,new_ispare3,new_ispare4,new_ispare5,new_ispare6,new_ispare7,new_ispare8,new_ispare9,is_messagetoinsurer,new_sspare1,new_sspare2,new_sspare3,new_sspare4,new_sspare5,new_sspare6,new_sspare7,new_sspare8,new_sspare9,new_sspare10,bInfoSeen,ss_bIsSecuritySurvey,ss_bIsComplete,ss_nowindows,ss_nodoors,ss_gencondition,ss_gencondition_other,ss_matwindows,ss_matwindows_other,ss_matdoors,ss_matdoors_other,ss_lockwindows,ss_lockwindows_other,ss_lockdoors,ss_lockdoors_other,ss_add_window_security,ss_location_windows_other,ss_secwindows_other,ss_add_door_security,ss_location_doors_other,ss_secdoors_other,ss_time_required,ss_no_of_photos,door_type,model_type,unique_serial,door_size,door_manufacturer,powerered_operator_type,operator_manufacturer,site_address,decleration_by,on_behalf_of_person,on_behalf_of_company,decleration_received_by,date,print_name,date_cust,i_signed,i_signed_cust,directive_complete,branch,name,job,name1,safety_boots_worn1,safety_gloves_worn1,safety_googles_worn1,safety_helmet_worn1,wristguards_worn1,uniform_worn_complete1,id_card_available1,name2,safety_boots_worn2,safety_gloves_worn2,safety_googles_worn2,safety_helmet_worn2,wristguards_worn2,uniform_worn_complete2,id_card_available2,chemicals_stored_correctly,are_sheets_available,area_above_been_checked,obstructions_checked,lintel_ok,ladders_secure,safe_work_at_height,condition_of_ladders,tools_set_out_safely,fire_extinguisher_on_van,first_aid_kit_on_van,electrical_equipment_tested,safety_boots_worn1_s,safety_gloves_worn1_s,safety_googles_worn1_s,safety_helmet_worn1_s,wristguards_worn1_s,uniform_worn_complete1_s,id_card_available1_s,safety_boots_worn2_s,safety_gloves_worn2_s,safety_googles_worn2_s,safety_helmet_worn2_s,wristguards_worn2_s,uniform_worn_complete2_s,id_card_available2_s,chemicals_stored_correctly_s,are_sheets_available_s,area_above_been_checked_s,obstructions_checked_s,lintel_ok_s,ladders_secure_s,safe_work_at_height_s,condition_of_ladders_s,tools_set_out_safely_s,fire_extinguisher_on_van_s,first_aid_kit_on_van_s,electrical_equipment_tested_s,comments,uspot_qualityofworks,current_summary_num,uspot_replacement,garage_door_motor,isTowerScaff")] Header header)
        {
            if (id != header.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(header);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeaderExists(header.Id))
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
            return View(header);
        }

        // GET: Headers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Header == null)
            {
                return NotFound();
            }

            var header = await _context.Header
                .FirstOrDefaultAsync(m => m.Id == id);
            if (header == null)
            {
                return NotFound();
            }

            return View(header);
        }

        // POST: Headers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Header == null)
            {
                return Problem("Entity set 'PropertySurveyServiceContext.Header'  is null.");
            }
            var header = await _context.Header.FindAsync(id);
            if (header != null)
            {
                _context.Header.Remove(header);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HeaderExists(int id)
        {
          return (_context.Header?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
