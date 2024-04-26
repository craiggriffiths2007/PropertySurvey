using System.ComponentModel.DataAnnotations;

namespace PropertySurveyService.Models
{
    public class GreenTable
    {
        [Key]
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public string? udi_cont { get; set; }
        public int item_number { get; set; }
        public int isComplete { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public string? cause_of_damage { get; set; }
        public string? cause_of_damage_reason_different { get; set; }
        public string? rep_reason { get; set; }
        public string? material_type { get; set; }
        public string? colour { get; set; }
        public string? glaze_type { get; set; }
        public string? base_size { get; set; }
        public string? base_size_x { get; set; }
        public string? base_size_y { get; set; }
        public string? type_of_glass { get; set; }
        public string? door_opening_type { get; set; }
        public string? window_opening_type { get; set; }
        public int roof_opening_lights { get; set; }
        public int auto_or_manual { get; set; }
        public string? overall_height { get; set; }
        public string? summary { get; set; }
        public int no_of_pics { get; set; }
        public int no_of_photos { get; set; }
        public int no_of_vids { get; set; }
        public string? parts_to_order { get; set; }
        public string? point_of_entry { get; set; }
        public string? type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public string? ChangeItemTo { get; set; }
        public string? print_name { get; set; }
        public bool glass_complete { get; set; }
        public int replace_glass { get; set; }
        public int repair_or_replace { get; set; }

        public SurveyItem AsSurveyItem() { return new SurveyItem(Id, enum_item_type.green); }
    }


}
