using System.ComponentModel.DataAnnotations;

namespace PropertySurveyService.Models
{
    public class GarageTable
    {
        [Key]
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public string? udi_cont { get; set; }
        public int item_number { get; set; }
        public int opening_direction { get; set; }
        public string? cause_of_damage { get; set; }
        public string? cause_of_damage_reason_different { get; set; }
        public int door_fits_into { get; set; }
        public int new_subframe_req { get; set; }
        public int obstruction_outside_b { get; set; }
        public string? obstruction_outside { get; set; }
        public int obstruction_inside_b { get; set; }
        public string? obstruction_inside { get; set; }
        public string? actual_door_width { get; set; }
        public string? actual_door_height { get; set; }
        public int frame_fix_type { get; set; }
        public string? type_of_garage { get; set; }
        public string? new_electric_operator_req { get; set; }
        public string? side_size_A { get; set; }
        public string? side_size_B { get; set; }
        public string? side_size_C { get; set; }
        public string? side_size_D { get; set; }
        public string? side_size_E { get; set; }
        public string? side_size_F { get; set; }
        public string? side_size_G { get; set; }
        public string? side_timber_1 { get; set; }
        public string? side_timber_2 { get; set; }
        public string? plan_size_A { get; set; }
        public string? plan_size_B { get; set; }
        public string? plan_size_C1 { get; set; }
        public string? plan_size_C2 { get; set; }
        public string? plan_size_D { get; set; }
        public string? plan_timber_1 { get; set; }
        public string? plan_timber_2 { get; set; }
        public string? color { get; set; }
        public string? opening_type { get; set; }
        public string? finish { get; set; }
        public int power_points { get; set; }
        public int electric_door { get; set; }
        public int handle_outside { get; set; }
        public int other_access { get; set; }
        public int need_safety_release { get; set; }
        public int no_of_pics { get; set; }
        public int no_of_photos { get; set; }
        public int insulated { get; set; }
        public int door_stuck_shut { get; set; }
        public int motor_position { get; set; }
        public int no_of_vids { get; set; }
        public bool bDifferentFromOriginal { get; set; }
        public string? ChangeItemTo { get; set; }
        public string? print_name { get; set; }
        public string? long_comments { get; set; }
        public int isComplete { get; set; }
        public int door_within_perimeter { get; set; }
        public int socket_within_1m { get; set; }
        public string? wire_type { get; set; }
        public int colour_match_roll_box { get; set; }
        public bool additional_drawn { get; set; }
        public string? roller_door_type { get; set; }
        public string? roller_box_type { get; set; }
        public string? parts_to_order { get; set; }
        public string? point_of_entry { get; set; }
        public string? type_of_lockng_system_required { get; set; }
        public int was_it_locked { get; set; }
        public string? where_is_garage { get; set; }

        public SurveyItem AsSurveyItem() { return new SurveyItem(Id, enum_item_type.garage); }

    }

}
