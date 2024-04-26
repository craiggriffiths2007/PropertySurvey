using System.ComponentModel.DataAnnotations;

namespace PropertySurveyService.Models
{
    public class GlassTable
    {
        [Key]
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public string? udi_cont { get; set; }
        public int item_number { get; set; }
        public int isComplete { get; set; }
        public string? cause_of_damage { get; set; }
        public string? cause_of_damage_reason_different { get; set; }
        public int units_required { get; set; }
        public string? glass_width { get; set; }
        public string? glass_height { get; set; }
        public string? glass_width2 { get; set; }
        public string? glass_height2 { get; set; }
        public string? glass_width3 { get; set; }
        public string? glass_height3 { get; set; }
        public string? glass_width4 { get; set; }
        public string? glass_height4 { get; set; }
        public string? glass_width5 { get; set; }
        public string? glass_height5 { get; set; }
        public string? glass_width6 { get; set; }
        public string? glass_height6 { get; set; }
        public string? glass_width7 { get; set; }
        public string? glass_height7 { get; set; }
        public string? glass_width8 { get; set; }
        public string? glass_height8 { get; set; }
        public int stepped_unit { get; set; }
        public string? int_width { get; set; }
        public string? int_height { get; set; }
        public int single_or_double { get; set; }
        public string? glass_type { get; set; }
        public string? sizeA { get; set; }
        public string? sizeB { get; set; }
        public string? sizeC { get; set; }
        public string? sizeD { get; set; }
        public string? lead_CWidth { get; set; }
        public string? lead_CHeight { get; set; }
        public int lead_anti_rattle { get; set; }
        public string? lead_thickness { get; set; }
        public string? lead_sod { get; set; }
        public string? lead_type { get; set; }
        public bool lead_bDiamondComplete { get; set; }
        public bool lead_bGeorgianComplete { get; set; }
        public bool lead_bBarComplete { get; set; }
        public string? glass_pattern { get; set; }
        public string? spacer_color { get; set; }
        public string? spacer_thickness { get; set; }
        public string? special_glass { get; set; }
        public int no_of_pics { get; set; }
        public string? docl_old { get; set; }
        public int no_of_photos { get; set; }
        public int gb_trim { get; set; }
        public string? docl { get; set; }
        public string? room_location { get; set; }
        public int no_of_vids { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public string? ChangeItemTo { get; set; }
        public string? print_name { get; set; }
        public string? ProductInto { get; set; }
        public string? glazing_type { get; set; }
        public string? long_comments { get; set; }
        public float lead_posX { get; set; }
        public float lead_posY { get; set; }
        public string? TapeorGasket { get; set; }
        public int glaze { get; set; }
        public string? lead_comments { get; set; }
        public int collect_and_copy { get; set; }
        public int temporary { get; set; }
        public string? parts_to_order { get; set; }
        public string? point_of_entry { get; set; }
        public string? type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public string? back_to_back_spacer_width { get; set; }
        public string? back_to_back_spacer_height { get; set; }
        public float lead_CWidthf { get; set; }
        public float lead_CHeightf { get; set; }
        public float sizeAf { get; set; }
        public float sizeBf { get; set; }
        public float sizeCf { get; set; }
        public float sizeDf { get; set; }
        public string? lead_CWidths { get; set; }
        public string? lead_CHeights { get; set; }
        public int parent_item { get; set; }

        public SurveyItem AsSurveyItem() { return new SurveyItem(Id, enum_item_type.glass); }
    }

}
