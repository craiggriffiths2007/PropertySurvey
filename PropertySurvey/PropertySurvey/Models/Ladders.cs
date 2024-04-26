using SQLite;
namespace PropertySurvey
{
    public class LaddersTable
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string date_done { get; set; }
        public string branch { get; set; }
        public string ladder_number { get; set; }
        public string registration { get; set; }
        public string fitter_surveyor_name { get; set; }
        public string managers_name { get; set; }
        public string CheckID { get; set; }
        public int in_reasonable_condition { get; set; }
        public int rungs_missing_or_loose { get; set; }
        public int stiles_damaged_or_bent { get; set; }
        public int any_cracks { get; set; }
        public int any_corrosion { get; set; }
        public int rubber_plastic_feet { get; set; }
        public int sharp_or_metal_splinters { get; set; }
        public int rungs_dented { get; set; }
        public int painted_or_decorated { get; set; }
        public int hooks_sit_properly { get; set; }
        public int ladders_been_repaired { get; set; }
        public string comments { get; set; }
        public bool bSent { get; set; }
        public bool bSigned { get; set; }
        public bool bSigned2 { get; set; }
        public string signature_filename { get; set; }
        public int i_spare4 { get; set; } // ANY DAMAGE
        public int i_spare5 { get; set; } // no. of photos
        public int i_spare6 { get; set; }
        public string s_spare4 { get; set; }  // signature_filename_2
        public string s_spare5 { get; set; }  // type of ladder
        public string s_spare6 { get; set; }
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
        public int total_photos { get; set; }
    }
}
