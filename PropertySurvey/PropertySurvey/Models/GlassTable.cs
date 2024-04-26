using SQLite;
namespace PropertySurvey
{
    public class GlassTable
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string udi_cont { get; set; }
        public int item_number { get; set; }
        public int isComplete { get; set; }
        public string cause_of_damage { get; set; }
        public string cause_of_damage_reason_different { get; set; }
        public int units_required { get; set; }
        public string glass_width { get; set; }
        public string glass_height { get; set; }
        public string glass_width2 { get; set; }
        public string glass_height2 { get; set; }
        public string glass_width3 { get; set; }
        public string glass_height3 { get; set; }
        public string glass_width4 { get; set; }
        public string glass_height4 { get; set; }
        public string glass_width5 { get; set; }
        public string glass_height5 { get; set; }
        public string glass_width6 { get; set; }
        public string glass_height6 { get; set; }
        public string glass_width7 { get; set; }
        public string glass_height7 { get; set; }
        public string glass_width8 { get; set; }
        public string glass_height8 { get; set; }
        public int stepped_unit { get; set; }
        public string int_width { get; set; }
        public string int_height { get; set; }
        public int single_or_double { get; set; }
        public string glass_type { get; set; }
        public string sizeA { get; set; }
        public string sizeB { get; set; }
        public string sizeC { get; set; }
        public string sizeD { get; set; }
        public string lead_CWidth { get; set; }
        public string lead_CHeight { get; set; }
        public int lead_anti_rattle { get; set; }
        public string lead_thickness { get; set; }
        public string lead_sod { get; set; }
        public string lead_type { get; set; }
        public bool lead_bDiamondComplete { get; set; }
        public bool lead_bGeorgianComplete { get; set; }
        public bool lead_bBarComplete { get; set; }
        public string glass_pattern { get; set; }
        public string spacer_color { get; set; }
        public string spacer_thickness { get; set; }
        public string special_glass { get; set; }
        public int no_of_pics { get; set; }		// Number of pictures
        public string docl_old { get; set; }	// no longer used
        public int no_of_photos { get; set; }		// Number of pictures
        public int gb_trim { get; set; }			// Georgian bar trim
        public string docl { get; set; }
        public string room_location { get; set; }
        public int no_of_vids { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public string ChangeItemTo { get; set; }
        public string print_name { get; set; }
        public string ProductInto { get; set; }
        public string glazing_type { get; set; } // Renamed from JointType, sent to David as JointType
        public string long_comments { get; set; }
        public float lead_posX { get; set; }
        public float lead_posY { get; set; }
        public string TapeorGasket { get; set; }
        public int glaze { get; set; } // Renamed from i_spare3
        public string lead_comments { get; set; }
        public int collect_and_copy { get; set; } // Renamed from new_ispare1
        public int temporary { get; set; } // Renamed from new_ispare2
        public string parts_to_order { get; set; } // Renamed from new_sspare4
        public string point_of_entry { get; set; }
        public string type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public string back_to_back_spacer_width { get; set; }    // Spacer Thickness - Renamed from ex_s_spare4
        public string back_to_back_spacer_height { get; set; }   // Overall Spacer Width - Renamed from ex_s_spare5
        public float lead_CWidthf { get; set; }
        public float lead_CHeightf { get; set; }
        public float sizeAf { get; set; }
        public float sizeBf { get; set; }
        public float sizeCf { get; set; }
        public float sizeDf { get; set; }
        public string lead_CWidths { get; set; }
        public string lead_CHeights { get; set; }
        public int parent_item { get; set; } // 0=none,1=alum,2=bifold,3=comp,4=cons,5=green,6=timber,7=upvc
        public int HeaderId { get; set; }
        public int Id { get; set; }
    }
}
