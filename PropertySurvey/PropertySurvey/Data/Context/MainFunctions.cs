using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using System;
using Xamarin.Forms;

namespace PropertySurvey
{
    public partial class Functions
    {
        readonly SQLiteConnection database;

        public Functions(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            update_database();

            database.CreateTable<PropertySurveyItem>();
            database.CreateTable<Header>();
            //database.CreateTable<Latch>();
            //database.CreateTable<Latch_Measurement>();
            database.CreateTable<Message_Text>();
            database.CreateTable<Header_Index>();
            database.CreateTable<Milage_sheet>();
            database.CreateTable<Accident_sheet>();
            database.CreateTable<WhitnessesData>();
            database.CreateTable<VanChecksHeader>();
            database.CreateTable<DeliveryVehicleCheckList>();
            database.CreateTable<WeeklyVanCheckSheet>();
            database.CreateTable<CarPanelSheet>();
            database.CreateTable<DamageLabels>();
            database.CreateTable<DeliveryVanVehicleCheckList>();
            database.CreateTable<LaddersTable>();
            database.CreateTable<FAccidentsTable>();
            database.CreateTable<SCheckHnS>();
            database.CreateTable<MotorSheet>();
            database.CreateTable<ToolsTable>();
            database.CreateTable<app_settings>();

            if(App.net.receive_test_data==false)
                set_debug_data(); // Creates a blank survey for when on a network blocked by Martindales firewall.
        }

        
        private void set_debug_data()
        {
            string todays_date = DateTime.Today.ToShortDateString();

            // Change the dates if you use this.
            database.CreateCommand("delete from header where RecID=46").ExecuteNonQuery();
            database.CreateCommand("insert into header (RecID,iRecordType,udi_date,udi_cont,sn_name,uc_name,udi_jobtext,udi_start,udi_fin,fname1,uc_inceden,COD_String,type,udi_inst,policy_number,stimea) "
                                             + "values (46,0,'" + todays_date + " 00:00:00','00327787','GFD LTD (The Composite Door Sh','Ms Clare Test','','30/07/2019 09:00:00','30/07/2019 13:00:00','GFD04','03/11/2017 00:00:00','Unknown','Survey','','','11:07')").ExecuteNonQuery();
            CreateHeaderIndex();
        }
        
        public class app_setting_version
        {
            public int db_version_number { get; set; }
        }

        private int get_database_version()
        {
            // Return values
            // 0 it's a new database
            // 1 existing database in the formats prior to 01/08/19
            // 2 Update 01/08/19 - Removal of hundreds of unused fields and addition of some secondary indexes
            // 3 Update to conservatory 24/9/19
            // 4 Adding addon's yes/no + width + height to bifold 26/9/19
            // 4+ For future use

            try
            {
                List<app_setting_version> app_info = database.Query<app_setting_version>("select db_version_number FROM app_settings");
                return app_info [0].db_version_number;
            }
            catch
            {
                database.CreateTable<app_settings>();
                LoadSettings();

                // Test if the aluminium table exists to determine if it's a new database
                try
                {
                    database.Query<AlumTable>("select * from AlumTable");
                    // Aluminium table must exist if we get this far.
                    // Therefore it's a database from before 01/08/19
                    App.net.App_Settings.db_version_number = 1;
                    SaveSettings();
                    return 1;
                }
                catch
                {
                    // It's a new database
                    App.net.App_Settings.db_version_number = 0;
                    SaveSettings();
                    return 0;
                }
            }
        }

        private void set_database_version(int new_version_number)
        {
            database.CreateCommand ("update app_settings set db_version_number=?", new string[1] { new_version_number.ToString() }).ExecuteNonQuery();
        }

