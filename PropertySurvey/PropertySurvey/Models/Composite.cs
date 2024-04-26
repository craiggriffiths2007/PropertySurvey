using SQLite;
namespace PropertySurvey
{
    public class CompositeTable
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string udi_cont { get; set; }				// Key fieled contract number
        public int item_number { get; set; }
        public int isComplete { get; set; }
        public string cause_of_damage { get; set; }
        public string cause_of_damage_reason_different { get; set; }
        public string door_make { get; set; }
        public int opens { get; set; }
        public int is_lock { get; set; }
        public string frame_colour_inside { get; set; }
        public string frame_colour_outside { get; set; }
        public string door_colour_inside { get; set; }
        public string door_colour_outside { get; set; }
        public string door_design { get; set; }
        public string glass_design { get; set; }
        public string internal_width { get; set; }
        public string internal_height { get; set; }
        public string brick_width { get; set; }
        public string brick_height { get; set; }
        public string trickle_vents { get; set; }
        public int addons { get; set; }
        public string addons_height { get; set; }
        public string addons_width { get; set; }
        public string handle_colour { get; set; }
        public string threshold_type { get; set; }
        public int lever_pad_handles { get; set; }
        public string glass_pattern { get; set; }
        public string glass_type { get; set; }
        public string spacer_thickness { get; set; }
        public string spacer_colour { get; set; }
        public int profile_type { get; set; }
        public string room_location { get; set; }
        public string special_glass { get; set; }
        public string comments { get; set; }
        public int lead_CWidth { get; set; }
        public int lead_CHeight { get; set; }
        public int lead_anti_rattle { get; set; }
        public string lead_thickness { get; set; }
        public string lead_sod { get; set; }
        public string lead_type { get; set; }
        public string docl { get; set; }
        public string letteredit { get; set; }
        public string letter_box_pos { get; set; }
        public string pet_flap { get; set; }
        public string pet_type { get; set; }
        public int pet_magnetic { get; set; }
        public int glaze { get; set; }
        public string print_name { get; set; }
        public bool lead_bDiamondComplete { get; set; }
        public bool lead_bGeorgianComplete { get; set; }
        public bool lead_bBarComplete { get; set; }
        public int no_of_pics { get; set; }
        public int no_of_photos { get; set; }
        public int no_of_vids { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public string lock_other_text { get; set; }
        public int head_drip { get; set; }
        public string ChangeItemTo { get; set; }
        public string cills { get; set; }
        public string door_wood { get; set; }
        public int hinged_on { get; set; }                  // Renamed from i_spare1
        public string reason_not_repaired { get; set; }     // Renamed from s_spare3
        public string lead_comments { get; set; }
        public string parts_to_order { get; set; }          // Renamed from new_sspare4
        public int is_a_flat { get; set; }
        public string point_of_entry { get; set; }
        public string type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public int fire_door { get; set; }                  // Renamed from ex_new_ispare4
        public float lead_CWidthf { get; set; }
        public float lead_CHeightf { get; set; }
        public string lead_CWidths { get; set; }
        public string lead_CHeights { get; set; }
        public bool glass_complete { get; set; }
        public int replace_glass { get; set; }
        public bool bRepair { get; set; }
        public bool fensa { get; set; }
        public string WER_rating { get; set; }
        public int gaskets { get; set; }
        public string gaskets_text { get; set; }
        public int handles_req { get; set; }
        public bool bHandleDrawingComplete { get; set; }
        public string handles_text { get; set; }
        public int HeaderId { get; set; }
        public int Id { get; set; }
    }
}
