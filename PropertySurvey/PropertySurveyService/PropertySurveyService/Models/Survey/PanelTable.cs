using System.ComponentModel.DataAnnotations;

namespace PropertySurveyService.Models
{
    public class PanelTable
    {
        [Key]
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public string? udi_cont { get; set; }
        public int item_number { get; set; }
        public int isComplete { get; set; }
        public string? cause_of_damage { get; set; }
        public string? cause_of_damage_reason_different { get; set; }
        public string? knockedit { get; set; }
        public string? knocoledit { get; set; }
        public string? letteredit { get; set; }
        public string? letter_box_pos { get; set; }
        public string? wedit { get; set; }
        public string? hedit { get; set; }
        public string? typeedit { get; set; }
        public string? thickedit { get; set; }
        public string? backgedit { get; set; }
        public string? coledit { get; set; }
        public string? gltext { get; set; }
        public string? spaccoloedit { get; set; }
        public string? pet_flap { get; set; }
        public string? pet_type { get; set; }
        public int pet_magnetic { get; set; }
        public int no_of_pics { get; set; }
        public int no_of_photos { get; set; }
        public int no_of_vids { get; set; }
        public string? room_location { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public string? ChangeItemTo { get; set; }
        public string? print_name { get; set; }
        public string? long_sptext { get; set; }
        public int upvc_item_number { get; set; }
        public int alum_item_number { get; set; }
        public string? parts_to_order { get; set; }
        public string? point_of_entry { get; set; }
        public string? type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }

        public SurveyItem AsSurveyItem() { return new SurveyItem(Id, enum_item_type.panel); }
    }
}
