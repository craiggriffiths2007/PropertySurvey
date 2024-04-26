using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ladders : ContentPage
	{
        public List<string> menu_items = new List<string>() {
            "DELETE",
            "RESEND",};

        public class ListData
        {
            public int uid { get; set; }
            public string sdate { get; set; }

            public bool bSent { get; set; }
            public string back_colour { get; set; }

            public ListData(int uID, string _sdate, string _spcode, bool _bSent)
            {
                this.uid = uID;

                this.sdate = _sdate;

                this.bSent = _bSent;

                if (_bSent == false)
                {
                    this.back_colour = "#b38e91";
                }
                else
                {
                    this.back_colour = "#1881bf";
                }
            }
        }

        ListData selected_data = null;
        bool bSelected = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DrawList();
        }

        public Ladders ()
		{
			InitializeComponent ();
		}

        private void OnAdd(object sender, EventArgs e)
        {
            CreateLadder();

            Navigation.PushAsync(new Ladder(), false);
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            if (selected_data != null)
            {
                var answer = await DisplayAlert("Delete ladder check sheet?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    App.data.DeleteLadderCheck(selected_data.uid);
                    DrawList();
                }
            }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (selected_data == (listView as ListView).SelectedItem as ListData)
            {
                DoSelect();
            }
            else
            {
                selected_data = (listView as ListView).SelectedItem as ListData;
            }
        }

        private void DoSelect()
        {
            if (selected_data != null && bSelected == false)
            {
                bSelected = true;
                App.data.LoadLadderCheck(selected_data.uid);
                Navigation.PushAsync(new Ladder(), false);
            }
        }

        private void OnSelect(object sender, EventArgs e)
        {
            DoSelect();
        }

        private void DrawList()
        {
            List<ListData> dataSource = new List<ListData>();

            var query = App.data.GetLadderChecks();

            foreach (var item in query)
            {
                DateTime delete_date = DateTime.Now.AddDays(-90);

                if (item.date_done == null || ( DateTime.Parse(item.date_done) < delete_date ) )
                {
                    App.data.DeleteLadderCheck(item.RecID);
                }
                else
                {
                    dataSource.Add(new ListData(item.RecID, item.date_done + "  -  " + item.registration, "code", item.bSent));
                }
            }

            listView.ItemsSource = dataSource;
        }

        public void CreateLadder()
        {
            App.CurrentApp.LadderRecord = new LaddersTable();
            
            App.CurrentApp.LadderRecord.date_done = DateTime.Now.ToShortDateString();
            App.CurrentApp.LadderRecord.ladder_number = "";
            App.CurrentApp.LadderRecord.registration = "";
            App.CurrentApp.LadderRecord.fitter_surveyor_name = "";
            App.CurrentApp.LadderRecord.managers_name = "";
            App.CurrentApp.LadderRecord.branch = App.net.App_Settings.set_branchcode;
            App.CurrentApp.LadderRecord.CheckID = "";

            App.CurrentApp.LadderRecord.in_reasonable_condition = 0;
            App.CurrentApp.LadderRecord.rungs_missing_or_loose = 0;
            App.CurrentApp.LadderRecord.stiles_damaged_or_bent = 0;
            App.CurrentApp.LadderRecord.any_cracks = 0;
            App.CurrentApp.LadderRecord.any_corrosion = 0;
            App.CurrentApp.LadderRecord.rubber_plastic_feet = 0;
            App.CurrentApp.LadderRecord.sharp_or_metal_splinters = 0;
            App.CurrentApp.LadderRecord.rungs_dented = 0;
            App.CurrentApp.LadderRecord.painted_or_decorated = 0;
            App.CurrentApp.LadderRecord.hooks_sit_properly = 0;
            App.CurrentApp.LadderRecord.ladders_been_repaired = 0;
            App.CurrentApp.LadderRecord.comments = "";

            App.CurrentApp.LadderRecord.bSent = false;
            App.CurrentApp.LadderRecord.i_spare4 = 0;
            App.CurrentApp.LadderRecord.i_spare5 = 0;
            App.CurrentApp.LadderRecord.i_spare6 = 0;

            App.CurrentApp.LadderRecord.s_spare4 = "";
            App.CurrentApp.LadderRecord.s_spare5 = "";
            App.CurrentApp.LadderRecord.s_spare6 = "";

            App.data.SaveLadderRecord();
            /*
            App.CurrentApp.MileageRecord.start_postcode = "";
            App.CurrentApp.MileageRecord.finish_postcode = "";
            App.CurrentApp.MileageRecord.start_mileage = "";
            App.CurrentApp.MileageRecord.end_mileage = "";
            App.CurrentApp.MileageRecord.no_of_other_places = 0;
            App.CurrentApp.MileageRecord.time1 = DateTime.Now;
            App.CurrentApp.MileageRecord.pcode1 = "";
            App.CurrentApp.MileageRecord.time2 = DateTime.Now;
            App.CurrentApp.MileageRecord.pcode2 = "";
            App.CurrentApp.MileageRecord.time3 = DateTime.Now;
            App.CurrentApp.MileageRecord.pcode3 = "";
            App.CurrentApp.MileageRecord.registration = "";
            App.CurrentApp.MileageRecord.s_spare1 = "";
            App.CurrentApp.MileageRecord.s_spare2 = "";
            App.CurrentApp.MileageRecord.s_spare3 = "";
*/

        }

        private void OnMenuClicked(object sender, EventArgs e)
        {

        }
    }
}