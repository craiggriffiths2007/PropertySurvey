using System.ComponentModel.DataAnnotations;

namespace PropertySurveyService.Models
{ 
    public class ConsTable
    {
        [Key]
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public string? udi_cont { get; set; }
        public int item_number { get; set; }
        public int isComplete { get; set; }
        public string? type { get; set; }
        public string? cause_of_damage { get; set; }
        public string? cause_of_damage_reason_different { get; set; }
        public int material_type { get; set; }
        public string? sizeA { get; set; }
        public string? sizeB { get; set; }
        public string? sizeC { get; set; }
        public string? sizeD { get; set; }
        public string? sizeE { get; set; }
        public string? sizeF { get; set; }
        public string? sizeG { get; set; }
        public string? angle1 { get; set; }
        public string? angle2 { get; set; }
        public string? angle3 { get; set; }
        public string? angle4 { get; set; }
        public string? pitch_height { get; set; }
        public string? profile_section_size { get; set; }
        public string? sheet_width_1 { get; set; }
        public string? sheet_height_1 { get; set; }
        public string? sheet_width_2 { get; set; }
        public string? sheet_height_2 { get; set; }
        public string? sheet_width_3 { get; set; }
        public string? sheet_height_3 { get; set; }
        public string? sheet_width_4 { get; set; }
        public string? sheet_height_4 { get; set; }
        public string? sheet_width_5 { get; set; }
        public string? sheet_height_5 { get; set; }
        public string? sheet_width_6 { get; set; }
        public string? sheet_height_6 { get; set; }
        public string? sheet_width_7 { get; set; }
        public string? sheet_height_7 { get; set; }
        public string? sheet_width_8 { get; set; }
        public string? sheet_height_8 { get; set; }
        public string? sheet_width_9 { get; set; }
        public string? sheet_height_9 { get; set; }
        public string? sheet_width_10 { get; set; }
        public string? sheet_height_10 { get; set; }
        public string? flute_size { get; set; }
        public string? color { get; set; }
        public string? roof_color { get; set; }
        public int new_firrings_req { get; set; }
        public int new_gutters_req { get; set; }
        public string? roof_glazing_thickness { get; set; }
        public int no_of_pics { get; set; }
        public int no_of_photos { get; set; }
        public string? room_location { get; set; }
        public int no_of_vids { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public string? ChangeItemTo { get; set; }
        public string? print_name { get; set; }
        public string? wall_pos { get; set; }
        public string? pitch_degree { get; set; }
        public string? long_comments { get; set; }
        public int bDrawingsOnly { get; set; }
        public bool cons_roof_under_drawn { get; set; }
        public int does_roof_fit_under { get; set; }
        public int spars_line_up { get; set; }
        public int roof_sheets_quantity_1 { get; set; }
        public int roof_sheets_quantity_2 { get; set; }
        public int roof_sheets_quantity_3 { get; set; }
        public int roof_sheets_quantity_4 { get; set; }
        public int roof_sheets_quantity_5 { get; set; }
        public int roof_sheets_quantity_6 { get; set; }
        public int roof_sheets_quantity_7 { get; set; }
        public int roof_sheets_quantity_8 { get; set; }
        public int roof_sheets_quantity_9 { get; set; }
        public int roof_sheets_quantity_10 { get; set; }
        public int good_conditions { get; set; }
        public string? ridge_length { get; set; }
        public string? parts_to_order { get; set; }
        public string? point_of_entry { get; set; }
        public string? type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public bool glass_complete { get; set; }
        public int replace_glass { get; set; }
        public string? reason_not_repaired { get; set; }
        public bool bRepair { get; set; }
        public bool fensa { get; set; }
        public string? WER_rating { get; set; }
        public string? overall_length_of_sheet { get; set; }

        public SurveyItem AsSurveyItem() { return new SurveyItem(Id, enum_item_type.cons); }
    }

}
