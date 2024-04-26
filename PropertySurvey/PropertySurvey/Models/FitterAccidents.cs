using SQLite;
namespace PropertySurvey
{
    public class FAccidentsTable
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public string date_time { get; set; }
        public bool bSent { get; set; }
        public bool bComplete { get; set; }
        public string full_name { get; set; }
        public string add1 { get; set; }
        public string add2 { get; set; }
        public string add3 { get; set; }
        public string pcode { get; set; }
        public string occupation { get; set; }
        public string filer_full_name { get; set; }
        public string filer_add1 { get; set; }
        public string filer_add2 { get; set; }
        public string filer_add3 { get; set; }
        public string filer_pcode { get; set; }
        public string filer_occupation { get; set; }
        public string sign_date { get; set; }
        public string filer_sign_date { get; set; }
        public string date_happened { get; set; }
        public string time_happened { get; set; }
        public string how_did_accident_happen { get; set; }
        public string materials_used_in_treatment { get; set; }
        public int person_signed { get; set; }
        public int supervisor_signed { get; set; }
        public int num_of_photographs { get; set; }
        public string spare1 { get; set; }
        public string spare2 { get; set; }
        public string spare3 { get; set; }
        public string spare4 { get; set; }
        public string spare5 { get; set; }
        public string spare6 { get; set; }
        public string spare7 { get; set; }
        public string spare8 { get; set; }
        public string spare9 { get; set; }
        public string spare10 { get; set; }
        public string spare11 { get; set; }
        public string spare12 { get; set; }
        public string spare13 { get; set; }
        public string spare14 { get; set; }
        public string spare15 { get; set; }
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
}
