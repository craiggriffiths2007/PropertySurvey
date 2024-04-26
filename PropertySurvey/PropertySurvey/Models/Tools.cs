using SQLite;
namespace PropertySurvey
{
    public class ToolsTable
    {
        [PrimaryKey, AutoIncrement]
        public int RecID { get; set; }
        public int files_a { get; set; }
        public int pliers_a { get; set; }
        public int chisels_a { get; set; }
        public int pincers_a { get; set; }
        public int scraper_a { get; set; }
        public int hacksaw_a { get; set; }
        public int crowbar_a { get; set; }
        public int handsaw_a { get; set; }
        public int molegrips_a { get; set; }
        public int sidecutters_a { get; set; }
        public int hammer_a { get; set; }
        public int spiritlevel_a { get; set; }
        public int screwdrivers_a { get; set; }
        public int bolsterchisel_a { get; set; }
        public int setsquare_a { get; set; }
        public int stanleyknife_a { get; set; }
        public int clubhammer_a { get; set; }
        public int tapemeasure_a { get; set; }
        public int slidingbevel_a { get; set; }
        public int glazingshovel_a { get; set; }
        public int pointingtrowel_a { get; set; }
        public int setofallenkeys_a { get; set; }
        public int adjustablespanner_a { get; set; }
        public int augerbits_a { get; set; }
        public int nailpunch_a { get; set; }
        public int puttyknife_a { get; set; }
        public int socketset_a { get; set; }
        public int copingsaw_a { get; set; }
        public int augerbitsjoin_a { get; set; }
        public int nailpunchjoin_a { get; set; }
        public int puttyknifejoin_a { get; set; }
        public int socketsetjoin_a { get; set; }
        public int copingsawjoin_a { get; set; }
        public int rivetgunjoin_a { get; set; }


        public int files_f { get; set; }
        public int pliers_f { get; set; }
        public int chisels_f { get; set; }
        public int pincers_f { get; set; }
        public int scraper_f { get; set; }
        public int hacksaw_f { get; set; }
        public int crowbar_f { get; set; }
        public int handsaw_f { get; set; }
        public int molegrips_f { get; set; }
        public int sidecutters_f { get; set; }
        public int hammer_f { get; set; }
        public int spiritlevel_f { get; set; }
        public int screwdrivers_f { get; set; }
        public int bolsterchisel_f { get; set; }
        public int setsquare_f { get; set; }
        public int stanleyknife_f { get; set; }
        public int clubhammer_f { get; set; }
        public int tapemeasure_f { get; set; }
        public int slidingbevel_f { get; set; }
        public int glazingshovel_f { get; set; }
        public int pointingtrowel_f { get; set; }
        public int setofallenkeys_f { get; set; }
        public int adjustablespanner_f { get; set; }
        public int augerbits_f { get; set; }
        public int nailpunch_f { get; set; }
        public int puttyknife_f { get; set; }
        public int socketset_f { get; set; }
        public int copingsaw_f { get; set; }

        public int augerbitsjoin_f { get; set; }
        public int nailpunchjoin_f { get; set; }
        public int puttyknifejoin_f { get; set; }
        public int socketsetjoin_f { get; set; }
        public int copingsawjoin_f { get; set; }
        public int rivetgunjoin_f { get; set; }


        public string date_done { get; set; }

        public bool bSent { get; set; }
        public bool bSigned { get; set; }
        public bool bSigned2 { get; set; }
        public string signature_filename { get; set; }
        public string signature_filename2 { get; set; }
        public string signature_printed { get; set; }
        public string signature_printed2 { get; set; }
        public string registration { get; set; }
        public string branch { get; set; }
        public string CheckID { get; set; }
        public string photo_filename { get; set; }
    }
}
