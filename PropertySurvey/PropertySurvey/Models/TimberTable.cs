using SQLite;
namespace PropertySurvey
{
    public class TimberTable
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string udi_cont { get; set; }		// Key fieled contract number			CONTRACT NUMBER
        public int item_number { get; set; }
        public int isComplete { get; set; }
        public bool bRepair { get; set; }
        public int cosmetic_damage { get; set; }
        public string additional_locks { get; set; }
        public int gaskets { get; set; }
        public string gaskets_text { get; set; }
        public int handles_req { get; set; }
        public string handles_text { get; set; }
        public string replace_reason { get; set; }
        public string replace_explain { get; set; }
        public string timber_item { get; set; }			// multi choice drop down
        public string cause_of_damage { get; set; }
        public string cause_of_damage_reason_different { get; set; }
        public string timber_wood { get; set; }
        public string timber_frame_wood { get; set; }
        public int timber_new_frame_req { get; set; }
        public string brick_width { get; set; }	// numeric only
        public string brick_height { get; set; }		// numeric only
        public string internal_width { get; set; }	// "
        public string internal_height { get; set; }// "
        public int repair_frame { get; set; }				// spinner
        public string door_thickness { get; set; }		// numeric
        public string door_width { get; set; }		//"
        public string door_height { get; set; }			//"
        public int opens { get; set; }						// spinner ( In/Out )
        public int new_sash_required { get; set; }		// spinner ( Yes/No )
        public int head_drip { get; set; }					// "
        public string cills { get; set; }					// multi choice drop down + other
        public int draught_strip { get; set; }				// spinner ( yes/no )
        public string pet_flap { get; set; }
        public string pet_type { get; set; }
        public int pet_magnetic { get; set; }
        public bool bDoorComplete { get; set; }
        public bool bWindowComplete { get; set; }
        public int beading_type { get; set; }				// Now used for fire rated door
        public int thresher { get; set; }					// spinner
        public int single_double { get; set; }				// spinner ( single/double )
        public string trickle_vents { get; set; }			// multi choice drop down + other
        public string locks { get; set; }					// multi choice drop down + other
        public string hardware_color { get; set; }		// multi choice drop down + other
        public string door_color { get; set; }			// multi choice drop down + other
        public string frame_color { get; set; }			// multi choice drop down + other
        public string spacer_thickness { get; set; }
        public string spacer_color { get; set; }
        public string glass_type { get; set; }
        public string glass_pattern { get; set; }
        public string special_glass { get; set; }
        public int bNewLockingMech { get; set; }			// 0-not selected 1-Yes 2-No
        public bool bLockComplete { get; set; }				// Is the locking mechanism complete ????????????
        public bool bHandleDrawingComplete { get; set; }
        public int no_of_pics { get; set; }// Number of pictures
        public int no_of_photos { get; set; }	// Number of pictures
        public int no_of_vids { get; set; }
        public string docl { get; set; }
        public bool bSashDrawn { get; set; }
        public bool bSectionDrawn { get; set; }
        public bool bMouldingDrawn { get; set; }
        public string room_location { get; set; }
        public string doc_l_compliant_reason { get; set; } // Changed to this to reuse old memory
        public int doc_l_compliant { get; set; }
        public string door_color_out { get; set; }			// multi choice drop down + other
        public string frame_color_out { get; set; }		// multi choice drop down + other
        public string door_color_code { get; set; }
        public string door_color_code_out { get; set; }
        public string frame_color_code { get; set; }
        public string frame_color_code_out { get; set; }
        public bool b_signed { get; set; }
        public int slide_position { get; set; }
        public int timber_glazed { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public string ChangeItemTo { get; set; }
        public string print_name { get; set; }
        public string standard_sizes { get; set; }
        public string reasonnonstandard { get; set; }
        public bool Fensa { get; set; } // Renamed from bFencer
        public string WER_rating { get; set; } // Renamed from FecerRating
        public string long_timber_comments { get; set; }
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
        public string lock_make { get; set; }
        public string lock_codes { get; set; }
        public int GearBox { get; set; }
        public int left_bolt { get; set; }
        public int right_bolt { get; set; }
        public string letter_box { get; set; }
        public string letter_box_pos { get; set; }
        public string moulding { get; set; }
        public string hinge_type { get; set; }
        public int collect_and_copy { get; set; } // Renamed from i_spare1
        public int temporary { get; set; }        // Renamed from i_spare2
        public int pre_glazed_door { get; set; }  // Renamed from i_spare3
        public string lead_comments { get; set; }
        public int weather_bar { get; set; } // Renamed from new_ispare3
        public string parts_to_order { get; set; } // Renamed from new_sspare4
        public int is_a_flat { get; set; }
        public string point_of_entry { get; set; }
        public string type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public string back_to_back_spacer_width { get; set; }    // Spacer Thickness - Renamed from ex_s_spare4
        public string back_to_back_spacer_height { get; set; }   // Overall Spacer Width - Renamed from ex_s_spare5
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
