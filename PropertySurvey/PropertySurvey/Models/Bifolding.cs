using SQLite;
namespace PropertySurvey
{
    public class BifoldTable
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string udi_cont { get; set; }				// Key field contract number
        public int item_number { get; set; }
        public string internal_width { get; set; }
        public string internal_height { get; set; }
        public string overall_width { get; set; }
        public string overall_height { get; set; }
        public int opens { get; set; }					    // in / out
        public int trickle_vents { get; set; }			    // yes/no
        public string hardware { get; set; }
        public string color_internal { get; set; }
        public string color_external { get; set; }
        public string threshold_type { get; set; }          // renamed from sill_type
        public int no_of_pics { get; set; }
        public int no_of_photos { get; set; }
        public int no_of_vids { get; set; }
        public int isComplete { get; set; }
        public string comments { get; set; }
        public int bifold_signed { get; set; }              // renamed from i_spare1
        public int number_of_doors { get; set; }            // renamed from i_spare3
        public string cause_of_damage { get; set; }
        public string cause_of_damage_reason_different { get; set; }
        public string door_type { get; set; }               // door type eg. warmcore - renamed from s_spare2
        public string glazing_options { get; set; }         // renamed from s_spare3
        public string number_of_doors_text { get; set; }    // renamed from s_spare4
        public string colour_of_doors { get; set; }         // renamed from s_spare5
        public string handle_colour { get; set; }           // renamed from s_spare6
        public string cill_type { get; set; }               // renamed from s_spare7
        public string knock_on { get; set; }                // renamed from s_spare9
        public string internal_door_colour { get; set; }    // renamed from s_spare10
        public string s_spare12 { get; set; }
        public string parts_to_order { get; set; }          // renamed from new_sspare4
        public string type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public int point_of_entry { get; set; }             // renamed from ex_i_spare6
        public string ChangeItemTo { get; set; }
        public string print_name { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public bool glass_complete { get; set; }
        public int replace_glass { get; set; }
        public string reason_not_repaired { get; set; }
        public bool bRepair { get; set; }
        public bool fensa { get; set; }
        public string WER_rating { get; set; }
        public int gaskets { get; set; }
        public string gaskets_text { get; set; }
        public int handles_req { get; set; }
        public bool bHandleDrawingComplete { get; set; }
        public string handles_text { get; set; }
        public int addons { get; set; }                     // Added 25/9/19
        public string addon_width { get; set; }			    // enabled if addons==TRUE
        public string addon_height { get; set; }            // enabled if addons==TRUE
        public int HeaderId { get; set; }
        public int Id { get; set; }
    }
}