        private void update_database()
        {
            int version_number = get_database_version();
            if (version_number == 0) // New database
            {
                database.CreateTable<AlumTable>();
                database.CreateTable<BifoldTable>();
                database.CreateTable<CompositeTable>();
                database.CreateTable<ConsTable>();
                database.CreateTable<GarageTable>();
                database.CreateTable<GlassTable>();
                database.CreateTable<GreenTable>();
                database.CreateTable<LockingTable>();
                database.CreateTable<PanelTable>();
                database.CreateTable<TimberTable>();
                database.CreateTable<UPVCTable>();

                database.CreateCommand("create index alum_contract_item on AlumTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index bifold_contract_item on BifoldTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index composite_contract_item on CompositeTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index cons_contract_item on ConsTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index garage_contract_item on GarageTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index glass_contract_item on GlassTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index green_contract_item on GreenTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index locking_contract_item on LockingTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index panel_contract_item on PanelTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index timber_contract_item on TimberTable (udi_cont,item_number)").ExecuteNonQuery();
                database.CreateCommand("create index upvc_contract_item on UPVCTable (udi_cont,item_number)").ExecuteNonQuery();

                set_database_version(2);
            }
            else
            {
                // To add column(s)
                // 1. Change the table definition
                // 2. Add an "if" for the next version_number
                // 3. Call CreateTable

                // To remove or rename column(s)
                // 1. Change the table definition
                // 2. Add an "if" for the next version_number
                // 3. Build the list of columns that are not renamed
                // 4. Rename the table
                // 5. Create the new table without any columns that are to be deleted, and with renamed columns
                // 6. Add in the list of columns that need renaming
                // 7. Copy the old data to the new table
                // 8. Remove the old table
                // 9. Create the secondary index

                // For all changes also do these
                // 1. Update the version_number
                // 2. Remove the update version_number from the previous if
                // 3. Remove any code that is not compatible with the latest version of the table or which is not required.

                string unchanged_field_list;

                if (version_number <= 1) // Do update from prior to 1/8/19 to after 1/8/19
                {
                    string error_str = "";

                    // Aluminium
                    unchanged_field_list = "RecID,item_number,udi_cont,bRepair,cosmetic_damage,additional_locks,gaskets,gaskets_text,handles_req,handles_text,replace_panel,replace_reason,"
                                         + "replace_explain,type,cause_of_damage,cause_of_damage_reason_different,section_type,new_timber_sub_frame,sub_frame_depth,item_frame_width,item_frame_height,"
                                         + "brick_width,brick_height,internal_width,internal_height,frame_type,cill,drip,night_vent,midrail_type,item_color,locking_type,letter_box,letter_box_pos,"
                                         + "pet_flap,pet_type,pet_magnetic,opens,handle_color,spacer_thickness,spacer_color,glass_type,glass_pattern,special_glass,sub_frame_color,bNewLockingMech,"
                                         + "bLockComplete,bHandleDrawingComplete,no_of_pics,midrail_height,no_of_photos,docl,room_location,no_of_vids,LPHandles,threshold_type,bDifferentFromOriginal,"
                                         + "ChangeItemTo,print_name,bFencer,FecerRating,long_comments,bDoorComplete,bWindowComplete,lead_sizeA,lead_sizeB,lead_sizeC,lead_sizeD,"
                                         + "lead_CWidth,lead_CHeight,lead_anti_rattle,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,lead_bSGDesignComplete,"
                                         + "lock_make,lock_codes,bPanelComplete,GearBox,left_bolt,right_bolt,isComplete,lead_comments,is_a_flat,type_of_lockng_system_required,"
                                         + "was_it_locked,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,"
                                         + "lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,";

                    database.CreateCommand("alter table AlumTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<AlumTable>();
                    try
                    {
                        database.CreateCommand("insert into AlumTable (" + unchanged_field_list
                            + "collect_and_copy,temporary,point_of_entry,cill_on_subframe,cill_type,outer_section_width,outer_section_height,glazed,bead_type,parts_to_order,back_to_back_spacer_width,back_to_back_spacer_height) select " + unchanged_field_list
                            + "new_ispare1,new_ispare2,ex_i_spare6,i_spare1,i_spare2, new_sspare1,new_sspare2,new_ispare4,new_ispare5,new_sspare4,ex_s_spare4,ex_s_spare5 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    // Bifold
                    unchanged_field_list = "RecID,item_number,udi_cont,internal_width,internal_height,overall_width,overall_height,opens,trickle_vents,hardware,color_internal,color_external,"
                                         + "no_of_pics,no_of_photos,no_of_vids,isComplete,comments,cause_of_damage,cause_of_damage_reason_different,type_of_lockng_system_required,"
                                         + "was_it_locked,ChangeItemTo,print_name,bDifferentFromOriginal,";

                    database.CreateCommand("alter table BifoldTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<BifoldTable>();
                    try
                    {
                        database.CreateCommand("insert into BifoldTable (" + unchanged_field_list
                        + "threshold_type,bifold_signed,number_of_doors,door_type,glazing_options,number_of_doors_text,colour_of_doors,handle_colour,cill_type,knock_on,internal_door_colour,parts_to_order,point_of_entry) select " + unchanged_field_list
                        + "sill_type,i_spare1,i_spare3,s_spare2,s_spare3,s_spare4,s_spare5,s_spare6,s_spare7,s_spare9,s_spare10,new_sspare4,ex_i_spare6 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    // Composite
                    unchanged_field_list = "RecID,udi_cont,item_number,cause_of_damage,isComplete,cause_of_damage_reason_different,door_make,opens,is_lock,frame_colour_inside,frame_colour_outside,"
                                         + "door_colour_inside,door_colour_outside,door_design,glass_design,internal_width,internal_height,brick_width,brick_height,trickle_vents,"
                                         + "addons,addons_height,addons_width,handle_colour,threshold_type,lever_pad_handles,glass_pattern,glass_type,spacer_thickness,spacer_colour,"
                                         + "profile_type,room_location,special_glass,comments,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,docl,"
                                         + "letteredit,letter_box_pos,pet_flap,pet_type,pet_magnetic,glaze,print_name,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,"
                                         + "no_of_pics,no_of_photos,no_of_vids,bDifferentFromOriginal,lock_other_text,head_drip,ChangeItemTo,cills,door_wood,lead_comments,is_a_flat,"
                                         + "type_of_lockng_system_required,was_it_locked,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,";

                    database.CreateCommand("alter table CompositeTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<CompositeTable>();
                    try
                    {
                        database.CreateCommand("insert into CompositeTable (" + unchanged_field_list
                        + "reason_not_repaired,fire_door,hinged_on,point_of_entry,parts_to_order) select " + unchanged_field_list
                        + "s_spare3,ex_new_ispare4,i_spare1,ex_i_spare6,new_sspare4 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    // Conservatory
                    unchanged_field_list = "RecID,udi_cont,item_number,isComplete,type,cause_of_damage,cause_of_damage_reason_different,material_type,sizeA,sizeB,sizeC,sizeD,sizeE,sizeF,sizeG,"
                                         + "angle1,angle2,angle3,angle4,pitch_height,profile_section_size,sheet_width_1,sheet_height_1,sheet_width_2,sheet_height_2,sheet_width_3,sheet_height_3,"
                                         + "sheet_width_4,sheet_height_4,sheet_width_5,sheet_height_5,sheet_width_6,sheet_height_6,sheet_width_7,sheet_height_7,sheet_width_8,sheet_height_8,"
                                         + "sheet_width_9,sheet_height_9,sheet_width_10,sheet_height_10,flute_size,color,roof_color,new_firrings_req,new_gutters_req,roof_glazing_thickness,"
                                         + "no_of_pics,no_of_photos,bDifferentFromOriginal,ChangeItemTo,print_name,wall_pos,pitch_degree,long_comments,bDrawingsOnly,cons_roof_under_drawn,"
                                         + "type_of_lockng_system_required,was_it_locked,";
                    database.CreateCommand("alter table ConsTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<ConsTable>();
                    try
                    {
                        database.CreateCommand("insert into ConsTable (" + unchanged_field_list
                            + "point_of_entry,spars_line_up,good_conditions,ridge_length,parts_to_order,does_roof_fit_under,roof_sheets_quantity_1,roof_sheets_quantity_2,roof_sheets_quantity_3,roof_sheets_quantity_4,roof_sheets_quantity_5,roof_sheets_quantity_6,roof_sheets_quantity_7,roof_sheets_quantity_8,roof_sheets_quantity_9,roof_sheets_quantity_10) select " + unchanged_field_list
                            + "ex_i_spare6,i_spare3,new_ispare3,new_sspare1,new_sspare4,i_spare2,sheet_width_11,sheet_width_12,sheet_width_13,sheet_width_14,sheet_width_15,sheet_width_16,sheet_width_17,sheet_width_18,sheet_width_19,sheet_width_20 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    // Garage
                    unchanged_field_list = "RecID,item_number,udi_cont,cause_of_damage,cause_of_damage_reason_different,door_fits_into,new_subframe_req,obstruction_outside_b,obstruction_outside,"
                                         + "obstruction_inside_b,obstruction_inside,actual_door_width,actual_door_height,frame_fix_type,type_of_garage,new_electric_operator_req,"
                                         + "side_size_A,side_size_B,side_size_C,side_size_D,side_size_E,side_size_F,side_size_G,side_timber_1,side_timber_2,"
                                         + "plan_size_A,plan_size_B,plan_size_C1,plan_size_C2,plan_size_D,plan_timber_1,plan_timber_2,color,opening_type,finish,"
                                         + "power_points,electric_door,handle_outside,other_access,need_safety_release,no_of_pics,no_of_photos,insulated,door_stuck_shut,motor_position,"
                                         + "no_of_vids,bDifferentFromOriginal,ChangeItemTo,print_name,long_comments,isComplete,type_of_lockng_system_required,was_it_locked,";

                    database.CreateCommand("alter table GarageTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<GarageTable>();
                    try
                    {
                        database.CreateCommand("insert into GarageTable (" + unchanged_field_list
                            + "point_of_entry,opening_direction,door_within_perimeter,wire_type,socket_within_1m,roller_door_type,roller_box_type,colour_match_roll_box,parts_to_order) select " + unchanged_field_list
                            + "ex_i_spare6,item_attached_to,i_spare1,s_spare3,i_spare2,new_sspare1,new_sspare2,new_ispare3,new_sspare4 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    // Glass
                    unchanged_field_list = "RecID,udi_cont,item_number,isComplete,cause_of_damage,cause_of_damage_reason_different,units_required,glass_width,glass_height,glass_width2,glass_height2,"
                                         + "glass_width3,glass_height3,glass_width4,glass_height4,glass_width5,glass_height5,glass_width6,glass_height6,glass_width7,glass_height7,glass_width8,glass_height8,"
                                         + "stepped_unit,int_width,int_height,single_or_double,glass_type,sizeA,sizeB,sizeC,sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,"
                                         + "lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,glass_pattern,spacer_color,spacer_thickness,special_glass,no_of_pics,docl_old,no_of_photos,"
                                         + "gb_trim,docl,room_location,no_of_vids,bDifferentFromOriginal,ChangeItemTo,print_name,ProductInto,long_comments,lead_posX,lead_posY,lead_comments,TapeorGasket,"
                                         + "type_of_lockng_system_required,was_it_locked,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,sizeAf,sizeBf,sizeCf,sizeDf,";
                    database.CreateCommand("alter table GlassTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<GlassTable>();
                    try
                    {
                        database.CreateCommand("insert into GlassTable (" + unchanged_field_list
                            + "glazing_type,point_of_entry,collect_and_copy,temporary,glaze,parts_to_order,back_to_back_spacer_width,back_to_back_spacer_height) select " + unchanged_field_list
                            + "JointType,ex_i_spare6,new_ispare1,new_ispare2,i_spare3,new_sspare4,ex_s_spare4,ex_s_spare5 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();
                    database.CreateCommand("update GlassTable set parent_item=0").ExecuteNonQuery();

                    // Greenhouse
                    unchanged_field_list = "RecID,udi_cont,item_number,isComplete,bDifferentFromOriginal,cause_of_damage,cause_of_damage_reason_different,rep_reason,material_type,colour,"
                                         + "glaze_type,base_size,base_size_x,base_size_y,type_of_glass,door_opening_type,window_opening_type,roof_opening_lights,auto_or_manual,overall_height,"
                                         + "summary,no_of_pics,no_of_photos,no_of_vids,type_of_lockng_system_required,was_it_locked,ChangeItemTo,print_name,";
                    database.CreateCommand("alter table GreenTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<GreenTable>();
                    try
                    {
                        database.CreateCommand("insert into GreenTable (" + unchanged_field_list
                            + "point_of_entry,parts_to_order) select " + unchanged_field_list
                            + "ex_i_spare6,new_sspare4 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    // Locking
                    unchanged_field_list = "RecID,udi_cont,item_number,isComplete,comments,type_of_lockng_system_required,was_it_locked,no_of_pics,no_of_photos,bMulti,item,locking_make,locking_codes,"
                                         + "bDoorComplete,bWindowComplete,lock_colour,pagenum,bDifferentFromOriginal,ChangeItemTo,print_name,COD_Code,cause_of_damage,cause_of_damage_reason_different,"
                                         + "GearBox,no_of_vids,left_bolt,right_bolt,bLockComplete,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,"
                                         + "l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,long_comments,";
                    database.CreateCommand("alter table LockingTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<LockingTable>();
                    try
                    {
                        database.CreateCommand("insert into LockingTable (" + unchanged_field_list
                            + "parts_to_order) select " + unchanged_field_list
                            + "new_sspare4 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    // Panel
                    unchanged_field_list = "RecID,udi_cont,item_number,isComplete,cause_of_damage,cause_of_damage_reason_different,knockedit,knocoledit,letteredit,letter_box_pos,wedit,hedit,"
                                         + "typeedit,thickedit,backgedit,coledit,gltext,spaccoloedit,pet_flap,pet_type,pet_magnetic,no_of_pics,no_of_photos,no_of_vids,room_location,"
                                         + "bDifferentFromOriginal,ChangeItemTo,print_name,long_sptext,upvc_item_number,alum_item_number,type_of_lockng_system_required,was_it_locked,";
                    database.CreateCommand("alter table PanelTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<PanelTable>();
                    try
                    {
                        database.CreateCommand("insert into PanelTable (" + unchanged_field_list
                            + "point_of_entry,parts_to_order) select " + unchanged_field_list
                            + "ex_i_spare6,new_sspare4 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    // Timber
                    unchanged_field_list = "RecID,udi_cont,item_number,isComplete,bRepair,cosmetic_damage,additional_locks,gaskets,gaskets_text,handles_req,handles_text,replace_reason,replace_explain,"
                                         + "timber_item,cause_of_damage,cause_of_damage_reason_different,timber_wood,timber_frame_wood,timber_new_frame_req,brick_width,brick_height,"
                                         + "internal_width,internal_height,repair_frame,door_thickness,door_width,door_height,opens,new_sash_required,head_drip,cills,draught_strip,"
                                         + "pet_flap,pet_type,pet_magnetic,bDoorComplete,bWindowComplete,beading_type,thresher,single_double,trickle_vents,locks,hardware_color,door_color,"
                                         + "frame_color,spacer_thickness,spacer_color,glass_type,glass_pattern,special_glass,bNewLockingMech,bLockComplete,bHandleDrawingComplete,"
                                         + "no_of_pics,no_of_photos,no_of_vids,docl,bSashDrawn,bSectionDrawn,bMouldingDrawn,room_location,doc_l_compliant_reason,doc_l_compliant,"
                                         + "door_color_out,frame_color_out,door_color_code,door_color_code_out,frame_color_code,frame_color_code_out,b_signed,slide_position,timber_glazed,"
                                         + "bDifferentFromOriginal,ChangeItemTo,print_name,standard_sizes,reasonnonstandard,long_timber_comments,lead_sizeA,lead_sizeB,lead_sizeC,lead_sizeD,"
                                         + "lead_CWidth,lead_CHeight,lead_anti_rattle,lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,"
                                         + "lock_make,lock_codes,GearBox,left_bolt,right_bolt,letter_box,letter_box_pos,moulding,hinge_type,lead_comments,is_a_flat,type_of_lockng_system_required,"
                                         + "was_it_locked,l_size1,l_size2,l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,"
                                         + "l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,"
                                         + "lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,";
                    database.CreateCommand("alter table TimberTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<TimberTable>();
                    try
                    {
                        database.CreateCommand("insert into TimberTable (" + unchanged_field_list
                            + "Fensa,WER_rating,collect_and_copy,temporary,pre_glazed_door,point_of_entry,weather_bar,parts_to_order,back_to_back_spacer_width,back_to_back_spacer_height) select " + unchanged_field_list
                            + "bFencer,FecerRating,i_spare1,i_spare2,i_spare3,ex_i_spare6,new_ispare3,new_sspare4,ex_s_spare4,ex_s_spare5 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    // UPVC
                    unchanged_field_list = "RecID,udi_cont,item_number,isComplete,bRepair,cosmetic_damage,additional_locks,gaskets,gaskets_text,handles_req,handles_text,replace_panel,"
                                         + "replace_reason,replace_explain,upvc_item,cause_of_damage,cause_of_damage_reason_different,colour,cills,outer_section_size,internal_width,internal_height,"
                                         + "brick_width,brick_height,midrail,addons,head_drip,handle_colour,locking_type,letter_box,letter_box_pos,pet_flap,pet_type,pet_magnetic,bead_type,"
                                         + "opens,glaze,trickle_vents,spacer_thickness,spacer_colour,glass_type,glass_pattern,special_glass,internal_lock,bNewLockingMech,bLockComplete,bHandleDrawingComplete,"
                                         + "no_of_pics,midrail_height,no_of_photos,frame_depth,docl,profile_type,room_location,no_of_vids,LPHandles,slide_position,threshold_type,bDifferentFromOriginal,"
                                         + "ChangeItemTo,print_name,long_comments,bDoorComplete,bWindowComplete,lead_sizeA,lead_sizeB,lead_sizeC,lead_sizeD,lead_CWidth,lead_CHeight,lead_anti_rattle,"
                                         + "lead_thickness,lead_sod,lead_type,lead_bDiamondComplete,lead_bGeorgianComplete,lead_bBarComplete,lock_make,lock_codes,bPanelComplete,left_bolt,right_bolt,"
                                         + "GearBox,lead_comments,is_a_flat,type_of_lockng_system_required,was_it_locked,l_size1,l_size2,"
                                         + "l_sizeA,l_sizeB,l_sizeC,l_sizeD,l_sizeE,l_sizeF,l_sizeG,l_num,l_fpos1,l_fpos2,l_fpos3,l_fpos4,l_fpos5,l_fpos6,l_fpos7,lock_position,"
                                         + "l_itype1,l_itype2,l_itype3,l_itype4,l_itype5,l_itype6,l_itype7,lead_CWidthf,lead_CHeightf,lead_CWidths,lead_CHeights,";
                    database.CreateCommand("alter table UPVCTable rename to temp").ExecuteNonQuery();
                    database.CreateTable<UPVCTable>();
                    try
                    {
                        database.CreateCommand("insert into UPVCTable (" + unchanged_field_list
                            + "addon_width,addon_height,double_tripple,fensa,WER_Rating,collect_and_copy,temporary,point_of_entry,hinge_colour,parts_to_order,back_to_back_spacer_width,back_to_back_spacer_height) select " + unchanged_field_list
                            + "addon_width_long,addon_height_long,single_double,bFencer,FecerRating,new_ispare1,new_ispare2,ex_i_spare6,s_spare3,new_sspare4,ex_s_spare4,ex_s_spare5 from temp").ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        error_str += ex.Message;
                    };
                    database.CreateCommand("drop table temp").ExecuteNonQuery();

                    database.CreateCommand("create index alum_contract_item on AlumTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index bifold_contract_item on BifoldTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index composite_contract_item on CompositeTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index cons_contract_item on ConsTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index garage_contract_item on GarageTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index glass_contract_item on GlassTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index green_contract_item on GreenTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index locking_contract_item on LockingTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index panel_contract_item on PanelTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index timber_contract_item on TimberTable (udi_cont,item_number)").ExecuteNonQuery();
                    database.CreateCommand("create index upvc_contract_item on UPVCTable (udi_cont,item_number)").ExecuteNonQuery();
                }

                if (version_number <= 2) // Do update from prior to 24/9/19
                    database.CreateTable<ConsTable>();

                if (version_number <= 3) // Do update from prior to 26/9/19
                {
                    database.CreateTable<BifoldTable>();
                    set_database_version(4); // When we add more updates, move this to the last update and increase number
                }
            }
        }

        public void CreateMileage()
        {
            App.net.MileageRecord = new Milage_sheet();

            App.net.MileageRecord.sheet_date = DateTime.Now.ToShortDateString();
            App.net.MileageRecord.start_postcode = "";
            App.net.MileageRecord.finish_postcode = "";
            App.net.MileageRecord.start_time = "00:00";
            App.net.MileageRecord.end_time = "00:00";
            App.net.MileageRecord.start_mileage = "";
            App.net.MileageRecord.end_mileage = "";
            App.net.MileageRecord.no_of_other_places = 0;
            App.net.MileageRecord.time1 = DateTime.Now.ToShortDateString();
            App.net.MileageRecord.pcode1 = "";
            App.net.MileageRecord.time2 = DateTime.Now.ToShortDateString();
            App.net.MileageRecord.pcode2 = "";
            App.net.MileageRecord.time3 = DateTime.Now.ToShortDateString();
            App.net.MileageRecord.pcode3 = "";
            App.net.MileageRecord.registration = "";

            App.net.MileageRecord.toll_charges = 2;
            App.net.MileageRecord.toll_charge_for = "";
            App.net.MileageRecord.toll_charge_ammount = "";
            App.net.MileageRecord.new_sspare1 = "";
            App.net.MileageRecord.new_sspare2 = "";

            App.net.MileageRecord.op_postcode1 = "";
            App.net.MileageRecord.op_postcode2 = "";
            App.net.MileageRecord.op_postcode3 = "";
            /*
            App.net.MileageRecord.op_postcode4 = "";
            App.net.MileageRecord.op_postcode5 = "";
            App.net.MileageRecord.op_postcode6 = "";
            App.net.MileageRecord.op_postcode7 = "";
            App.net.MileageRecord.op_postcode8 = "";
            App.net.MileageRecord.op_postcode9 = "";
            App.net.MileageRecord.op_postcode10 = "";
            App.net.MileageRecord.op_postcode11 = "";
            App.net.MileageRecord.op_postcode12 = "";
            App.net.MileageRecord.op_postcode13 = "";
            App.net.MileageRecord.op_postcode14 = "";
            App.net.MileageRecord.op_postcode15 = "";
            */

            App.net.MileageRecord.op_time1 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time2 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time3 = DateTime.Now.ToShortTimeString();
            /*
            App.net.MileageRecord.op_time4 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time5 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time6 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time7 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time8 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time9 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time10 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time11 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time12 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time13 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time14 = DateTime.Now.ToShortTimeString();
            App.net.MileageRecord.op_time15 = DateTime.Now.ToShortTimeString();
            */
        }

        

        public void SaveMessage(Message_Text message_text_rec)
        {
            if (database.Table<Message_Text>().Where(i => i.ID == message_text_rec.ID).Count() == 0)
            {
                database.Insert(message_text_rec);
            }
            else
            {
                database.Update(message_text_rec);
            }
        }

        public List<Message_Text> GetMessages()
        {
            return database.Query<Message_Text>("SELECT * FROM [Message_Text]");
        }

        public int CountUnsentMessages()
        {
            return database.Table<Message_Text>().Where(i => i.bRead == false).Count();
        }

        public Message_Text GetMessage(int id)
        {
            return database.Table<Message_Text>().Where(i => i.RecID == id).FirstOrDefault();
        }

        public void DeleteOldMessages()
        {
            DateTime daysago = DateTime.Today.AddDays(-30);

            List<Message_Text> messages = database.Query<Message_Text>("SELECT * FROM [Message_Text] WHERE DATE([message_date]) < DATE(" + daysago.ToShortDateString() + ")");
            foreach (var item in messages)
            {
                database.Delete(item);
                // Needs delete photos
            }
        }


        public void DeleteMileageSheet(int id)
        {
            database.Delete(database.Table<Milage_sheet>().Where(i => i.RecID == id).FirstOrDefault());
        }

        public void LoadMileage(int id)
        {
            App.net.MileageRecord = database.Table<Milage_sheet>().Where(i => i.RecID == id).FirstOrDefault();
        }

        public List<Milage_sheet> GetMileageSheets()
        {
            DeleteMileageSheets();
            return database.Query<Milage_sheet>("SELECT * FROM [Milage_sheet]");
        }

        public void DeleteMileageSheets()
        {
            DateTime daysago = DateTime.Today.AddDays(-30);

            List<Milage_sheet> sheets = database.Query<Milage_sheet>("SELECT * FROM [Milage_sheet] WHERE DATE([sheet_date]) < DATE(" + daysago.ToShortDateString() + ")");
            foreach (var item in sheets)
            {
                database.Delete(item);
                App.files.DeleteFile(item.signature_filename);
                App.files.DeleteFile("Photos/" + item.new_sspare1);
                App.files.DeleteFile("Photos/" + item.new_sspare2);
            }
        }

        public void SaveMileage()
        {
            if (App.net.MileageRecord.RecID != 0)
            {
                database.Update(App.net.MileageRecord);
            }
            else
            {
                database.Insert(App.net.MileageRecord);
            }
        }

        public int SaveHeader()
        {
            if (App.net.HeaderRecord == null)
                return 0;
            //Update header index
            List<Header_Index> list = database.Table<Header_Index>().Where(i => i.udi_cont == App.net.HeaderRecord.udi_cont).ToList();

            foreach(var item in list)
            {
                item.bSent = App.net.HeaderRecord.bSent;
                item.bDone = App.net.HeaderRecord.bDone;
                database.Update(item);
            }

            if (App.net.HeaderRecord.RecID != 0)
                return database.Update(App.net.HeaderRecord);
            else
                return database.Insert(App.net.HeaderRecord);
        }

        public int DeleteHeader(Header item)
        {
            return database.Delete(item);
        }

        public void CreateHeaderIndex()
        {
            database.DeleteAll<Header_Index>();// .execSQL("delete from " + TABLE_NAME);
            DeleteOldContracts();

            List<Header> items = database.Query<Header>("SELECT * FROM [Header]");

            foreach (var item in items)
            {
                Header_Index tempindex = new Header_Index();

                if (item.udi_jobtext != "" && item.udi_jobtext != "?")
                {
                    tempindex.bSpecialIns = true;
                    //if (item.bInfoSeen==true)
                    //{
                        //tempindex.bSpecialIns = false;
                    //}
                    //else
                    //{
                    ///    tempindex.bSpecialIns = true;
                    //}
                }
                else
                {
                    //tempindex.bSpecialIns = false;
                }

                tempindex.iRecordType = item.iRecordType;
                tempindex.header_rec_id = item.RecID;

                if (tempindex.iRecordType == 0)
                {
                    tempindex.udi_cont = item.udi_cont;
                    tempindex.udi_date = item.udi_date;
                    tempindex.udi_start = item.udi_start;
                    tempindex.udi_fin = item.udi_fin;
                    tempindex.uc_postcode = item.uc_postcode;
                    tempindex.uc_name = item.uc_name;
                }
                else
                //if (tempindex.iRecordType == 1 || tempindex.iRecordType == 2 )
                {
                    tempindex.udi_cont = item.udi_cont;

                    tempindex.udi_date = item.fit_diary;
                    tempindex.udi_start = item.fit_start;
                    tempindex.udi_fin = item.fit_fin;
                    tempindex.uc_postcode = item.uc_postcode;
                    tempindex.uc_name = item.uc_name;
                }

                tempindex.bSent = item.bSent;
                tempindex.bDone = item.bDone;
                tempindex.b_mrk = item.b_mrk;
                if (item.type == "Complaint")
                {
                    tempindex.typeA = "Complaint";
                }
                else
                {
                    tempindex.typeA = item.typeA;
                }
                tempindex.typeB = item.typeB;

                database.Insert(tempindex);
            }
        }

        public List<Header> GetUnsentHeaders()
        {
            return database.Table<Header>().Where(i => i.bDone == true & i.bSent == false).ToList();
        }

        public List<Header> GetUnsentHeadersSurveys()
        {
            return database.Table<Header>().Where(i => i.bDone == true & i.bSent == false & (i.iRecordType == 0 ||
                              i.survey_on_fit == 1)).ToList();
        }

        public List<Header> GetUnsentHeadersFittings()
        {
            return database.Table<Header>().Where(i => i.bDone == true & i.bSent == false & i.iRecordType > 0).ToList();
        }

        public void AddSurveyHeader(Header survey_header)
        {
            List<Header> headers = database.Query<Header>("SELECT * FROM [Header] WHERE [udi_cont] = '" + survey_header.udi_cont + "'");

            bool bFound = false;
            if (true)
            {
                foreach (var header in headers)
                {
                    DateTime header_udi_date = DateTime.Parse(header.udi_date);

                    if (header.udi_date != App.net.HeaderRecord.udi_date &&
                        header_udi_date < DateTime.Today)
                    {
                        database.Delete(header);
                    }
                    else
                    {
                        bFound = true;
                    }
                }
                if (bFound == false)
                {
                    database.Insert(survey_header);
                }
            }
        }

        public void DeleteOldContracts()
        {
            return;

            DateTime daysago = DateTime.Today.AddDays(-30);

            //Surveys
            List<Header> headers = database.Query<Header>("SELECT * FROM [Header] WHERE [iRecordType] = 0 AND DATE([udi_date]) < DATE(" + daysago.ToShortDateString() + ")");
            foreach (var item in headers)
            {
                DeleteContract(item.udi_cont);
            }

            //Fitting
            List<Header> headers2 = database.Query<Header>("SELECT * FROM [Header] WHERE [iRecordType] = 1 AND DATE([fit_diary]) < DATE(" + daysago.ToShortDateString() + ")");
            foreach (var item in headers2)
            {
                DeleteContract(item.udi_cont);
            }
        }

        public void DeleteContract(string udi_cont, bool delete_image = true)
        {
            Header header = database.Table<Header>().Where(i => i.udi_cont == udi_cont).FirstOrDefault();

            if(header!=null)
            {
                database.Delete(header);
                if(delete_image==true)
                { 
                    foreach (var item in App.files.GetFileList("Photos/", udi_cont + "*.*", "Photos/"))
                    {
                        App.files.DeleteFile(item);
                    }
                    foreach (var item in App.files.GetFileList("Photos/SS/", udi_cont + "*.*", "Photos/SS/"))
                    {
                        App.files.DeleteFile(item);
                    }
                    foreach (var item in App.files.GetFileList("Drawings/", udi_cont + "*.*", "Drawings/"))
                    {
                        App.files.DeleteFile(item);
                    }
                    foreach (var item in App.files.GetFileList("Signatures/", udi_cont + "*.*", "Signatures/"))
                    {
                        App.files.DeleteFile(item);
                    }
                    foreach (var item in App.files.GetFileList("Videos/", udi_cont + "*.*", "Videos/"))
                    {
                        App.files.DeleteFile(item);
                    }
                }

                {
                    List<PanelTable> query = App.data.GetPanelsByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
                {
                    var query = App.data.GetAlumsByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
                {
                    var query = App.data.GetConssByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
                {
                    var query = App.data.GetCompByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
                {
                    var query = App.data.GetGaragesByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
                {
                    var query = App.data.GetTimberByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
                {
                    var query = App.data.GetUPVCsByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
                {
                    var query = App.data.GetGlasssByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
                {
                    var query = App.data.GetLockByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
                {
                    var query = App.data.GetBifoldsByContract(udi_cont);
                    foreach (var item in query)
                    {
                        database.Delete(item);
                    }
                }
            }
        }

        public List<Header_Index> GetHeaderIndexByDate(DateTime date)
        {
            //return database.Query<Header_Index>("SELECT * FROM [Header_Index]");
            return database.Query<Header_Index>("SELECT * FROM [Header_Index] WHERE [udi_date] = '" + date.ToShortDateString() + "' OR [udi_date] = '" + date.ToString() + "'");
        }

        public List<Header> GetHeadersNotDone()
        {
            return database.Query<Header>("SELECT * FROM [Header] WHERE [bDone] = 0");
        }

        public Header GetHeader(int id)
        {
            return database.Table<Header>().Where(i => i.RecID == id).FirstOrDefault();
        }

        public Header GetHeaderByContract(string udi_cont)
        {
            return database.Table<Header>().Where(i => i.udi_cont == udi_cont).FirstOrDefault();
        }


        public void SaveItem()
        {
            if (App.net.CurrentItem == "upvc") { SaveUPVC(); }
            if (App.net.CurrentItem == "panel") { SavePanel(); }
            if (App.net.CurrentItem == "green") { SaveGreen(); }
            if (App.net.CurrentItem == "glass") { SaveGlass(); }
            if (App.net.CurrentItem == "alum") { SaveAlum(); }
            if (App.net.CurrentItem == "garage") { SaveGarage(); }
            if (App.net.CurrentItem == "timber") { SaveTimber(); }
            if (App.net.CurrentItem == "cons") { SaveCons(); }
            if (App.net.CurrentItem == "lock") { SaveLocking(); }
            if (App.net.CurrentItem == "comp") { SaveComp(); }
            if (App.net.CurrentItem == "bifold") { SaveBifold(); }
        }

        public async void SaveItemAsync()
        {
            if (App.net.CurrentItem == "upvc") { SaveUPVC(); }
            if (App.net.CurrentItem == "panel") { SavePanel(); }
            if (App.net.CurrentItem == "green") { SaveGreen(); }
            if (App.net.CurrentItem == "glass") { SaveGlass(); }
            if (App.net.CurrentItem == "alum") { SaveAlum(); }
            if (App.net.CurrentItem == "garage") { SaveGarage(); }
            if (App.net.CurrentItem == "timber") { SaveTimber(); }
            if (App.net.CurrentItem == "cons") { SaveCons(); }
            if (App.net.CurrentItem == "lock") { SaveLocking(); }
            if (App.net.CurrentItem == "comp") { SaveComp(); }
            if (App.net.CurrentItem == "bifold") { SaveBifold(); }
        }

        public List<PanelTable> GetPanelsByContract(string udi_cont, bool isComplete = false)
        {
            if (isComplete == true)
                return database.Query<PanelTable>("SELECT * FROM [PanelTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Query<PanelTable>("SELECT * FROM [PanelTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }
        public List<PanelTable> GetPanelsByContractNotSubItem(string udi_cont)
        {
            return database.Table<PanelTable>().Where(i => i.udi_cont == udi_cont & i.upvc_item_number ==0 & i.alum_item_number == 0).ToList();
        }
        public PanelTable GetPanelByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<PanelTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public PanelTable GetPanelByContractUPVCItemNo(string udi_cont, int item_no)
        {
            return database.Table<PanelTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public PanelTable GetPanelByContractAlumItemNo(string udi_cont, int item_no)
        {
            return database.Table<PanelTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public int DeletePanelByContractItemNo(string udi_cont, int item_no)
        {
            return database.Delete(database.Table<PanelTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }

        public void CopyPanelByContractItemNo(string udi_cont, int item_no)
        {
            PanelTable record = database.Table<PanelTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);
        }

        //List<Latch_Measurement> GetLatchMeasurementsByContractItemNo(string udi_cont, int item_no)
        //{
        //    return database.Table<Latch_Measurement>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).ToList();
        //}

        public void SavePanel()
        {
            if (App.net.PanelRecord.RecID != 0)
                database.Update(App.net.PanelRecord);
            else
                database.Insert(App.net.PanelRecord);
        }
        public void SavePanel(PanelTable p)
        {
            if (p.RecID != 0)
                database.Update(p);
            else
                database.Insert(p);
        }
        public int SavePanel(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.PanelRecord.isComplete = 1;
            else
                App.net.PanelRecord.isComplete = 0;

            if (App.net.PanelRecord.RecID != 0)
                return database.Update(App.net.PanelRecord);
            else
                return database.Insert(App.net.PanelRecord);
        }

        public List<UPVCTable> GetUPVCsByContract(string udi_cont, bool isComplete = false)
        {
            if (isComplete == true)
                return database.Query<UPVCTable>("SELECT * FROM [UPVCTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Table<UPVCTable>().Where(i => i.udi_cont == udi_cont).ToList();
        }

        public UPVCTable GetUPVCByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<UPVCTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public int DeleteUPVCByContractItemNo(string udi_cont, int item_no)
        {
            SQLiteCommand sqlCommand = database.CreateCommand("delete from GlassTable where udi_cont=? and item_number=?",
                                                              new string[2] { udi_cont, item_no.ToString() });
            sqlCommand.ExecuteNonQuery();
            return database.Delete(database.Table<UPVCTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }
        public void CopyUPVCByContractItemNo(string udi_cont, int item_no)
        {
            UPVCTable record = database.Table<UPVCTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);

            copy_glass_if_it_exists (udi_cont, item_no, record.item_number);
            copy_panel_if_it_exists(udi_cont, item_no, record.item_number);
        }
        public void SaveUPVC()
        {
            if (App.net.UPVCRecord.RecID != 0)
                database.Update(App.net.UPVCRecord);
            else
                database.Insert(App.net.UPVCRecord);
        }
        public void SaveUPVC(UPVCTable u)
        {
            if (u.RecID != 0)
                database.Update(u);
            else
                database.Insert(u);
        }
        public int SaveUPVC(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.UPVCRecord.isComplete = 1;
            else
                App.net.UPVCRecord.isComplete = 0;

            if (App.net.UPVCRecord.RecID != 0)
                return database.Update(App.net.UPVCRecord);
            else
                return database.Insert(App.net.UPVCRecord);
        }

        public List<GarageTable> GetGaragesByContract(string udi_cont, bool isComplete = false)
        {
            if (isComplete == true)
                return database.Query<GarageTable>("SELECT * FROM [GarageTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Query<GarageTable>("SELECT * FROM [GarageTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }
        public GarageTable GetGarageByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<GarageTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public int DeleteGarageByContractItemNo(string udi_cont, int item_no)
        {
            return database.Delete(database.Table<GarageTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }
        public void CopyGarageByContractItemNo(string udi_cont, int item_no)
        {
            GarageTable record = database.Table<GarageTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);
        }
        public void SaveGarage()
        {
            if (App.net.GarageRecord.RecID != 0)
                database.Update(App.net.GarageRecord);
            else
                database.Insert(App.net.GarageRecord);
        }
        public void SaveGarage(GarageTable g)
        {
            if (g.RecID != 0)
                database.Update(g);
            else
                database.Insert(g);
        }
        public int SaveGarage(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.GarageRecord.isComplete = 1;
            else
                App.net.GarageRecord.isComplete = 0;

            if (App.net.GarageRecord.RecID != 0)
                return database.Update(App.net.GarageRecord);
            else
                return database.Insert(App.net.GarageRecord);
        }

        public List<AlumTable> GetAlumsByContract(string udi_cont, bool isComplete = false)
        {
            if (isComplete == true)
                return database.Query<AlumTable>("SELECT * FROM [AlumTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Query<AlumTable>("SELECT * FROM [AlumTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }

        public AlumTable GetAlumByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<AlumTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public int DeleteAlumByContractItemNo(string udi_cont, int item_no)
        {
            SQLiteCommand sqlCommand = database.CreateCommand("delete from GlassTable where udi_cont=? and item_number=?",
                                                              new string[2] { udi_cont, item_no.ToString() });
            sqlCommand.ExecuteNonQuery();
            return database.Delete(database.Table<AlumTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }
        public void CopyAlumByContractItemNo(string udi_cont, int item_no)
        {
            AlumTable record = database.Table<AlumTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);

            copy_glass_if_it_exists (udi_cont, item_no, record.item_number);
            copy_panel_if_it_exists(udi_cont, item_no, record.item_number);
        }
        public void SaveAlum()
        {
            if (App.net.AlumRecord.RecID != 0)
                database.Update(App.net.AlumRecord);
            else
                database.Insert(App.net.AlumRecord);
        }
        public void SaveAlum(AlumTable a)
        {
            if (a.RecID != 0)
                database.Update(a);
            else
                database.Insert(a);
        }
        public int SaveAlum(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.AlumRecord.isComplete = 1;
            else
                App.net.AlumRecord.isComplete = 0;

            if (App.net.AlumRecord.RecID != 0)
                return database.Update(App.net.AlumRecord);
            else
                return database.Insert(App.net.AlumRecord);
        }

        public List<TimberTable> GetTimberByContract(string udi_cont, bool isComplete = false)
        {
            if (isComplete == true)
                return database.Query<TimberTable>("SELECT * FROM [TimberTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Query<TimberTable>("SELECT * FROM [TimberTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }

        public TimberTable GetTimberByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<TimberTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }

        public int DeleteTimberByContractItemNo(string udi_cont, int item_no)
        {
            SQLiteCommand sqlCommand = database.CreateCommand("delete from GlassTable where udi_cont=? and item_number=?",
                                                              new string[2] { udi_cont, item_no.ToString() });
            sqlCommand.ExecuteNonQuery();
            return database.Delete(database.Table<TimberTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }
        public void CopyTimberByContractItemNo(string udi_cont, int item_no)
        {
            TimberTable record = database.Table<TimberTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            record.bSectionDrawn = false;
            record.bMouldingDrawn = false;
            record.bSashDrawn = false;
            
            database.Insert(record);

            copy_glass_if_it_exists (udi_cont, item_no, record.item_number);
        }
        public void SaveTimber()
        {
            if (App.net.TimberRecord.RecID != 0)
                database.Update(App.net.TimberRecord);
            else
                database.Insert(App.net.TimberRecord);
        }        
        public void SaveTimber(TimberTable t)
        {
            if (t.RecID != 0)
                database.Update(t);
            else
                database.Insert(t);
        }
        public int SaveTimber(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.TimberRecord.isComplete = 1;
            else
                App.net.TimberRecord.isComplete = 0;

            if (App.net.TimberRecord.RecID != 0)
                return database.Update(App.net.TimberRecord);
            else
                return database.Insert(App.net.TimberRecord);
        }

        public List<GlassTable> GetGlasssByContract(string udi_cont, bool isComplete = false)
        {
            if(isComplete == true)
                return database.Query<GlassTable>("SELECT * FROM [GlassTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Query<GlassTable>("SELECT * FROM [GlassTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }

        public List<GlassTable> GetGlasssByContractNotSubItem(string udi_cont)
        {
            return database.Query<GlassTable>("SELECT * FROM GlassTable WHERE udi_cont='" + udi_cont + "' and parent_item=0");
        }

        public GlassTable GetGlassByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }

        public GlassTable GetGlassByContractAlumItemNo(string udi_cont, int item_no)
        {
            return database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }

        public GlassTable GetGlassByContractBifoldItemNo(string udi_cont, int item_no)
        {
            return database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }

        public GlassTable GetGlassByContractCompositeItemNo(string udi_cont, int item_no)
        {
            return database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }

        public GlassTable GetGlassByContractConservatoryItemNo(string udi_cont, int item_no)
        {
            return database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }

        public GlassTable GetGlassByContractGreenhouseItemNo(string udi_cont, int item_no)
        {
            return database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }

        public GlassTable GetGlassByContractTimberItemNo(string udi_cont, int item_no)
        {
            return database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }

        public GlassTable GetGlassByContractUPVCItemNo(string udi_cont, int item_no)
        {
            return database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }

        public int DeleteGlassByContractItemNo(string udi_cont, int item_no)
        {
            return database.Delete(database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }
        public void CopyGlassByContractItemNo(string udi_cont, int item_no)
        {
            GlassTable record = database.Table<GlassTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);
        }

        public void copy_glass_if_it_exists(string udi_cont, int item_no, int new_item_no)
        {
            GlassTable glass_item = App.data.GetGlassByContractItemNo(App.CurrentApp.HeaderRecord.udi_cont, item_no);
            if (glass_item != null)
            {
                glass_item.RecID = 0;
                glass_item.item_number = new_item_no;
                glass_item.no_of_photos = 0;
                glass_item.no_of_pics = 0;
                glass_item.isComplete = 0;
                database.Insert(glass_item);
            }
        }

        public void copy_panel_if_it_exists(string udi_cont, int item_no, int new_item_no)
        {
            PanelTable panel_item = App.data.GetPanelByContractItemNo(App.CurrentApp.HeaderRecord.udi_cont, item_no);
            if (panel_item != null)
            {
                panel_item.RecID = 0;
                panel_item.item_number = new_item_no;
                panel_item.no_of_photos = 0;
                panel_item.no_of_pics = 0;
                panel_item.isComplete = 0;
                database.Insert(panel_item);
            }
        }


        public void SaveGlass()
        {
            if (App.net.GlassRecord.RecID != 0)
                database.Update(App.net.GlassRecord);
            else
                database.Insert(App.net.GlassRecord);
        }        
        public void SaveGlass(GlassTable g)
        {
            if (g.RecID != 0)
                database.Update(g);
            else
                database.Insert(g);
        }
        public int SaveGlass(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.GlassRecord.isComplete = 1;
            else
                App.net.GlassRecord.isComplete = 0;

            if (App.net.GlassRecord.RecID != 0)
                return database.Update(App.net.GlassRecord);
            else
                return database.Insert(App.net.GlassRecord);
        }

        public List<BifoldTable> GetBifoldsByContract(string udi_cont, bool isComplete = false)
        {
            if (isComplete == true)
                return database.Query<BifoldTable>("SELECT * FROM [BifoldTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Query<BifoldTable>("SELECT * FROM [BifoldTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }
        public BifoldTable GetBifoldByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<BifoldTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public int DeleteBifoldByContractItemNo(string udi_cont, int item_no)
        {
            SQLiteCommand sqlCommand = database.CreateCommand("delete from GlassTable where udi_cont=? and item_number=?",
                                                              new string[2] { udi_cont, item_no.ToString() });
            sqlCommand.ExecuteNonQuery();
            return database.Delete(database.Table<BifoldTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }

        public void CopyBifoldByContractItemNo(string udi_cont, int item_no)
        {
            BifoldTable record = database.Table<BifoldTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);

            copy_glass_if_it_exists (udi_cont, item_no, record.item_number);
        }
        public void SaveBifold(BifoldTable b)
        {
            if (b.RecID != 0)
                database.Update(b);
            else
                database.Insert(b);
        }
        public void SaveBifold()
        {
            if (App.net.BifoldRecord.RecID != 0)
                database.Update(App.net.BifoldRecord);
            else
                database.Insert(App.net.BifoldRecord);
        }
        public int SaveBifold(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.BifoldRecord.isComplete = 1;
            else
                App.net.BifoldRecord.isComplete = 0;

            if (App.net.BifoldRecord.RecID != 0)
                return database.Update(App.net.BifoldRecord);
            else
                return database.Insert(App.net.BifoldRecord);
        }

        public List<ConsTable> GetConssByContract(string udi_cont, bool isComplete = false)
        {
            if (isComplete == true)
                return database.Query<ConsTable>("SELECT * FROM [ConsTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Query<ConsTable>("SELECT * FROM [ConsTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }
        public ConsTable GetConsByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<ConsTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public int DeleteConsByContractItemNo(string udi_cont, int item_no)
        {
            SQLiteCommand sqlCommand = database.CreateCommand("delete from GlassTable where udi_cont=? and item_number=?",
                                                              new string[2] { udi_cont, item_no.ToString() });
            sqlCommand.ExecuteNonQuery();
            return database.Delete(database.Table<ConsTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }
        public void CopyConsByContractItemNo(string udi_cont, int item_no)
        {
            ConsTable record = database.Table<ConsTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);

            copy_glass_if_it_exists (udi_cont, item_no, record.item_number);
        }
        public void SaveCons()
        {
            if (App.net.ConsRecord.RecID != 0)
                database.Update(App.net.ConsRecord);
            else
                database.Insert(App.net.ConsRecord);
        }
        public void SaveCons(ConsTable c)
        {
            if (c.RecID != 0)
                database.Update(c);
            else
                database.Insert(c);
        }
        public int SaveCons(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.ConsRecord.isComplete = 1;
            else
                App.net.ConsRecord.isComplete = 0;

            if (App.net.ConsRecord.RecID != 0)
                return database.Update(App.net.ConsRecord);
            else
                return database.Insert(App.net.ConsRecord);
        }

        public List<LockingTable> GetLockByContract(string udi_cont)
        {
            return database.Query<LockingTable>("SELECT * FROM [LockingTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }
        public LockingTable GetLockByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<LockingTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public int DeleteLockByContractItemNo(string udi_cont, int item_no)
        {
            return database.Delete(database.Table<LockingTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }
        public void CopyLockByContractItemNo(string udi_cont, int item_no)
        {
            LockingTable record = database.Table<LockingTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);
        }
        public void SaveLocking()
        {
            if (App.net.LockingRecord.RecID != 0)
                database.Update(App.net.LockingRecord);
            else
                database.Insert(App.net.LockingRecord);
        }        
        public void SaveLocking(LockingTable l)
        {
            if (l.RecID != 0)
                database.Update(l);
            else
                database.Insert(l);
        }
        public int SaveLocking(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.LockingRecord.isComplete = 1;
            else
                App.net.LockingRecord.isComplete = 0;

            if (App.net.LockingRecord.RecID != 0)
                return database.Update(App.net.LockingRecord);
            else
                return database.Insert(App.net.LockingRecord);
        }

        public List<CompositeTable> GetCompByContract(string udi_cont, bool isComplete = false)
        {
            if (isComplete == true)
                return database.Query<CompositeTable>("SELECT * FROM [CompositeTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Query<CompositeTable>("SELECT * FROM [CompositeTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }
        public CompositeTable GetCompByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<CompositeTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public int DeleteCompByContractItemNo(string udi_cont, int item_no)
        {
            SQLiteCommand sqlCommand = database.CreateCommand("delete from GlassTable where udi_cont=? and item_number=?",
                                                              new string[2] { udi_cont, item_no.ToString() });
            sqlCommand.ExecuteNonQuery();
            return database.Delete(database.Table<CompositeTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }
        public void CopyCompByContractItemNo(string udi_cont, int item_no)
        {
            CompositeTable record = database.Table<CompositeTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);

            copy_glass_if_it_exists (udi_cont, item_no, record.item_number);
        }
        public void SaveComp()
        {
            if (App.net.CompRecord.RecID != 0)
                database.Update(App.net.CompRecord);
            else
                database.Insert(App.net.CompRecord);
        }
        public void SaveComp(CompositeTable c)
        {
            if (c.RecID != 0)
                database.Update(c);
            else
                database.Insert(c);
        }
        public int SaveComp(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.CompRecord.isComplete = 1;
            else
                App.net.CompRecord.isComplete = 0;

            if (App.net.CompRecord.RecID != 0)
                return database.Update(App.net.CompRecord);
            else
                return database.Insert(App.net.CompRecord);
        }

        public List<GreenTable> GetGreensByContract(string udi_cont, bool isComplete = false)
        {
            if (isComplete == true)
                return database.Query<GreenTable>("SELECT * FROM [GreenTable] WHERE [udi_cont] = '" + udi_cont + "' AND [isComplete] = 1");
            else
                return database.Query<GreenTable>("SELECT * FROM [GreenTable] WHERE [udi_cont] = '" + udi_cont + "'");
        }
        public GreenTable GetGreenByContractItemNo(string udi_cont, int item_no)
        {
            return database.Table<GreenTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
        }
        public int DeleteGreenByContractItemNo(string udi_cont, int item_no)
        {
            SQLiteCommand sqlCommand = database.CreateCommand("delete from GlassTable where udi_cont=? and item_number=?",
                                                              new string[2] { udi_cont, item_no.ToString() });
            sqlCommand.ExecuteNonQuery();
            return database.Delete(database.Table<GreenTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault());
        }
        public void CopyGreenByContractItemNo(string udi_cont, int item_no)
        {
            GreenTable record = database.Table<GreenTable>().Where(i => i.udi_cont == udi_cont & i.item_number == item_no).FirstOrDefault();
            record.RecID = 0;
            record.item_number = ++App.net.HeaderRecord.current_item_number;
            record.no_of_photos = 0;
            record.no_of_pics = 0;
            record.isComplete = 0;
            database.Insert(record);

            copy_glass_if_it_exists (udi_cont, item_no, record.item_number);
        }
        public void SaveGreen()
        {
            if (App.net.GreenRecord.RecID != 0)
                database.Update(App.net.GreenRecord);
            else
                database.Insert(App.net.GreenRecord);
        }        
        public void SaveGreen(GreenTable g)
        {
            if (g.RecID != 0)
                database.Update(g);
            else
                database.Insert(g);
        }
        public int SaveGreen(bool IsComplete)
        {
            if (IsComplete == true)
                App.net.GreenRecord.isComplete = 1;
            else
                App.net.GreenRecord.isComplete = 0;

            if (App.net.GreenRecord.RecID != 0)
                return database.Update(App.net.GreenRecord);
            else
                return database.Insert(App.net.GreenRecord);
        }

        public void LoadSettings()
        {
            App.net.App_Settings = database.Table<app_settings>().FirstOrDefault();
            if (App.net.App_Settings == null || App.net.App_Settings.set_url == null) // Initialize strings
            {
                App.net.App_Settings = new app_settings();

                App.net.App_Settings.set_url = "http://192.168.137.15:7293";
                App.net.App_Settings.set_name = "";
                App.net.App_Settings.set_password = "";
                App.net.App_Settings.set_ownercode = "";
                App.net.App_Settings.set_branchcode = "";
                App.net.App_Settings.set_usertype = "SUR";
                App.net.App_Settings.miles_am = "";
                App.net.App_Settings.miles_pm = "";
                App.net.App_Settings.pcode_am = "";
                App.net.App_Settings.pcode_pm = "";
                App.net.App_Settings.op_pcode1 = "";
                App.net.App_Settings.op_pcode2 = "";
                App.net.App_Settings.op_pcode3 = "";
                App.net.App_Settings.op_time1 = "";
                App.net.App_Settings.op_time2 = "";
                App.net.App_Settings.op_time3 = "";
                App.net.App_Settings.miles_date = "";
                App.net.App_Settings.last_connected_date = "";
                App.net.App_Settings.connect_password = "";
                App.net.App_Settings.last_reg = "";
                App.net.App_Settings.last_pcode_to = "";
                App.net.App_Settings.last_pcode_from = "";
                App.net.App_Settings.hnsfilename = "";
                App.net.App_Settings.Contractnumber = "";
                App.net.App_Settings.ContractName = "";
                App.net.App_Settings.ContractAdd1 = "";
                App.net.App_Settings.ContractAdd2 = "";
                App.net.App_Settings.ContractAdd3 = "";
                App.net.App_Settings.ContractAdd4 = "";
                App.net.App_Settings.ContractPCode = "";
                App.net.App_Settings.ContractHPhone = "";
                App.net.App_Settings.ContractWPhone = "";
                App.net.App_Settings.ContractMPhone = "";
                App.net.App_Settings.ContractAddPhone1 = "";
                App.net.App_Settings.ContractAddPhone2 = "";
                App.net.App_Settings.ContractComments = "";
                App.net.App_Settings.voice_pitch = 100;
                App.net.App_Settings.voice_speed = 100;
                App.net.App_Settings.db_version_number = 0;

                database.Insert(App.net.App_Settings);
            }
            if(App.net.App_Settings.set_url == "")
                App.net.App_Settings.set_url = "http://192.168.137.15:7293";
            if (App.net.App_Settings.set_ownercode == "")
                App.net.App_Settings.set_ownercode = "H1";
        }

        public void SaveSettings()
        {
            database.Update(App.net.App_Settings);
        }

        public string table_debug_str(string table_name)
        {
            // For structure stuff
            // select * from sqlite_master

            SQLitePCL.sqlite3_stmt qry = SQLite3.Prepare2 (database.Handle, "select sql from sqlite_master where type='index' and tbl_name='" + table_name + "'");
            string s = "";
            string indexes = "";

            while (SQLite3.Step(qry) == SQLite3.Result.Row)
            {
                indexes += "\n";
                indexes += SQLite3.ColumnString(qry, 0);
            }

            qry = SQLite3.Prepare2(database.Handle, "Select * from " + table_name);
            int col_count = SQLite3.ColumnCount(qry);
            for (int i = 0; i < col_count; i++)
                s += SQLite3.ColumnName(qry, i) + ",";

            s += "\n-------------";
            s += indexes;

            while (SQLite3.Step(qry) == SQLite3.Result.Row)
            {
                s += "\n-------------";

                for (int i = 0; i < col_count; i++)
                {
                    s += "\n" + SQLite3.ColumnName(qry, i) + "=";

                    switch (SQLite3.ColumnType(qry, i))
                    {
                        case SQLite3.ColType.Float:
                            s += SQLite3.ColumnDouble(qry, i).ToString();
                            break;
                        case SQLite3.ColType.Integer:
                            s += SQLite3.ColumnInt64(qry, i).ToString();
                            break;
                        case SQLite3.ColType.Text:
                            s += SQLite3.ColumnString(qry, i);
                            break;
                        case SQLite3.ColType.Null:
                            s += "Null";
                            break;
                        default:
                            s += "UNKNOWN";
                            break;
                    }
                }
            }

            return s;
        }
    }
}

