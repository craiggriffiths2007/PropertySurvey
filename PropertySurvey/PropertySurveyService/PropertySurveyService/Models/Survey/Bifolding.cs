using System.ComponentModel.DataAnnotations;

namespace PropertySurveyService.Models
{
    public class BifoldTable
    {
        [Key]
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public string? udi_cont { get; set; }
        public int item_number { get; set; }
        public string? ernal_width { get; set; }
        public string? ernal_height { get; set; }
        public string? overall_width { get; set; }
        public string? overall_height { get; set; }
        public int opens { get; set; }
        public int trickle_vents { get; set; }
        public string? hardware { get; set; }
        public string? color_ernal { get; set; }
        public string? color_external { get; set; }
        public string? threshold_type { get; set; }
        public int no_of_pics { get; set; }
        public int no_of_photos { get; set; }
        public int no_of_vids { get; set; }
        public int isComplete { get; set; }
        public string? comments { get; set; }
        public int bifold_signed { get; set; }
        public int number_of_doors { get; set; }
        public string? cause_of_damage { get; set; }
        public string? cause_of_damage_reason_different { get; set; }
        public string? door_type { get; set; }
        public string? glazing_options { get; set; }
        public string? number_of_doors_text { get; set; }
        public string? colour_of_doors { get; set; }
        public string? handle_colour { get; set; }
        public string? cill_type { get; set; }
        public string? knock_on { get; set; }
        public string? ernal_door_colour { get; set; }
        public string? s_spare12 { get; set; }
        public string? parts_to_order { get; set; }
        public string? type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public int point_of_entry { get; set; }
        public string? ChangeItemTo { get; set; }
        public string? pr_name { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public bool glass_complete { get; set; }
        public int replace_glass { get; set; }
        public string? reason_not_repaired { get; set; }
        public bool bRepair { get; set; }
        public bool fensa { get; set; }
        public string? WER_rating { get; set; }
        public int gaskets { get; set; }
        public string? gaskets_text { get; set; }
        public int handles_req { get; set; }
        public bool bHandleDrawingComplete { get; set; }
        public string? handles_text { get; set; }
        public int addons { get; set; }
        public string? addon_width { get; set; }
        public string? addon_height { get; set; }

        public SurveyItem AsSurveyItem() { return new SurveyItem(Id, enum_item_type.bifold); }

    }
}
