using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySurvey
{
    public class TableInitialization
    {
        public void CreatePanel()
        {
            App.net.PanelRecord = new PanelTable();

            App.net.PanelRecord.point_of_entry = "";
            App.net.PanelRecord.type_of_lockng_system_required = "";
            //App.net.PanelRecord.point_of_entry_b = "";
            //App.net.PanelRecord.ex_s_spare4 = "";
            //App.net.PanelRecord.ex_s_spare5 = "";
            //App.net.PanelRecord.ex_s_spare6 = "";

            App.net.PanelRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.PanelRecord.item_number = ++App.net.HeaderRecord.current_item_number;

            App.net.PanelRecord.item_number = App.net.HeaderRecord.current_item_number;

            App.net.PanelRecord.cause_of_damage = "";
            App.net.PanelRecord.cause_of_damage_reason_different = "";
            App.net.PanelRecord.knockedit = "";
            App.net.PanelRecord.knocoledit = "";
            App.net.PanelRecord.letteredit = "";
            App.net.PanelRecord.letter_box_pos = "";
            App.net.PanelRecord.wedit = "";
            App.net.PanelRecord.hedit = "";
            App.net.PanelRecord.typeedit = "";
            App.net.PanelRecord.thickedit = "";
            App.net.PanelRecord.backgedit = "";
            App.net.PanelRecord.coledit = "";
            App.net.PanelRecord.gltext = "";
            App.net.PanelRecord.spaccoloedit = "";
            App.net.PanelRecord.pet_flap = "";
            App.net.PanelRecord.pet_type = "";
            //App.net.PanelRecord.sptext = "";
            //App.net.PanelRecord.type = "";
            //App.net.PanelRecord.codeedit = "";
            App.net.PanelRecord.room_location = "";
            App.net.PanelRecord.ChangeItemTo = "";
            App.net.PanelRecord.print_name = "";
            App.net.PanelRecord.long_sptext = "";
            App.net.PanelRecord.alum_item_number = 0;
            App.net.PanelRecord.upvc_item_number = 0;
            //App.net.PanelRecord.SpacerType = "";
            App.net.loaded_item_number = App.net.PanelRecord.item_number;
            App.net.root_item_number = App.net.PanelRecord.item_number;

            //App.net.PanelRecord.s_spare1 = "";
            //App.net.PanelRecord.s_spare2 = "";
            //App.net.PanelRecord.s_spare3 = "";

            //App.net.PanelRecord.new_sspare1 = "";
            //App.net.PanelRecord.new_sspare2 = "";
            //App.net.PanelRecord.new_sspare3 = "";
            App.net.PanelRecord.parts_to_order = "";
            //App.net.PanelRecord.new_sspare5 = "";
            //App.net.PanelRecord.new_sspare6 = "";
            //App.net.PanelRecord.new_sspare7 = "";
            //App.net.PanelRecord.new_sspare8 = "";
            //App.net.PanelRecord.new_sspare9 = "";
            //App.net.PanelRecord.new_sspare10 = "";

            //App.net.PanelRecord.i_rep = 0;
            //App.net.PanelRecord.s_rep = "";

            /*
                        App.net.PanelRecord.i_spare1 = pt.i_spare1;
                        App.net.PanelRecord.i_spare2 = pt.i_spare2;
                        App.net.PanelRecord.i_spare3 = pt.i_spare3;

                        App.net.PanelRecord.s_spare1 = pt.s_spare1;
                        App.net.PanelRecord.s_spare2 = pt.s_spare2;
                        App.net.PanelRecord.s_spare3 = pt.s_spare3;
                        */
        }

        public void CreateGarage()
        {
            App.net.GarageRecord = new GarageTable();
            App.net.GarageRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.GarageRecord.item_number = ++App.net.HeaderRecord.current_item_number;
            App.net.GarageRecord.point_of_entry = "";
            App.net.GarageRecord.type_of_lockng_system_required = "";
            //App.net.GarageRecord.point_of_entry_b = "";
            //App.net.GarageRecord.ex_s_spare4 = "";
            //App.net.GarageRecord.ex_s_spare5 = "";
            //App.net.GarageRecord.ex_s_spare6 = "";
            App.net.GarageRecord.cause_of_damage = "";
            App.net.GarageRecord.cause_of_damage_reason_different = "";
            App.net.GarageRecord.obstruction_outside = "";
            App.net.GarageRecord.obstruction_inside = "";
            App.net.GarageRecord.actual_door_width = "";
            App.net.GarageRecord.actual_door_height = "";
            App.net.GarageRecord.type_of_garage = "";
            App.net.GarageRecord.side_size_A = "";
            App.net.GarageRecord.side_size_B = "";
            App.net.GarageRecord.side_size_C = "";
            App.net.GarageRecord.side_size_D = "";
            App.net.GarageRecord.side_size_E = "";
            App.net.GarageRecord.side_size_F = "";
            App.net.GarageRecord.side_timber_1 = "";
            App.net.GarageRecord.side_timber_2 = "";
            App.net.GarageRecord.plan_size_A = "";
            App.net.GarageRecord.plan_size_B = "";
            App.net.GarageRecord.plan_size_C1 = "";
            App.net.GarageRecord.plan_size_C2 = "";
            App.net.GarageRecord.plan_size_D = "";
            App.net.GarageRecord.plan_timber_1 = "";
            App.net.GarageRecord.plan_timber_2 = "";
            App.net.GarageRecord.color = "";
            App.net.GarageRecord.opening_type = "";
            App.net.GarageRecord.finish = "";
            //App.net.GarageRecord.timber_section_size = "";
            //App.net.GarageRecord.comments = "";
            //App.net.GarageRecord.docl = "";
            App.net.GarageRecord.side_size_G = "";
            //App.net.GarageRecord.room_location = "";
            App.net.GarageRecord.ChangeItemTo = "";
            App.net.GarageRecord.print_name = "";
            //App.net.GarageRecord.over_guide_size = "";
            //App.net.GarageRecord.track_height = "";
            App.net.GarageRecord.long_comments = "";
            App.net.GarageRecord.new_electric_operator_req = "";
            //App.net.GarageRecord.s_spare1 = "";
            //App.net.GarageRecord.s_spare2 = "";
            App.net.GarageRecord.wire_type = "";
            App.net.GarageRecord.roller_door_type = "";
            App.net.GarageRecord.roller_box_type = "";
            //App.net.GarageRecord.new_sspare3 = "";
            App.net.GarageRecord.parts_to_order = "";
            //App.net.GarageRecord.new_sspare5 = "";
            //App.net.GarageRecord.new_sspare6 = "";
            //App.net.GarageRecord.new_sspare7 = "";
            //App.net.GarageRecord.new_sspare8 = "";
            //App.net.GarageRecord.new_sspare9 = "";
            //App.net.GarageRecord.new_sspare10 = "";
            App.net.loaded_item_number = App.net.GarageRecord.item_number;
            App.net.root_item_number = App.net.GarageRecord.item_number;
        }

        public void CreateGreenhouse()
        {
            App.net.GreenRecord = new GreenTable();

            App.net.GreenRecord.point_of_entry = "";
            App.net.GreenRecord.type_of_lockng_system_required = "";
            //App.net.GreenRecord.point_of_entry_b = "";
            //App.net.GreenRecord.ex_s_spare4 = "";
            //App.net.GreenRecord.ex_s_spare5 = "";
            //App.net.GreenRecord.ex_s_spare6 = "";

            App.net.GreenRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.GreenRecord.item_number = ++App.net.HeaderRecord.current_item_number;

            App.net.loaded_item_number = App.net.GreenRecord.item_number;
            App.net.root_item_number = App.net.GreenRecord.item_number;

            App.net.GreenRecord.cause_of_damage = "";
            App.net.GreenRecord.cause_of_damage_reason_different = "";
            App.net.GreenRecord.rep_reason = "";
            App.net.GreenRecord.material_type = "";
            App.net.GreenRecord.colour = "";
            App.net.GreenRecord.glaze_type = "";
            App.net.GreenRecord.base_size = "";
            App.net.GreenRecord.base_size_x = "";
            App.net.GreenRecord.base_size_y = "";
            App.net.GreenRecord.door_opening_type = "";
            App.net.GreenRecord.window_opening_type = "";
            App.net.GreenRecord.overall_height = "";
            //App.net.GreenRecord.overall_height_inches = "";
            App.net.GreenRecord.summary = "";
            App.net.GreenRecord.type_of_glass = "";

            //App.net.GreenRecord.s_spare1 = "";
            //App.net.GreenRecord.s_spare2 = "";
            //App.net.GreenRecord.s_spare3 = "";

            //App.net.GreenRecord.new_sspare1 = "";
            //App.net.GreenRecord.new_sspare2 = "";
            //App.net.GreenRecord.new_sspare3 = "";
            App.net.GreenRecord.parts_to_order = "";
            //App.net.GreenRecord.new_sspare5 = "";
            //App.net.GreenRecord.new_sspare6 = "";
            //App.net.GreenRecord.new_sspare7 = "";
            //App.net.GreenRecord.new_sspare8 = "";
            //App.net.GreenRecord.new_sspare9 = "";
            //App.net.GreenRecord.new_sspare10 = "";
            App.net.GreenRecord.glass_complete = false;
            App.net.GreenRecord.replace_glass = 0;
            App.net.GreenRecord.repair_or_replace = 0;
        }

        public void CreateGlass()
        {
            App.net.GlassRecord = new GlassTable();
            App.net.GlassRecord.RecID = App.net.GlassRecord.RecID;
            App.net.GlassRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.GlassRecord.item_number = ++App.net.HeaderRecord.current_item_number;

            App.net.GlassRecord.point_of_entry = "";
            App.net.GlassRecord.type_of_lockng_system_required = "";
            //App.net.GlassRecord.point_of_entry_b = "";
            App.net.GlassRecord.back_to_back_spacer_width = "";
            App.net.GlassRecord.back_to_back_spacer_height = "";
            //App.net.GlassRecord.ex_s_spare6 = "";
            App.net.GlassRecord.cause_of_damage = "";
            App.net.GlassRecord.cause_of_damage_reason_different = "";
            //App.net.GlassRecord.type = "";
            App.net.GlassRecord.glass_width = "";
            App.net.GlassRecord.glass_height = "";
            App.net.GlassRecord.glass_width2 = "";
            App.net.GlassRecord.glass_height2 = "";
            App.net.GlassRecord.glass_width3 = "";
            App.net.GlassRecord.glass_height3 = "";
            App.net.GlassRecord.glass_width4 = "";
            App.net.GlassRecord.glass_height4 = "";
            App.net.GlassRecord.glass_width5 = "";
            App.net.GlassRecord.glass_height5 = "";
            App.net.GlassRecord.glass_width6 = "";
            App.net.GlassRecord.glass_height6 = "";
            App.net.GlassRecord.glass_width7 = "";
            App.net.GlassRecord.glass_height7 = "";
            App.net.GlassRecord.glass_width8 = "";
            App.net.GlassRecord.glass_height8 = "";
            App.net.GlassRecord.int_width = "";
            App.net.GlassRecord.int_height = "";
            App.net.GlassRecord.glass_type = "";
            //App.net.GlassRecord.pet_flap = "";
            //App.net.GlassRecord.pet_type = "";
            App.net.GlassRecord.sizeA = "";
            App.net.GlassRecord.sizeB = "";
            App.net.GlassRecord.sizeC = "";
            App.net.GlassRecord.sizeD = "";
            App.net.GlassRecord.lead_CWidth = "";
            App.net.GlassRecord.lead_CHeight = "";
            App.net.GlassRecord.lead_thickness = "";
            App.net.GlassRecord.lead_sod = "";
            App.net.GlassRecord.lead_type = "";
            App.net.GlassRecord.glass_pattern = "";
            App.net.GlassRecord.spacer_color = "";
            App.net.GlassRecord.spacer_thickness = "";
            App.net.GlassRecord.special_glass = "";
            //App.net.GlassRecord.comments = "";
            //App.net.GlassRecord.comments2 = "";
            App.net.GlassRecord.docl_old = "";
            App.net.GlassRecord.docl = "";
            App.net.GlassRecord.room_location = "";
            App.net.GlassRecord.ChangeItemTo = "";
            App.net.GlassRecord.print_name = "";
            //App.net.GlassRecord.FitterAdditional = "";
            App.net.GlassRecord.ProductInto = "";
            App.net.GlassRecord.glazing_type = "";
            App.net.GlassRecord.long_comments = "";
            //App.net.GlassRecord.SpacerType = "";
            App.net.GlassRecord.TapeorGasket = "";
            //App.net.GlassRecord.s_spare1 = "";
            //App.net.GlassRecord.s_spare2 = "";
            //App.net.GlassRecord.s_spare3 = "";
            //App.net.GlassRecord.new_sspare1 = "";
            //App.net.GlassRecord.new_sspare2 = "";
            //App.net.GlassRecord.new_sspare3 = "";
            App.net.GlassRecord.parts_to_order = "";
            //App.net.GlassRecord.new_sspare5 = "";
            //App.net.GlassRecord.new_sspare6 = "";
            //App.net.GlassRecord.new_sspare7 = "";
            //App.net.GlassRecord.new_sspare8 = "";
            //App.net.GlassRecord.new_sspare9 = "";
            //App.net.GlassRecord.new_sspare10 = "";
            App.net.GlassRecord.lead_comments = "";
            App.net.GlassRecord.parent_item = 0;
            App.net.loaded_item_number = App.net.GlassRecord.item_number;
            App.net.root_item_number = App.net.GlassRecord.item_number;
        }

        public void CreateComposite()
        {
            App.net.CompRecord = new CompositeTable();
            App.net.CompRecord.point_of_entry = "";
            App.net.CompRecord.type_of_lockng_system_required = "";
            //App.net.CompRecord.point_of_entry_b = "";
            //App.net.CompRecord.ex_s_spare4 = "";
            //App.net.CompRecord.ex_s_spare5 = "";
            //App.net.CompRecord.ex_s_spare6 = "";
            App.net.CompRecord.RecID = App.net.CompRecord.RecID;
            App.net.CompRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.CompRecord.item_number = ++App.net.HeaderRecord.current_item_number;
            App.net.CompRecord.cause_of_damage = "";
            App.net.CompRecord.cause_of_damage_reason_different = "";
            App.net.CompRecord.door_make = "";
            App.net.CompRecord.frame_colour_inside = "";
            App.net.CompRecord.frame_colour_outside = "";
            App.net.CompRecord.door_colour_inside = "";
            App.net.CompRecord.door_colour_outside = "";
            //App.net.CompRecord.midrail_height = "";
            App.net.CompRecord.door_design = "";
            App.net.CompRecord.glass_design = "";
            App.net.CompRecord.internal_width = "";
            App.net.CompRecord.internal_height = "";
            App.net.CompRecord.brick_width = "";
            App.net.CompRecord.brick_height = "";
            App.net.CompRecord.trickle_vents = "";
            App.net.CompRecord.addons_height = "";
            App.net.CompRecord.addons_width = "";
            App.net.CompRecord.handle_colour = "";
            App.net.CompRecord.threshold_type = "";
            App.net.CompRecord.glass_pattern = "";
            App.net.CompRecord.glass_type = "";
            App.net.CompRecord.spacer_thickness = "";
            App.net.CompRecord.spacer_colour = "";
            App.net.CompRecord.room_location = "";
            App.net.CompRecord.comments = "";
            App.net.CompRecord.ChangeItemTo = "";
            App.net.CompRecord.special_glass = "";
            App.net.CompRecord.lock_other_text = "";
            App.net.CompRecord.lead_thickness = "";
            App.net.CompRecord.lead_sod = "";
            App.net.CompRecord.lead_type = "";
            App.net.CompRecord.docl = "";
            App.net.CompRecord.letteredit = "";
            App.net.CompRecord.letter_box_pos = "";
            App.net.CompRecord.pet_flap = "";
            App.net.CompRecord.pet_type = "";
            //App.net.CompRecord.SpacerType = "";
            App.net.CompRecord.cills = "";
            App.net.CompRecord.door_wood = "";

            /*
            App.net.CompRecord.i_spare1 = "";
            App.net.CompRecord.i_spare2 = "";
            App.net.CompRecord.i_spare3 = "";
            */

            //App.net.CompRecord.s_spare1 = "";
            //App.net.CompRecord.s_spare2 = "";
            App.net.CompRecord.reason_not_repaired = "";

            //App.net.CompRecord.new_sspare1 = "";
            //App.net.CompRecord.new_sspare2 = "";
            //App.net.CompRecord.new_sspare3 = "";
            App.net.CompRecord.parts_to_order = "";
            //App.net.CompRecord.new_sspare5 = "";
            //App.net.CompRecord.new_sspare6 = "";
            //App.net.CompRecord.new_sspare7 = "";
            //App.net.CompRecord.new_sspare8 = "";
            //App.net.CompRecord.new_sspare9 = "";
            //App.net.CompRecord.new_sspare10 = "";
            App.net.CompRecord.lead_comments = "";
            App.net.CompRecord.glass_complete = false;
            App.net.CompRecord.replace_glass = 0;
            App.net.CompRecord.WER_rating = "";
            App.net.CompRecord.gaskets = 0;
            App.net.CompRecord.gaskets_text = "";
            App.net.CompRecord.handles_req = 0;
            App.net.CompRecord.bHandleDrawingComplete = false;
            App.net.CompRecord.handles_text = "";

            App.net.loaded_item_number = App.net.CompRecord.item_number;
            App.net.root_item_number = App.net.CompRecord.item_number;
        }

        public void CreateBifold()
        {
            App.net.BifoldRecord = new BifoldTable();

            App.net.BifoldRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.BifoldRecord.item_number = ++App.net.HeaderRecord.current_item_number;

            //App.net.BifoldRecord.point_of_entry = "";
            App.net.BifoldRecord.type_of_lockng_system_required = "";
            //App.net.BifoldRecord.point_of_entry_b = "";
            App.net.BifoldRecord.comments = "";
            App.net.BifoldRecord.internal_width = "";
            App.net.BifoldRecord.internal_height = "";
            App.net.BifoldRecord.overall_width = "";
            App.net.BifoldRecord.overall_height = "";
            App.net.BifoldRecord.hardware = "";
            App.net.BifoldRecord.color_internal = "";
            App.net.BifoldRecord.color_external = "";
            App.net.BifoldRecord.threshold_type = "";
            //App.net.BifoldRecord.additional_notes = "";
            App.net.BifoldRecord.cause_of_damage = "";
            App.net.BifoldRecord.cause_of_damage_reason_different = "";
            App.net.BifoldRecord.door_type = "";
            App.net.BifoldRecord.glazing_options = "";
            App.net.BifoldRecord.number_of_doors_text = "";
            App.net.BifoldRecord.colour_of_doors = "";
            App.net.BifoldRecord.handle_colour = "";
            App.net.BifoldRecord.cill_type = "";
            //App.net.BifoldRecord.s_spare8 = "";
            App.net.BifoldRecord.knock_on = "";
            //App.net.BifoldRecord.s_spare10 = "";
            //App.net.BifoldRecord.s_spare11 = "";
            //App.net.BifoldRecord.s_spare12 = "";
            //App.net.BifoldRecord.s_spare13 = "";
            //App.net.BifoldRecord.s_spare14 = "";
            //App.net.BifoldRecord.s_spare15 = "";
            //App.net.BifoldRecord.s_spare16 = "";
            //App.net.BifoldRecord.s_spare17 = "";
            //App.net.BifoldRecord.s_spare18 = "";

            //App.net.BifoldRecord.new_sspare1 = "";
            //App.net.BifoldRecord.new_sspare2 = "";
            //App.net.BifoldRecord.new_sspare3 = "";
            App.net.BifoldRecord.parts_to_order = "";
            //App.net.BifoldRecord.new_sspare5 = "";
            //App.net.BifoldRecord.new_sspare6 = "";
            //App.net.BifoldRecord.new_sspare7 = "";
            //App.net.BifoldRecord.new_sspare8 = "";
            //App.net.BifoldRecord.new_sspare9 = "";
            //App.net.BifoldRecord.new_sspare10 = "";
            App.net.BifoldRecord.glass_complete = false;
            App.net.BifoldRecord.replace_glass = 0;
            App.net.BifoldRecord.reason_not_repaired = "";
            App.net.BifoldRecord.WER_rating = "";
            App.net.BifoldRecord.gaskets = 0;
            App.net.BifoldRecord.gaskets_text = "";
            App.net.BifoldRecord.handles_req = 0;
            App.net.BifoldRecord.bHandleDrawingComplete = false;
            App.net.BifoldRecord.handles_text = "";

            App.net.loaded_item_number = App.net.BifoldRecord.item_number;
            App.net.root_item_number = App.net.BifoldRecord.item_number;
        }

        public void CreateConservatory()
        {
            App.net.ConsRecord = new ConsTable();

            App.net.ConsRecord.point_of_entry = "";
            App.net.ConsRecord.type_of_lockng_system_required = "";
            //App.net.ConsRecord.point_of_entry_b = "";
            //App.net.ConsRecord.ex_s_spare4 = "";
            //App.net.ConsRecord.ex_s_spare5 = "";
            //App.net.ConsRecord.ex_s_spare6 = "";
            App.net.ConsRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.ConsRecord.item_number = ++App.net.HeaderRecord.current_item_number;
            App.net.ConsRecord.type = "";
            App.net.ConsRecord.cause_of_damage = "";
            App.net.ConsRecord.cause_of_damage_reason_different = "";
            App.net.ConsRecord.sizeA = "";
            App.net.ConsRecord.sizeB = "";
            App.net.ConsRecord.roof_glazing_thickness = "";
            App.net.ConsRecord.sizeC = "";
            App.net.ConsRecord.sizeD = "";
            App.net.ConsRecord.sizeE = "";
            App.net.ConsRecord.angle1 = "";
            App.net.ConsRecord.angle2 = "";
            App.net.ConsRecord.angle3 = "";
            App.net.ConsRecord.angle4 = "";
            App.net.ConsRecord.pitch_height = "";
            App.net.ConsRecord.profile_section_size = "";
            App.net.ConsRecord.sheet_width_1 = "";
            App.net.ConsRecord.sheet_height_1 = "";
            App.net.ConsRecord.sheet_width_2 = "";
            App.net.ConsRecord.sheet_height_2 = "";
            App.net.ConsRecord.sheet_width_3 = "";
            App.net.ConsRecord.sheet_height_3 = "";
            App.net.ConsRecord.sheet_width_4 = "";
            App.net.ConsRecord.sheet_height_4 = "";
            App.net.ConsRecord.sheet_width_5 = "";
            App.net.ConsRecord.sheet_height_5 = "";
            App.net.ConsRecord.sheet_width_6 = "";
            App.net.ConsRecord.sheet_height_6 = "";
            App.net.ConsRecord.sheet_width_7 = "";
            App.net.ConsRecord.sheet_height_7 = "";
            App.net.ConsRecord.sheet_width_8 = "";
            App.net.ConsRecord.sheet_height_8 = "";
            App.net.ConsRecord.sheet_width_9 = "";
            App.net.ConsRecord.sheet_height_9 = "";
            App.net.ConsRecord.sheet_width_10 = "";
            App.net.ConsRecord.sheet_height_10 = "";

            App.net.ConsRecord.roof_sheets_quantity_1 = 0;
            App.net.ConsRecord.roof_sheets_quantity_2 = 0;
            App.net.ConsRecord.roof_sheets_quantity_3 = 0;
            App.net.ConsRecord.roof_sheets_quantity_4 = 0;
            App.net.ConsRecord.roof_sheets_quantity_5 = 0;
            App.net.ConsRecord.roof_sheets_quantity_6 = 0;
            App.net.ConsRecord.roof_sheets_quantity_7 = 0;
            App.net.ConsRecord.roof_sheets_quantity_8 = 0;
            App.net.ConsRecord.roof_sheets_quantity_9 = 0;
            App.net.ConsRecord.roof_sheets_quantity_10 = 0;

            App.net.ConsRecord.color = "";
            App.net.ConsRecord.roof_color = "";
            App.net.ConsRecord.flute_size = "";
            //App.net.ConsRecord.comments = "";
            App.net.ConsRecord.sizeF = "";
            App.net.ConsRecord.sizeG = "";
            App.net.ConsRecord.room_location = "";
            App.net.ConsRecord.ChangeItemTo = "";
            App.net.ConsRecord.print_name = "";
            App.net.ConsRecord.wall_pos = "";
            App.net.ConsRecord.pitch_degree = "";
            //App.net.ConsRecord.ring_beam_height = "";
            App.net.ConsRecord.long_comments = "";
            //App.net.ConsRecord.overall_length_of_sheet = "";

            //App.net.ConsRecord.s_spare1 = "";
            //App.net.ConsRecord.s_spare2 = "";
            //App.net.ConsRecord.s_spare3 = "";

            App.net.ConsRecord.ridge_length = "";
            //App.net.ConsRecord.new_sspare2 = "";
            //App.net.ConsRecord.new_sspare3 = "";
            App.net.ConsRecord.parts_to_order = "";
            //App.net.ConsRecord.new_sspare5 = "";
            //App.net.ConsRecord.new_sspare6 = "";
            //App.net.ConsRecord.new_sspare7 = "";
            //App.net.ConsRecord.new_sspare8 = "";
            //App.net.ConsRecord.new_sspare9 = "";
            //App.net.ConsRecord.new_sspare10 = "";
            App.net.ConsRecord.glass_complete = false;
            App.net.ConsRecord.replace_glass = 0;
            App.net.ConsRecord.reason_not_repaired = "";
            App.net.ConsRecord.WER_rating = "";

            App.net.loaded_item_number = App.net.ConsRecord.item_number;
            App.net.root_item_number = App.net.ConsRecord.item_number;
        }

        public void CreateTimber()
        {
            App.net.TimberRecord = new TimberTable();
            App.net.TimberRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.TimberRecord.item_number = ++App.net.HeaderRecord.current_item_number;

            App.net.TimberRecord.point_of_entry = "";
            App.net.TimberRecord.type_of_lockng_system_required = "";
            //App.net.TimberRecord.point_of_entry_b = "";
            App.net.TimberRecord.back_to_back_spacer_width = "";
            App.net.TimberRecord.back_to_back_spacer_height = "";
            //App.net.TimberRecord.ex_s_spare6 = "";
            App.net.TimberRecord.additional_locks = "";
            App.net.TimberRecord.gaskets_text = "";
            App.net.TimberRecord.handles_text = "";
            //App.net.TimberRecord.add_req_rep = "";
            App.net.TimberRecord.replace_reason = "";
            App.net.TimberRecord.replace_explain = "";
            App.net.TimberRecord.timber_item = "";
            App.net.TimberRecord.cause_of_damage = "";
            App.net.TimberRecord.cause_of_damage_reason_different = "";
            App.net.TimberRecord.timber_wood = "";
            App.net.TimberRecord.timber_frame_wood = "";
            App.net.TimberRecord.brick_width = "";
            App.net.TimberRecord.brick_height = "";
            App.net.TimberRecord.internal_width = "";
            App.net.TimberRecord.internal_height = "";
            App.net.TimberRecord.door_thickness = "";
            App.net.TimberRecord.door_width = "";
            App.net.TimberRecord.door_height = "";
            App.net.TimberRecord.cills = "";
            App.net.TimberRecord.pet_flap = "";
            App.net.TimberRecord.pet_type = "";
            //App.net.TimberRecord.moulding_size1 = "";
            //App.net.TimberRecord.moulding_size2 = "";
            //App.net.TimberRecord.beading_size1 = "";
            //App.net.TimberRecord.beading_size2 = "";
            App.net.TimberRecord.trickle_vents = "";
            App.net.TimberRecord.locks = "";
            App.net.TimberRecord.hardware_color = "";
            App.net.TimberRecord.door_color = "";
            App.net.TimberRecord.frame_color = "";
            App.net.TimberRecord.spacer_thickness = "";
            App.net.TimberRecord.spacer_color = "";
            App.net.TimberRecord.glass_type = "";
            App.net.TimberRecord.glass_pattern = "";
            App.net.TimberRecord.special_glass = "";
            //App.net.TimberRecord.timber_comments = "";
            //App.net.TimberRecord.docl_old = "";
            App.net.TimberRecord.docl = "";
            App.net.TimberRecord.room_location = "";
            App.net.TimberRecord.doc_l_compliant_reason = "";
            //App.net.TimberRecord.door_color_bother_out = "";
            App.net.TimberRecord.door_color_out = "";
            App.net.TimberRecord.frame_color_out = "";
            App.net.TimberRecord.door_color_code = "";
            App.net.TimberRecord.door_color_code_out = "";
            App.net.TimberRecord.frame_color_code = "";
            App.net.TimberRecord.frame_color_code_out = "";
            App.net.TimberRecord.ChangeItemTo = "";
            App.net.TimberRecord.print_name = "";
            App.net.TimberRecord.standard_sizes = "";
            App.net.TimberRecord.reasonnonstandard = "";
            App.net.TimberRecord.WER_rating = "";
            //App.net.TimberRecord.MouldType = "";
            App.net.TimberRecord.long_timber_comments = "";
            App.net.loaded_item_number = App.net.TimberRecord.item_number;
            App.net.TimberRecord.lead_thickness = "";
            App.net.TimberRecord.lead_type = "";
            App.net.TimberRecord.lead_sod = "";
            App.net.TimberRecord.lock_make = "";
            App.net.TimberRecord.lock_codes = "";
            //App.net.TimberRecord.glass_item_number = 0;
            //App.net.TimberRecord.iLockPos = 200;
            //App.net.TimberRecord.SpacerType = "";
            App.net.TimberRecord.letter_box = "";
            App.net.TimberRecord.letter_box_pos = "";
            App.net.TimberRecord.hinge_type = "";
            App.net.TimberRecord.moulding = "";
            //App.net.TimberRecord.s_spare1 = "";
            //App.net.TimberRecord.s_spare2 = "";
            //App.net.TimberRecord.s_spare3 = "";
            //App.net.TimberRecord.new_sspare1 = "";
            //App.net.TimberRecord.new_sspare2 = "";
            //App.net.TimberRecord.new_sspare3 = "";
            App.net.TimberRecord.parts_to_order = "";
            //App.net.TimberRecord.new_sspare5 = "";
            //App.net.TimberRecord.new_sspare6 = "";
            //App.net.TimberRecord.new_sspare7 = "";
            //App.net.TimberRecord.new_sspare8 = "";
            //App.net.TimberRecord.new_sspare9 = "";
            //App.net.TimberRecord.new_sspare10 = "";
            App.net.TimberRecord.lead_comments = "";
            App.net.loaded_item_number = App.net.TimberRecord.item_number;
            App.net.root_item_number = App.net.TimberRecord.item_number;

            App.net.TimberRecord.l_size1 = "";
            App.net.TimberRecord.l_size2 = "";

            App.net.TimberRecord.l_sizeA = "";
            App.net.TimberRecord.l_sizeB = "";
            App.net.TimberRecord.l_sizeC = "";
            App.net.TimberRecord.l_sizeD = "";
            App.net.TimberRecord.l_sizeE = "";
            App.net.TimberRecord.l_sizeF = "";
            App.net.TimberRecord.l_sizeG = "";
            App.net.TimberRecord.glass_complete = false;
            App.net.TimberRecord.replace_glass = 0;
        }

        public void CreateAlum()
        {
            App.net.AlumRecord = new AlumTable();
            App.net.AlumRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.AlumRecord.item_number = ++App.net.HeaderRecord.current_item_number;
            App.net.AlumRecord.item_number = App.net.HeaderRecord.current_item_number;
            App.net.AlumRecord.point_of_entry = "";
            App.net.AlumRecord.type_of_lockng_system_required = "";
            //App.net.AlumRecord.point_of_entry_b = "";
            App.net.AlumRecord.back_to_back_spacer_width = "";
            App.net.AlumRecord.back_to_back_spacer_height = "";
            //App.net.AlumRecord.ex_s_spare6 = "";
            App.net.AlumRecord.additional_locks = "";
            App.net.AlumRecord.gaskets_text = "";
            App.net.AlumRecord.handles_text = "";
            //App.net.AlumRecord.add_req_rep = "";
            App.net.AlumRecord.replace_reason = "";
            App.net.AlumRecord.replace_explain = "";
            App.net.AlumRecord.type = "";
            App.net.AlumRecord.cause_of_damage = "";
            App.net.AlumRecord.sub_frame_depth = "";
            App.net.AlumRecord.item_frame_width = "";
            App.net.AlumRecord.item_frame_height = "";
            App.net.AlumRecord.brick_width = "";
            App.net.AlumRecord.internal_width = "";
            App.net.AlumRecord.internal_height = "";
            App.net.AlumRecord.brick_width = "";
            App.net.AlumRecord.brick_height = "";
            App.net.AlumRecord.internal_width = "";
            App.net.AlumRecord.internal_height = "";
            App.net.AlumRecord.midrail_type = "";
            App.net.AlumRecord.pet_flap = "";
            App.net.AlumRecord.item_color = "";
            App.net.AlumRecord.locking_type = "";
            App.net.AlumRecord.letter_box = "";
            App.net.AlumRecord.letter_box_pos = "";
            App.net.AlumRecord.pet_type = "";
            App.net.AlumRecord.handle_color = "";
            App.net.AlumRecord.spacer_thickness = "";
            App.net.AlumRecord.spacer_color = "";
            App.net.AlumRecord.glass_type = "";
            App.net.AlumRecord.glass_pattern = "";
            App.net.AlumRecord.special_glass = "";
            App.net.AlumRecord.sub_frame_color = "";
            //App.net.AlumRecord.comments = "";
            App.net.AlumRecord.midrail_height = "";
            //App.net.AlumRecord.docl_old = "";
            App.net.AlumRecord.docl = "";
            App.net.AlumRecord.lead_sod = "";
            App.net.AlumRecord.lead_thickness = "";
            App.net.AlumRecord.lead_type = "";
            App.net.AlumRecord.room_location = "";
            App.net.AlumRecord.threshold_type = "";
            App.net.AlumRecord.ChangeItemTo = "";
            App.net.AlumRecord.print_name = "";
            App.net.AlumRecord.FecerRating = "";
            App.net.AlumRecord.long_comments = "";
            App.net.loaded_item_number = App.net.AlumRecord.item_number;
            App.net.AlumRecord.lock_make = "";
            App.net.AlumRecord.lock_codes = "";
            //App.net.AlumRecord.glass_item_number = 0;
            //App.net.AlumRecord.iLockPos = 200;
            //App.net.AlumRecord.SpacerType = "";
            //App.net.AlumRecord.s_spare1 = "";
            //App.net.AlumRecord.s_spare2 = "";
            //App.net.AlumRecord.s_spare3 = "";
            App.net.AlumRecord.outer_section_width = "";
            App.net.AlumRecord.outer_section_height = "";
            //App.net.AlumRecord.new_sspare3 = "";
            App.net.AlumRecord.parts_to_order = "";
            //App.net.AlumRecord.new_sspare5 = "";
            //App.net.AlumRecord.new_sspare6 = "";
            //App.net.AlumRecord.new_sspare7 = "";
            //App.net.AlumRecord.new_sspare8 = "";
            //App.net.AlumRecord.new_sspare9 = "";
            //App.net.AlumRecord.new_sspare10 = "";
            App.net.AlumRecord.lead_comments = "";
            App.net.loaded_item_number = App.net.AlumRecord.item_number;
            App.net.root_item_number = App.net.AlumRecord.item_number;

            App.net.AlumRecord.l_size1 = "";
            App.net.AlumRecord.l_size2 = "";

            App.net.AlumRecord.l_sizeA = "";
            App.net.AlumRecord.l_sizeB = "";
            App.net.AlumRecord.l_sizeC = "";
            App.net.AlumRecord.l_sizeD = "";
            App.net.AlumRecord.l_sizeE = "";
            App.net.AlumRecord.l_sizeF = "";
            App.net.AlumRecord.l_sizeG = "";
            App.net.AlumRecord.glass_complete = false;
            App.net.AlumRecord.replace_glass = 0;
        }

        public void CreateLock()
        {
            App.net.LockingRecord = new LockingTable();

            App.net.LockingRecord.point_of_entry = "";
            App.net.LockingRecord.type_of_lockng_system_required = "";
            //App.net.LockingRecord.point_of_entry_b = "";
            App.net.LockingRecord.udi_cont = App.net.HeaderRecord.udi_cont;
            App.net.LockingRecord.item_number = ++App.net.HeaderRecord.current_item_number;
            App.net.LockingRecord.comments = "";
            App.net.LockingRecord.item = "";
            App.net.LockingRecord.locking_make = "";
            App.net.LockingRecord.locking_codes = "";
            App.net.LockingRecord.lock_colour = "";
            App.net.LockingRecord.pagenum = "";
            //App.net.LockingRecord.FitterAdditional = "";
            App.net.LockingRecord.ChangeItemTo = "";
            App.net.LockingRecord.print_name = "";
            App.net.LockingRecord.COD_Code = "";
            //App.net.LockingRecord.COD_String = "";
            App.net.LockingRecord.cause_of_damage = "";
            App.net.LockingRecord.cause_of_damage_reason_different = "";
            App.net.loaded_item_number = App.net.LockingRecord.item_number;
            App.net.root_item_number = App.net.LockingRecord.item_number;

            //App.net.LockingRecord.new_sspare1 = "";
            //App.net.LockingRecord.new_sspare2 = "";
            //App.net.LockingRecord.new_sspare3 = "";
            App.net.LockingRecord.parts_to_order = "";
            //App.net.LockingRecord.new_sspare5 = "";
            //App.net.LockingRecord.new_sspare6 = "";
            //App.net.LockingRecord.new_sspare7 = "";
            //App.net.LockingRecord.new_sspare8 = "";
            //App.net.LockingRecord.new_sspare9 = "";
            //App.net.LockingRecord.new_sspare10 = "";
        }

        public void CreateUpvc()
        {
            App.net.UPVCRecord = new UPVCTable();

            App.net.UPVCRecord.udi_cont = App.net.HeaderRecord.udi_cont;

            App.net.UPVCRecord.item_number = ++App.net.HeaderRecord.current_item_number;
            App.net.UPVCRecord.RecID = App.net.UPVCRecord.RecID;
            App.net.UPVCRecord.point_of_entry = "";
            App.net.UPVCRecord.type_of_lockng_system_required = "";
            //App.net.UPVCRecord.point_of_entry_b = "";
            App.net.UPVCRecord.back_to_back_spacer_width = "";
            App.net.UPVCRecord.back_to_back_spacer_height = "";
            //App.net.UPVCRecord.ex_s_spare6 = "";
            App.net.UPVCRecord.additional_locks = "";
            App.net.UPVCRecord.gaskets_text = "";
            App.net.UPVCRecord.handles_text = "";
            //App.net.UPVCRecord.add_req_rep = "";
            App.net.UPVCRecord.replace_reason = "";
            App.net.UPVCRecord.replace_explain = "";
            App.net.UPVCRecord.upvc_item = "";
            App.net.UPVCRecord.cause_of_damage = "";
            App.net.UPVCRecord.cause_of_damage_reason_different = "";
            App.net.UPVCRecord.colour = "";
            App.net.UPVCRecord.cills = "";
            //App.net.UPVCRecord.gasket_colour = "";
            App.net.UPVCRecord.outer_section_size = "";
            App.net.UPVCRecord.internal_width = "";
            App.net.UPVCRecord.internal_height = "";
            App.net.UPVCRecord.brick_width = "";
            App.net.UPVCRecord.lead_sod = "";
            App.net.UPVCRecord.lead_type = "";
            App.net.UPVCRecord.lead_thickness = "";
            App.net.UPVCRecord.brick_height = "";
            App.net.UPVCRecord.addon_width = "";
            App.net.UPVCRecord.addon_height = "";
            App.net.UPVCRecord.handle_colour = "";
            App.net.UPVCRecord.locking_type = "";
            App.net.UPVCRecord.letter_box = "";
            App.net.UPVCRecord.letter_box_pos = "";
            App.net.UPVCRecord.pet_flap = "";
            App.net.UPVCRecord.pet_type = "";
            App.net.UPVCRecord.bead_type = "";
            App.net.UPVCRecord.spacer_thickness = "";
            App.net.UPVCRecord.spacer_colour = "";
            App.net.UPVCRecord.glass_type = "";
            App.net.UPVCRecord.glass_pattern = "";
            App.net.UPVCRecord.special_glass = "";
            //App.net.UPVCRecord.comments = "";
            App.net.UPVCRecord.midrail_height = "";
            //App.net.UPVCRecord.docl_old = "";
            App.net.UPVCRecord.frame_depth = "70mm";
            App.net.UPVCRecord.docl = "";
            App.net.UPVCRecord.room_location = "";
            //App.net.UPVCRecord.reason60mm = "";
            //App.net.UPVCRecord.slide_position = 0;
            App.net.UPVCRecord.threshold_type = "";
            App.net.UPVCRecord.ChangeItemTo = "";
            App.net.UPVCRecord.print_name = "";
            App.net.UPVCRecord.WER_Rating = "";
            //App.net.UPVCRecord.addon_width_long = "";
            //App.net.UPVCRecord.addon_height_long = "";
            App.net.UPVCRecord.long_comments = "";
            App.net.UPVCRecord.lock_make = "";
            App.net.UPVCRecord.lock_codes = "";
            //App.net.UPVCRecord.glass_item_number = 0;
            //App.net.UPVCRecord.iLockPos = 200;
            //App.net.UPVCRecord.SpacerType = "";
            //App.net.UPVCRecord.s_spare1 = "";
            //App.net.UPVCRecord.s_spare2 = "";
            App.net.UPVCRecord.hinge_colour = "";
            //App.net.UPVCRecord.new_sspare1 = "";
            //App.net.UPVCRecord.new_sspare2 = "";
            //App.net.UPVCRecord.new_sspare3 = "";
            App.net.UPVCRecord.parts_to_order = "";
            //App.net.UPVCRecord.new_sspare5 = "";
            //App.net.UPVCRecord.new_sspare6 = "";
            //App.net.UPVCRecord.new_sspare7 = "";
            //App.net.UPVCRecord.new_sspare8 = "";
            //App.net.UPVCRecord.new_sspare9 = "";
            //App.net.UPVCRecord.new_sspare10 = "";
            App.net.UPVCRecord.lead_comments = "";
            App.net.loaded_item_number = App.net.UPVCRecord.item_number;
            App.net.root_item_number = App.net.UPVCRecord.item_number;

            App.net.UPVCRecord.l_size1 = "";
            App.net.UPVCRecord.l_size2 = "";

            App.net.UPVCRecord.l_sizeA = "";
            App.net.UPVCRecord.l_sizeB = "";
            App.net.UPVCRecord.l_sizeC = "";
            App.net.UPVCRecord.l_sizeD = "";
            App.net.UPVCRecord.l_sizeE = "";
            App.net.UPVCRecord.l_sizeF = "";
            App.net.UPVCRecord.l_sizeG = "";
            App.net.UPVCRecord.glass_complete = false;
            App.net.UPVCRecord.replace_glass = 0;
        }

        public void CreateHeader()
        {
            App.net.HeaderRecord.mop = "";
            App.net.HeaderRecord.typeA = "";
            App.net.HeaderRecord.typeB = "";
            App.net.HeaderRecord.fitters_instructions = "";
            App.net.HeaderRecord.fitter_work = "";
            App.net.HeaderRecord.parts_used = "";
            App.net.HeaderRecord.claim_ref = "";
            App.net.HeaderRecord.fitter_comments = "";
            App.net.HeaderRecord.spare2 = "";
            App.net.HeaderRecord.udi_inst = "";
            App.net.HeaderRecord.acc_text = "";
            App.net.HeaderRecord.obs_wires_text = "";
            App.net.HeaderRecord.loose_brick_text = "";
            App.net.HeaderRecord.rep_text = "";
            App.net.HeaderRecord.card_cheq = "";
            App.net.HeaderRecord.reason_excess_not_collected = "";
            App.net.HeaderRecord.summ_text = "";
            App.net.HeaderRecord.code_text = "";
            App.net.HeaderRecord.job_grade = "";
            App.net.HeaderRecord.njs = "";
            App.net.HeaderRecord.nsn = "";
            App.net.HeaderRecord.si_bday1 = "";
            App.net.HeaderRecord.si_mpay = "";
            App.net.HeaderRecord.si_cnum = "";
            App.net.HeaderRecord.si_inum = "";
            App.net.HeaderRecord.udi_jobtext = "";
            App.net.HeaderRecord.udi_staff = "";
            App.net.HeaderRecord.type = "";
            App.net.HeaderRecord.sub_type = "";
            App.net.HeaderRecord.old_date = "";
            App.net.HeaderRecord.cover_instructions = "";
            App.net.HeaderRecord.old_start = "";
            App.net.HeaderRecord.old_finish = "";
            App.net.HeaderRecord.add_comm = "";
            App.net.HeaderRecord.udi_estrem = "";
            App.net.HeaderRecord.r_fault = "";
            App.net.HeaderRecord.rexcedit = "";
            App.net.HeaderRecord.rno_hours = "";
            App.net.HeaderRecord.r_work_txt = "";
            App.net.HeaderRecord.readdtxt = "";
            App.net.HeaderRecord.f_add_txt = "";
            App.net.HeaderRecord.fmclrf = "";
            App.net.HeaderRecord.funfincode = "";
            App.net.HeaderRecord.funfinoth = "";
            App.net.HeaderRecord.freuntxt = "";
            App.net.HeaderRecord.fpartreq = "";
            App.net.HeaderRecord.fname1 = "";
            App.net.HeaderRecord.fname2 = "";
            App.net.HeaderRecord.fname3 = "";
            App.net.HeaderRecord.fname4 = "";
            App.net.HeaderRecord.fname5 = "";
            App.net.HeaderRecord.fname6 = "";
            App.net.HeaderRecord.fname7 = "";
            App.net.HeaderRecord.fname8 = "";
            App.net.HeaderRecord.fexcessoth = "";
            App.net.HeaderRecord.fmandoth = "";
            App.net.HeaderRecord.commtxt = "";
            App.net.HeaderRecord.wkcartxt = "";
            App.net.HeaderRecord.parttxt = "";
            App.net.HeaderRecord.inst_height = "";
            App.net.HeaderRecord.ground_surface = "";
            App.net.HeaderRecord.type_of_equipment = "";
            App.net.HeaderRecord.risks_and_dangers = "";
            App.net.HeaderRecord.uc_desc = "";
            App.net.HeaderRecord.reason_not_complete = "";
            App.net.HeaderRecord.survey_on_fit = 0;
            App.net.HeaderRecord.funfinished_code = "";
            App.net.HeaderRecord.freason_unfinished = "";
            App.net.HeaderRecord.fparts_required = "";
            App.net.HeaderRecord.ffitter_name1 = "";
            App.net.HeaderRecord.ffitter_name2 = "";
            App.net.HeaderRecord.readditimage = false;
            App.net.HeaderRecord.stimea = "";// DateTime.Now.ToShortTimeString();
            //App.net.HeaderRecord.fbexcess_paid = "";
            App.net.HeaderRecord.freason_excess_not_paid = "";
            //App.net.HeaderRecord.fbmandate_signed = "";
            App.net.HeaderRecord.freason_mandate_not_signed = "";
            App.net.HeaderRecord.uc_inceden = DateTime.Today.ToString();
            App.net.HeaderRecord.ftime_arrived = "08:00";
            App.net.HeaderRecord.ftime_left = "17:00";
            App.net.HeaderRecord.no_of_fitters = 1;
            App.net.HeaderRecord.udi_start = DateTime.Today.ToString();
            App.net.HeaderRecord.udi_fin = DateTime.Today.ToString();
            App.net.HeaderRecord.udi_date = DateTime.Today.ToString();
            App.net.HeaderRecord.fmdate = DateTime.Today.ToString();
            App.net.HeaderRecord.ftimearr = DateTime.Today.ToString();
            App.net.HeaderRecord.ftimeleft = DateTime.Today.ToString();
            //App.net.HeaderRecord.stimea = DateTime.Today.ToString();
            App.net.HeaderRecord.f_sign_date = DateTime.Today.ToString();
            App.net.HeaderRecord.uspot_date = DateTime.Today.ToString();
            App.net.HeaderRecord.uspot_signeddate = DateTime.Today.ToString();
            App.net.HeaderRecord.fit_diary = DateTime.Today.ToString();
            App.net.HeaderRecord.fit_start = DateTime.Today.ToString();
            App.net.HeaderRecord.fit_fin = DateTime.Today.ToString();
            //App.net.HeaderRecord.fbadditional_paid = "";
            App.net.HeaderRecord.fhow_mutch_additional_paid = "";
            //App.net.HeaderRecord.bfitter_complete = "";
            //App.net.HeaderRecord.fitter_info_done = "";
            App.net.HeaderRecord.fbunfinother = "";
            //App.net.HeaderRecord.bcompletion_signed = "";
            //App.net.HeaderRecord.bad_image_complete = "";
            App.net.HeaderRecord.remedial_number = "";
            //App.net.HeaderRecord.r_bsigned = "";
            App.net.HeaderRecord.r_bcomp = "";
            App.net.HeaderRecord.r_sign_date = "";
            App.net.HeaderRecord.stimea = "00:00"; // DateTime.Today.ToString();
            App.net.HeaderRecord.expiry = DateTime.Today.ToString();
            App.net.HeaderRecord.f1_or_s2 = "";
            //App.net.HeaderRecord.no_of_photos = "";
            App.net.HeaderRecord.bClosest = "";
            App.net.HeaderRecord.Group = "";
            App.net.HeaderRecord.bProcessed = "";
            // App.net.HeaderRecord.id = "";
            App.net.HeaderRecord.inevitable_damage = "";
            //App.net.HeaderRecord.fbstockusagecomplete = "";
            App.net.HeaderRecord.uc_h_phone2 = "";
            App.net.HeaderRecord.uc_h_phone3 = "";
            App.net.HeaderRecord.int_type_of_lock = "";
            App.net.HeaderRecord.add_long = "";

            //App.net.HeaderRecord.securing_surveyor_required = "";
            App.net.HeaderRecord.policy_number = "";
            App.net.HeaderRecord.asvizex = "";
            App.net.HeaderRecord.refmessage = "";
            App.net.HeaderRecord.uspot_fitter = "";
            App.net.HeaderRecord.uspot_trainee = "";
            App.net.HeaderRecord.uspot_customer = "";
            App.net.HeaderRecord.uspot_postcode = "";
            App.net.HeaderRecord.uspot_insuranceco = "";
            App.net.HeaderRecord.uspot_branch = "";
            App.net.HeaderRecord.doc_l_compliant_reason = "";
            App.net.HeaderRecord.lintel_present_text = "";
            App.net.HeaderRecord.uspot_customersatisfaction_improvementsOld = "";
            App.net.HeaderRecord.uspot_otherobservationsOld = "";
            App.net.HeaderRecord.uspot_appearence_improvements = "";
            App.net.HeaderRecord.uspot_qualityofworks_improvements = "";
            App.net.HeaderRecord.uspot_customersatisfaction_improvements = "";
            App.net.HeaderRecord.uspot_otherobservations = "";
            App.net.HeaderRecord.messagetoinsurer = "";
            App.net.HeaderRecord.COD_Code = "";
            App.net.HeaderRecord.COD_String = "";
            App.net.HeaderRecord.old_cover_instructions = "";
            App.net.HeaderRecord.rcodchanged = "";
            App.net.HeaderRecord.goaheadstr = "";
            App.net.HeaderRecord.subcontracttext = "";
            App.net.HeaderRecord.reason_not_booked_in = "";
            App.net.HeaderRecord.add_phone_1 = "";
            App.net.HeaderRecord.add_phone_2 = "";

            App.net.HeaderRecord.s_spare1 = "";
            App.net.HeaderRecord.s_spare2 = "";
            App.net.HeaderRecord.s_spare3 = "";

            App.net.HeaderRecord.doorbell = 2;
            App.net.HeaderRecord.alarm_cont = 2;
            App.net.HeaderRecord.acroreq = 2;
            App.net.HeaderRecord.acrosboy = 2;
            App.net.HeaderRecord.sand_cemen = 2;
            App.net.HeaderRecord.plaster = 2;
            App.net.HeaderRecord.genreq = 2;
            App.net.HeaderRecord.architreq = 2;

            App.net.HeaderRecord.ss_nowindows = "";
            App.net.HeaderRecord.ss_nodoors = "";
            App.net.HeaderRecord.ss_gencondition = "";
            App.net.HeaderRecord.ss_gencondition_other = "";
            App.net.HeaderRecord.ss_matwindows = "";
            App.net.HeaderRecord.ss_matwindows_other = "";
            App.net.HeaderRecord.ss_matdoors = "";
            App.net.HeaderRecord.ss_matdoors_other = "";
            App.net.HeaderRecord.ss_lockwindows = "";
            App.net.HeaderRecord.ss_lockwindows_other = "";
            App.net.HeaderRecord.ss_lockdoors = "";
            App.net.HeaderRecord.ss_lockdoors_other = "";
            App.net.HeaderRecord.ss_location_windows_other = "";
            App.net.HeaderRecord.ss_secwindows_other = "";
            App.net.HeaderRecord.ss_location_doors_other = "";
            App.net.HeaderRecord.ss_secdoors_other = "";
            App.net.HeaderRecord.ss_time_required = "";

            App.net.HeaderRecord.s_spare1 = "";
            App.net.HeaderRecord.s_spare2 = "";
            App.net.HeaderRecord.s_spare3 = "";

            App.net.HeaderRecord.new_sspare1 = "";
            App.net.HeaderRecord.new_sspare2 = "";
            App.net.HeaderRecord.new_sspare3 = "";
            App.net.HeaderRecord.new_sspare4 = "";
            App.net.HeaderRecord.new_sspare5 = "";
            App.net.HeaderRecord.new_sspare6 = "";
            App.net.HeaderRecord.new_sspare7 = "";
            App.net.HeaderRecord.new_sspare8 = "";
            App.net.HeaderRecord.new_sspare9 = "£";
            App.net.HeaderRecord.new_sspare10 = "";

            App.net.HeaderRecord.branch = "";
            App.net.HeaderRecord.name = "";
            App.net.HeaderRecord.job = "";
            App.net.HeaderRecord.name1 = "";
            App.net.HeaderRecord.name2 = "";
            App.net.HeaderRecord.safety_boots_worn1_s = "";
            App.net.HeaderRecord.safety_gloves_worn1_s = "";
            App.net.HeaderRecord.safety_googles_worn1_s = "";
            App.net.HeaderRecord.safety_helmet_worn1_s = "";
            App.net.HeaderRecord.wristguards_worn1_s = "";
            App.net.HeaderRecord.uniform_worn_complete1_s = "";
            App.net.HeaderRecord.id_card_available1_s = "";
            App.net.HeaderRecord.safety_boots_worn2_s = "";
            App.net.HeaderRecord.safety_gloves_worn2_s = "";
            App.net.HeaderRecord.safety_googles_worn2_s = "";
            App.net.HeaderRecord.safety_helmet_worn2_s = "";
            App.net.HeaderRecord.wristguards_worn2_s = "";
            App.net.HeaderRecord.uniform_worn_complete2_s = "";
            App.net.HeaderRecord.id_card_available2_s = "";
            App.net.HeaderRecord.chemicals_stored_correctly_s = "";
            App.net.HeaderRecord.are_sheets_available_s = "";
            App.net.HeaderRecord.area_above_been_checked_s = "";
            App.net.HeaderRecord.obstructions_checked_s = "";
            App.net.HeaderRecord.lintel_ok_s = "";
            App.net.HeaderRecord.ladders_secure_s = "";
            App.net.HeaderRecord.safe_work_at_height_s = "";
            App.net.HeaderRecord.condition_of_ladders_s = "";
            App.net.HeaderRecord.tools_set_out_safely_s = "";
            App.net.HeaderRecord.fire_extinguisher_on_van_s = "";
            App.net.HeaderRecord.first_aid_kit_on_van_s = "";
            App.net.HeaderRecord.electrical_equipment_tested_s = "";

            App.net.HeaderRecord.comments = "";

            App.CurrentApp.HeaderRecord.door_type = "";
            App.CurrentApp.HeaderRecord.model_type = "";
            App.CurrentApp.HeaderRecord.unique_serial = "";
            App.CurrentApp.HeaderRecord.door_size = "";
            App.CurrentApp.HeaderRecord.door_manufacturer = "";
            App.CurrentApp.HeaderRecord.powerered_operator_type = "";
            App.CurrentApp.HeaderRecord.operator_manufacturer = "";
            App.CurrentApp.HeaderRecord.site_address = "";
            App.CurrentApp.HeaderRecord.decleration_by = "";
            App.CurrentApp.HeaderRecord.date = "";
            App.CurrentApp.HeaderRecord.on_behalf_of_person = "";
            App.CurrentApp.HeaderRecord.s_spare1 = "";
            App.CurrentApp.HeaderRecord.s_spare2 = "";
            App.CurrentApp.HeaderRecord.print_name = "";
            App.CurrentApp.HeaderRecord.time_to_complete = "";

        }

        public void CreateVanCheck()
        {
            string date_string;
            string pda_code;
            string random_id;

            string check_id;
            Random ran = new Random();
            int i = ran.Next(1, 999999);

            //AppSettings TheSettings = new AppSettings();

            App.net.VanChecksHeader = new VanChecksHeader();
            random_id = App.net.App_Settings.current_van_check.ToString().PadLeft(8, '0');

            App.net.App_Settings.current_van_check++;
            App.data.SaveSettings();

            date_string = DateTime.Today.ToString("yyyyMMddhhmm");
            pda_code = App.net.App_Settings.set_ownercode.PadLeft(5, '-').Substring(0, 5);
            check_id = i.ToString();

            App.net.VanChecksHeader.unique_id = random_id + date_string + pda_code;
            App.net.VanChecksHeader.current_item_no = 0;

            App.net.VanChecksHeader.PDACode = App.net.App_Settings.set_name;
            App.net.VanChecksHeader.bComplete = false;
            App.net.VanChecksHeader.bSent = false;
            App.net.VanChecksHeader.check_date = DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToShortDateString();
            App.net.VanChecksHeader.current_item_no = 0;

            App.net.VanChecksHeader.damage_pass = "";
            App.net.VanChecksHeader.damage_driver = "";
            App.net.VanChecksHeader.damage_front = "";
            App.net.VanChecksHeader.damage_back = "";
            App.net.VanChecksHeader.spare_s_1 = "";
            App.net.VanChecksHeader.spare_s_2 = "";
            App.net.VanChecksHeader.spare_s_3 = "";
            App.net.VanChecksHeader.spare_s_4 = "";
        }

        public void CreateDelivery()
        {
            App.net.DeliveryVehicleCheckList = new DeliveryVehicleCheckList();
            App.net.DeliveryVehicleCheckList.CheckID = App.net.VanChecksHeader.unique_id;
            App.net.DeliveryVehicleCheckList.item_no = App.net.VanChecksHeader.current_item_no++;

            App.net.DeliveryVehicleCheckList.not_complete_reason = "";

            App.net.DeliveryVehicleCheckList.name = "";
            App.net.DeliveryVehicleCheckList.destination = "";

            App.net.DeliveryVehicleCheckList.date = DateTime.Today.ToShortDateString();

            App.net.DeliveryVehicleCheckList.vehicle_registration = "";
            App.net.DeliveryVehicleCheckList.mileage = "";

            App.net.DeliveryVehicleCheckList.report_defects = "";
            App.net.DeliveryVehicleCheckList.date_signed = DateTime.Today.ToShortDateString(); ;

            App.net.DeliveryVehicleCheckList.item_no_4D = "";

            App.net.DeliveryVehicleCheckList.passenger_front_pressure_s = "";
            App.net.DeliveryVehicleCheckList.passenger_rear_pressure_s = "";
            App.net.DeliveryVehicleCheckList.driver_front_pressure_s = "";
            App.net.DeliveryVehicleCheckList.driver_rear_pressure_s = "";
            App.net.DeliveryVehicleCheckList.spare_tyre_pressure_s = "";

            App.net.DeliveryVehicleCheckList.damage_pass = "";
            App.net.DeliveryVehicleCheckList.damage_driver = "";
            App.net.DeliveryVehicleCheckList.damage_front = "";
            App.net.DeliveryVehicleCheckList.damage_back = "";


            App.net.DeliveryVehicleCheckList.driver_printed = "";
            App.net.DeliveryVehicleCheckList.checked_printed = "";

            App.net.DeliveryVehicleCheckList.spare_s_1 = "";
            App.net.DeliveryVehicleCheckList.spare_s_2 = "";
            App.net.DeliveryVehicleCheckList.spare_s_3 = "";
            App.net.DeliveryVehicleCheckList.spare_s_4 = "";

        }

        public void CreateCar()
        {
            App.net.CarPanelSheet = new CarPanelSheet();
            App.net.CarPanelSheet.CheckID = App.net.VanChecksHeader.unique_id;
            App.net.CarPanelSheet.item_no = App.net.VanChecksHeader.current_item_no++;

            App.net.CarPanelSheet.not_complete_reason = "";


            App.net.CarPanelSheet.date = DateTime.Now.ToShortDateString();
            App.net.CarPanelSheet.vehicle_reg = "";

            App.net.CarPanelSheet.item_no_4D = "";

            App.net.CarPanelSheet.pressure_passenger_front_s = "";
            App.net.CarPanelSheet.pressure_passenger_rear_s = "";
            App.net.CarPanelSheet.pressure_driver_front_s = "";
            App.net.CarPanelSheet.pressure_driver_rear_s = "";
            App.net.CarPanelSheet.spare_tyre_pressure_s = "";


            App.net.CarPanelSheet.driver_printed = "";
            App.net.CarPanelSheet.checked_printed = "";
            App.net.CarPanelSheet.mileage = "";
            App.net.CarPanelSheet.fuel_card_s = "";

            App.net.CarPanelSheet.shell_points_card_s = "";
            App.net.CarPanelSheet.interior_clean_s = "";
            App.net.CarPanelSheet.oil_level_s = "";
            App.net.CarPanelSheet.water_level_s = "";
            App.net.CarPanelSheet.windscreen_wash_s = "";
            App.net.CarPanelSheet.spare_wheel_s = "";
            App.net.CarPanelSheet.jack_s = "";
            App.net.CarPanelSheet.wheel_brace_s = "";
            App.net.CarPanelSheet.tools_s = "";
            App.net.CarPanelSheet.tyre_condition_s = "";

            App.net.CarPanelSheet.pressure_passenger_front_s = "";
            App.net.CarPanelSheet.pressure_passenger_rear_s = "";
            App.net.CarPanelSheet.pressure_driver_front_s = "";
            App.net.CarPanelSheet.pressure_driver_rear_s = "";
            App.net.CarPanelSheet.spare_tyre_pressure_s = "";

            App.net.CarPanelSheet.damage_pass = "";
            App.net.CarPanelSheet.damage_driver = "";
            App.net.CarPanelSheet.damage_front = "";
            App.net.CarPanelSheet.damage_back = "";

            App.net.CarPanelSheet.shell_fuel_card_s = "";
            App.net.CarPanelSheet.spare_s_2 = "";
            App.net.CarPanelSheet.spare_s_3 = "";
            App.net.CarPanelSheet.spare_s_4 = "";
        }


        public void CreateVan()
        {
            App.net.WeeklyVanCheckSheet = new WeeklyVanCheckSheet();
            App.net.WeeklyVanCheckSheet.CheckID = App.net.VanChecksHeader.unique_id;
            App.net.WeeklyVanCheckSheet.item_no = App.net.VanChecksHeader.current_item_no++;

            App.net.WeeklyVanCheckSheet.not_complete_reason = "";

            App.net.WeeklyVanCheckSheet.branch = "";
            App.net.WeeklyVanCheckSheet.reg_no = "";
            App.net.WeeklyVanCheckSheet.date = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.mileage = "";
            App.net.WeeklyVanCheckSheet.vehicle_reg = "";

            App.net.WeeklyVanCheckSheet.item_no_4D = "";

            App.net.WeeklyVanCheckSheet.driver_printed = "";
            App.net.WeeklyVanCheckSheet.checked_printed = "";
            App.net.WeeklyVanCheckSheet.panel_date = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.vehicle_reg = "";
            App.net.WeeklyVanCheckSheet.driver_name = "";
            App.net.WeeklyVanCheckSheet.van_check_name = "";

            App.net.WeeklyVanCheckSheet.circuit_breaker_s = "";
            App.net.WeeklyVanCheckSheet.power_breaker_s = "";
            App.net.WeeklyVanCheckSheet.hammer_drill_s = "";
            App.net.WeeklyVanCheckSheet.ordinary_drill_s = "";
            App.net.WeeklyVanCheckSheet.cordless_drill_s = "";
            App.net.WeeklyVanCheckSheet.spare_battery_and_charger_s = "";
            App.net.WeeklyVanCheckSheet.circular_saw_s = "";
            App.net.WeeklyVanCheckSheet.jig_saw_s = "";
            App.net.WeeklyVanCheckSheet.planer_check_blade_s = "";
            App.net.WeeklyVanCheckSheet.heat_gun_s = "";
            App.net.WeeklyVanCheckSheet.sander_s = "";
            App.net.WeeklyVanCheckSheet.hoover_s = "";
            App.net.WeeklyVanCheckSheet.halogen_lamp_s = "";
            App.net.WeeklyVanCheckSheet.extension_lead_s = "";
            App.net.WeeklyVanCheckSheet.router_s = "";
            App.net.WeeklyVanCheckSheet.industrial_ladders_s = "";
            App.net.WeeklyVanCheckSheet.ladder_clamps_s = "";
            App.net.WeeklyVanCheckSheet.step_ladders_s = "";
            App.net.WeeklyVanCheckSheet.ladder_stopper_s = "";
            App.net.WeeklyVanCheckSheet.philips_bit_s = "";
            App.net.WeeklyVanCheckSheet.screw_box_s = "";
            App.net.WeeklyVanCheckSheet.tresles_x2_s = "";
            App.net.WeeklyVanCheckSheet.torch_working_s = "";
            App.net.WeeklyVanCheckSheet.ratchett_straps_x4_s = "";
            App.net.WeeklyVanCheckSheet.spare_wheel_s = "";
            App.net.WeeklyVanCheckSheet.blue_external_dust_sheet_s = "";
            App.net.WeeklyVanCheckSheet.internal_dust_sheets_x3_s = "";
            App.net.WeeklyVanCheckSheet.brush_and_shovel_s = "";
            App.net.WeeklyVanCheckSheet.cleaner_bottle_s = "";
            App.net.WeeklyVanCheckSheet.ecloth_s = "";
            App.net.WeeklyVanCheckSheet.mastic_guns_s = "";
            App.net.WeeklyVanCheckSheet.glass_suckers_s = "";
            App.net.WeeklyVanCheckSheet.safety_helmets_s = "";
            App.net.WeeklyVanCheckSheet.helmet_manufacture_date_s = "";
            App.net.WeeklyVanCheckSheet.gloves_s = "";
            App.net.WeeklyVanCheckSheet.wrist_guards_s = "";
            App.net.WeeklyVanCheckSheet.goggles_s = "";
            App.net.WeeklyVanCheckSheet.ear_defenders_s = "";
            App.net.WeeklyVanCheckSheet.dust_masks_s = "";
            App.net.WeeklyVanCheckSheet.customer_care_cards_s = "";
            App.net.WeeklyVanCheckSheet.completion_forms_s = "";
            App.net.WeeklyVanCheckSheet.freepost_envelopes_s = "";
            App.net.WeeklyVanCheckSheet.mandate_forms_s = "";
            App.net.WeeklyVanCheckSheet.quality_manuals_s = "";
            App.net.WeeklyVanCheckSheet.stapler_s = "";
            App.net.WeeklyVanCheckSheet.worksheets_s = "";
            App.net.WeeklyVanCheckSheet.plasters_s = "";
            App.net.WeeklyVanCheckSheet.dressing_s = "";
            App.net.WeeklyVanCheckSheet.eyewashers_s = "";
            App.net.WeeklyVanCheckSheet.steri_wipes_s = "";
            App.net.WeeklyVanCheckSheet.bag_s = "";
            App.net.WeeklyVanCheckSheet.flexi_meter_s = "";
            App.net.WeeklyVanCheckSheet.merlin_low_e_detector_s = "";
            App.net.WeeklyVanCheckSheet.cabin_condition_s = "";
            App.net.WeeklyVanCheckSheet.national_tyres_card_s = "";
            App.net.WeeklyVanCheckSheet.breakdown_card_s = "";
            App.net.WeeklyVanCheckSheet.fuel_card_s = "";
            App.net.WeeklyVanCheckSheet.shell_points_card_s = "";
            App.net.WeeklyVanCheckSheet.fire_extinguisher_s = "";
            App.net.WeeklyVanCheckSheet.jack_s = "";
            App.net.WeeklyVanCheckSheet.wheelbrace_s = "";
            App.net.WeeklyVanCheckSheet.jump_leads_s = "";
            App.net.WeeklyVanCheckSheet.fan_belt_s = "";
            App.net.WeeklyVanCheckSheet.tow_ropes_s = "";
            App.net.WeeklyVanCheckSheet.spare_oil_s = "";
            App.net.WeeklyVanCheckSheet.coolant_anti_freeze_s = "";
            App.net.WeeklyVanCheckSheet.van_height_sticker_s = "";
            App.net.WeeklyVanCheckSheet.wheel_nut_check_sticker_s = "";
            App.net.WeeklyVanCheckSheet.no_smoking_sticker_s = "";
            App.net.WeeklyVanCheckSheet.racks_and_poles_s = "";
            App.net.WeeklyVanCheckSheet.tyre_conditions_s = "";
            App.net.WeeklyVanCheckSheet.van_locks_s = "";
            App.net.WeeklyVanCheckSheet.oil_and_water_checked_s = "";
            App.net.WeeklyVanCheckSheet.hows_my_driving_sticker_s = "";
            App.net.WeeklyVanCheckSheet.pda_setup_date_s = "";
            App.net.WeeklyVanCheckSheet.accident_pack_on_pda_s = "";
            App.net.WeeklyVanCheckSheet.hi_vis_vests_s = "";
            App.net.WeeklyVanCheckSheet.grinder_s = "";
            App.net.WeeklyVanCheckSheet.windscreen_good_contidion_s = "";

            App.net.WeeklyVanCheckSheet.driver_printed = "";
            App.net.WeeklyVanCheckSheet.checked_printed = "";
            App.net.WeeklyVanCheckSheet.panel_date = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.vehicle_reg = "";
            App.net.WeeklyVanCheckSheet.driver_name = "";
            App.net.WeeklyVanCheckSheet.van_check_name = "";

            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.PDASetupDate = DateTime.Now.ToShortDateString();

            App.net.WeeklyVanCheckSheet.passenger_front_pressure_s = "";
            App.net.WeeklyVanCheckSheet.passenger_rear_pressure_s = "";
            App.net.WeeklyVanCheckSheet.driver_front_pressure_s = "";
            App.net.WeeklyVanCheckSheet.driver_rear_pressure_s = "";
            App.net.WeeklyVanCheckSheet.spare_tyre_pressure_s = "";

            App.net.WeeklyVanCheckSheet.driver_printed = "";
            App.net.WeeklyVanCheckSheet.checked_printed = "";

            App.net.WeeklyVanCheckSheet.marks_out_of_10 = "";

            App.net.WeeklyVanCheckSheet.damage_pass = "";
            App.net.WeeklyVanCheckSheet.damage_driver = "";
            App.net.WeeklyVanCheckSheet.damage_front = "";
            App.net.WeeklyVanCheckSheet.damage_back = "";

            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet2 = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet3 = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet4 = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet5 = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet6 = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet7 = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet8 = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet9 = DateTime.Now.ToShortDateString();
            App.net.WeeklyVanCheckSheet.ManufactureDateOnHelmet10 = DateTime.Now.ToShortDateString();

            App.net.WeeklyVanCheckSheet.spare_s_1 = "";
            App.net.WeeklyVanCheckSheet.spare_s_2 = "";
            App.net.WeeklyVanCheckSheet.spare_s_3 = "";
            App.net.WeeklyVanCheckSheet.spare_s_4 = "";

            App.net.WeeklyVanCheckSheet.new_sspare1 = "";
            App.net.WeeklyVanCheckSheet.new_sspare2 = "";
            App.net.WeeklyVanCheckSheet.new_sspare3 = "";
            App.net.WeeklyVanCheckSheet.new_sspare4 = "";
            App.net.WeeklyVanCheckSheet.new_sspare5 = "";
            App.net.WeeklyVanCheckSheet.new_sspare6 = "";
            App.net.WeeklyVanCheckSheet.new_sspare7 = "";
            App.net.WeeklyVanCheckSheet.new_sspare8 = "";
            App.net.WeeklyVanCheckSheet.new_sspare9 = "";
            App.net.WeeklyVanCheckSheet.new_sspare10 = "";

        }

        public void CreateDeliveryVan()
        {
            App.net.DeliveryVanVehicleCheckList = new DeliveryVanVehicleCheckList();
            App.net.DeliveryVanVehicleCheckList.CheckID = App.net.VanChecksHeader.unique_id;
            App.net.DeliveryVanVehicleCheckList.item_no = App.net.VanChecksHeader.current_item_no++;

            App.net.DeliveryVanVehicleCheckList.not_complete_reason = "";

            App.net.DeliveryVanVehicleCheckList.name = "";
            App.net.DeliveryVanVehicleCheckList.destination = "";
            App.net.DeliveryVanVehicleCheckList.date = DateTime.Today.ToShortDateString();
            App.net.DeliveryVanVehicleCheckList.vehicle_registration = "";
            App.net.DeliveryVanVehicleCheckList.mileage = "";
            App.net.DeliveryVanVehicleCheckList.item_no_4D = "";

            App.net.DeliveryVanVehicleCheckList.ats_card_s = "";
            App.net.DeliveryVanVehicleCheckList.bodywork_check_s = "";
            App.net.DeliveryVanVehicleCheckList.breakdown_card_s = "";
            App.net.DeliveryVanVehicleCheckList.clean_external_s = "";
            App.net.DeliveryVanVehicleCheckList.clean_internal_s = "";
            App.net.DeliveryVanVehicleCheckList.fan_belt_s = "";
            App.net.DeliveryVanVehicleCheckList.fire_extinguisher_s = "";
            App.net.DeliveryVanVehicleCheckList.first_aid_box_s = "";

            App.net.DeliveryVanVehicleCheckList.fuel_card_s = "";

            App.net.DeliveryVanVehicleCheckList.horn_s = "";

            App.net.DeliveryVanVehicleCheckList.jack_s = "";

            App.net.DeliveryVanVehicleCheckList.jump_leads_s = "";

            App.net.DeliveryVanVehicleCheckList.keys_for_branches_s = "";

            App.net.DeliveryVanVehicleCheckList.lights_inducators_s = "";

            App.net.DeliveryVanVehicleCheckList.oil_water_checked_s = "";

            App.net.DeliveryVanVehicleCheckList.racks_poles_s = "";

            App.net.DeliveryVanVehicleCheckList.ratchet_straps_s = "";

            App.net.DeliveryVanVehicleCheckList.receipt_book_s = "";

            App.net.DeliveryVanVehicleCheckList.bump_hats_s = "";

            App.net.DeliveryVanVehicleCheckList.service_due_sticker_s = "";

            App.net.DeliveryVanVehicleCheckList.spanners_for_rack_removal_s = "";

            App.net.DeliveryVanVehicleCheckList.spare_oil_s = "";

            App.net.DeliveryVanVehicleCheckList.coolant_anti_freeze_mix_s = "";

            App.net.DeliveryVanVehicleCheckList.spare_wheel_s = "";

            App.net.DeliveryVanVehicleCheckList.tow_ropes_s = "";

            App.net.DeliveryVanVehicleCheckList.tyre_pressure_s = "";

            App.net.DeliveryVanVehicleCheckList.van_height_sticker_s = "";

            App.net.DeliveryVanVehicleCheckList.van_locks_s = "";

            App.net.DeliveryVanVehicleCheckList.wheel_nut_check_sticker_s = "";

            App.net.DeliveryVanVehicleCheckList.wheelbrace_s = "";

            App.net.DeliveryVanVehicleCheckList.windscreen_washer_s = "";

            App.net.DeliveryVanVehicleCheckList.pda_phone_accident_pack_s = "";

            App.net.DeliveryVanVehicleCheckList.branch_keys_s = "";

            App.net.DeliveryVanVehicleCheckList.passenger_front_pressure_s = "";
            App.net.DeliveryVanVehicleCheckList.passenger_rear_pressure_s = "";
            App.net.DeliveryVanVehicleCheckList.driver_front_pressure_s = "";
            App.net.DeliveryVanVehicleCheckList.driver_rear_pressure_s = "";
            App.net.DeliveryVanVehicleCheckList.spare_tyre_pressure_s = "";

            App.net.DeliveryVanVehicleCheckList.driver_printed = "";
            App.net.DeliveryVanVehicleCheckList.checked_printed = "";

            App.net.DeliveryVanVehicleCheckList.damage_pass = "";
            App.net.DeliveryVanVehicleCheckList.damage_driver = "";
            App.net.DeliveryVanVehicleCheckList.damage_front = "";
            App.net.DeliveryVanVehicleCheckList.damage_back = "";

            App.net.DeliveryVanVehicleCheckList.spare_s_1 = "";
            App.net.DeliveryVanVehicleCheckList.spare_s_2 = "";
            App.net.DeliveryVanVehicleCheckList.spare_s_3 = "";
            App.net.DeliveryVanVehicleCheckList.spare_s_4 = "";

        }

    }
}
