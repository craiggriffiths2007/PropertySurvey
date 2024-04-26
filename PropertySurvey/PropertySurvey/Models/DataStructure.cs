
using SQLite;
using System;

namespace PropertySurvey
{
    public class OKRecordDTO
    {
        public string comments { get; set; }
        public int DBId { get; set; }
    }

    public class ImageDTO
    {
        public string Filename { get; set; }
        public string Data { get; set; }

    }

    public class SurvImage
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public byte[] image_data { get; set; }
        public string contract { get; set; }
        public string type { get; set; }
    }
    public class PropertySurveyItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }
    public class app_settings
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string set_url { get; set; }
        public string set_name { get; set; }
        public string set_password { get; set; }
        public string set_ownercode { get; set; }
        public string set_branchcode { get; set; }
        public string set_usertype { get; set; }
        public int crosshair { get; set; }
        public int delay { get; set; }
        public int above_position { get; set; }
        public string miles_am { get; set; }
        public string miles_pm { get; set; }
        public string pcode_am { get; set; }
        public string pcode_pm { get; set; }
        public int no_op { get; set; }
        public string op_pcode1 { get; set; }
        public string op_pcode2 { get; set; }
        public string op_pcode3 { get; set; }
        public string op_time1 { get; set; }
        public string op_time2 { get; set; }
        public string op_time3 { get; set; }
        public string miles_date { get; set; }
        public string last_connected_date { get; set; }
        public string connect_password { get; set; }
        public string last_reg { get; set; }
        public string last_pcode_to { get; set; }
        public string last_pcode_from { get; set; }
        public int new_mail { get; set; }
        public int AccidentsToSend { get; set; }
        public int current_accident_id { get; set; }
        public bool survey_and_fit { get; set; }
        public int hnsver { get; set; }
        public int hnsverlast { get; set; }
        public string hnsfilename { get; set; }
        public int hnsread { get; set; }
        public int hnssign1 { get; set; }
        public int hnssign2 { get; set; }
        public int hnssignsent1 { get; set; }
        public int hnssignsent2 { get; set; }
        public int hnspoptup { get; set; }
        public int override_version { get; set; }
        public int allow_phone8_camera { get; set; }
        public int current_van_check { get; set; }
        public int able_to_send_comments { get; set; }
        public int able_to_ladder_check { get; set; }
        public string Contractnumber { get; set; }
        public string ContractName { get; set; }
        public string ContractAdd1 { get; set; }
        public string ContractAdd2 { get; set; }
        public string ContractAdd3 { get; set; }
        public string ContractAdd4 { get; set; }
        public string ContractPCode { get; set; }
        public string ContractHPhone { get; set; }
        public string ContractWPhone { get; set; }
        public string ContractMPhone { get; set; }
        public string ContractAddPhone1 { get; set; }
        public string ContractAddPhone2 { get; set; }
        public string ContractComments { get; set; }
        public int bOneTimeSetCamera { get; set; }
        public int bOneTimeSetSSL { get; set; }
        public int voice_pitch { get; set; }
        public int voice_speed { get; set; }
        public bool send_data_file { get; set; }
        public bool vertical_text { get; set; }
        public int db_version_number { get; set; }

        public string iemi { get; set; }
    }

    public class WhitnessesData
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public int AccidentRecID { get; set; }
        public string p_name { get; set; }
        public string p_add1 { get; set; }
        public string p_add2 { get; set; }
        public string p_add3 { get; set; }
        public string p_pcode { get; set; }
        public string p_wittel { get; set; }
        public int new_ispare1 { get; set; }
        public int new_ispare2 { get; set; }
        public int new_ispare3 { get; set; }
        public int new_ispare4 { get; set; }
        public int new_ispare5 { get; set; }
        public int new_ispare6 { get; set; }
        public int new_ispare7 { get; set; }
        public int new_ispare8 { get; set; }
        public int new_ispare9 { get; set; }
        public int new_ispare10 { get; set; }
        public string new_sspare1 { get; set; }
        public string new_sspare2 { get; set; }
        public string new_sspare3 { get; set; }
        public string new_sspare4 { get; set; }
        public string new_sspare5 { get; set; }
        public string new_sspare6 { get; set; }
        public string new_sspare7 { get; set; }
        public string new_sspare8 { get; set; }
        public string new_sspare9 { get; set; }
        public string new_sspare10 { get; set; }
        public bool complete { get; set; }
    }



    public class Accident_sheet
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        //Header
        public string date_time { get; set; }
        public bool bSent { get; set; }
        public bool bComplete { get; set; }
        public string brief { get; set; }
        public bool c_instructions { get; set; }
        public bool c_details { get; set; }
        public bool c_you { get; set; }
        public bool c_them { get; set; }
        public bool c_police { get; set; }
        public bool c_witness { get; set; }
        public bool c_photographs { get; set; }
        public bool c_drawings { get; set; }
        // DETAILS ///////////////////
        public bool d_bPolice { get; set; }
        public string d_officers_name { get; set; }
        public string d_officers_number { get; set; }
        public string d_station { get; set; }
        public string d_place { get; set; }
        public string d_speed { get; set; }
        public string d_weather { get; set; }
        public string d_description { get; set; }
        public string d_sign_date { get; set; }
        // YOU ////////////////////
        public string y_make { get; set; }
        public string y_model { get; set; }
        public string y_reg { get; set; }
        public string y_used_for { get; set; }
        public string y_driver_full_name { get; set; }
        public string y_driver_dob { get; set; }
        public string y_address1 { get; set; }
        public string y_address2 { get; set; }
        public string y_address3 { get; set; }
        public string y_pcode { get; set; }
        public string y_occupation { get; set; }
        public string y_years_employed { get; set; }
        public string y_months_employed { get; set; }
        public string y_any_other_accidents { get; set; }
        public string y_infirmity { get; set; }
        public string y_prosecution { get; set; }
        public string y_vehicle_damage { get; set; }
        public int y_driveable { get; set; }
        public string y_damage_to_property { get; set; }
        public string y_injuries_sustained { get; set; }
        public bool y_signed { get; set; }
        // THEM //////////////////
        public string t_name { get; set; }
        public string t_add1 { get; set; }
        public string t_add2 { get; set; }
        public string t_add3 { get; set; }
        public string t_pcode { get; set; }
        public string t_make { get; set; }
        public string t_reg { get; set; }
        public string t_model { get; set; }
        public string t_insurer { get; set; }
        public string t_policy_no { get; set; }
        public string t_telnum { get; set; }
        public int no_of_other_people { get; set; } // Number of other people in the vehicle
        // PERSON ///////////////
        public string p_name { get; set; }
        public string p_add1 { get; set; }
        public string p_add2 { get; set; }
        public string p_add3 { get; set; }
        public string p_pcode { get; set; }
        public string p_wittel { get; set; }
        // VEHICLE ////////////////////
        public string v_reg { get; set; }
        public string v_model { get; set; }
        public int i_spare1 { get; set; }
        public int i_spare2 { get; set; }
        public int i_spare3 { get; set; }
        public string s_spare1 { get; set; }
        public string s_spare2 { get; set; }
        public string s_spare3 { get; set; }
        public int new_ispare1 { get; set; } // Cill on subframe
        public int new_ispare2 { get; set; } // Cill type
        public int new_ispare3 { get; set; }
        public int new_ispare4 { get; set; }
        public int new_ispare5 { get; set; }
        public int new_ispare6 { get; set; }
        public int new_ispare7 { get; set; }
        public int new_ispare8 { get; set; }
        public int new_ispare9 { get; set; }
        public int new_ispare10 { get; set; }
        public string new_sspare1 { get; set; }
        public string new_sspare2 { get; set; }
        public string new_sspare3 { get; set; }
        public string new_sspare4 { get; set; }
        public string new_sspare5 { get; set; }
        public string new_sspare6 { get; set; }
        public string new_sspare7 { get; set; }
        public string new_sspare8 { get; set; }
        public string new_sspare9 { get; set; }
        public string new_sspare10 { get; set; }

        public string acc_date { get; set; }

        public string acc_time { get; set; }

    }



    public class Header_Index
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public int header_rec_id { get; set; }
        public string udi_cont { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int iRecordType { get; set; }
        public string uc_name { get; set; }
        public bool bDone { get; set; }
        public bool bSent { get; set; }
        public bool bSpecialIns { get; set; }
        public string uc_postcode { get; set; }
        public string udi_start { get; set; }
        public string udi_fin { get; set; }
        [Indexed]
        public string udi_date { get; set; }		// Diary date of job
        public string typeA { get; set; }		// typeA
        public string typeB { get; set; }		// typeB
        public bool b_mrk { get; set; }             // Is this a complaint type job ?
        public int new_ispare1 { get; set; } // Cill on subframe
        public int new_ispare2 { get; set; } // Cill type
        public int new_ispare3 { get; set; }
        public int new_ispare4 { get; set; }
        public int new_ispare5 { get; set; }
        public int new_ispare6 { get; set; }
        public int new_ispare7 { get; set; }
        public int new_ispare8 { get; set; }
        public int new_ispare9 { get; set; }
        public int new_ispare10 { get; set; }
        public string new_sspare1 { get; set; }
        public string new_sspare2 { get; set; }
        public string new_sspare3 { get; set; }
        public string new_sspare4 { get; set; }
        public string new_sspare5 { get; set; }
        public string new_sspare6 { get; set; }
        public string new_sspare7 { get; set; }
        public string new_sspare8 { get; set; }
        public string new_sspare9 { get; set; }
        public string new_sspare10 { get; set; }
    }

    public class Milage_sheet
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string sheet_date { get; set; }
        public string start_postcode { get; set; }
        public string finish_postcode { get; set; }
        public string start_mileage { get; set; }
        public string end_mileage { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public int no_of_other_places { get; set; }
        public string time1 { get; set; }
        public string pcode1 { get; set; }
        public string time2 { get; set; }
        public string pcode2 { get; set; }
        public string time3 { get; set; }
        public string pcode3 { get; set; }
        public string registration { get; set; }
        public bool bSigned { get; set; }
        public string signature_filename { get; set; }
        public bool bSent { get; set; }
        public string comments { get; set; }
        public int OtehrPlaceNo { get; set; }

        public string op_time1 { get; set; }
        public string op_postcode1 { get; set; }
        public string op_time2 { get; set; }
        public string op_postcode2 { get; set; }
        public string op_time3 { get; set; }
        public string op_postcode3 { get; set; }
        public string op_time4 { get; set; }
        public string op_postcode4 { get; set; }
        public string op_time5 { get; set; }
        public string op_postcode5 { get; set; }
        public string op_time6 { get; set; }
        public string op_postcode6 { get; set; }
        public string op_time7 { get; set; }
        public string op_postcode7 { get; set; }
        public string op_time8 { get; set; }
        public string op_postcode8 { get; set; }
        public string op_time9 { get; set; }
        public string op_postcode9 { get; set; }
        public string op_time10 { get; set; }
        public string op_postcode10 { get; set; }
        public string op_time11 { get; set; }
        public string op_postcode11 { get; set; }
        public string op_time12 { get; set; }
        public string op_postcode12 { get; set; }
        public string op_time13 { get; set; }
        public string op_postcode13 { get; set; }
        public string op_time14 { get; set; }
        public string op_postcode14 { get; set; }
        public string op_time15 { get; set; }
        public string op_postcode15 { get; set; }

        public int toll_charges { get; set; }
        public string toll_charge_for { get; set; }
        public string toll_charge_ammount { get; set; }
        
        public string new_sspare1 { get; set; }
        public string new_sspare2 { get; set; }
    }
    

    public class SCheckHnS // Spot Check Health 
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string udi_cont { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string branch { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string name { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string job { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string name1 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int safety_boots_worn1 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int safety_gloves_worn1 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int safety_googles_worn1 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int safety_helmet_worn1 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int wristguards_worn1 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int uniform_worn_complete1 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int id_card_available1 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string name2 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int safety_boots_worn2 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int safety_gloves_worn2 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int safety_googles_worn2 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int safety_helmet_worn2 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int wristguards_worn2 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int uniform_worn_complete2 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int id_card_available2 { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int chemicals_stored_correctly { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int are_sheets_available { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int area_above_been_checked { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int obstructions_checked { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int lintel_ok { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int ladders_secure { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int safe_work_at_height { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int condition_of_ladders { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int tools_set_out_safely { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int fire_extinguisher_on_van { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int first_aid_kit_on_van { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int electrical_equipment_tested { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string safety_boots_worn1_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string safety_gloves_worn1_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string safety_googles_worn1_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string safety_helmet_worn1_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string wristguards_worn1_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string uniform_worn_complete1_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string id_card_available1_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string safety_boots_worn2_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string safety_gloves_worn2_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string safety_googles_worn2_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string safety_helmet_worn2_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string wristguards_worn2_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string uniform_worn_complete2_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string id_card_available2_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string chemicals_stored_correctly_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string are_sheets_available_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string area_above_been_checked_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string obstructions_checked_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string lintel_ok_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string ladders_secure_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string safe_work_at_height_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string condition_of_ladders_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string tools_set_out_safely_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string fire_extinguisher_on_van_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string first_aid_kit_on_van_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string electrical_equipment_tested_s { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public string comments { get; set; }		// Key filed contract number			CONTRACT NUMBER
        public int i_spare1 { get; set; }
        public int i_spare2 { get; set; }
        public int i_spare3 { get; set; }
        public string s_spare1 { get; set; }
        public string s_spare2 { get; set; }
        public string s_spare3 { get; set; }
        public int new_ispare1 { get; set; } // Cill on subframe
        public int new_ispare2 { get; set; } // Cill type
        public int new_ispare3 { get; set; }
        public int new_ispare4 { get; set; }
        public int new_ispare5 { get; set; }
        public int new_ispare6 { get; set; }
        public int new_ispare7 { get; set; }
        public int new_ispare8 { get; set; }
        public int new_ispare9 { get; set; }
        public int new_ispare10 { get; set; }
        public string new_sspare1 { get; set; }
        public string new_sspare2 { get; set; }
        public string new_sspare3 { get; set; }
        public string new_sspare4 { get; set; }
        public string new_sspare5 { get; set; }
        public string new_sspare6 { get; set; }
        public string new_sspare7 { get; set; }
        public string new_sspare8 { get; set; }
        public string new_sspare9 { get; set; }
        public string new_sspare10 { get; set; }
    }



    public class MotorSheet
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string udi_cont { get; set; }		// Key filed contract number	
        public int item_no { get; set; }
        public string door_type { get; set; }
        public string model_type { get; set; }
        public string unique_serial { get; set; }
        public string door_size { get; set; }
        public string door_manufacturer { get; set; }
        public string powerered_operator_type { get; set; }
        public string operator_manufacturer { get; set; }
        public string site_address { get; set; }
        public string decleration_by { get; set; }
        public string on_behalf_of_person { get; set; }
        public string on_behalf_of_company { get; set; }
        public string decleration_received_by { get; set; }
        public string date { get; set; }
        public string print_name { get; set; }
        public string date_cust { get; set; }
        public int i_signed { get; set; }
        public int i_signed_cust { get; set; }
        public int i_spare1 { get; set; }
        public int i_spare2 { get; set; }
        public int i_spare3 { get; set; }
        public string s_spare1 { get; set; }
        public string s_spare2 { get; set; }
        public string s_spare3 { get; set; }
        public int new_ispare1 { get; set; } // Cill on subframe
        public int new_ispare2 { get; set; } // Cill type
        public int new_ispare3 { get; set; }
        public int new_ispare4 { get; set; }
        public int new_ispare5 { get; set; }
        public int new_ispare6 { get; set; }
        public int new_ispare7 { get; set; }
        public int new_ispare8 { get; set; }
        public int new_ispare9 { get; set; }
        public int new_ispare10 { get; set; }
        public string new_sspare1 { get; set; }
        public string new_sspare2 { get; set; }
        public string new_sspare3 { get; set; }
        public string new_sspare4 { get; set; }
        public string new_sspare5 { get; set; }
        public string new_sspare6 { get; set; }
        public string new_sspare7 { get; set; }
        public string new_sspare8 { get; set; }
        public string new_sspare9 { get; set; }
        public string new_sspare10 { get; set; }
    }



    public class Message_Text
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string ID { get; set; }
        public string message_date { get; set; }
        public string from { get; set; }
        public string message_text { get; set; }
        public bool bRead { get; set; }
    }

        public class Header
    {

        //[Indexed]
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public bool bDone { get; set; }
        public bool bSent { get; set; }
        public int iRecordType { get; set; }
        public string typeA { get; set; }
        public string typeB { get; set; }
        public string fit_diary { get; set; }
        public string fitters_instructions { get; set; }
        public string fit_start { get; set; }
        public string fit_fin { get; set; }
        public string fitter_work { get; set; }
        public string parts_used { get; set; }
        public string claim_ref { get; set; }
        public string fitter_comments { get; set; }
        [Indexed]
        public string udi_cont { get; set; }        // Key filed contract number			CONTRACT NUMBER
        public string sn_name { get; set; }     // Insurance company name				COMPANY NAME
        public string uc_laname { get; set; }       // Loss adjusters name					DELE AUTH
        public string uc_name { get; set; }     // Client name							CLIENT NAME
        public string uc_add1 { get; set; }
        public string uc_add2 { get; set; }
        public string uc_add3 { get; set; }
        public string uc_add4 { get; set; }
        public string uc_postcode { get; set; } // Client Postcode						POSTCODE
        public string uc_h_phone { get; set; }  // Client Phone number					PHONE NUMBER
        public bool uc_goahead { get; set; }        // Go ahead ?							GO AHEAD IF REPAIR
        public string uc_inceden { get; set; }      // Incident date						INCIDENT DATE
        public string spare2 { get; set; }
        public double uc_excess { get; set; }       // Excess ammount						EXCESS ( main page )
        public int bExcessCollected { get; set; }  // TRUE or FALSE
        public int udi_tlight { get; set; }     // Traffic light						
        public int si_numitem { get; set; }         // Number of items
        //////////////////////
        // Instructions
        //////////////////////
        public string udi_inst { get; set; }        // Instructions
        ///////////////////////////
        // Special requirements
        ///////////////////////////
        public int alarm_cont { get; set; }     // Alarm Contacts							ALARM CONTACTS
        public int ladder_req { get; set; }     // Ladders required							LADDERS REQUIRED
        public int height_res { get; set; }     // Height restriction						ANY HEIGHT RESTICTION
        public int sand_cemen { get; set; }     // Sand Cement required						SAND AND CEMENT REQUIRED
        public int plaster { get; set; }        // Plaster required						    PLASTER REQUIRED
        public int doorbell { get; set; }       // Doorbel								    DOOR BELL
        public int genreq { get; set; }         // Generator required						GENERATOR REQUIRED
        public int architreq { get; set; }      // Architraves required					    ARCHITRAVES REQUIRED
        public int acroreq { get; set; }        // Acro Prop required					    ACRO PROP REQUIRED
        public int acrosboy { get; set; }       // Acro Prop required					    ACRO PROP REQUIRED
        public string acc_text { get; set; }        // Access requirements comments			COMMENTS
        ////////////////////////////
        // Health and safety 
        /////////////////////////////
        public int obs_wires { get; set; }          // Obstructive wires						ANY OBSTRUCTIVE WIRES
        public string obs_wires_text { get; set; }
        public int loose_brick { get; set; }        // Is their loose brick work above item ?	LOOSE BRICKWORK ABOVE ITEM
        public string loose_brick_text { get; set; }
        public int easy_park { get; set; }          // Easy Parking 1=Yes						EASY PARKING
        public int access_rear { get; set; }        // Access to rear of property				ACCESS TO REAR OF PROPERTY
        //////////////////
        // REPORT
        //////////////////
        public string rep_text { get; set; }        // Report text								REPORT
        ////////////////////
        // PAYMENT SCREEN 
        ////////////////////
        public string mop { get; set; }             // 0=none selected 1=cash 2=cheque 3=mastercard 4=switch 5=visa
        public string card_cheq { get; set; }       // card/cheq number
        public string expiry { get; set; }          // Expiry date
        public int issue_no { get; set; }           // Issue number
        public string reason_excess_not_collected { get; set; }
        public string paych { get; set; }           // Method of payment	
        //////////////////////
        // SUMMARY
        //////////////////////
        public string summ_text { get; set; }   // Summary
        public string code_text { get; set; }   // Code text
        public int imchup { get; set; }         // NOT USED     Items matching up ( starts at 0 ( for not selected ) 1 for 0 
        public string job_grade { get; set; }   // Job grade
        public string njs { get; set; }         // Job Size
        public int photo { get; set; }          // Photos taken
        public int booked { get; set; }         // Booked on day 1
        public string nsn { get; set; }         // Surveyors name
        ///////////////////
        // first job menu
        /////////////////
        public string udi_start { get; set; }       // Start time of ?	
        public string udi_fin { get; set; }         // Finish time
        public bool si_done { get; set; }           // super Fitter Y or N
        public string udi_date { get; set; }        // Diary date of job
        public string si_bday1 { get; set; }
        public string si_mpay { get; set; }         // account code
        public string si_cnum { get; set; }
        public string si_inum { get; set; }
        public string udi_jobtext { get; set; } // Job instructions
        public string udi_staff { get; set; }       // NOT USED     Staff member name doing job
        public string type { get; set; }            // "Fitting" / "Complaint" / "Unfinished" / "Remedial" / " " =Surveyor
        public string sub_type { get; set; }
        public string old_date { get; set; }
        public string cover_instructions { get; set; }
        public string old_start { get; set; }
        public string old_finish { get; set; }
        public string add_comm { get; set; }        // Aditional comments 
        public string udi_estrem { get; set; }      // Remedial number
        public string r_fault { get; set; }             // Remedial fault
        public int r_excess { get; set; }                   // Remedial excess
        public string rexcedit { get; set; }                // Reason in no excess (r_excess=FALSE)
        public int r_comp { get; set; }                 // Is Completed
        public string rno_hours { get; set; }               // Number of hours to complete
        public string r_work_txt { get; set; }          // Work carried out
        public bool readditimage { get; set; }              // Is their a remedial additional image
        public string readdtxt { get; set; }                // Remedial additional text
        public bool r_sigimage { get; set; }                // Is their a remedial signature image
        public string f_add_txt { get; set; }           // Fitters additional requirements text
        public string fmclrf { get; set; }              // Fitters mandate claim reference
        public string fmdate { get; set; }              // Fitters mandate date
        public string funfincode { get; set; }          // Code 
        public string funfinoth { get; set; }           // If Code is not listed or funfincode "other"
        public string freuntxt { get; set; }            // Reason unfinished
        public string fpartreq { get; set; }            // Parts required
        public bool fjobfin { get; set; }               // Job finnished
        public string fname1 { get; set; }          // First fitters name's
        public string fname2 { get; set; }          // Second fitters name
        public bool fexcess { get; set; }               // Excess paid
        public string fexcessoth { get; set; }      // Why excess paid if(!fexcess) not paid
        public bool fmand { get; set; }                 // Mandate signed
        public string fmandoth { get; set; }            // Why madate not signed if(!fmand)
        public string ftimearr { get; set; }            // Time arrived
        public string ftimeleft { get; set; }       // Time left
        public bool faddpaid { get; set; }              // Additional paid ? ( set if faddmuch > 0.0 )
        public double faddmuch { get; set; }            // How much additional paid
        public string commtxt { get; set; }         // Job comments
        public string wkcartxt { get; set; }            // Work carried out
        public string parttxt { get; set; }         // Parts used
        public bool faddimage { get; set; }             // Additional image
        public bool fmanimage { get; set; }             // Mandate image
        public bool fsigimage { get; set; }             // Their is an image
        public int bWorkInside { get; set; }            // Can the work be carried out from inside ?
        public string inst_height { get; set; }     // At what height is the insallation taking place ?
        public int bBothHands { get; set; }          // NOT USED        Used as value for tower scaffold checklist 0 - not tower scaffold 1 - is tower scaffold 3 - has been signed
        public string ground_surface { get; set; }  // what is the ground surface made of ?
        public string type_of_equipment { get; set; }
        public string risks_and_dangers { get; set; }   // are their any other risks and/or dangers ?
        public string uc_desc { get; set; }             // Brief description			DAMAGE is first 250 chars if this
        public int work_at_height { get; set; }         //  ?  1-Yes  2-No
        public int no_ladders { get; set; }
        //////////////////////////////////
        // More fitter things
        //////////////////////////////////
        public string funfinished_code { get; set; }
        public string freason_unfinished { get; set; }
        public string fparts_required { get; set; }
        public string ffitter_name1 { get; set; }
        public string ffitter_name2 { get; set; }
        public int fbexcess_paid { get; set; }          // has the person paid the excess to the fitter ( 0- not complete  1- Yes    2- No  )
        public string freason_excess_not_paid { get; set; }
        public int fbmandate_signed { get; set; }       // ( 0- not complete  1- Yes    2- No  )
        public string freason_mandate_not_signed { get; set; }
        public string ftime_arrived { get; set; }       // hours and minutes ( use a time/date picher )
        public string ftime_left { get; set; }          // as above
        public int fbadditional_paid { get; set; }              // ( 0- not complete  1- Yes    2- No  )
        public string fhow_mutch_additional_paid { get; set; }
        public int bfitter_complete { get; set; }       // is the fitting complete ( 0 - not selected 1Yes 2No )
        public int fitter_info_done { get; set; }       // When all the fitter info is complete
        public string fbunfinother { get; set; }        // barrier size
        public bool bcompletion_signed { get; set; }    // Is the completion signed in fitter screens
        public bool bad_image_complete { get; set; }    // is additional image comlpete
        public string remedial_number { get; set; }
        public bool r_bsigned { get; set; }             // has remedial been signed ?
        public string r_bcomp { get; set; }
        public string r_sign_date { get; set; }     // Day month year remedial signed
        public string stimea { get; set; }              // Surveyor time arrived
        public string f1_or_s2 { get; set; }                // 1 for a fitter	2 for a surveyor
        public string f_sign_date { get; set; }     // fitter signature date
        public long distance { get; set; }              // GPS distance			
        public long duration { get; set; }              // Duration of the journey in seconds
        public int no_of_photos { get; set; }           // used for fitting and remedial
        public string bClosest { get; set; }
        public string Group { get; set; }                   // Group of closestness bull shit
        public string bProcessed { get; set; }          // Have we found its distance ?
        public int ind { get; set; }
        public string inevitable_damage { get; set; }
        public bool fbstockusagecomplete { get; set; }  // is fitter stock usage complete
        public string uc_h_phone2 { get; set; } // Client Phone number					PHONE NUMBER
        public string uc_h_phone3 { get; set; } // Client Phone number					PHONE NUMBER
        public bool bSecuring { get; set; }
        public int ins_board { get; set; }
        public int ins_lock { get; set; }
        public int ins_temp { get; set; }
        public int ins_perm { get; set; }
        public int int_num_of_locks { get; set; }
        public string int_type_of_lock { get; set; }
        public int parking_at_rear { get; set; } // Parking at rear
        public int work_on_public_footpath { get; set; } // public footpath
        public string add_long { get; set; }
        public bool b_mrk { get; set; }         // Is this a complaint type job ?
        public bool bSurvey { get; set; }
        public int items_above_roof { get; set; }
        public bool added_to_otherrisks { get; set; }
        public bool bMSFJob { get; set; }
        public int securing_surveyor_required { get; set; }
        public string policy_number { get; set; } // Is this used ??
        public bool photo_front_of_house { get; set; }
        public int asbestos_visible { get; set; }
        public string asvizex { get; set; }
        public string refmessage { get; set; }
        // Spot Checks
        public string uspot_fitter { get; set; }
        public string uspot_trainee { get; set; }
        public string uspot_date { get; set; }
        //char uspot_contract
        public string uspot_customer { get; set; }
        public string uspot_postcode { get; set; }
        public string uspot_insuranceco { get; set; }
        public string uspot_branch { get; set; }
        public bool uspot_repair { get; set; }
        public bool uspot_repair_arrived { get; set; }
        public bool uspot_repair_setup { get; set; }
        public bool uspot_repair_ongoing { get; set; }
        public bool uspot_repair_completed { get; set; }
        public bool uspot_replace { get; set; }
        public bool uspot_replace_arrived { get; set; }
        public bool uspot_replace_setup { get; set; }
        public bool uspot_replace_unitmoved { get; set; }
        public bool uspot_replace_completed { get; set; }
        public bool uspot_rev_door { get; set; }
        public bool uspot_rev_window { get; set; }
        public bool uspot_rev_garagedoor { get; set; }
        public bool uspot_rev_glass { get; set; }
        public bool uspot_rev_locks { get; set; }
        public bool uspot_rev_other { get; set; }
        public bool uspot_revb_upvc { get; set; }
        public bool uspot_revb_ali { get; set; }
        public bool uspot_revb_timber { get; set; }
        public bool uspot_revb_other { get; set; }
        public int uspot_appearence { get; set; }
        public string doc_l_compliant_reason { get; set; } // Changed to this to reuse old memory
        public int lintel_present { get; set; }
        public string lintel_present_text { get; set; }
        public int uspot_customersatisfaction { get; set; }
        public string uspot_customersatisfaction_improvementsOld { get; set; }
        public string uspot_otherobservationsOld { get; set; }
        public bool uspot_signed { get; set; }
        public string uspot_signeddate { get; set; }
        public bool bSpotCheck { get; set; }
        public bool uspot_replace_fit { get; set; }
        public int uspot_p1 { get; set; }
        public int uspot_p2 { get; set; }
        public int uspot_p3 { get; set; }
        public int uspot_p4 { get; set; }
        public string uspot_appearence_improvements { get; set; }
        public string uspot_qualityofworks_improvements { get; set; }
        public string uspot_customersatisfaction_improvements { get; set; }
        public string uspot_otherobservations { get; set; }
        public bool idampassword_entered { get; set; }
        public int fit_no_of_videos { get; set; } // no of bifold
        public int doc_l_compliant { get; set; }
        public int shop_front_work { get; set; }
        public int fitter_videos { get; set; } // incomplete bifold
        public bool is_halifax { get; set; } // Set to 1 for halifax insurance company.
        public string messagetoinsurer { get; set; }
        public string COD_Code { get; set; }
        public string COD_String { get; set; }
        public bool bDamTicked { get; set; }
        public bool bSSTicked { get; set; }
        public int SSRequired { get; set; }
        public string old_cover_instructions { get; set; }
        public string rcodchanged { get; set; }
        public bool bcodchanged { get; set; }
        public string goaheadstr { get; set; }  // NOT USED
        public int b_subcontract { get; set; }
        public string subcontracttext { get; set; }
        public bool truecomm { get; set; }
        public bool truecommconf { get; set; }
        public string reason_not_booked_in { get; set; }
        public bool bSurveyRequiredOnSecuring { get; set; }
        public bool requiring_load_bearing_jacks { get; set; }
        public bool bSRFin { get; set; }
        public bool bMOPFin { get; set; }
        public bool bRepFin { get; set; }
        public bool bSumFin { get; set; }
        public bool bHazFin { get; set; }
        public bool bAllPictures { get; set; }
        public bool bSubFin { get; set; }
        public int total_upvc { get; set; }
        public int total_panels { get; set; }
        public int total_glass { get; set; }
        public int total_alum { get; set; }
        public int total_garage { get; set; }
        public int total_timber { get; set; }
        public int total_cons { get; set; }
        public int total_lock { get; set; }
        public int total_comp { get; set; }
        public int total_green { get; set; }
        public int total_bifold { get; set; }
        public int incomplete_upvc { get; set; }
        public int incomplete_panels { get; set; }
        public int incomplete_glass { get; set; }
        public int incomplete_alum { get; set; }
        public int incomplete_garage { get; set; }
        public int incomplete_timber { get; set; }
        public int incomplete_cons { get; set; }
        public int incomplete_lock { get; set; }
        public int incomplete_comp { get; set; }
        public int incomplete_green { get; set; }
        public int incomplete_bifold { get; set; }
        public int front_house_photos { get; set; }
        public string time_to_complete { get; set; }
        public int current_item_number { get; set; }
        public int survey_complete { get; set; }
        public string reason_not_complete { get; set; }
        public string add_phone_1 { get; set; }
        public string add_phone_2 { get; set; }
        public int no_of_fitters { get; set; }
        public string fname3 { get; set; }
        public string fname4 { get; set; }
        public string fname5 { get; set; }
        public string fname6 { get; set; }
        public string fname7 { get; set; }
        public string fname8 { get; set; }
        public int ownquote { get; set; }
        public int survey_on_fit { get; set; }
        public int i_spare1 { get; set; }
        public int i_spare2 { get; set; }
        public int i_spare3 { get; set; }
        public string s_spare1 { get; set; }
        public string s_spare2 { get; set; }
        public string s_spare3 { get; set; }
        public int new_ispare1 { get; set; } // Cill on subframe
        public int new_ispare2 { get; set; } // Cill type
        public int new_ispare3 { get; set; }
        public int new_ispare4 { get; set; }
        public int new_ispare5 { get; set; }
        public int new_ispare6 { get; set; }
        public int new_ispare7 { get; set; }
        public int new_ispare8 { get; set; }
        public int new_ispare9 { get; set; }
        public int is_messagetoinsurer { get; set; }
        public string new_sspare1 { get; set; }
        public string new_sspare2 { get; set; }
        public string new_sspare3 { get; set; }
        public string new_sspare4 { get; set; }
        public string new_sspare5 { get; set; }
        public string new_sspare6 { get; set; }
        public string new_sspare7 { get; set; }
        public string new_sspare8 { get; set; }
        public string new_sspare9 { get; set; }
        public string new_sspare10 { get; set; }

        public bool bInfoSeen { get; set; }

        public int ss_bIsSecuritySurvey { get; set; }
        public int ss_bIsComplete { get; set; }
        public string ss_nowindows { get; set; }
        public string ss_nodoors { get; set; }
        public string ss_gencondition { get; set; }
        public string ss_gencondition_other { get; set; }
        public string ss_matwindows { get; set; }
        public string ss_matwindows_other { get; set; }
        public string ss_matdoors { get; set; }
        public string ss_matdoors_other { get; set; }
        public string ss_lockwindows { get; set; }
        public string ss_lockwindows_other { get; set; }
        public string ss_lockdoors { get; set; }
        public string ss_lockdoors_other { get; set; }
        public int ss_add_window_security { get; set; }
        public string ss_location_windows_other { get; set; }
        public string ss_secwindows_other { get; set; }
        public int ss_add_door_security { get; set; }
        public string ss_location_doors_other { get; set; }
        public string ss_secdoors_other { get; set; }
        public string ss_time_required { get; set; }

        public int ss_no_of_photos { get; set; }

        // Motor sheet
        public string door_type { get; set; }
        public string model_type { get; set; }
        public string unique_serial { get; set; }
        public string door_size { get; set; }
        public string door_manufacturer { get; set; }
        public string powerered_operator_type { get; set; }
        public string operator_manufacturer { get; set; }
        public string site_address { get; set; }
        public string decleration_by { get; set; }
        public string on_behalf_of_person { get; set; }
        public string on_behalf_of_company { get; set; }
        public string decleration_received_by { get; set; }
        public string date { get; set; }
        public string print_name { get; set; }
        public string date_cust { get; set; }
        public int i_signed { get; set; }
        public int i_signed_cust { get; set; }

        public int directive_complete { get; set; }

        public string branch { get; set; }		 			
        public string name { get; set; }		 			
        public string job { get; set; }		 			
        public string name1 { get; set; }		 			
        public int safety_boots_worn1 { get; set; }		 			
        public int safety_gloves_worn1 { get; set; }		 			
        public int safety_googles_worn1 { get; set; }		 			
        public int safety_helmet_worn1 { get; set; }		 			
        public int wristguards_worn1 { get; set; }		 			
        public int uniform_worn_complete1 { get; set; }		 			
        public int id_card_available1 { get; set; }		 			
        public string name2 { get; set; }		 			
        public int safety_boots_worn2 { get; set; }		 			
        public int safety_gloves_worn2 { get; set; }		 			
        public int safety_googles_worn2 { get; set; }		 			
        public int safety_helmet_worn2 { get; set; }		 			
        public int wristguards_worn2 { get; set; }		 			
        public int uniform_worn_complete2 { get; set; }		 			
        public int id_card_available2 { get; set; }		 			
        public int chemicals_stored_correctly { get; set; }		 			
        public int are_sheets_available { get; set; }		 			
        public int area_above_been_checked { get; set; }		 			
        public int obstructions_checked { get; set; }		 			
        public int lintel_ok { get; set; }		 			
        public int ladders_secure { get; set; }		 			
        public int safe_work_at_height { get; set; }		 			
        public int condition_of_ladders { get; set; }		 			
        public int tools_set_out_safely { get; set; }		 			
        public int fire_extinguisher_on_van { get; set; }		 			
        public int first_aid_kit_on_van { get; set; }		 			
        public int electrical_equipment_tested { get; set; }		 			
        public string safety_boots_worn1_s { get; set; }		 			
        public string safety_gloves_worn1_s { get; set; }		 			
        public string safety_googles_worn1_s { get; set; }		 			
        public string safety_helmet_worn1_s { get; set; }		 			
        public string wristguards_worn1_s { get; set; }		 			
        public string uniform_worn_complete1_s { get; set; }		 			
        public string id_card_available1_s { get; set; }		 			
        public string safety_boots_worn2_s { get; set; }		 			
        public string safety_gloves_worn2_s { get; set; }		 			
        public string safety_googles_worn2_s { get; set; }		 			
        public string safety_helmet_worn2_s { get; set; }		 			
        public string wristguards_worn2_s { get; set; }		 			
        public string uniform_worn_complete2_s { get; set; }		 			
        public string id_card_available2_s { get; set; }		 			
        public string chemicals_stored_correctly_s { get; set; }		 			
        public string are_sheets_available_s { get; set; }		 			
        public string area_above_been_checked_s { get; set; }		 			
        public string obstructions_checked_s { get; set; }		 			
        public string lintel_ok_s { get; set; }		 			
        public string ladders_secure_s { get; set; }		 			
        public string safe_work_at_height_s { get; set; }		 			
        public string condition_of_ladders_s { get; set; }		 			
        public string tools_set_out_safely_s { get; set; }		 			
        public string fire_extinguisher_on_van_s { get; set; }		 			
        public string first_aid_kit_on_van_s { get; set; }		 			
        public string electrical_equipment_tested_s { get; set; }		 			
        public string comments { get; set; }		 			
        public int uspot_qualityofworks { get; set; }
        public int current_summary_num { get; set; }

        public int uspot_replacement { get; set; }

        public int garage_door_motor { get; set; }        // Plaster required						    PLASTER REQUIRED

        public int isTowerScaff { get; set; }

        public int Id { get; set; } // ID of record in database 
    }
}
