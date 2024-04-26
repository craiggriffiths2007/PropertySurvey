using SQLite;
namespace PropertySurvey
{
    public class UPVCTable
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string udi_cont { get; set; }		        // Key fieled contract number			CONTRACT NUMBER
        public int item_number { get; set; }
        public int isComplete { get; set; }
        public bool bRepair { get; set; }
        public int cosmetic_damage { get; set; }			// Cosmetic damage	Yes/No/Not answered				ur_cos_dam
        public string additional_locks { get; set; }		// multi choice + text entry						ur_add_lock
        public int gaskets { get; set; }					// Multi choice spinner + text box	
        public string gaskets_text { get; set; }
        public int handles_req { get; set; }
        public string handles_text { get; set; }		    // Handles text
        public int replace_panel { get; set; }
        public string replace_reason { get; set; }
        public string replace_explain { get; set; }
        public string upvc_item { get; set; }	            // multi choice drop down
        public string cause_of_damage { get; set; }	        // multi choice drop down + other		
        public string cause_of_damage_reason_different { get; set; }
        public string colour { get; set; }				    // multi choice drop down with other 
        public string cills { get; set; }                   // multi choice drop down + other
        public string outer_section_size { get; set; }	    // number
        public string internal_width { get; set; }
        public string internal_height { get; set; }
        public string brick_width { get; set; }				// numeric only
        public string brick_height { get; set; }            // numeric only
        public int midrail { get; set; }				    // spinner
        public int addons { get; set; }
        public string addon_width { get; set; }			    // number ( enabled if addons==TRUE )
        public string addon_height { get; set; }	        // number
        public int head_drip { get; set; }					// Spinner
        public string handle_colour { get; set; }		    // multi choice with other
        public string locking_type { get; set; }
        public string letter_box { get; set; }
        public string letter_box_pos { get; set; }
        public string pet_flap { get; set; }
        public string pet_type { get; set; }
        public int pet_magnetic { get; set; }
        public string bead_type { get; set; }
        public int opens { get; set; }						// spinner ( in / out )
        public int glaze { get; set; }						// spinner ( internal / external )
        public int trickle_vents { get; set; }			    // spinner ( yes/no )
        public string spacer_thickness { get; set; }		// multi choice with other
        public string spacer_colour { get; set; }			// multi choice with other
        public string glass_type { get; set; }			    // multi choice with other
        public string glass_pattern { get; set; }		    // multi choice with other
        public string special_glass { get; set; }			// multi choice with other
        public int double_tripple { get; set; }				// Renamed from single_double
        public int internal_lock { get; set; }				// multi choice
        public int bNewLockingMech { get; set; }			// 0-not selected 1-Yes 2-No
        public bool bLockComplete { get; set; }				// Is the locking mechanism complete ????????????
        public bool bHandleDrawingComplete { get; set; }
        public int no_of_pics { get; set; }	                // Number of pictures
        public string midrail_height { get; set; }
        public int no_of_photos { get; set; }	            // Number of pictures
        public string frame_depth { get; set; }
        public string docl { get; set; }
        public int profile_type { get; set; }
        public string room_location { get; set; }
        public int no_of_vids { get; set; }
        public int LPHandles { get; set; }
        public int slide_position { get; set; }
        public string threshold_type { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public string ChangeItemTo { get; set; }
        public string print_name { get; set; }
        public bool fensa { get; set; }                     // Renamed from bFencer
        public string WER_Rating { get; set; }              // Renamed from FecerRating
        public string long_comments { get; set; }			// text
        public bool bDoorComplete { get; set; }
        public bool bWindowComplete { get; set; }
        public int lead_sizeA { get; set; }
        public int lead_sizeB { get; set; }
        public int lead_sizeC { get; set; }
        public int lead_sizeD { get; set; }
        public int lead_CWidth { get; set; }
        public int lead_CHeight { get; set; }
        public int lead_anti_rattle { get; set; }
        public string lead_thickness { get; set; }
        public string lead_sod { get; set; }
        public string lead_type { get; set; }
        public bool lead_bDiamondComplete { get; set; }
        public bool lead_bGeorgianComplete { get; set; }
        public bool lead_bBarComplete { get; set; }
        public bool lead_bSGDesignComplete { get; set; }
        public string lock_make { get; set; }
        public string lock_codes { get; set; }
        public bool bPanelComplete { get; set; }
        public int left_bolt { get; set; }
        public int right_bolt { get; set; }
        public int GearBox { get; set; }
        public string hinge_colour { get; set; }                // Renamed from s_spare3
        public string lead_comments { get; set; }
        public int collect_and_copy { get; set; }               // Renamed from new_ispare1
        public int temporary { get; set; }                      // Renamed from new_ispare2
        public string parts_to_order { get; set; }              // Renamed from new_sspare4
        public int is_a_flat { get; set; }
        public string point_of_entry { get; set; }
        public string type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public string back_to_back_spacer_width { get; set; }   // Spacer Thickness - Renamed from ex_s_spare4
        public string back_to_back_spacer_height { get; set; }  // Overall Spacer Width - Renamed from ex_s_spare5

        // Lock Mech
        public string l_size1 { get; set; }
        public string l_size2 { get; set; }
        public string l_sizeA { get; set; }
        public string l_sizeB { get; set; }
        public string l_sizeC { get; set; }
        public string l_sizeD { get; set; }
        public string l_sizeE { get; set; }
        public string l_sizeF { get; set; }
        public string l_sizeG { get; set; }
        public int l_num { get; set; }
        public float l_fpos1 { get; set; }
        public float l_fpos2 { get; set; }
        public float l_fpos3 { get; set; }
        public float l_fpos4 { get; set; }
        public float l_fpos5 { get; set; }
        public float l_fpos6 { get; set; }
        public float l_fpos7 { get; set; }
        public float lock_position { get; set; }
        public int l_itype1 { get; set; }
        public int l_itype2 { get; set; }
        public int l_itype3 { get; set; }
        public int l_itype4 { get; set; }
        public int l_itype5 { get; set; }
        public int l_itype6 { get; set; }
        public int l_itype7 { get; set; }

        public float lead_CWidthf { get; set; }
        public float lead_CHeightf { get; set; }
        public string lead_CWidths { get; set; }
        public string lead_CHeights { get; set; }
        public bool glass_complete { get; set; }
        public int replace_glass { get; set; }
        public int HeaderId { get; set; }
        public int Id { get; set; }
    }
}
