using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReceiveShared : ContentPage
	{
        protected class ImageFiles
        {
            public string image_filename { get; set; }  
            public string image_path { get; set; }
            public string image_contract { get; set; }

            public ImageFiles(string _image_filename, string _image_path, string _image_contract)
            {
                this.image_filename = _image_filename;
                this.image_path = _image_path;
                this.image_contract = _image_contract;
            }
        }

        protected StreamReader mrs_sr;
        protected string next_line;
        protected List<ImageFiles> images_list = new List<ImageFiles>();

        public ReceiveShared ()
		{
			InitializeComponent ();
		}

        public int YNtoInt(string yorn)
        {
            if (yorn.Length > 0)
            {
                switch (yorn[0])
                {
                    case 'Y': return 1;
                    case 'N': return 2;
                    default: return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public int decode_via_button_list(string value, List<string> button_list)
        {
            for (int index = 1; index < button_list.Count; index++)
                if (value == button_list[index])
                    return index;

            // Default value for not found. Also used for blank values.
            return 0;
        }

        private int parse_int(string s)
        {
            int result;
            if (int.TryParse(s, out result))
                return result;
            else
                return 0;
        }

        private float parse_float(string s)
        {
            float result;
            if (!float.TryParse(s, out result))
                return result;
            else
                return 0;
        }

        private int decode_lock(string value)
        {
            switch (value)
            {
                case "Deadlock": return 1;
                case "Hook": return 2;
                case "Latch": return 3;
                case "Cam": return 4;
                case "Mushroom": return 5;
                default: return 0;
            }
        }

        private int decode_NY_to_int(string value) // Y=1, Yes=1, N=0, No=0
        {
            if (value.Length > 0)
            {
                switch (value[0])
                {
                    case 'Y': return 1;
                    //case 'N': return 0;
                    default: return 0;
                }
            }
            else
                return 0;
        }

        protected void load_fitter_header()
        {
            bool finished = false;
            string key;
            string value;

            App.net.HeaderRecord = new Header();
            App.net.table_init.CreateHeader(); // Initialize
            next_line = mrs_sr.ReadLine();

            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "UDI_CONT"                : App.CurrentApp.HeaderRecord.udi_cont = value; break;
                        case "TYPEA"                   : App.CurrentApp.HeaderRecord.typeA = value; break;
                        case "TYPEB"                   : App.CurrentApp.HeaderRecord.typeB = value; break;
                        case "FIT_DIARY"               : App.CurrentApp.HeaderRecord.fit_diary = DateTime.Parse(value).ToString(); break;
                        case "FITTERS_INSTRUCTIONS"    : App.CurrentApp.HeaderRecord.fitters_instructions = value; break;
                        case "FIT_START"               : App.CurrentApp.HeaderRecord.fit_start = App.CurrentApp.HeaderRecord.udi_start = DateTime.Parse (value).ToString(); break;
                        case "FIT_END"                 : App.CurrentApp.HeaderRecord.fit_fin = App.CurrentApp.HeaderRecord.udi_fin = DateTime.Parse (value).ToString(); break;
                        case "ADDR"                    : App.CurrentApp.HeaderRecord.add_long = value; break;
                        case "ADD1"                    : App.CurrentApp.HeaderRecord.uc_add1 = value; break;
                        case "ADD2"                    : App.CurrentApp.HeaderRecord.uc_add2 = value; break;
                        case "ADD3"                    : App.CurrentApp.HeaderRecord.uc_add3 = value; break;
                        case "UN_INCEDEN"              : App.CurrentApp.HeaderRecord.uc_inceden = value; break;
                        case "UC_DESC"                 : App.CurrentApp.HeaderRecord.uc_desc = value; break;
                        case "UC_EXCESS"               : App.CurrentApp.HeaderRecord.uc_excess = parse_float (value); break;
                        case "SI_DONE"                 : App.CurrentApp.HeaderRecord.si_done = value == "Y"; break;
                        case "UC_NAME"                 : App.CurrentApp.HeaderRecord.uc_name = value; break;
                        case "UC_POSTCODE"             : App.CurrentApp.HeaderRecord.uc_postcode = value; break;
                        case "H_PHONE"                 : App.CurrentApp.HeaderRecord.uc_h_phone = value; break;
                        case "W_PHONE"                 : App.CurrentApp.HeaderRecord.uc_h_phone2 = value; break;
                        case "M_PHONE"                 : App.CurrentApp.HeaderRecord.uc_h_phone3 = value; break;
                        case "SI_MPAY"                 : App.CurrentApp.HeaderRecord.si_mpay = value; break;
                        case "UDI_DATE"                : App.CurrentApp.HeaderRecord.udi_date = DateTime.Parse (value).ToString(); break;
                        case "SN_NAME"                 : App.CurrentApp.HeaderRecord.sn_name = value; break;
                        case "LA_NAME"                 : App.CurrentApp.HeaderRecord.uc_laname = value; break;
                        case "CARDCHEQ"                : App.CurrentApp.HeaderRecord.card_cheq = value; break;
                        case "EXPIRY"                  : App.CurrentApp.HeaderRecord.expiry = value; break;
                        case "SURVEYOR_REPORT"         : App.CurrentApp.HeaderRecord.rep_text = value; break;
                        case "JOB_GRADE"               : App.CurrentApp.HeaderRecord.job_grade = value; break;
                        case "ACC_TEXT"                : App.CurrentApp.HeaderRecord.acc_text = value; break;
                        case "SUMMARY_TEXT"            : App.CurrentApp.HeaderRecord.summ_text = value; break;
                        case "SUMMARY_TEXT2"           : App.CurrentApp.HeaderRecord.summ_text += value; break;
                        case "CODE_TEXT"               : App.CurrentApp.HeaderRecord.code_text = value; break;
                        case "CODE_TEXT2"              : App.CurrentApp.HeaderRecord.code_text += value; break;
                        case "PHOTO"                   : App.CurrentApp.HeaderRecord.photo = YNtoInt (value); break;
                        case "BOOKED"                  : App.CurrentApp.HeaderRecord.booked = YNtoInt (value); break;
                        case "EASY_PARKING"            : App.CurrentApp.HeaderRecord.easy_park = YNtoInt (value); break;
                        case "REAR_ACCESS"             : App.CurrentApp.HeaderRecord.access_rear = YNtoInt (value); break;
                        case "ALARM_CONT"              : App.CurrentApp.HeaderRecord.alarm_cont = YNtoInt (value); break;
                        case "LADDER_REQUIRED"         : App.CurrentApp.HeaderRecord.ladder_req = YNtoInt (value); break;
                        case "HEIGHT_RESTRICTION"      : App.CurrentApp.HeaderRecord.height_res = YNtoInt (value); break;
                        case "OBSTRUCTIVE_WIRES"       : App.CurrentApp.HeaderRecord.obs_wires = YNtoInt (value); break;
                        case "OBSTRUCTIVE_WIRE_TEXT"   : App.CurrentApp.HeaderRecord.obs_wires_text = value; break;
                        case "SAND_AND_CEMENT"         : App.CurrentApp.HeaderRecord.sand_cemen = YNtoInt (value); break;
                        case "PLASTER"                 : App.CurrentApp.HeaderRecord.plaster = YNtoInt (value); break;
                        case "DOORBELL"                : App.CurrentApp.HeaderRecord.doorbell = YNtoInt (value); break;
                        case "LOOSE_BRICK"             : App.CurrentApp.HeaderRecord.loose_brick = YNtoInt (value); break;
                        case "LOOSE_BRICK_TEXT"        : App.CurrentApp.HeaderRecord.loose_brick_text = value; break;
                        case "GENERATOR_REQUIRED"      : App.CurrentApp.HeaderRecord.genreq = YNtoInt (value); break;
                        case "ARCHITRAVES_REQUIRED"    : App.CurrentApp.HeaderRecord.architreq = YNtoInt (value); break;
                        case "ACRO_PROP_REQUIRED"      : App.CurrentApp.HeaderRecord.acroreq = YNtoInt (value); break;
                        case "JOB_SIZE"                : App.CurrentApp.HeaderRecord.njs = value; break;
                        case "SURVEYORS_NAME"          : App.CurrentApp.HeaderRecord.nsn = value; break;
                        case "WORK_INSIDE"             : App.CurrentApp.HeaderRecord.bWorkInside = YNtoInt (value); break;
                        case "GROUND_SURFACE"          : App.CurrentApp.HeaderRecord.ground_surface = value; break;
                        case "TYPE_OF_EQUIPMENT"       : App.CurrentApp.HeaderRecord.type_of_equipment = value; break;
                        case "RISKS_AND_DANGERS"       : App.CurrentApp.HeaderRecord.risks_and_dangers = value; break;
                        case "STIME_ARRIVAL"           : App.CurrentApp.HeaderRecord.stimea = value; break;
                        case "NO_LADDERS"              : App.CurrentApp.HeaderRecord.no_ladders = YNtoInt (value); break;
                        case "ITEMS_ABOVE_ROOF"        : App.CurrentApp.HeaderRecord.items_above_roof = YNtoInt (value); break;
                        case "ASBESTOS_VISIBLE"        : App.CurrentApp.HeaderRecord.asbestos_visible = YNtoInt (value); break;
                        case "ASBESTOS_VISIBLE_TEXT"   : App.CurrentApp.HeaderRecord.asvizex = value; break;
                        case "SUBCONTRACT"             : App.CurrentApp.HeaderRecord.b_subcontract = YNtoInt (value); break;
                        case "SUBCONTRACT_TEXT"        : App.CurrentApp.HeaderRecord.subcontracttext = value; break;
                        case "SHOP_FRONT_WORK"         : App.CurrentApp.HeaderRecord.shop_front_work = YNtoInt (value); break;
                        case "WORK_ON_PUBLIC_FOOTPATH" : App.net.HeaderRecord.work_on_public_footpath = YNtoInt (value); break;
                        case "BARRIERS"                : App.net.HeaderRecord.fbunfinother = value; break;

                        case "TIME_TO_COMPLETE" :
                        {
                            switch (value)
                            {
                                case "01:00": App.CurrentApp.HeaderRecord.time_to_complete = "1 hour"; break;
                                case "01:30": App.CurrentApp.HeaderRecord.time_to_complete = "1 hours 30 minutes"; break;
                                case "02:00": App.CurrentApp.HeaderRecord.time_to_complete = "2 hours"; break;
                                case "02:30": App.CurrentApp.HeaderRecord.time_to_complete = "2 hours 30 minutes"; break;
                                case "03:00": App.CurrentApp.HeaderRecord.time_to_complete = "3 hours"; break;
                                case "03:30": App.CurrentApp.HeaderRecord.time_to_complete = "3 hours 30 minutes"; break;
                                case "04:00": App.CurrentApp.HeaderRecord.time_to_complete = "4 hours"; break;
                                case "04:30": App.CurrentApp.HeaderRecord.time_to_complete = "4 hours 30 minutes"; break;
                                case "05:00": App.CurrentApp.HeaderRecord.time_to_complete = "5 hours"; break;
                                case "05:30": App.CurrentApp.HeaderRecord.time_to_complete = "5 hours 30 minutes"; break;
                                case "06:00": App.CurrentApp.HeaderRecord.time_to_complete = "6 hours"; break;
                                case "06:30": App.CurrentApp.HeaderRecord.time_to_complete = "6 hours 30 minutes"; break;
                                case "07:00": App.CurrentApp.HeaderRecord.time_to_complete = "7 hours"; break;
                                case "07:30": App.CurrentApp.HeaderRecord.time_to_complete = "7 hours 30 minutes"; break;
                                case "08:00": App.CurrentApp.HeaderRecord.time_to_complete = "8 hours"; break;
                            }
                            break;
                        }

                        case "REASON_EXCESS_NOTCOL" :
                        {
                            App.CurrentApp.HeaderRecord.reason_excess_not_collected = value;
                            if (value == "")
                                App.CurrentApp.HeaderRecord.bExcessCollected = 2;
                            else
                                App.CurrentApp.HeaderRecord.bExcessCollected = 1;
                            break;
                        }

                        case "INSTALLATION_HEIGHT" :
                        {
                            App.CurrentApp.HeaderRecord.inst_height = value;
                            if (value == "")
                                App.CurrentApp.HeaderRecord.work_at_height = 2;
                            else
                                App.CurrentApp.HeaderRecord.work_at_height = 1;

                            break;
                        }

                        case "UC_TLIGHT" :
                        {
                            switch (value)
                            {
                                //case "3": this wants udi_tlight=0, so let default handle it
                                case "2": App.CurrentApp.HeaderRecord.udi_tlight = 1; break;
                                case "1": App.CurrentApp.HeaderRecord.udi_tlight = 2; break;
                                default : App.CurrentApp.HeaderRecord.udi_tlight = 0; break;
                            }
                            break;
                        }
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            App.CurrentApp.HeaderRecord.iRecordType = 1;
            App.CurrentApp.HeaderRecord.front_house_photos = 1;
            App.CurrentApp.HeaderRecord.bSurvey = true;
        }

        protected void load_aluminium()
        {
            bool finished = false;
            string key;
            string value;
            string lead_thickness = "";
            string georgian_bar = "";

            App.net.table_init.CreateAlum();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT": App.CurrentApp.AlumRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE": App.CurrentApp.AlumRecord.item_number = parse_int(value); break;
                        case "LEADCOMMENT" : App.CurrentApp.AlumRecord.lead_comments = value; break;
                        case "URCOSDAM" : App.CurrentApp.AlumRecord.cosmetic_damage = YNtoInt(value); break;
                        case "URADDLOCK" : App.CurrentApp.AlumRecord.additional_locks = value; break;
                        case "URGASKET" : App.CurrentApp.AlumRecord.gaskets = YNtoInt(value); break;
                        case "URGASEDIT" : App.CurrentApp.AlumRecord.gaskets_text = value; break;
                        case "URHANDLES" : App.CurrentApp.AlumRecord.handles_req = YNtoInt(value); break;
                        case "URHANDEDIT" : App.CurrentApp.AlumRecord.handles_text = value; break;
                        case "PARTS_ORDER": App.CurrentApp.AlumRecord.parts_to_order = value; break;
                        case "URREPPAN" : App.CurrentApp.AlumRecord.replace_panel = YNtoInt(value); break;
                        case "ADAMCH" : App.CurrentApp.AlumRecord.cause_of_damage = value; break;
                        case "AITEMCH" : App.CurrentApp.AlumRecord.type = value; break;
                        case "ASECCH" : App.CurrentApp.AlumRecord.section_type = decode_via_button_list(value, SurveyFitterButtonLists.aluminium_section_type_list); break;
                        case "ATIMBSUB" : App.CurrentApp.AlumRecord.new_timber_sub_frame = YNtoInt(value); break;
                        case "ASUBDEDIT" : App.CurrentApp.AlumRecord.sub_frame_depth = value; break;
                        case "ATIMSZWEDI" : App.CurrentApp.AlumRecord.item_frame_width = value; break;
                        case "ARRCH" : App.CurrentApp.AlumRecord.bRepair = value == "Repair"; break;
                        case "COL_COPY" : App.CurrentApp.AlumRecord.collect_and_copy = YNtoInt (value); break;
                        case "TEMP_DOOR" : App.CurrentApp.AlumRecord.temporary = decode_via_button_list(value, SurveyFitterButtonLists.shared_temporary_button_list); break;
                        case "GLAZED" : App.CurrentApp.AlumRecord.glazed = decode_via_button_list(value, SurveyFitterButtonLists.aluminium_glazed_button_list); break;
                        case "BEADTYPE" : App.CurrentApp.AlumRecord.bead_type = decode_via_button_list(value, SurveyFitterButtonLists.aluminium_bead_type_list); break;
                        case "OSWIDTH" : App.CurrentApp.AlumRecord.outer_section_width = value; break;
                        case "OSHEIGHT" : App.CurrentApp.AlumRecord.outer_section_height = value; break;
                        case "ATIMSZHEDI" : App.CurrentApp.AlumRecord.item_frame_height = value; break;
                        case "ABRKSZWEDI" : App.CurrentApp.AlumRecord.brick_width = value; break;
                        case "ABRKSZHEDI" : App.CurrentApp.AlumRecord.brick_height = value; break;
                        case "AINTWEDIT" : App.CurrentApp.AlumRecord.internal_width = value; break;
                        case "AINTHEDIT" : App.CurrentApp.AlumRecord.internal_height = value; break;
                        case "ACILL" : App.CurrentApp.AlumRecord.cill = YNtoInt(value); break;
                        case "ADRIP" : App.CurrentApp.AlumRecord.drip = YNtoInt(value); break;
                        case "ANVENT" : App.CurrentApp.AlumRecord.night_vent = decode_via_button_list(value, SurveyFitterButtonLists.aluminium_night_vent_list); break;
                        case "AMIDTYPE" : App.CurrentApp.AlumRecord.midrail_type = value; break;
                        case "AITEMCOL" : App.CurrentApp.AlumRecord.item_color = value; break;
                        case "ALOCK" : App.CurrentApp.AlumRecord.locking_type = value; break;
                        case "ALETTER" : App.CurrentApp.AlumRecord.letter_box = value; break;
                        case "AOPENS" : App.CurrentApp.AlumRecord.opens = decode_via_button_list(value, SurveyFitterButtonLists.shared_in_out_button_list); break;
                        case "AHCOLOUR" : App.CurrentApp.AlumRecord.handle_color = value; break;
                        case "ASPACTHIC" : App.CurrentApp.AlumRecord.spacer_thickness = value; break;
                        case "ASPACCOLO" : App.CurrentApp.AlumRecord.spacer_color = value; break;
                        case "AGLATYPE" : App.CurrentApp.AlumRecord.glass_type = value; break;
                        case "AGLAPATT" : App.CurrentApp.AlumRecord.glass_pattern = value; break;
                        case "ASPEGLAS" : App.CurrentApp.AlumRecord.special_glass = value; break;
                        case "ANSFC" : App.CurrentApp.AlumRecord.sub_frame_color = value; break;
                        case "ANFTY" : App.CurrentApp.AlumRecord.frame_type = decode_via_button_list(value, SurveyFitterButtonLists.aluminium_frame_type_list); break;
                        case "ASPTEXT" : App.CurrentApp.AlumRecord.long_comments = value; break;
                        case "SIZEA" : App.CurrentApp.AlumRecord.lead_sizeA = parse_int(value); break;
                        case "SIZEB" : App.CurrentApp.AlumRecord.lead_sizeB = parse_int(value); break;
                        case "SIZEC" : App.CurrentApp.AlumRecord.lead_sizeC = parse_int(value); break;
                        case "SIZED" : App.CurrentApp.AlumRecord.lead_sizeD = parse_int(value); break;
                        case "LEADWIDTH" : App.CurrentApp.AlumRecord.lead_CWidthf = parse_float (value); break;
                        case "LEADHEIGHT" : App.CurrentApp.AlumRecord.lead_CHeightf = parse_float (value); break;
                        case "ANTIRATTLE" : App.CurrentApp.AlumRecord.lead_anti_rattle = decode_via_button_list(value, georgian_bar_logic.anti_rattle); break;
                        case "LEADSINGDOUB" : App.CurrentApp.AlumRecord.lead_sod = value; break;
                        case "TYPEOFLEAD" : App.CurrentApp.AlumRecord.lead_type = value; break;
                        case "LETBOXPOS" : App.CurrentApp.AlumRecord.letter_box_pos = value; break;
                        case "PETFLAP" : App.CurrentApp.AlumRecord.pet_flap = value; break;
                        case "PETTYPE" : App.CurrentApp.AlumRecord.pet_type = value; break;
                        case "NOOFLOCKS" : App.CurrentApp.AlumRecord.l_num = parse_int(value); break;
                        case "LOCKDIST1" : App.CurrentApp.AlumRecord.l_size1 = value; break;
                        case "LOCKDIST2" : App.CurrentApp.AlumRecord.l_size2 = value; break;
                        case "LOCKDISTA" : App.CurrentApp.AlumRecord.l_sizeA = value; break;
                        case "LOCKDISTB" : App.CurrentApp.AlumRecord.l_sizeB = value; break;
                        case "LOCKDISTC" : App.CurrentApp.AlumRecord.l_sizeC = value; break;
                        case "LOCKDISTD" : App.CurrentApp.AlumRecord.l_sizeD = value; break;
                        case "LOCKDISTE" : App.CurrentApp.AlumRecord.l_sizeE = value; break;
                        case "LOCKDISTF" : App.CurrentApp.AlumRecord.l_sizeF = value; break;
                        case "LOCKDISTG" : App.CurrentApp.AlumRecord.l_sizeG = value; break;
                        case "LOCKTYPE1" : App.CurrentApp.AlumRecord.l_itype1 = decode_lock(value); break;
                        case "LOCKTYPE2" : App.CurrentApp.AlumRecord.l_itype2 = decode_lock(value); break;
                        case "LOCKTYPE3" : App.CurrentApp.AlumRecord.l_itype3 = decode_lock(value); break;
                        case "LOCKTYPE4" : App.CurrentApp.AlumRecord.l_itype4 = decode_lock(value); break;
                        case "LOCKTYPE5" : App.CurrentApp.AlumRecord.l_itype5 = decode_lock(value); break;
                        case "LOCKTYPE6" : App.CurrentApp.AlumRecord.l_itype6 = decode_lock(value); break;
                        case "LOCKTYPE7" : App.CurrentApp.AlumRecord.l_itype7 = decode_lock(value); break;
                        case "PICDIST1" : App.CurrentApp.AlumRecord.l_fpos1 = parse_float (value) * 2; break;
                        case "PICDIST2" : App.CurrentApp.AlumRecord.l_fpos2 = parse_float (value) * 2; break;
                        case "PICDIST3" : App.CurrentApp.AlumRecord.l_fpos3 = parse_float (value) * 2; break;
                        case "PICDIST4" : App.CurrentApp.AlumRecord.l_fpos4 = parse_float (value) * 2; break;
                        case "PICDIST5" : App.CurrentApp.AlumRecord.l_fpos5 = parse_float (value) * 2; break;
                        case "PICDIST6" : App.CurrentApp.AlumRecord.l_fpos6 = parse_float (value) * 2; break;
                        case "PICDIST7" : App.CurrentApp.AlumRecord.l_fpos7 = parse_float (value) * 2; break;
                        case "MECHDIST" : App.CurrentApp.AlumRecord.lock_position = parse_float(value) * 2; break;
                        case "LEFTBOLT" : App.CurrentApp.AlumRecord.left_bolt = decode_NY_to_int(value); break;
                        case "RIGHTBOLT" : App.CurrentApp.AlumRecord.right_bolt = decode_NY_to_int(value); break;
                        case "GEARBOX" : App.CurrentApp.AlumRecord.GearBox = decode_via_button_list(value, window_lock_logic.gearbox_list); break;
                        case "LOCKMAKE" : App.CurrentApp.AlumRecord.lock_make = value; break;
                        case "LOCKCODES" : App.CurrentApp.AlumRecord.lock_codes = value; break;
                        case "MRHEIGHT" : App.CurrentApp.AlumRecord.midrail_height = value; break;
                        case "QDOCL" : App.CurrentApp.AlumRecord.docl = value; break;
                        case "REPLACREAS" : App.CurrentApp.AlumRecord.replace_reason = value; break;
                        case "WHYNOREPAIR" : App.CurrentApp.AlumRecord.replace_explain = value; break;
                        case "ROOMLOCATION" : App.CurrentApp.AlumRecord.room_location = value; break;
                        case "LPHANDLES" : App.CurrentApp.AlumRecord.LPHandles = decode_via_button_list(value, SurveyFitterButtonLists.shared_handle_operation_list); break;
                        case "THRESHOLD" : App.CurrentApp.AlumRecord.threshold_type = value; break;
                        case "FENCERRATE" : App.CurrentApp.AlumRecord. FecerRating = value; break;
                        case "PETMAGNET" : App.CurrentApp.AlumRecord.pet_magnetic = decode_via_button_list(value, MartControls.pet_flap_logic.magnetic_list); break;
                        case "CONSUBFRME" : App.CurrentApp.AlumRecord.cill_on_subframe = decode_NY_to_int(value); break;
                        case "CONSUBFRTY" : App.CurrentApp.AlumRecord.cill_type = decode_via_button_list(value, SurveyFitterButtonLists.aluminium_cill_type_list); break;
                        case "ISFLAT" : App.CurrentApp.AlumRecord.is_a_flat = decode_NY_to_int(value); break;
                        case "POINTENTER" : App.CurrentApp.AlumRecord.point_of_entry = value; break;
                        case "WASLOCKED" : App.CurrentApp.AlumRecord.was_it_locked = decode_NY_to_int(value); break;
                        case "LOCKREQU" : App.CurrentApp.AlumRecord.type_of_lockng_system_required = value; break;
                        case "BBSPACERWID" : App.CurrentApp.AlumRecord.back_to_back_spacer_width = value; break;
                        case "BBOVERALWID" : App.CurrentApp.AlumRecord.back_to_back_spacer_height = value; break;
                        case "REPLACEGLASS" : App.CurrentApp.AlumRecord.replace_glass = YNtoInt(value); break;
                        case "LEADTHICK" : lead_thickness = value; break;
                        case "GEORGBAR" : georgian_bar = value; break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            if (App.CurrentApp.AlumRecord.l_size1 != "") // Is this correct? What was in old system was definitely wrong.
                App.CurrentApp.AlumRecord.bNewLockingMech = 1;
            else
                App.CurrentApp.AlumRecord.bNewLockingMech = 2;

            if (App.CurrentApp.AlumRecord.special_glass == "Georgian Bar")
                App.CurrentApp.AlumRecord.lead_thickness = georgian_bar;
            else
                App.CurrentApp.AlumRecord.lead_thickness = lead_thickness;

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.AlumRecord.item_number);
            App.CurrentApp.AlumRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.AlumRecord.item_number);
            App.CurrentApp.AlumRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.AlumRecord.item_number);
            App.CurrentApp.AlumRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetAlumByContractItemNo(App.CurrentApp.AlumRecord.udi_cont, App.CurrentApp.AlumRecord.item_number) == null)
                App.data.SaveAlum();
        }

        protected void load_bifold()
        {
            bool finished = false;
            string key;
            string value;

            App.net.table_init.CreateBifold();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT": App.CurrentApp.BifoldRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE": App.CurrentApp.BifoldRecord.item_number = parse_int(value); break;
                        case "COSDAM": App.CurrentApp.BifoldRecord.cause_of_damage = value; break;
                        case "DOORTYPE": App.CurrentApp.BifoldRecord.door_type = value; break;
                        case "HARDWARE" : App.CurrentApp.BifoldRecord.hardware = value; break;
                        case "COLINT": App.CurrentApp.BifoldRecord.color_internal = value; break;
                        case "COLEXT": App.CurrentApp.BifoldRecord.color_external = value; break;
                        case "CILLTYPE": App.CurrentApp.BifoldRecord.threshold_type = value; break;
                        case "NUMDOORS": App.CurrentApp.BifoldRecord.number_of_doors = parse_int(value); break;
                        case "NUMDOORSTXT": App.CurrentApp.BifoldRecord.number_of_doors_text = value; break;
                        case "GLAZEOPTS": App.CurrentApp.BifoldRecord.glazing_options = value; break;
                        case "OWIDTH": App.CurrentApp.BifoldRecord.overall_width = value; break;
                        case "OHEIGHT": App.CurrentApp.BifoldRecord.overall_height = value; break;
                        case "IWIDTH": App.CurrentApp.BifoldRecord.internal_width = value; break;
                        case "IHEIGHT": App.CurrentApp.BifoldRecord.internal_height = value; break;
                        case "DOORCOLOR": App.CurrentApp.BifoldRecord.colour_of_doors = value; break;
                        case "INTCOLOR": App.CurrentApp.BifoldRecord.internal_door_colour = value; break;
                        case "HANDCOLOR": App.CurrentApp.BifoldRecord.handle_colour = value; break;
                        case "CILLREQ": App.CurrentApp.BifoldRecord.cill_type = value; break;
                        case "KNOCKON": App.CurrentApp.BifoldRecord.knock_on = value; break;
                        case "COMMENTS": App.CurrentApp.BifoldRecord.comments = value; break;
                        case "PARTS_ORDER" : App.CurrentApp.BifoldRecord.parts_to_order = value; break;
                        case "OPENS": App.CurrentApp.BifoldRecord.opens = decode_via_button_list(value, SurveyFitterButtonLists.shared_in_out_button_list); break;
                        case "TVENTS": App.CurrentApp.BifoldRecord.trickle_vents = YNtoInt(value); break;
                        case "REPPEPL" : App.CurrentApp.BifoldRecord.bRepair = value == "Repair"; break;
                        case "REPLACEGLASS": App.CurrentApp.BifoldRecord.replace_glass = YNtoInt(value); break;
                        case "REASNOREPAIR": App.CurrentApp.BifoldRecord.reason_not_repaired = value; break;
                        case "HANDLES_REQ": App.CurrentApp.BifoldRecord.handles_req = YNtoInt(value); break;
                        case "HANDLES_TEXT": App.CurrentApp.BifoldRecord.handles_text = value; break;
                        case "FENSA": App.CurrentApp.BifoldRecord.fensa = value == "Y"; break;
                        case "WER_RATING": App.CurrentApp.BifoldRecord.WER_rating = value; break;
                        case "GASKETS": App.CurrentApp.BifoldRecord.gaskets = decode_via_button_list(value, SurveyFitterButtonLists.shared_gasket_button_list); break;
                        case "GASKETS_TEXT" : App.CurrentApp.BifoldRecord.gaskets_text = value; break;
                        case "ADDONS"       : App.CurrentApp.BifoldRecord.addons = YNtoInt(value); break;
                        case "ADDON_WIDTH"  : App.CurrentApp.BifoldRecord.addon_width = value; break;
                        case "ADDON_HEIGHT" : App.CurrentApp.BifoldRecord.addon_height = value; break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.BifoldRecord.item_number);
            App.CurrentApp.BifoldRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.BifoldRecord.item_number);
            App.CurrentApp.BifoldRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.BifoldRecord.item_number);
            App.CurrentApp.BifoldRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetBifoldByContractItemNo(App.CurrentApp.BifoldRecord.udi_cont, App.CurrentApp.BifoldRecord.item_number) == null)
                App.data.SaveBifold();
        }

        protected void load_composite()
        {
            bool finished = false;
            string key;
            string value;

            App.net.table_init.CreateComposite();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT": App.CurrentApp.CompRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE": App.CurrentApp.CompRecord.item_number = parse_int (value); break;
                        case "CosDam": App.CurrentApp.CompRecord.cause_of_damage = value; break;
                        case "DOORMAKE": App.CurrentApp.CompRecord.door_make = value; break;
                        case "LEAD_COMMENT" : App.CurrentApp.CompRecord.lead_comments = value; break;
                        case "PARTS_ORDER" : App.CurrentApp.CompRecord.parts_to_order = value; break;
                        case "LOCK": App.CurrentApp.CompRecord.is_lock = decode_via_button_list (value, SurveyFitterButtonLists.composite_is_lock_button_list); break;
                        case "OPENS": App.CurrentApp.CompRecord.opens = decode_via_button_list (value, SurveyFitterButtonLists.shared_in_out_button_list); break;
                        case "TVENTS": App.CurrentApp.CompRecord.trickle_vents = value; break;
                        case "FCOLINS": App.CurrentApp.CompRecord.frame_colour_inside = value; break;
                        case "FCOLOUTS": App.CurrentApp.CompRecord.frame_colour_outside = value; break;
                        case "DCOLINS": App.CurrentApp.CompRecord.door_colour_inside = value; break;
                        case "DCOLOUTS": App.CurrentApp.CompRecord.door_colour_outside = value; break;
                        case "DOORDES": App.CurrentApp.CompRecord.door_design = value; break;
                        case "GLASSDES": App.CurrentApp.CompRecord.glass_design = value; break;
                        case "INTWIDTH": App.CurrentApp.CompRecord.internal_width = value; break;
                        case "INTHEIGHT": App.CurrentApp.CompRecord.internal_height = value; break;
                        case "BRWIDTH": App.CurrentApp.CompRecord.brick_width = value; break;
                        case "BRHEIGHT": App.CurrentApp.CompRecord.brick_height = value; break;
                        case "LPHANDLES" : App.CurrentApp.CompRecord.lever_pad_handles = decode_via_button_list (value, SurveyFitterButtonLists.shared_handle_operation_list); break;
                        case "ADDONS": App.CurrentApp.CompRecord.addons = YNtoInt(value); break;
                        case "ADDONSW": App.CurrentApp.CompRecord.addons_width = value; break;
                        case "ADDONSH": App.CurrentApp.CompRecord.addons_height = value; break;
                        case "HINGEDON" : App.CurrentApp.CompRecord.hinged_on = decode_via_button_list (value, SurveyFitterButtonLists.composite_hinged_on_button_list); break;
                        case "GPATTERN": App.CurrentApp.CompRecord.glass_pattern = value; break;
                        case "GTYPE": App.CurrentApp.CompRecord.glass_type = value; break;
                        case "SPTHICK": App.CurrentApp.CompRecord.spacer_thickness = value; break;
                        case "SPCOLOUR": App.CurrentApp.CompRecord.spacer_colour = value; break;
                        case "PTYPE": App.CurrentApp.CompRecord.profile_type = decode_via_button_list (value, SurveyFitterButtonLists.shortened_profile_type_button_list); break;
                        case "RLOCATION": App.CurrentApp.CompRecord.room_location = value; break;
                        case "LOCKOTHER": App.CurrentApp.CompRecord.lock_other_text = value; break;
                        case "SPECIALGLASS" : App.CurrentApp.CompRecord.special_glass = value; break;
                        case "DOCL": App.CurrentApp.CompRecord.docl = value; break;
                        case "LETBOX": App.CurrentApp.CompRecord.letteredit = value; break;
                        case "LETBOXPOS": App.CurrentApp.CompRecord.letter_box_pos = value; break;
                        case "PETFLAP": App.CurrentApp.CompRecord.pet_flap = value; break;
                        case "PETTYPE": App.CurrentApp.CompRecord.pet_type = value; break;
                        case "PETMAGNET" : App.CurrentApp.CompRecord.pet_magnetic = decode_via_button_list (value, MartControls.pet_flap_logic.magnetic_list); break;
                        case "GLAZE": App.CurrentApp.CompRecord.glaze = decode_via_button_list(value, SurveyFitterButtonLists.shared_internal_external_button_list); break;
                        case "SINGORDOUBLE" : App.CurrentApp.CompRecord.lead_sod = value; break;
                        case "LEADTHICK" : App.CurrentApp.CompRecord.lead_thickness = value; break;
                        case "TYPEOFLEAD" : App.CurrentApp.CompRecord.lead_type = value; break;
                        case "ANTIRATTLE" : App.CurrentApp.CompRecord.lead_anti_rattle = decode_via_button_list (value, georgian_bar_logic.anti_rattle); break;
                        case "COMMENTS": App.CurrentApp.CompRecord.comments = value; break;
                        case "LEADWIDTH": App.CurrentApp.CompRecord.lead_CWidth = parse_int(value); break;
                        case "LEADHEIGHT": App.CurrentApp.CompRecord.lead_CHeight = parse_int(value); break;
                        case "CILLS": App.CurrentApp.CompRecord.cills = value; break;
                        case "TTYPE": App.CurrentApp.CompRecord.threshold_type = value; break;
                        case "HCOLOUR": App.CurrentApp.CompRecord.handle_colour = value; break;
                        case "REPL_REAS" : App.CurrentApp.CompRecord.reason_not_repaired = value; break;
                        case "ISFLAT" : App.CurrentApp.CompRecord.is_a_flat = YNtoInt (value); break;
                        case "POINTENTER" : App.CurrentApp.CompRecord.point_of_entry = value; break;
                        case "WASLOCKED" : App.CurrentApp.CompRecord.was_it_locked = YNtoInt (value); break;
                        case "LOCKREQU" : App.CurrentApp.CompRecord.type_of_lockng_system_required = value; break;
                        case "REPAREPL" : App.CurrentApp.CompRecord.bRepair = value == "Repair"; break;
                        case "REPLACEGLASS": App.CurrentApp.CompRecord.replace_glass = YNtoInt(value); break;
                        case "HANDLES_REQ": App.CurrentApp.CompRecord.handles_req = YNtoInt(value); break;
                        case "HANDLES_TEXT": App.CurrentApp.CompRecord.handles_text = value; break;
                        case "FENSA": App.CurrentApp.CompRecord.fensa = value == "Y"; break;
                        case "WER_RATING": App.CurrentApp.CompRecord.WER_rating = value; break;
                        case "GASKETS": App.CurrentApp.CompRecord.gaskets = decode_via_button_list(value, SurveyFitterButtonLists.shared_gasket_button_list); break;
                        case "GASKETS_TEXT": App.CurrentApp.CompRecord.gaskets_text = value; break;
                        case "FIRE_DOOR" : App.CurrentApp.CompRecord.fire_door = YNtoInt (value); break;
                        case "HEADDRIP": App.CurrentApp.CompRecord.head_drip = YNtoInt (value); break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.CompRecord.item_number);
            App.CurrentApp.CompRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.CompRecord.item_number);
            App.CurrentApp.CompRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.CompRecord.item_number);
            App.CurrentApp.CompRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetCompByContractItemNo(App.CurrentApp.CompRecord.udi_cont, App.CurrentApp.CompRecord.item_number) == null)
                App.data.SaveComp();
        }

        protected void load_conservatory()
        {
            bool finished = false;
            string key;
            string value;

            App.CurrentApp.table_init.CreateConservatory();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT": App.CurrentApp.ConsRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE": App.CurrentApp.ConsRecord.item_number = parse_int(value); break;
                        case "CDAMCH": App.CurrentApp.ConsRecord.cause_of_damage = value; break;
                        case "CTYPEROOF" : App.CurrentApp.ConsRecord.type = value; break;
                        case "PARTS_ORDER" : App.CurrentApp.ConsRecord.parts_to_order = value; break;
                        case "CMATTYPE": App.CurrentApp.ConsRecord.material_type = decode_via_button_list (value, SurveyFitterButtonLists.conservatory_material_type_list); break;
                        case "CSIZEA": App.CurrentApp.ConsRecord.sizeA = value; break;
                        case "CSIZEB": App.CurrentApp.ConsRecord.sizeB = value; break;
                        case "CSIZEC": App.CurrentApp.ConsRecord.sizeC = value; break;
                        case "CSIZED": App.CurrentApp.ConsRecord.sizeD = value; break;
                        case "CSIZEE": App.CurrentApp.ConsRecord.sizeE = value; break;
                        case "CSIZEF": App.CurrentApp.ConsRecord.sizeF = value; break;
                        case "CSIZEG": App.CurrentApp.ConsRecord.sizeG = value; break;
                        case "CANGLE1": App.CurrentApp.ConsRecord.angle1 = value; break;
                        case "CANGLE2": App.CurrentApp.ConsRecord.angle2 = value; break;
                        case "CANGLE3": App.CurrentApp.ConsRecord.angle3 = value; break;
                        case "CANGLE4": App.CurrentApp.ConsRecord.angle4 = value; break;
                        case "CPITCHHEDI": App.CurrentApp.ConsRecord.pitch_height = value; break;
                        case "CPSECEDIT": App.CurrentApp.ConsRecord.profile_section_size = value; break;
                        case "CWID1EDIT": App.CurrentApp.ConsRecord.sheet_width_1 = value; break;
                        case "CHEI1EDIT": App.CurrentApp.ConsRecord.sheet_height_1 = value; break;
                        case "CWID2EDIT": App.CurrentApp.ConsRecord.sheet_width_2 = value; break;
                        case "CHEI2EDIT": App.CurrentApp.ConsRecord.sheet_height_2 = value; break;
                        case "CWID3EDIT": App.CurrentApp.ConsRecord.sheet_width_3 = value; break;
                        case "CHEI3EDIT": App.CurrentApp.ConsRecord.sheet_height_3 = value; break;
                        case "CWID4EDIT": App.CurrentApp.ConsRecord.sheet_width_4 = value; break;
                        case "CHEI4EDIT": App.CurrentApp.ConsRecord.sheet_height_4 = value; break;
                        case "CWID5EDIT": App.CurrentApp.ConsRecord.sheet_width_5 = value; break;
                        case "CHEI5EDIT": App.CurrentApp.ConsRecord.sheet_height_5 = value; break;
                        case "CWID6EDIT": App.CurrentApp.ConsRecord.sheet_width_6 = value; break;
                        case "CHEI6EDIT": App.CurrentApp.ConsRecord.sheet_height_6 = value; break;
                        case "CWID7EDIT": App.CurrentApp.ConsRecord.sheet_width_7 = value; break;
                        case "CHEI7EDIT": App.CurrentApp.ConsRecord.sheet_height_7 = value; break;
                        case "CWID8EDIT": App.CurrentApp.ConsRecord.sheet_width_8 = value; break;
                        case "CHEI8EDIT": App.CurrentApp.ConsRecord.sheet_height_8 = value; break;
                        case "CWID9EDIT": App.CurrentApp.ConsRecord.sheet_width_9 = value; break;
                        case "CHEI9EDIT": App.CurrentApp.ConsRecord.sheet_height_9 = value; break;
                        case "CWID10EDIT": App.CurrentApp.ConsRecord.sheet_width_10 = value; break;
                        case "CHEI10EDIT": App.CurrentApp.ConsRecord.sheet_height_10 = value; break;
                        case "CRGLAZTH": App.CurrentApp.ConsRecord.roof_glazing_thickness = value; break;
                        case "CCOLOUR": App.CurrentApp.ConsRecord.color = value; break;
                        case "CRCOLOUR": App.CurrentApp.ConsRecord.roof_color = value; break;
                        case "CFIRRING": App.CurrentApp.ConsRecord.new_firrings_req = YNtoInt(value); break;
                        case "CGUTTER": App.CurrentApp.ConsRecord.new_gutters_req = YNtoInt(value); break;
                        case "CSPTEXT": App.CurrentApp.ConsRecord.long_comments = value; break;
                        case "RIDGE_LENGTH" : App.CurrentApp.ConsRecord.ridge_length = value; break;
                        case "FLUTESIZE": App.CurrentApp.ConsRecord.flute_size = value; break;
                        case "WALLPOS": App.CurrentApp.ConsRecord.wall_pos = value; break;
                        case "PITCHDEGREE": App.CurrentApp.ConsRecord.pitch_degree = value; break;
                        case "BDRAWONLY": App.CurrentApp.ConsRecord.bDrawingsOnly = YNtoInt(value); break;
                        case "SPARS_LINE" : App.CurrentApp.ConsRecord.spars_line_up = YNtoInt (value); break;
                        case "POINTENTER" : App.CurrentApp.ConsRecord.point_of_entry = value; break;
                        case "WASLOCKED" : App.CurrentApp.ConsRecord.was_it_locked = YNtoInt (value); break;
                        case "REPLACORREPR" : App.CurrentApp.ConsRecord.bRepair = value == "Repair"; break;
                        case "LOCKREQU" : App.CurrentApp.ConsRecord.type_of_lockng_system_required = value; break;
                        case "REPLACEGLASS": App.CurrentApp.ConsRecord.replace_glass = YNtoInt(value); break;
                        case "REASNOREPAIR": App.CurrentApp.ConsRecord.reason_not_repaired = value; break;
                        case "FENSA": App.CurrentApp.ConsRecord.fensa = value == "Y"; break;
                        case "WER_RATING": App.CurrentApp.ConsRecord.WER_rating = value; break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.ConsRecord.item_number);
            App.CurrentApp.ConsRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.ConsRecord.item_number);
            App.CurrentApp.ConsRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.ConsRecord.item_number);
            App.CurrentApp.ConsRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetConsByContractItemNo(App.CurrentApp.ConsRecord.udi_cont, App.CurrentApp.ConsRecord.item_number) == null)
                App.data.SaveCons();
        }

        protected void load_garage()
        {
            bool finished = false;
            string key;
            string value;
            string new_subframe_req = "";

            App.net.table_init.CreateGarage();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT": App.CurrentApp.GarageRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE": App.CurrentApp.GarageRecord.item_number = parse_int(value); break;
                        case "EDAMEDIT": App.CurrentApp.GarageRecord.cause_of_damage = value; break;
                        case "EFIXTYPE": App.CurrentApp.GarageRecord.frame_fix_type = decode_via_button_list(value, SurveyFitterButtonLists.garage_frame_fix_type_list); break;
                        case "EINTIMSUB": App.CurrentApp.GarageRecord.door_fits_into = decode_via_button_list(value, SurveyFitterButtonLists.garage_subframe_colour_list); break;
                        case "ENEWTIMSUB": new_subframe_req = value; break;
                        case "EACTWEDIT": App.CurrentApp.GarageRecord.actual_door_width = value; break;
                        case "EACTHEDIT": App.CurrentApp.GarageRecord.actual_door_height = value; break;
                        case "ECOLOUR": App.CurrentApp.GarageRecord.color = value; break;
                        case "EFINISH": App.CurrentApp.GarageRecord.finish = value; break;
                        case "EOPENTYPE": App.CurrentApp.GarageRecord.opening_type = value; break;
                        case "PARTS_ORDER" : App.CurrentApp.GarageRecord.parts_to_order = value; break;
                        case "EELECPOWER": App.CurrentApp.GarageRecord.power_points = YNtoInt(value); break;
                        case "EELECOPER" : App.CurrentApp.GarageRecord.electric_door = YNtoInt (value); break;
                        case "ESPTEXT": App.CurrentApp.GarageRecord.long_comments = value; break;
                        case "ENOBSIN": App.CurrentApp.GarageRecord.obstruction_inside_b = YNtoInt(value); break;
                        case "ENOBSIEDIT": App.CurrentApp.GarageRecord.obstruction_inside = value; break;
                        case "ENOBSOUT": App.CurrentApp.GarageRecord.obstruction_outside_b = YNtoInt(value); break;
                        case "ENOBSOEDIT": App.CurrentApp.GarageRecord.obstruction_outside = value; break;
                        case "ENTYPEG": App.CurrentApp.GarageRecord.type_of_garage = value; break;
                        case "ENNEOR": App.CurrentApp.GarageRecord.new_electric_operator_req = value; break;
                        case "ENINTFH": App.CurrentApp.GarageRecord.side_size_A = value; break;
                        case "ENLINTD": App.CurrentApp.GarageRecord.side_size_B = value; break;
                        case "ENBRH": App.CurrentApp.GarageRecord.side_size_C = value; break;
                        case "ENLNGG": App.CurrentApp.GarageRecord.side_size_D = value; break;
                        case "ENFTCH": App.CurrentApp.GarageRecord.side_size_E = value; break;
                        case "ENFTCHM": App.CurrentApp.GarageRecord.side_size_F = value; break;
                        case "SIZEG": App.CurrentApp.GarageRecord.side_size_G = value; break;
                        case "ENSTSA": App.CurrentApp.GarageRecord.side_timber_1 = value; break;
                        case "ENSTSB": App.CurrentApp.GarageRecord.side_timber_2 = value; break;
                        case "ENINTTS": App.CurrentApp.GarageRecord.plan_size_A = value; break;
                        case "ENBOS": App.CurrentApp.GarageRecord.plan_size_B = value; break;
                        case "ENRSA": App.CurrentApp.GarageRecord.plan_size_C1 = value; break;
                        case "ENRSB": App.CurrentApp.GarageRecord.plan_size_C2 = value; break;
                        case "ENRDTS": App.CurrentApp.GarageRecord.plan_size_D = value; break;
                        case "ENPTSA": App.CurrentApp.GarageRecord.plan_timber_1 = value; break;
                        case "ENPTSB": App.CurrentApp.GarageRecord.plan_timber_2 = value; break;
                        case "RDOORTYPE" : App.CurrentApp.GarageRecord.roller_door_type = value; break;
                        case "RBOXTYPE" : App.CurrentApp.GarageRecord.roller_box_type = value; break;
                        case "COLMATCH" : App.CurrentApp.GarageRecord.colour_match_roll_box = YNtoInt (value); break;
                        case "ELECDOOR": App.CurrentApp.GarageRecord.electric_door = YNtoInt(value); break;
                        case "HANDLEOUTS": App.CurrentApp.GarageRecord.handle_outside = YNtoInt(value); break;
                        case "OTHERACCESS": App.CurrentApp.GarageRecord.other_access = YNtoInt(value); break;
                        case "SAFERELEASE": App.CurrentApp.GarageRecord.need_safety_release = YNtoInt(value); break;
                        case "OPENINGDIR" : App.CurrentApp.GarageRecord.opening_direction = decode_via_button_list (value, SurveyFitterButtonLists.garage_opening_direction_list); break;
                        case "WALLS": App.CurrentApp.GarageRecord.insulated = YNtoInt(value); break;
                        case "DOORSTUCK": App.CurrentApp.GarageRecord.door_stuck_shut = YNtoInt(value); break;
                        case "MOTORPOS": App.CurrentApp.GarageRecord.motor_position = decode_via_button_list(value, SurveyFitterButtonLists.garage_motor_position_list); break;
                        case "PERIMETER" : App.CurrentApp.GarageRecord.door_within_perimeter = YNtoInt (value); break;
                        case "WIRETYPE" : App.CurrentApp.GarageRecord.wire_type = value; break;
                        case "WITHINMETER" : App.CurrentApp.GarageRecord.socket_within_1m = YNtoInt (value); break;
                        case "POINTENTER" : App.CurrentApp.GarageRecord.point_of_entry = value; break;
                        case "WASLOCKED" : App.CurrentApp.GarageRecord.was_it_locked = YNtoInt (value); break;
                        case "LOCKREQU" : App.CurrentApp.GarageRecord.type_of_lockng_system_required = value; break;
                        case "OTHER_PLACE" : App.CurrentApp.GarageRecord.where_is_garage = value; break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            if (App.CurrentApp.GarageRecord.door_fits_into != 2)
                App.CurrentApp.GarageRecord.new_subframe_req = decode_via_button_list(new_subframe_req, SurveyFitterButtonLists.garage_timber_subframe_list);
            else
                App.CurrentApp.GarageRecord.new_subframe_req = decode_via_button_list(new_subframe_req, SurveyFitterButtonLists.garage_metal_subframe_list);

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.GarageRecord.item_number);
            App.CurrentApp.GarageRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.GarageRecord.item_number);
            App.CurrentApp.GarageRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.GarageRecord.item_number);
            App.CurrentApp.GarageRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetGarageByContractItemNo(App.CurrentApp.GarageRecord.udi_cont, App.CurrentApp.GarageRecord.item_number) == null)
                App.data.SaveGarage();
         }

        protected void load_glass()
        {
            bool finished = false;
            string key;
            string value;
            string lead_thickness = "";
            string georgian_bar = "";

            App.net.table_init.CreateGlass();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT": App.CurrentApp.GlassRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE": App.CurrentApp.GlassRecord.item_number = parse_int(value); break;
                        case "GDAMCH": App.CurrentApp.GlassRecord.cause_of_damage = value; break;
                        case "GGLATYPE": App.CurrentApp.GlassRecord.glass_type = value; break;
                        case "GGLAPATT": App.CurrentApp.GlassRecord.glass_pattern = value; break;
                        case "GSPACCOLO": App.CurrentApp.GlassRecord.spacer_color = value; break;
                        case "GSPACTHIC": App.CurrentApp.GlassRecord.spacer_thickness = value; break;
                        case "GSPEGLAS": App.CurrentApp.GlassRecord.special_glass = value; break;
                        case "GSPTEXT": App.CurrentApp.GlassRecord.long_comments = value; break;
                        case "GSTEPINT": App.CurrentApp.GlassRecord.stepped_unit = YNtoInt(value); break;
                        case "GSTEPWEDIT": App.CurrentApp.GlassRecord.int_width = value; break;
                        case "GSTEPHEDIT": App.CurrentApp.GlassRecord.int_height = value; break;
                        case "PARTS_ORDER" : App.CurrentApp.GlassRecord.parts_to_order = value; break;
                        case "GSINGDOUB": App.CurrentApp.GlassRecord.single_or_double = decode_via_button_list(value, SurveyFitterButtonLists.shortened_single_or_double_list); break;
                        case "COL_COPY" : App.CurrentApp.GlassRecord.collect_and_copy = YNtoInt(value); break;
                        case "TEMP_DOOR" : App.CurrentApp.GlassRecord.temporary = decode_via_button_list(value, SurveyFitterButtonLists.shared_temporary_button_list); break;
                        case "SIZEA": App.CurrentApp.GlassRecord.sizeA = value; break;
                        case "SIZEB": App.CurrentApp.GlassRecord.sizeB = value; break;
                        case "SIZEC": App.CurrentApp.GlassRecord.sizeC = value; break;
                        case "SIZED": App.CurrentApp.GlassRecord.sizeD = value; break;
                        case "LEADWIDTH": App.CurrentApp.GlassRecord.lead_CWidth = value; break;
                        case "LEADHEIGHT": App.CurrentApp.GlassRecord.lead_CHeight = value; break;
                        case "ANTIRATTLE": App.CurrentApp.GlassRecord.lead_anti_rattle = decode_via_button_list(value, georgian_bar_logic.anti_rattle); break;
                        case "LEADSINGDOUB": App.CurrentApp.GlassRecord.lead_sod = value; break;
                        case "TYPEOFLEAD": App.CurrentApp.GlassRecord.lead_type = value; break;
                        case "QDOCL": App.CurrentApp.GlassRecord.docl = value; break;
                        case "TRIM30MM" : App.CurrentApp.GlassRecord.gb_trim = YNtoInt(value); break;
                        case "ROOMLOCATION": App.CurrentApp.GlassRecord.room_location = value; break;
                        case "GLASSIN": App.CurrentApp.GlassRecord.ProductInto = value; break;
                        case "JOINTTYPE" : App.CurrentApp.GlassRecord.glazing_type = value; break;
                        case "GEXWEDIT": App.CurrentApp.GlassRecord.glass_width = value; break;
                        case "GEXHEDIT": App.CurrentApp.GlassRecord.glass_height = value; break;
                        case "GEXWEDIT2": App.CurrentApp.GlassRecord.glass_width2 = value; break;
                        case "GEXHEDIT2": App.CurrentApp.GlassRecord.glass_height2 = value; break;
                        case "GEXWEDIT3": App.CurrentApp.GlassRecord.glass_width3 = value; break;
                        case "GEXHEDIT3": App.CurrentApp.GlassRecord.glass_height3 = value; break;
                        case "GEXWEDIT4": App.CurrentApp.GlassRecord.glass_width4 = value; break;
                        case "GEXHEDIT4": App.CurrentApp.GlassRecord.glass_height4 = value; break;
                        case "GEXWEDIT5": App.CurrentApp.GlassRecord.glass_width5 = value; break;
                        case "GEXHEDIT5": App.CurrentApp.GlassRecord.glass_height5 = value; break;
                        case "GEXWEDIT6": App.CurrentApp.GlassRecord.glass_width6 = value; break;
                        case "GEXHEDIT6": App.CurrentApp.GlassRecord.glass_height6 = value; break;
                        case "GEXWEDIT7": App.CurrentApp.GlassRecord.glass_width7 = value; break;
                        case "GEXHEDIT7": App.CurrentApp.GlassRecord.glass_height7 = value; break;
                        case "GEXWEDIT8": App.CurrentApp.GlassRecord.glass_width8 = value; break;
                        case "GEXHEDIT8": App.CurrentApp.GlassRecord.glass_height8 = value; break;
                        case "TAPEORGAS" : App.CurrentApp.GlassRecord.TapeorGasket = value; break;
                        case "LEAD_COMMENT" : App.CurrentApp.GlassRecord.lead_comments = value; break;
                        case "OVER_THICK" : App.CurrentApp.GlassRecord.docl_old = value; break;
                        case "GLAZED_INT" : App.CurrentApp.GlassRecord.glaze = decode_via_button_list(value, SurveyFitterButtonLists.glass_glaze_button_list); break;
                        case "POINTENTER" : App.CurrentApp.GlassRecord.point_of_entry = value; break;
                        case "WASLOCKED" : App.CurrentApp.GlassRecord.was_it_locked = YNtoInt(value); break;
                        case "LOCKREQU" : App.CurrentApp.GlassRecord.type_of_lockng_system_required = value; break;
                        case "BBSPACERWID" : App.CurrentApp.GlassRecord.back_to_back_spacer_width = value; break;
                        case "BBSPOVERALLWID" : App.CurrentApp.GlassRecord.back_to_back_spacer_height = value; break;
                        case "COMBI" : App.CurrentApp.GlassRecord.parent_item = parse_int (value); break;
                        case "LEADTHICK" : lead_thickness = value; break;
                        case "GEORGBAR" : georgian_bar = value; break;
                        case "NOUNITS": 
                            App.CurrentApp.GlassRecord.units_required = parse_int(value);
                            if (App.CurrentApp.GlassRecord.units_required == 0)
                                App.CurrentApp.GlassRecord.units_required = 1;
                            break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            if (App.CurrentApp.GlassRecord.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                App.CurrentApp.HeaderRecord.current_item_number = App.CurrentApp.GlassRecord.item_number + 1;

            if (App.CurrentApp.GlassRecord.special_glass == "Georgian Bar")
                App.CurrentApp.GlassRecord.lead_thickness = georgian_bar;
            else
                App.CurrentApp.GlassRecord.lead_thickness = lead_thickness;

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.GlassRecord.item_number);
            App.CurrentApp.GlassRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.GlassRecord.item_number);
            App.CurrentApp.GlassRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.GlassRecord.item_number);
            App.CurrentApp.GlassRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetGlassByContractItemNo(App.CurrentApp.GlassRecord.udi_cont, App.CurrentApp.GlassRecord.item_number) == null)
                App.data.SaveGlass();
        }

        protected void load_greenhouse()
        {
            bool finished = false;
            string key;
            string value;
            string base_size_x = "";
            string base_size_y = "";

            App.net.table_init.CreateGreenhouse();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT": App.CurrentApp.GreenRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE": App.CurrentApp.GreenRecord.item_number = parse_int(value); break;
                        case "COSDAM": App.CurrentApp.GreenRecord.cause_of_damage = value; break;
                        case "REPREAS": App.CurrentApp.GreenRecord.rep_reason = value; break;
                        case "MATTYPE": App.CurrentApp.GreenRecord.material_type = value; break;
                        case "COLOUR": App.CurrentApp.GreenRecord.colour = value; break;
                        case "GLAZETYPE": App.CurrentApp.GreenRecord.glaze_type = value; break;
                        case "BASESIZE": App.CurrentApp.GreenRecord.base_size = value; break;
                        case "BASEWIDTH": base_size_x = value; break;
                        case "BASEHEIGHT": base_size_y = value; break;
                        case "TYPEOFGLASS": App.CurrentApp.GreenRecord.type_of_glass = value; break;
                        case "DOPENTYPE": App.CurrentApp.GreenRecord.door_opening_type = value; break;
                        case "WOPENTYPE": App.CurrentApp.GreenRecord.window_opening_type = value; break;
                        case "OVERHEIGHT": App.CurrentApp.GreenRecord.overall_height = value; break;
                        case "SUMMARY": App.CurrentApp.GreenRecord.summary = value; break;
                        case "PARTS_ORDER" : App.CurrentApp.GreenRecord.parts_to_order = value; break;
                        case "POINTENTER" : App.CurrentApp.GreenRecord.point_of_entry = value; break;
                        case "WASLOCKED" : App.CurrentApp.GreenRecord.was_it_locked = YNtoInt (value); break;
                        case "LOCKREQU" : App.CurrentApp.GreenRecord.type_of_lockng_system_required = value; break;
                        case "REPLACEGLASS": App.CurrentApp.GreenRecord.replace_glass = YNtoInt (value); break;
                        case "REPLACORREPR": App.CurrentApp.GreenRecord.repair_or_replace = decode_via_button_list(value, SurveyFitterButtonLists.shared_repair_replace_list); break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            if (base_size_x.Length >= 4 && (base_size_x.Substring (base_size_x.Length - 3) == " ft" || base_size_x.Substring (base_size_x.Length - 3) == " mm"))
                App.CurrentApp.GreenRecord.base_size_x = base_size_x.Substring (0, base_size_x.Length - 3);
            else
                App.CurrentApp.GreenRecord.base_size_x = base_size_x;

            if (base_size_y.Length >= 4
                && (base_size_y.Substring (base_size_y.Length - 3) == " ft" || base_size_y.Substring (base_size_y.Length - 3) == " mm"))
                App.CurrentApp.GreenRecord.base_size_y = base_size_y.Substring (0, base_size_y.Length - 3);
            else
                App.CurrentApp.GreenRecord.base_size_y = base_size_y;

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.GreenRecord.item_number);
            App.CurrentApp.GreenRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.GreenRecord.item_number);
            App.CurrentApp.GreenRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.GreenRecord.item_number);
            App.CurrentApp.GreenRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetGreenByContractItemNo(App.CurrentApp.GreenRecord.udi_cont, App.CurrentApp.GreenRecord.item_number) == null)
                App.data.SaveGreen();
        }

        protected void load_lock()
        {
            bool finished = false;
            string key;
            string value;

            App.net.table_init.CreateLock();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT": App.CurrentApp.LockingRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE": App.CurrentApp.LockingRecord.item_number = parse_int(value); break;
                        case "LockMake": App.CurrentApp.LockingRecord.locking_make = value; break;
                        case "LOCKCODES": App.CurrentApp.LockingRecord.locking_codes = value; break;
                        case "LOCKCOLOUR": App.CurrentApp.LockingRecord.lock_colour = value; break;
                        case "ITEMTYPE": App.CurrentApp.LockingRecord.item = value; break;
                        case "LOCKCOMMENTS": App.CurrentApp.LockingRecord.long_comments = value; break;
                        case "PAGENUM": App.CurrentApp.LockingRecord.pagenum = value; break;
                        case "LDAMCH" : App.CurrentApp.LockingRecord.cause_of_damage = value; break;
                        case "SUPPBROC" : App.CurrentApp.LockingRecord.COD_Code = value; break;
                        case "PARTS_ORDER" : App.CurrentApp.LockingRecord.parts_to_order = value; break;
                        case "NoOfLocks": App.CurrentApp.LockingRecord.l_num = parse_int(value); break;
                        case "LOCKDIST1": App.CurrentApp.LockingRecord.l_size1 = value; break;
                        case "LOCKDIST2": App.CurrentApp.LockingRecord.l_size2 = value; break;
                        case "LOCKDISTA": App.CurrentApp.LockingRecord.l_sizeA = value; break;
                        case "LOCKDISTB": App.CurrentApp.LockingRecord.l_sizeB = value; break;
                        case "LOCKDISTC": App.CurrentApp.LockingRecord.l_sizeC = value; break;
                        case "LOCKDISTD": App.CurrentApp.LockingRecord.l_sizeD = value; break;
                        case "LOCKDISTE": App.CurrentApp.LockingRecord.l_sizeE = value; break;
                        case "LOCKDISTF": App.CurrentApp.LockingRecord.l_sizeF = value; break;
                        case "LOCKDISTG": App.CurrentApp.LockingRecord.l_sizeG = value; break;
                        case "LOCKTYPE1": App.CurrentApp.LockingRecord.l_itype1 = decode_lock(value); break;
                        case "LOCKTYPE2": App.CurrentApp.LockingRecord.l_itype2 = decode_lock(value); break;
                        case "LOCKTYPE3": App.CurrentApp.LockingRecord.l_itype3 = decode_lock(value); break;
                        case "LOCKTYPE4": App.CurrentApp.LockingRecord.l_itype4 = decode_lock(value); break;
                        case "LOCKTYPE5": App.CurrentApp.LockingRecord.l_itype5 = decode_lock(value); break;
                        case "LOCKTYPE6": App.CurrentApp.LockingRecord.l_itype6 = decode_lock(value); break;
                        case "LOCKTYPE7": App.CurrentApp.LockingRecord.l_itype7 = decode_lock(value); break;
                        case "PICDIST1": App.CurrentApp.LockingRecord.l_fpos1 = parse_float(value) * 2; break;
                        case "PICDIST2": App.CurrentApp.LockingRecord.l_fpos2 = parse_float(value) * 2; break;
                        case "PICDIST3": App.CurrentApp.LockingRecord.l_fpos3 = parse_float(value) * 2; break;
                        case "PICDIST4": App.CurrentApp.LockingRecord.l_fpos4 = parse_float(value) * 2; break;
                        case "PICDIST5": App.CurrentApp.LockingRecord.l_fpos5 = parse_float(value) * 2; break;
                        case "PICDIST6": App.CurrentApp.LockingRecord.l_fpos6 = parse_float(value) * 2; break;
                        case "PICDIST7": App.CurrentApp.LockingRecord.l_fpos7 = parse_float(value) * 2; break;
                        case "MECHDIST": App.CurrentApp.LockingRecord.lock_position = parse_float(value) * 2; break;
                        case "LEFTBOLT": App.CurrentApp.LockingRecord.left_bolt = decode_NY_to_int(value); break;
                        case "RIGHTBOLT": App.CurrentApp.LockingRecord.right_bolt = decode_NY_to_int(value); break;
                        case "GEARBOX": App.CurrentApp.LockingRecord.GearBox = decode_via_button_list(value, window_lock_logic.gearbox_list); break;
                        case "POINTENTER" : App.CurrentApp.LockingRecord.point_of_entry = value; break;
                        case "WASLOCKED" : App.CurrentApp.LockingRecord.was_it_locked = YNtoInt(value); break;
                        case "LOCKREQU" : App.CurrentApp.LockingRecord.type_of_lockng_system_required = value; break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.LockingRecord.item_number);
            App.CurrentApp.LockingRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.LockingRecord.item_number);
            App.CurrentApp.LockingRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.LockingRecord.item_number);
            App.CurrentApp.LockingRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetLockByContractItemNo(App.CurrentApp.LockingRecord.udi_cont, App.CurrentApp.LockingRecord.item_number) == null)
                App.data.SaveLocking();
        }

        protected void load_panel()
        {
            bool finished = false;
            string key;
            string value;

            //if(App.net.HeaderRecord.udi_cont=="00370935")
            //{
            ///    DisplayAlert("ok","ok","ok");
            //}

            App.net.table_init.CreatePanel();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT": App.CurrentApp.PanelRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE": App.CurrentApp.PanelRecord.item_number = parse_int(value); break;
                        case "PDAMCH": App.CurrentApp.PanelRecord.cause_of_damage = value; break;
                        case "PKNOCK": App.CurrentApp.PanelRecord.knockedit = value; break;
                        case "PKNOCOL": App.CurrentApp.PanelRecord.knocoledit = value; break;
                        case "LETBOXPOS": App.CurrentApp.PanelRecord.letter_box_pos = value; break;
                        case "PLETTER": App.CurrentApp.PanelRecord.letteredit = value; break;
                        case "PWEDIT": App.CurrentApp.PanelRecord.wedit = value; break;
                        case "PHEDIT": App.CurrentApp.PanelRecord.hedit = value; break;
                        case "PTYPEEDIT": App.CurrentApp.PanelRecord.typeedit = value; break;
                        case "PTHICK": App.CurrentApp.PanelRecord.thickedit = value; break;
                        case "PCOL": App.CurrentApp.PanelRecord.coledit = value; break;
                        case "PBACKG": App.CurrentApp.PanelRecord.backgedit = value; break;
                        case "PARTS_ORDER": App.CurrentApp.PanelRecord.parts_to_order = value; break;
                        case "PGLTEXT": App.CurrentApp.PanelRecord.gltext = value; break;
                        case "PSPACCOLO": App.CurrentApp.PanelRecord.spaccoloedit = value; break;
                        case "PSPTEXT": App.CurrentApp.PanelRecord.long_sptext = value; break;
                        case "PETFLAP": App.CurrentApp.PanelRecord.pet_flap = value; break;

                        case "PETTYPE": App.CurrentApp.PanelRecord.pet_type = value; break;
                        case "ROOMLOCATION": App.CurrentApp.PanelRecord.room_location = value; break;
                        case "PETMAGNET": App.CurrentApp.PanelRecord.pet_magnetic = decode_via_button_list(value, MartControls.pet_flap_logic.magnetic_list); break;
                        case "UPVCITEMNO": App.CurrentApp.PanelRecord.upvc_item_number = parse_int(value); break;
                        case "ALUMITEMNO": App.CurrentApp.PanelRecord.alum_item_number = parse_int(value); break;
                        case "POINTENTER": App.CurrentApp.PanelRecord.point_of_entry = value; break;
                        case "WASLOCKED": App.CurrentApp.PanelRecord.was_it_locked = YNtoInt(value); break;
                        case "LOCKREQU": App.CurrentApp.PanelRecord.type_of_lockng_system_required = value; break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.PanelRecord.item_number);
            App.CurrentApp.PanelRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.PanelRecord.item_number);
            App.CurrentApp.PanelRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.PanelRecord.item_number);
            App.CurrentApp.PanelRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
        }

        protected void load_timber()
        {
            bool finished = false;
            string key;
            string value;
            string lead_thickness = "";
            string georgian_bar = "";

            App.net.table_init.CreateTimber();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRAC"      : App.CurrentApp.TimberRecord.udi_cont = value; break;
                        case "ITEMCOD"      : App.CurrentApp.TimberRecord.item_number = parse_int (value); break;
                        case "LEAD_COMMENT" : App.CurrentApp.TimberRecord.lead_comments = value; break;
                        case "URADDLO"      : App.CurrentApp.TimberRecord.additional_locks = value; break;
                        case "PARTS_ORDER"  : App.CurrentApp.TimberRecord.parts_to_order = value; break;
                        case "TRRCH"        : App.CurrentApp.TimberRecord.bRepair = value == "Repair"; break;
                        case "URCOSDA"      : App.CurrentApp.TimberRecord.cosmetic_damage = YNtoInt (value); break;
                        case "COL_COPY"     : App.CurrentApp.TimberRecord.collect_and_copy = YNtoInt (value); break;
                        case "TEMP_DOOR"    : App.CurrentApp.TimberRecord.temporary = decode_via_button_list (value, SurveyFitterButtonLists.shared_temporary_button_list); break;
                        case "PREGLAZED_D"  : App.CurrentApp.TimberRecord.pre_glazed_door = YNtoInt (value); break;
                        case "URGASKE"      : App.CurrentApp.TimberRecord.gaskets = decode_via_button_list (value, SurveyFitterButtonLists.shared_gasket_button_list); break;
                        case "URGASED"      : App.CurrentApp.TimberRecord.gaskets_text = value; break;
                        case "URHANDL"      : App.CurrentApp.TimberRecord.handles_req = YNtoInt (value); break;
                        case "URHANDE"      : App.CurrentApp.TimberRecord.handles_text = value; break;
                        case "TDAMCH"       : App.CurrentApp.TimberRecord.cause_of_damage = value; break;
                        case "TITEMCH"      : App.CurrentApp.TimberRecord.timber_item = value; break;
                        case "TWOODTY"      : App.CurrentApp.TimberRecord.timber_wood = value; break;
                        case "TFRAME"       : App.CurrentApp.TimberRecord.timber_new_frame_req = YNtoInt (value); break;
                        case "TFWOOD"       : App.CurrentApp.TimberRecord.timber_frame_wood = value; break;
                        case "TBRKSWE"      : App.CurrentApp.TimberRecord.brick_width = value; break;
                        case "TBRKSHE"      : App.CurrentApp.TimberRecord.brick_height = value; break;
                        case "TINTWED"      : App.CurrentApp.TimberRecord.internal_width = value; break;
                        case "TINTHED"      : App.CurrentApp.TimberRecord.internal_height = value; break;
                        case "TREPFRA"      : App.CurrentApp.TimberRecord.repair_frame = YNtoInt (value); break;
                        case "TDRTHKE"      : App.CurrentApp.TimberRecord.door_thickness = value; break;
                        case "TDRWEDI"      : App.CurrentApp.TimberRecord.door_width = value; break;
                        case "TDRHEDI"      : App.CurrentApp.TimberRecord.door_height = value; break;
                        case "TOPENS"       : App.CurrentApp.TimberRecord.opens = decode_via_button_list(value, SurveyFitterButtonLists.shared_in_out_button_list); break;
                        case "TSASHER"      : App.CurrentApp.TimberRecord.new_sash_required = YNtoInt (value); break;
                        case "WBAREQ"       : App.CurrentApp.TimberRecord.weather_bar = YNtoInt (value); break;
                        case "THEADDR"      : App.CurrentApp.TimberRecord.head_drip = YNtoInt(value); break;
                        case "TCILL"        : App.CurrentApp.TimberRecord.cills = value; break;
                        case "TDRAUGH"      : App.CurrentApp.TimberRecord.draught_strip = YNtoInt (value); break;
                        case "TTHRESH"      : App.CurrentApp.TimberRecord.thresher = YNtoInt (value); break;
                        case "TSINGDO"      : App.CurrentApp.TimberRecord.single_double = decode_via_button_list (value, SurveyFitterButtonLists.shortened_single_or_double_list); break;
                        case "TTRICK1"      : App.CurrentApp.TimberRecord.trickle_vents = value; break;
                        case "TLOCK"        : App.CurrentApp.TimberRecord.locks = value; break;
                        case "THARDCO"      : App.CurrentApp.TimberRecord.hardware_color = value; break;
                        case "TDCOLOU"      : App.CurrentApp.TimberRecord.door_color = value; break;
                        case "TFCOLOU"      : App.CurrentApp.TimberRecord.frame_color = value; break;
                        case "TDCOLOURO"    : App.CurrentApp.TimberRecord.door_color_out = value; break;
                        case "TFCOLOURO"    : App.CurrentApp.TimberRecord.frame_color_out = value; break;
                        case "TDCOLCODE"    : App.CurrentApp.TimberRecord.door_color_code = value; break;
                        case "TDCOLCODEO"   : App.CurrentApp.TimberRecord.door_color_code_out = value; break;
                        case "TFCOLCODE"    : App.CurrentApp.TimberRecord.frame_color_code = value; break;
                        case "TFCOLCODEO"   : App.CurrentApp.TimberRecord.frame_color_code_out = value; break;
                        case "TSPACTH"      : App.CurrentApp.TimberRecord.spacer_thickness = value; break;
                        case "TSPACCO"      : App.CurrentApp.TimberRecord.spacer_color = value; break;
                        case "TGLATYP"      : App.CurrentApp.TimberRecord.glass_type = value; break;
                        case "TGLAPAT"      : App.CurrentApp.TimberRecord.glass_pattern = value; break;
                        case "TSPEGLA"      : App.CurrentApp.TimberRecord.special_glass = value; break;
                        case "TSPTEXT"      : App.CurrentApp.TimberRecord.long_timber_comments = value; break;
                        case "SIZEA"        : App.CurrentApp.TimberRecord.lead_sizeA = parse_int (value); break;
                        case "SIZEB"        : App.CurrentApp.TimberRecord.lead_sizeB = parse_int (value); break;
                        case "SIZEC"        : App.CurrentApp.TimberRecord.lead_sizeC = parse_int (value); break;
                        case "SIZED"        : App.CurrentApp.TimberRecord.lead_sizeD = parse_int (value); break;
                        case "LEADWIDTH"    : App.CurrentApp.TimberRecord.lead_CWidth = parse_int (value); break;
                        case "LEADHEIGHT"   : App.CurrentApp.TimberRecord.lead_CHeight = parse_int (value); break;
                        case "ANTIRATTLE"   : App.CurrentApp.TimberRecord.lead_anti_rattle = decode_via_button_list(value, georgian_bar_logic.anti_rattle); break;
                        case "LEADTHICK"    : lead_thickness = value; break;
                        case "GEORGBAR"     : georgian_bar = value; break;
                        case "LEAD_SOD"     : App.CurrentApp.TimberRecord.lead_sod = value; break;
                        case "TYPEOFLEAD"   : App.CurrentApp.TimberRecord.lead_type = value; break;
                        case "HINGE_TYPE"   : App.CurrentApp.TimberRecord.hinge_type = value; break;
                        case "NOOFLOCKS"    : App.CurrentApp.TimberRecord.l_num = parse_int (value); break;
                        case "LOCKDIST1"    : App.CurrentApp.TimberRecord.l_size1 = value; break;
                        case "LOCKDIST2"    : App.CurrentApp.TimberRecord.l_size2 = value; break;
                        case "LOCKDISTA"    : App.CurrentApp.TimberRecord.l_sizeA = value; break;
                        case "LOCKDISTB"    : App.CurrentApp.TimberRecord.l_sizeB = value; break;
                        case "LOCKDISTC"    : App.CurrentApp.TimberRecord.l_sizeC = value; break;
                        case "LOCKDISTD"    : App.CurrentApp.TimberRecord.l_sizeD = value; break;
                        case "LOCKDISTE"    : App.CurrentApp.TimberRecord.l_sizeE = value; break;
                        case "LOCKDISTF"    : App.CurrentApp.TimberRecord.l_sizeF = value; break;
                        case "LOCKDISTG"    : App.CurrentApp.TimberRecord.l_sizeG = value; break;
                        case "LOCKTYPE1"    : App.CurrentApp.TimberRecord.l_itype1 = decode_lock (value); break;
                        case "LOCKTYPE2"    : App.CurrentApp.TimberRecord.l_itype2 = decode_lock (value); break;
                        case "LOCKTYPE3"    : App.CurrentApp.TimberRecord.l_itype3 = decode_lock (value); break;
                        case "LOCKTYPE4"    : App.CurrentApp.TimberRecord.l_itype4 = decode_lock (value); break;
                        case "LOCKTYPE5"    : App.CurrentApp.TimberRecord.l_itype5 = decode_lock (value); break;
                        case "LOCKTYPE6"    : App.CurrentApp.TimberRecord.l_itype6 = decode_lock (value); break;
                        case "LOCKTYPE7"    : App.CurrentApp.TimberRecord.l_itype7 = decode_lock (value); break;
                        case "PICDIST1"     : App.CurrentApp.TimberRecord.l_fpos1 = parse_float (value) * 2; break;
                        case "PICDIST2"     : App.CurrentApp.TimberRecord.l_fpos2 = parse_float (value) * 2; break;
                        case "PICDIST3"     : App.CurrentApp.TimberRecord.l_fpos3 = parse_float (value) * 2; break;
                        case "PICDIST4"     : App.CurrentApp.TimberRecord.l_fpos4 = parse_float (value) * 2; break;
                        case "PICDIST5"     : App.CurrentApp.TimberRecord.l_fpos5 = parse_float (value) * 2; break;
                        case "PICDIST6"     : App.CurrentApp.TimberRecord.l_fpos6 = parse_float (value) * 2; break;
                        case "PICDIST7"     : App.CurrentApp.TimberRecord.l_fpos7 = parse_float (value) * 2; break;
                        case "MECHDIST"     : App.CurrentApp.TimberRecord.lock_position = parse_float (value) * 2; break;
                        case "LEFTBOLT"     : App.CurrentApp.TimberRecord.left_bolt = YNtoInt (value); break;
                        case "RIGHTBOLT"    : App.CurrentApp.TimberRecord.right_bolt = YNtoInt (value); break;
                        case "GEARBOX"      : App.CurrentApp.TimberRecord.GearBox = decode_via_button_list (value, window_lock_logic.gearbox_list); break;
                        case "LOCKMAKE"     : App.CurrentApp.TimberRecord.lock_make = value; break;
                        case "LOCKCODES"    : App.CurrentApp.TimberRecord.lock_codes = value; break;
                        case "REPLACREAS"   : App.CurrentApp.TimberRecord.replace_reason = value; break;
                        case "WHYNOREPAIR"  : App.CurrentApp.TimberRecord.replace_explain = value; break;
                        case "ROOMLOCATION" : App.CurrentApp.TimberRecord.room_location = value; break;
                        case "DOCLCOMP"     : App.CurrentApp.TimberRecord.doc_l_compliant = YNtoInt (value); break;
                        case "DOCLCOMPREAS" : App.CurrentApp.TimberRecord.doc_l_compliant_reason = value; break;
                        case "SLIDEPOS"     : App.CurrentApp.TimberRecord.slide_position = decode_via_button_list(value, SurveyFitterButtonLists.shared_inside_outside_button_list); break;
                        case "TIMGLAZED"    : App.CurrentApp.TimberRecord.timber_glazed = decode_via_button_list (value, SurveyFitterButtonLists.timber_glazed_button_list); break;
                        case "REASNONSTAND" : App.CurrentApp.TimberRecord.reasonnonstandard = value; break;
                        case "FENCERRATE"   : App.CurrentApp.TimberRecord.WER_rating = value; break;
                        case "LETBOX"       : App.CurrentApp.TimberRecord.letter_box = value; break;
                        case "LETBOXPOS"    : App.CurrentApp.TimberRecord.letter_box_pos = value; break;
                        case "PETFLAP"      : App.CurrentApp.TimberRecord.pet_flap = value; break;
                        case "PETMAGNET"    : App.CurrentApp.TimberRecord.pet_magnetic = decode_via_button_list (value, MartControls.pet_flap_logic.magnetic_list); break;
                        case "PETTYPE"      : App.CurrentApp.TimberRecord.pet_type = value; break;
                        case "MOULDINGS"    : App.CurrentApp.TimberRecord.moulding = value; break;
                        case "FIRE_DOOR"    : App.CurrentApp.TimberRecord.beading_type = YNtoInt (value); break; // Fire rated door
                        case "ISFLAT"       : App.CurrentApp.TimberRecord.is_a_flat = YNtoInt (value); break;
                        case "POINTENTER"   : App.CurrentApp.TimberRecord.point_of_entry = value; break;
                        case "WASLOCKED"    : App.CurrentApp.TimberRecord.was_it_locked = YNtoInt (value); break;
                        case "LOCKREQU"     : App.CurrentApp.TimberRecord.type_of_lockng_system_required = value; break;
                        case "BBSPACERWID"  : App.CurrentApp.TimberRecord.back_to_back_spacer_width = value; break;
                        case "BBOVERALWID"  : App.CurrentApp.TimberRecord.back_to_back_spacer_height = value; break;
                        case "REPLACEGLASS" : App.CurrentApp.TimberRecord.replace_glass = YNtoInt(value); break;
                        case "QDOCL"        : App.CurrentApp.TimberRecord.docl = value; break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            if (App.CurrentApp.TimberRecord.special_glass == "Georgian Bar")
                App.CurrentApp.TimberRecord.lead_thickness = georgian_bar;
            else
                App.CurrentApp.TimberRecord.lead_thickness = lead_thickness;

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.TimberRecord.item_number);
            App.CurrentApp.TimberRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.TimberRecord.item_number);
            App.CurrentApp.TimberRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.TimberRecord.item_number);
            App.CurrentApp.TimberRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetTimberByContractItemNo(App.CurrentApp.TimberRecord.udi_cont, App.CurrentApp.TimberRecord.item_number) == null)
                App.data.SaveTimber();
        }

        protected void load_upvc()
        {
            bool finished = false;
            string key;
            string value;
            string lead_thickness = "";
            string georgian_bar = "";

            App.CurrentApp.table_init.CreateUpvc();
            next_line = mrs_sr.ReadLine();
            do
            {
                int equals_pos = next_line.IndexOf("=");

                if (equals_pos > 0)
                {
                    key = next_line.Substring(0, equals_pos);
                    value = next_line.Substring(equals_pos + 1);

                    switch (key.ToUpper())
                    {
                        case "CONTRACT"    : App.CurrentApp.UPVCRecord.udi_cont = App.net.HeaderRecord.udi_cont; break;
                        case "ITEMCODE"    : App.CurrentApp.UPVCRecord.item_number = parse_int(value); break;
                        case "LEADCOMMENT" : App.CurrentApp.UPVCRecord.lead_comments = value; break;
                        case "URCDOSDAM"   : App.CurrentApp.UPVCRecord.cosmetic_damage = YNtoInt(value); break;
                        case "URADDEDIT"   : App.CurrentApp.UPVCRecord.additional_locks = value; break;
                        case "PARTS_ORDER" : App.CurrentApp.UPVCRecord.parts_to_order = value; break;
                        case "URGASKET"    : App.CurrentApp.UPVCRecord.gaskets = decode_via_button_list (value, SurveyFitterButtonLists.shared_gasket_button_list); break;
                        case "URGASEDIT"   : App.CurrentApp.UPVCRecord.gaskets_text = value; break;
                        case "URHANDLES"   : App.CurrentApp.UPVCRecord.handles_req = YNtoInt(value); break;
                        case "URHANDEDIT"  : App.CurrentApp.UPVCRecord.handles_text = value; break;
                        case "URRCH"       : App.CurrentApp.UPVCRecord.bRepair = value == "Repair"; break;
                        case "URREPPAN"    : App.CurrentApp.UPVCRecord.replace_panel = YNtoInt(value); break;
                        case "UITEMCH"     : App.CurrentApp.UPVCRecord.upvc_item = value; break;
                        case "UDAMCH"      : App.CurrentApp.UPVCRecord.cause_of_damage = value; break;
                        case "UCOLOUR"     : App.CurrentApp.UPVCRecord.colour = value; break;
                        case "USILL"       : App.CurrentApp.UPVCRecord.cills = value; break;
                        case "UOUTSEDIT"   : App.CurrentApp.UPVCRecord.outer_section_size = value; break;
                        case "UINTWEDIT"   : App.CurrentApp.UPVCRecord.internal_width = value; break;
                        case "UINTHEDIT"   : App.CurrentApp.UPVCRecord.internal_height = value; break;
                        case "UEXTWEDIT"   : App.CurrentApp.UPVCRecord.brick_width = value; break;
                        case "UEXTHEDIT"   : App.CurrentApp.UPVCRecord.brick_height = value; break;
                        case "UMIDRAIL"    : App.CurrentApp.UPVCRecord.midrail = YNtoInt(value); break;
                        case "UADDONS"     : App.CurrentApp.UPVCRecord.addons = YNtoInt(value); break;
                        case "UADDWEDIT"   : App.CurrentApp.UPVCRecord.addon_width = value; break;
                        case "UADDHEDIT"   : App.CurrentApp.UPVCRecord.addon_height = value; break;
                        case "UHEADDRIP"   : App.CurrentApp.UPVCRecord.head_drip = YNtoInt(value); break;
                        case "UHCOLOUR"    : App.CurrentApp.UPVCRecord.handle_colour = value; break;
                        case "ULOCK"       : App.CurrentApp.UPVCRecord.locking_type = value; break;
                        case "ULETTER"     : App.CurrentApp.UPVCRecord.letter_box = value; break;
                        case "UBEAD"       : App.CurrentApp.UPVCRecord.bead_type = value; break;
                        case "UOPENS"      : App.CurrentApp.UPVCRecord.opens = decode_via_button_list(value, SurveyFitterButtonLists.upvc_opens_list); break;
                        case "COL_COPY"    : App.CurrentApp.UPVCRecord.collect_and_copy = YNtoInt (value); break;
                        case "TEMP_DOOR"   : App.CurrentApp.UPVCRecord.temporary = decode_via_button_list (value, SurveyFitterButtonLists.shared_temporary_button_list); break;
                        case "UGLAZE"      : App.CurrentApp.UPVCRecord.glaze = decode_via_button_list(value, SurveyFitterButtonLists.shared_internal_external_button_list); break;
                        case "UTRICKLE"    : App.CurrentApp.UPVCRecord.trickle_vents = decode_via_button_list(value, SurveyFitterButtonLists.upvc_trickle_vents_button_list); break;
                        case "USPACTHIC"   : App.CurrentApp.UPVCRecord.spacer_thickness = value; break;
                        case "USPACCOLO"   : App.CurrentApp.UPVCRecord.spacer_colour = value; break;
                        case "UGLATYPE"    : App.CurrentApp.UPVCRecord.glass_type = value; break;
                        case "UGLAPATT"    : App.CurrentApp.UPVCRecord.glass_pattern = value; break;
                        case "USPEGLAS"    : App.CurrentApp.UPVCRecord.special_glass = value; break;
                        case "USINGDOUB"   : App.CurrentApp.UPVCRecord.double_tripple = decode_via_button_list(value, SurveyFitterButtonLists.upvc_double_or_triple_list); break;
                        case "UINTEXTLK"   : App.CurrentApp.UPVCRecord.internal_lock = decode_via_button_list(value, SurveyFitterButtonLists.upvc_extended_lock_type_button_list); break;
                        case "USPTEXT"     : App.CurrentApp.UPVCRecord.long_comments = value; break;
                        case "SIZEA"       : App.CurrentApp.UPVCRecord.lead_sizeA = parse_int(value); break;
                        case "SIZEB"       : App.CurrentApp.UPVCRecord.lead_sizeB = parse_int(value); break;
                        case "SIZEC"       : App.CurrentApp.UPVCRecord.lead_sizeC = parse_int(value); break;
                        case "SIZED"       : App.CurrentApp.UPVCRecord.lead_sizeD = parse_int(value); break;
                        case "LEADWIDTH"   : App.CurrentApp.UPVCRecord.lead_CWidth = parse_int(value); break;
                        case "LEADHEIGHT"  : App.CurrentApp.UPVCRecord.lead_CHeight = parse_int(value); break;
                        case "ANTIRATTLE"  : App.CurrentApp.UPVCRecord.lead_anti_rattle = decode_via_button_list(value, georgian_bar_logic.anti_rattle); break;
                        case "LEADTHICK"   : lead_thickness = value; break;
                        case "GEORGBAR"    : georgian_bar = value; break;
                        case "LEADSINGDOUB" : App.CurrentApp.UPVCRecord.lead_sod = value; break;
                        case "TYPEOFLEAD"  : App.CurrentApp.UPVCRecord.lead_type = value; break;
                        case "LETBOXPOS"   : App.CurrentApp.UPVCRecord.letter_box_pos = value; break;
                        case "PETFLAP"     : App.CurrentApp.UPVCRecord.pet_flap = value; break;
                        case "PETTYPE"     : App.CurrentApp.UPVCRecord.pet_type = value; break;
                        case "PETMAGNET"   : App.CurrentApp.UPVCRecord.pet_magnetic = decode_via_button_list (value, MartControls.pet_flap_logic.magnetic_list); break;
                        case "HINGECOL"    : App.CurrentApp.UPVCRecord.hinge_colour = value; break;
                        case "PROFILETYPE" : App.CurrentApp.UPVCRecord.profile_type = decode_via_button_list (value, SurveyFitterButtonLists.shortened_profile_type_button_list); break;                        
                        case "NOOFLOCKS"   : App.CurrentApp.UPVCRecord.l_num = parse_int(value); break;
                        case "LOCKDIST1"   : App.CurrentApp.UPVCRecord.l_size1 = value; break;
                        case "LOCKDIST2"   : App.CurrentApp.UPVCRecord.l_size2 = value; break;
                        case "LOCKDISTA"   : App.CurrentApp.UPVCRecord.l_sizeA = value; break;
                        case "LOCKDISTB"   : App.CurrentApp.UPVCRecord.l_sizeB = value; break;
                        case "LOCKDISTC"   : App.CurrentApp.UPVCRecord.l_sizeC = value; break;
                        case "LOCKDISTD"   : App.CurrentApp.UPVCRecord.l_sizeD = value; break;
                        case "LOCKDISTE"   : App.CurrentApp.UPVCRecord.l_sizeE = value; break;
                        case "LOCKDISTF"   : App.CurrentApp.UPVCRecord.l_sizeF = value; break;
                        case "LOCKDISTG"   : App.CurrentApp.UPVCRecord.l_sizeG = value; break;
                        case "LOCKTYPE1"   : App.CurrentApp.UPVCRecord.l_itype1 = decode_lock(value); break;
                        case "LOCKTYPE2"   : App.CurrentApp.UPVCRecord.l_itype2 = decode_lock(value); break;
                        case "LOCKTYPE3"   : App.CurrentApp.UPVCRecord.l_itype3 = decode_lock(value); break;
                        case "LOCKTYPE4"   : App.CurrentApp.UPVCRecord.l_itype4 = decode_lock(value); break;
                        case "LOCKTYPE5"   : App.CurrentApp.UPVCRecord.l_itype5 = decode_lock(value); break;
                        case "LOCKTYPE6"   : App.CurrentApp.UPVCRecord.l_itype6 = decode_lock(value); break;
                        case "LOCKTYPE7"   : App.CurrentApp.UPVCRecord.l_itype7 = decode_lock(value); break;
                        case "PICDIST1"    : App.CurrentApp.UPVCRecord.l_fpos1 = parse_float (value) * 2; break;
                        case "PICDIST2"    : App.CurrentApp.UPVCRecord.l_fpos2 = parse_float (value) * 2; break;
                        case "PICDIST3"    : App.CurrentApp.UPVCRecord.l_fpos3 = parse_float (value) * 2; break;
                        case "PICDIST4"    : App.CurrentApp.UPVCRecord.l_fpos4 = parse_float (value) * 2; break;
                        case "PICDIST5"    : App.CurrentApp.UPVCRecord.l_fpos5 = parse_float (value) * 2; break;
                        case "PICDIST6"    : App.CurrentApp.UPVCRecord.l_fpos6 = parse_float (value) * 2; break;
                        case "PICDIST7"    : App.CurrentApp.UPVCRecord.l_fpos7 = parse_float (value) * 2; break;
                        case "MECHDIST"    : App.CurrentApp.UPVCRecord.lock_position = parse_float(value) * 2; break;
                        case "LEFTBOLT"    : App.CurrentApp.UPVCRecord.left_bolt = decode_NY_to_int(value); break;
                        case "RIGHTBOLT"   : App.CurrentApp.UPVCRecord.right_bolt = decode_NY_to_int(value); break;
                        case "GEARBOX"     : App.CurrentApp.UPVCRecord.GearBox = decode_via_button_list(value, window_lock_logic.gearbox_list); break;
                        case "LOCKMAKE"    : App.CurrentApp.UPVCRecord.lock_make = value; break;
                        case "LOCKCODES"   : App.CurrentApp.UPVCRecord.lock_codes = value; break;
                        case "MRHEIGHT"    : App.CurrentApp.UPVCRecord.midrail_height = value; break;
                        case "QDOCL"       : App.CurrentApp.UPVCRecord.docl = value; break;
                        case "REPLACREAS"  : App.CurrentApp.UPVCRecord.replace_reason = value; break;
                        case "WHYNOREPAIR" : App.CurrentApp.UPVCRecord.replace_explain = value; break;
                        case "FRAMEDEPTHIO" : App.CurrentApp.UPVCRecord.frame_depth = value; break;
                        case "ROOMLOCATION" : App.CurrentApp.UPVCRecord.room_location = value; break;
                        case "LPHANDLES"   : App.CurrentApp.UPVCRecord.LPHandles = decode_via_button_list(value, SurveyFitterButtonLists.shared_handle_operation_list); break;
                        case "SLIDEPOS"    : App.CurrentApp.UPVCRecord.slide_position = decode_via_button_list(value, SurveyFitterButtonLists.shared_inside_outside_button_list); break;
                        case "THRESHOLD"   : App.CurrentApp.UPVCRecord.threshold_type = value; break;
                        case "FENCERRATE"  : App.CurrentApp.UPVCRecord.WER_Rating = value; break;
                        case "ISFLAT"      : App.CurrentApp.UPVCRecord.is_a_flat = YNtoInt (value); break;
                        case "POINTENTER"  : App.CurrentApp.UPVCRecord.point_of_entry = value; break;
                        case "WASLOCKED"   : App.CurrentApp.UPVCRecord.was_it_locked = YNtoInt (value); break;
                        case "LOCKREQU"    : App.CurrentApp.UPVCRecord.type_of_lockng_system_required = value; break;
                        case "BBSPACERWID" : App.CurrentApp.UPVCRecord.back_to_back_spacer_width = value; break;
                        case "BBOVERALWID" : App.CurrentApp.UPVCRecord.back_to_back_spacer_height = value; break;
                        case "REPLACEGLASS" : App.CurrentApp.UPVCRecord.replace_glass = YNtoInt(value); break;
                        case "URADDLOCK"   : App.CurrentApp.UPVCRecord.additional_locks = value; break;
                    }

                    if (mrs_sr.EndOfStream)
                        finished = true;
                    else
                        next_line = mrs_sr.ReadLine();
                }
                else if (mrs_sr.EndOfStream || next_line.Contains("**"))
                    finished = true;
                else // We don't understand this line, skip it
                    next_line = mrs_sr.ReadLine();
            } while (!finished);

            if (App.CurrentApp.UPVCRecord.special_glass == "Georgian Bar")
                App.CurrentApp.UPVCRecord.lead_thickness = georgian_bar;
            else
                App.CurrentApp.UPVCRecord.lead_thickness = lead_thickness;

            string fname = string.Format("{0:00000000}_cAZ", App.CurrentApp.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.UPVCRecord.item_number);
            App.CurrentApp.UPVCRecord.no_of_photos = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.UPVCRecord.item_number);
            App.CurrentApp.UPVCRecord.no_of_pics = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();
            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}", App.CurrentApp.UPVCRecord.item_number);
            App.CurrentApp.UPVCRecord.no_of_vids = images_list.Where(p => p.image_filename.StartsWith(fname)).Count();// App.files.GetFileList("Photos", fname).Count();

            if (App.data.GetUPVCByContractItemNo(App.CurrentApp.UPVCRecord.udi_cont, App.CurrentApp.UPVCRecord.item_number) == null)
                App.data.SaveUPVC();
        }
    }
}